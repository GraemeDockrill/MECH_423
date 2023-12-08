#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

// defines and global variables
unsigned volatile char RxByte = 0;
unsigned volatile char startByte;
unsigned volatile char cmdByte;
unsigned volatile char colorByte;
unsigned volatile char dataByte0;
unsigned volatile char dataByte1;
unsigned volatile char dataByte2;
unsigned volatile char dataByte3;
unsigned volatile char dataByte4;
unsigned volatile char dataByte5;
unsigned volatile char dataByte6;
unsigned volatile char dataByte7;
unsigned volatile char ESCByte;

unsigned volatile int dataWord01;
unsigned volatile int dataWord23;
unsigned volatile int dataWord45;
unsigned volatile int dataWord67;

// for XY gantry control
unsigned volatile int XTimerB1Count = 4713; // old 37700
unsigned volatile int YTimerB2Count = 4713; // old 37700
unsigned volatile int XcurrentSteps = 0;
unsigned volatile int XtargetSteps = 0;
unsigned volatile int YcurrentSteps = 0;
unsigned volatile int YtargetSteps = 0;
unsigned volatile int parsingMessage = 0;

unsigned volatile int newCommand = 0;
unsigned volatile int movementEnabled = 0;

// for marker servo control
unsigned volatile int servoTimerB0Count = 20000;
unsigned volatile int servoUpPosCount = 1000;       // servo 0 degrees
unsigned volatile int servoDownPosCount = 1600;     // servo 45 degrees
unsigned volatile int dotDelayCounter = 0;
unsigned volatile int dotMade = 0;                  // 1 = dot was made (send ACKByte over serial) 0 = dot not made yet
unsigned volatile int ACKsent = 0;                  // check if acknowledge was sent

// for cylinder stepper
unsigned volatile int halfStepState = 0;
unsigned volatile int stepMode = 1;         // 1 = whole stepping, 0 = half stepping
unsigned volatile int stepSpeed = 20000;
unsigned volatile int currentMarkerSteps = 0;   // 2048 total steps/revolution
unsigned volatile int targetMarkerSteps = 2048;
unsigned volatile int marker0Steps = 0;
unsigned volatile int marker1Steps = 341;
unsigned volatile int marker2Steps = 683;
unsigned volatile int marker3Steps = 1024;
unsigned volatile int marker4Steps = 1365;
unsigned volatile int marker5Steps = 1707;
unsigned volatile int markerReady = 0;

// circular buffer defines
#define BUFFER_SIZE 50
volatile CircularBuffer* cb;


// look up table for stepper control
static const unsigned int halfStepping[32] =
{
     1, 1, 0, 0, 0, 0, 0, 1,   // A1
     0, 0, 0, 1, 1, 1, 0, 0,   // A2
     0, 1, 1, 1, 0, 0, 0, 0,   // B1
     0, 0, 0, 0, 0, 1, 1, 1    // B2
};


/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer

	cb = createCircularBuffer(BUFFER_SIZE); // create circular buffer at cb location


	// -------- CONFIGURING CLOCKS --------
    CSCTL0 = CSKEY;                     // unlocking clock
    CSCTL1 |= DCOFSEL_3;                // set DCO to 8MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;  // ACLK = DCO, SMCLK = DCO, MCLK = DCO
    CSCTL3 = DIVS__8 + DIVM__8;         // SMCLK/8, MCLK/8


    // -------- SET UP UART --------
    UART1_Setup();


    // -------- SETTING UP TIMER B1 - X AXIS CONTROL --------
        TB1CTL |= TBSSEL__ACLK;             // TB1 using ACLK
        TB1CTL |= ID__4;                    // TB1 with a CLK divider of 4
        TB1CTL |= MC__UP;                   // setting TB to up mode
        TB1CCR0 = XTimerB1Count;            // setting compare latch TB1CL0

        // setting TB1.1 cycle to 53Hz, 50% duty cycle
        TB1CCTL1 = OUTMOD_7;                // set mode to Reset/Set
        TB1CCR1 = 0;                        // setting compare latch TB1CL1
        TB1CCTL1 |= CCIE;                   // enable interrupt flag

        TB1CCR2 = XTimerB1Count / 2;        // turn off at 50%
        TB1CCTL2 |= CCIE;                   // enable interrupt flag

        // setting up P3.3 as output for X AXIS STEPPING (switches on TB1.1)
        P3DIR |= BIT3;                      // setting P3.3 as output
        P3OUT &= ~BIT3;                     // start on PUL low

        // setting up P3.2 and P3.1 as X AXIS DIRECTION
        P3DIR |= BIT2;                      // setting P3.2 as DIR output
        P3OUT |= BIT2;                      // setting +ve as default
        P3DIR |= BIT1;                      // setting P3.1 as DIR output
        P3OUT &= ~BIT1;                     // setting +ve as default (opposite of P3.5)


    // -------- SETTING UP TIMER B2 - Y AXIS CONTROL --------
        TB2CTL |= TBSSEL__ACLK;             // TB2 using ACLK
        TB2CTL |= ID__4;                    // TB2 with a CLK divider of 4
        TB2CTL |= MC__UP;                   // setting TB to up mode
        TB2CCR0 = YTimerB2Count;            // setting compare latch TB1CL0


        // setting TB2.1 cycle to 53Hz, 50% duty cycle
        TB2CCTL1 = OUTMOD_7;                // set mode to Reset/Set
        TB2CCR1 = 0;                        // setting compare latch TB2CL1
        TB2CCTL1 |= CCIE;                   // enable interrupt flag

        TB2CCR2 = YTimerB2Count / 2;        // turn off at 50%
        TB2CCTL2 |= CCIE;                   // enable interrupt flag

        // setting up P3.0 as output for Y AXIS STEPPING (switches on TB2.1)
        P3DIR |= BIT0;                      // setting P3.0 as output
        P3OUT &= ~BIT0;                     // start PUL low

        // setting up P1.2 as Y AXIS DIRECTION
        P1DIR |= BIT2;                      // setting P1.2 as DIR output
        P1OUT |= BIT2;                      // setting +ve as default


    // -------- SETTING UP TIMER B0 - SERVO CONTROL --------
        TB0CTL |= TBSSEL__ACLK;             // TB0 using ACLK
        TB0CTL |= ID__8;                    // TB0 with a CLK divider of 4
        TB0CTL |= MC__UP;                   // setting TB to up mode
        TB0CCR0 = servoTimerB0Count;        // setting compare latch TB1CL0

        // setting TB0.1 cycle
        TB0CCTL1 = OUTMOD_7;                // set mode to Reset/Set
        TB0CCR1 = 0;                        // setting compare latch TB1CL1
        TB0CCTL1 |= CCIE;                   // enable interrupt flag

        TB0CCR2 = servoTimerB0Count / 20;   // turn off at 50%
        TB0CCTL2 |= CCIE;                   // enable interrupt flag

        // setting up P1.3 as output for servo-motor
        P1DIR |= BIT3;                      // setting P1.3 as output
        P1OUT &= ~BIT3;                     // turning off P1.3


    // -------- SETTING UP TIMER A0 - STEPPER PWM --------
        TA0CTL |= TASSEL__ACLK;             // TA0 using ACLK
        TA0CTL |= ID__1;                    // TA0 with divider of 1
        TA0CTL |= MC__UP;                   // TA0 in UP mode
        TA0CCR0 = 24000;                    // set UP counter to 24000

        // setting duty cycle for PMW to 25%
        TA0CCTL1 = OUTMOD_7;                // set up reset/set mode
        TA0CCR1 = 0;                        // turn on at 0
        TA0CCTL1 |= CCIE;                   // enable interrupt flag

        TA0CCR2 = 5000;                     // turn off at 5/12%
        TA0CCTL2 |= CCIE;                   // enable interrupt flag

        // initialize pins for stepper PWM
        P1DIR |= BIT4 + BIT5;               // P1.4, P1.5 as A1N1, A1N2
        P3DIR |= BIT4 + BIT5;               // P3.4, P3.5 as B1N1, B1N2
        P1OUT &= ~(BIT4 + BIT5);
        P3OUT &= ~(BIT4 + BIT5);


    // -------- SETTING UP TIMER A1 - STEPPER STEPPING --------
        // setting up TA1 for stepper stepping
        TA1CTL |= TASSEL__ACLK;             // TB0 using ACLK
        TA1CTL |= ID__4;                    // TB0 with a divider of 1
        TA1CTL |= MC__UP;                   // TB0 in UP mode
        TA1CCR0 = stepSpeed;                // set UP counter to 40000
        TA1CCTL0 |= CCIE;                   // enable interrupt for TB0



    // Global interrupt enable
    _EINT();

    // -------- infinite loop --------
    while(1){

        if(newCommand){

            // disable interrupts while parsing data
            TB1CCTL1 &= ~CCIE;
            TB1CCTL2 &= ~CCIE;

            TB2CCTL1 &= ~CCIE;
            TB2CCTL2 &= ~CCIE;

            TB0CCTL1 &= ~CCIE;
            TB0CCTL2 &= ~CCIE;

            TA0CCTL1 &= ~CCIE;
            TA0CCTL2 &= ~CCIE;

            TA1CCTL0 &= ~CCIE;

            // cases for cmdByte for controlling both X & Y axes
            switch(cmdByte){
                case 0: // zero X & Y axes
//                    movementEnabled = 0;
                    XcurrentSteps = 0;
                    XtargetSteps = 0;
                    YcurrentSteps = 0;
                    YtargetSteps = 0;
                    break;
                case 1: // move to (X,Y) (steps)
//                    movementEnabled = 1;
                    XtargetSteps = dataWord01;
                    YtargetSteps = dataWord23;
                    break;
                case 2: // Move to origin (0,0) (steps)
//                    movementEnabled = 1;
                    XtargetSteps = 0;
                    YtargetSteps = 0;
                    break;
                default:
                    break;
            }

            // change marker position to selected marker
            switch(colorByte){
                case 0:
                    targetMarkerSteps = marker0Steps;
                    break;
                case 1:
                    targetMarkerSteps = marker1Steps;
                    break;
                case 2:
                    targetMarkerSteps = marker2Steps;
                    break;
                case 3:
                    targetMarkerSteps = marker3Steps;
                    break;
                case 4:
                    targetMarkerSteps = marker4Steps;
                    break;
                case 5:
                    targetMarkerSteps = marker5Steps;
                    break;
                default:
                    break;
            }


            // change speed based on received packet
            TB1CCR0 = dataWord45;       // X axis speed change
            TB1CCR2 = dataWord45 / 2;

            TB2CCR0 = dataWord67;       // Y axis speed change
            TB2CCR2 = dataWord67 / 2;

            movementEnabled = 1;

            markerReady = 0;            // marker is not ready, as movement command received

            dotMade = 0;                // dot is not made for new command

            ACKsent = 0;                // reset that we've sent an acknowledge byte

            newCommand = 0;             // we've read and parsed the command

            // re-enable interrupts
            TB1CCTL1 |= CCIE;
            TB1CCTL2 |= CCIE;

            TB2CCTL1 |= CCIE;
            TB2CCTL2 |= CCIE;

            TB0CCTL1 |= CCIE;
            TB0CCTL2 |= CCIE;

            TA0CCTL1 |= CCIE;
            TA0CCTL2 |= CCIE;

            TA1CCTL0 |= CCIE;
        }

    }

	return 0;
}


// -------- INTERRUPT SERVICE ROUTINES --------


#pragma vector = USCI_A0_VECTOR             // interrupt vector for Rx interrupt
__interrupt void USCI_A0_ISR(void)
{
    RxByte = UART1_Rx();                         // Get the new byte from the Rx buffer

    if(parsingMessage)
        UART1_string("Currently parsing a byte!");
    // ensure you get the start byte first
    else if(cb->count == 0 && RxByte == 255){
        enqueue(cb, RxByte);                        // enqueue the start byte
    }
    else if(cb->count > 0){
        enqueue(cb, RxByte);                        // enqueue the received byte
    }

    if(cb->count >= 9){
        parsingMessage = 1;                     // signal we're parsing a message
        startByte = dequeue(cb);
        cmdByte = dequeue(cb);
        colorByte = dequeue(cb);
        dataByte0 = dequeue(cb);
        dataByte1 = dequeue(cb);
        dataByte2 = dequeue(cb);
        dataByte3 = dequeue(cb);
        dataByte4 = dequeue(cb);
        dataByte5 = dequeue(cb);
        dataByte6 = dequeue(cb);
        dataByte7 = dequeue(cb);
        ESCByte = dequeue(cb);

        // handle escape byte
        if(ESCByte & BIT7)
            dataByte0 = 255;
        if(ESCByte & BIT6)
            dataByte1 = 255;
        if(ESCByte & BIT5)
            dataByte2 = 255;
        if(ESCByte & BIT4)
            dataByte3 = 255;
        if(ESCByte & BIT3)
            dataByte4 = 255;
        if(ESCByte & BIT2)
            dataByte5 = 255;
        if(ESCByte & BIT1)
            dataByte6 = 255;
        if(ESCByte & BIT0)
            dataByte7 = 255;

        // combine UART bytes into 16 bit integer for X and Y step target positions
        dataWord01 = dataByte0 << 8;
        dataWord01 = dataWord01 + dataByte1;
        dataWord23 = dataByte2 << 8;
        dataWord23 = dataWord23 + dataByte3;

        // combine UART bytes into 16 bit integers for X and Y speeds
        dataWord45 = dataByte4 << 8;
        dataWord45 = dataWord45 + dataByte5;
        dataWord67 = dataByte6 << 8;
        dataWord67 = dataWord67 + dataByte7;

        // tell super loop a new message is received
        if(startByte == 255){
            newCommand = 1;
        }

        parsingMessage = 0;                     // signal we're done parsing a message
    }
}


// interrupt vector for PWM of servo-motor for moving marker
#pragma vector = TIMER0_B1_VECTOR
__interrupt void SERVO_PWM_ISR(void)
{

    switch(TB0IV){
        case TB0IV_TBCCR1:
            P1OUT |= BIT3;                      // turn on servo-pin
            break;
        case TB0IV_TBCCR2:
            P1OUT &= ~BIT3;                     // turn off servo-pin
            break;
        default:
            break;
    }

    TB0CCTL1 &= ~(CCIFG);                       // reset interrupt flag for TB0IFG
    TB0CCTL2 &= ~(CCIFG);                       // reset interrupt flag for TB0IFG
}



// interrupt vector TB1 X AXIS stepping
#pragma vector = TIMER1_B1_VECTOR
__interrupt void X_AXIS_STEPPING_ISR(void)
{

    // if in movement mode
    if(movementEnabled){

        switch(TB1IV){
            case TB1IV_TBCCR1:

                // if below target position, move +ve steps to reach target
                if(XcurrentSteps < XtargetSteps){

                    P3OUT &= ~BIT1;         // set direction to +ve of P3.1 (opposite of P3.2)
                    P3OUT |= (BIT2 + BIT3); // set direction to +ve & take step
                    XcurrentSteps++;        // increment current steps
                }
                // if above target position, move -ve steps to reach target
                else if(XcurrentSteps > XtargetSteps){
                    P3OUT &= ~BIT2;         // set direction -ve
                    P3OUT |= BIT1;          // set direction -ve of P3.1 (opposite of P3.2)
                    P3OUT |= BIT3;          // take step
                    XcurrentSteps--;        // decrement current steps
                }

                break;
            case TB1IV_TBCCR2:
                // turn off X AXIS PUL+
                P3OUT &= ~BIT3;
                break;
            default:
                break;
        }
    }

    TB1CCTL1 &= ~(CCIFG);                       // reset interrupt flag for TB1IFG
    TB1CCTL2 &= ~(CCIFG);                       // reset interrupt flag for TB1IFG
}


// interrupt vector TB2 Y AXIS stepping
#pragma vector = TIMER2_B1_VECTOR
__interrupt void Y_AXIS_STEPPING_ISR(void)
{

    // if in movement mode
    if(movementEnabled){

        switch(TB2IV){
            case TB2IV_TBCCR1:

                // if below target position, move +ve steps to reach target
                if(YcurrentSteps < YtargetSteps){

                    P1OUT |= BIT2;          // set direction to +ve
                    P3OUT |= BIT0;          // take step
                    YcurrentSteps++;        // increment current steps
                }
                // if above target position, move -ve steps to reach target
                else if(YcurrentSteps > YtargetSteps){
                    P1OUT &= ~BIT2;         // set direction -ve
                    P3OUT |= BIT0;          // take step
                    YcurrentSteps--;        // decrement current steps
                }

                break;
            case TB2IV_TBCCR2:
                // turn off Y AXIS PUL+
                P3OUT &= ~BIT0;
                break;
            default:
                break;
        }
    }

    TB2CCTL1 &= ~(CCIFG);                       // reset interrupt flag for TB1IFG
    TB2CCTL2 &= ~(CCIFG);                       // reset interrupt flag for TB1IFG
}


// interrupt vector TA0 PWM interrupt
#pragma vector = TIMER0_A1_VECTOR
__interrupt void STEPPER_PWM_ISR(void)
{
    switch(TA0IV){
        case TA0IV_TACCR1:
            updateCoils();                      // update stepper coils based on look up table
            break;
        case TA0IV_TACCR2:
            P1OUT &= ~BIT5;                     // reset A1N1
            P1OUT &= ~BIT4;                     // reset A1N2
            P3OUT &= ~BIT5;                     // reset B1N1
            P3OUT &= ~BIT4;                     // reset BIN2
            break;
        default:
            break;
    }

    TA0CCTL1 &= ~(CCIFG);                       // reset interrupt flag for TA0IFG
    TA0CCTL2 &= ~(CCIFG);                       // reset interrupt flag for TA0IFG
}


// interrupt vector TA1 for changing stepper state for marker stepper
#pragma vector = TIMER1_A0_VECTOR
__interrupt void STEPPER_STEP_ISR(void)
{

    // if marker is not ready, and XY target reached, move cylinder to ready position
    if(!markerReady && XcurrentSteps == XtargetSteps && YcurrentSteps == YtargetSteps){

        if(currentMarkerSteps > targetMarkerSteps){
            decrementStep();
            currentMarkerSteps--;
        }
        else if(currentMarkerSteps < targetMarkerSteps){
            incrementStep();
            currentMarkerSteps++;
        }
        else if(currentMarkerSteps == targetMarkerSteps){
            markerReady = 1;                        // marker is lined up and ready to dot
            TA1CCR0 = 50000;                        // set up TA1 for state switching the marker servo
            dotDelayCounter = 0;                    // reset dot delay
        }
    }

    // if after position reached and the dot hasn't been made yet, make dot
    else if(markerReady && !dotMade){

        dotDelayCounter++;                          // increment dot delay

        // lower servo
        if(dotDelayCounter == 1)
            TB0CCR2 = servoDownPosCount;
        // after 0.5s, raise servo
        else if(dotDelayCounter >= 20)
            TB0CCR2 = servoUpPosCount;
        // after another 0.5s, signal dot is made
        else if(dotDelayCounter >= 40){
            dotDelayCounter = 0;
            dotMade = 1;                            // signal we've made the dot
        }

    }

    // if marker is ready and dot is made, send acknowledge
    else if(markerReady && dotMade && !ACKsent){
        UART1_Tx(255);
        UART1_Tx(1);
        TA1CCR0 = stepSpeed;                        // reset interrupt of TA1 to stepper speed for cylinder
        ACKsent = 1;
    }


    TA1CCTL0 &= ~(CCIFG);                       // reset interrupt flag for TA1IFG
}


// update stepper coils based on halfStepState
void updateCoils(){

    if(halfStepping[halfStepState])
        P1OUT |= BIT5;                              // set A1N1
    else
        P1OUT &= ~BIT5;                             // reset A1N1
    if(halfStepping[halfStepState + 8])
        P1OUT |= BIT4;                              // set A1N2
    else
        P1OUT &= ~BIT4;                             // reset A1N2
    if(halfStepping[halfStepState + 16])
        P3OUT |= BIT5;                              // set BIN1
    else
        P3OUT &= ~BIT5;                             // reset B1N1
    if(halfStepping[halfStepState + 24])
        P3OUT |= BIT4;                              // set BIN2
    else
        P3OUT &= ~BIT4;                             // reset BIN2
}


// increment step on stepper motor (if stepMode == 1, take whole step)
void incrementStep(){
    // take a step or half step based on stepMode
    halfStepState++;                                // increment halfStepState
    if(stepMode)
        halfStepState++;                            // if in whole stepping mode, take 2 half steps

    // if halfStepState goes past the last state, reset to 0
    if(halfStepState > 7)
        halfStepState = 0;
}


// decrement step on stepper motor (if stepMode == 1, take whole step)
void decrementStep(){
    halfStepState--;                                // decrement halfStepState
    if(stepMode)
        halfStepState--;                            // if in whole stepping mode, take 2 half steps

    // if halfStepState goes past the first state, reset to 7
    if(halfStepState < 0 || halfStepState > 7)

        if(stepMode)
            halfStepState = 6;
        else
            halfStepState = 7;
}
