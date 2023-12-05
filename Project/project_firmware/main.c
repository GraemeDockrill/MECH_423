#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

// defines and global variables
unsigned volatile char RxByte = 0;
unsigned volatile char startByte;
unsigned volatile char cmdByte0;
unsigned volatile char cmdByte1;
unsigned volatile char ColorByte;
unsigned volatile char dataByte0;
unsigned volatile char dataByte1;
unsigned volatile char dataByte2;
unsigned volatile char dataByte3;
unsigned volatile char ESCByte;

unsigned volatile int dataWord01;
unsigned volatile int dataWord23;

unsigned volatile int TimerB1Count = 37700;
unsigned volatile int TimerB2Count = 37700;
unsigned volatile int XcurrentSteps = 0;
unsigned volatile int XtargetSteps = 0;
unsigned volatile int YcurrentSteps = 0;
unsigned volatile int YtargetSteps = 0;
unsigned volatile int parsingMessage = 0;

unsigned volatile int newCommand = 0;
unsigned volatile int movementEnabled = 0;

// circular buffer defines
#define BUFFER_SIZE 50
volatile CircularBuffer* cb;


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
    UART_Setup();


    // -------- SETTING UP TIMER B1 --------
    TB1CTL |= TBSSEL__ACLK;             // TB1 using ACLK
    TB1CTL |= ID__4;                    // TB1 with a CLK divider of 4
    TB1CTL |= MC__UP;                   // setting TB to up mode
    TB1CCR0 = TimerB1Count;             // setting compare latch TB1CL0

    // setting TB1.1 cycle to 53Hz, 50% duty cycle
    TB1CCTL1 = OUTMOD_7;                // set mode to Reset/Set
    TB1CCR1 = 0;                        // setting compare latch TB1CL1
    TB1CCTL1 |= CCIE;                   // enable interrupt flag

    TB1CCR2 = TimerB1Count / 2;         // turn off at 50%
    TB1CCTL2 |= CCIE;                   // enable interrupt flag


    // -------- SETTING UP TIMER B2 --------
    TB2CTL |= TBSSEL__ACLK;             // TB2 using ACLK
    TB2CTL |= ID__4;                    // TB2 with a CLK divider of 4
    TB2CTL |= MC__UP;                   // setting TB to up mode
    TB2CCR0 = TimerB2Count;             // setting compare latch TB1CL0


    // setting TB2.1 cycle to 53Hz, 50% duty cycle
    TB2CCTL1 = OUTMOD_7;                // set mode to Reset/Set
    TB2CCR1 = 0;                        // setting compare latch TB2CL1
    TB2CCTL1 |= CCIE;                   // enable interrupt flag

    TB2CCR2 = TimerB2Count / 2;         // turn off at 50%
    TB2CCTL2 |= CCIE;                   // enable interrupt flag


    // setting up P3.4 as output for X AXIS STEPPING (switches on TB1.1)
    P3DIR |= BIT4;                      // setting P3.4 as output
//    P3SEL0 |= BIT4;                     // setting P3.4 as TB1.1
    P3OUT &= ~BIT4;                     // start on PUL low

    // setting up P3.6 as output for Y AXIS STEPPING (switches on TB2.1)
    P3DIR |= BIT6;                      // setting P3.6 as output
//    P3SEL0 |= BIT6;                     // setting P3.6 as TB2.1
    P3OUT &= ~BIT6;                     // start PUL low

    // setting up P3.5 as X AXIS DIRECTION
    P3DIR |= BIT5;                      // setting P3.5 as output
    P3OUT |= BIT5;                      // setting +ve as default

    // setting up P3.7 as Y AXIS DIRECTION
    P3DIR |= BIT7;                      // setting P3.7 as output
    P3OUT |= BIT7;                      // setting +ve as default

    // Global interrupt enable
    _EINT();

    // -------- infinite loop --------
    while(1){

        if(newCommand){

            // cases for cmdByte0 for X axis
            switch(cmdByte0){
                case 0: // Step +X
                    movementEnabled = 1;
                    XtargetSteps = XcurrentSteps + 1;
                    break;
                case 1: // Step -X
                    movementEnabled = 1;
                    if(XcurrentSteps > 0)
                        XtargetSteps = XcurrentSteps - 1;
                    break;
                case 2: // Move to X position (steps)
                    movementEnabled = 1;
                    XtargetSteps = dataWord01;
                    break;
                case 3: // zero X axis
                    movementEnabled = 0;
                    XcurrentSteps = 0;
                    break;
                case 4: // STOP GANTRY!
                    movementEnabled = 0;
                    XcurrentSteps = 0;
                default:
                    break;
            }

            // cases for cmdByte1 for Y axis
            switch(cmdByte1){
                case 0: // Step +Y
                    movementEnabled = 1;
                    YtargetSteps = YcurrentSteps + 1;
                    break;
                case 1: // Step -Y
                    movementEnabled = 1;
                    if(YcurrentSteps > 0)
                        YtargetSteps = YcurrentSteps - 1;
                    break;
                case 2: // Move to Y position (steps)
                    movementEnabled = 1;
                    YtargetSteps = dataWord23;
                    break;
                case 3: // zero Y axis
                    movementEnabled = 0;
                    break;
                default:
                    break;
            }

            movementEnabled = 1;

            newCommand = 0;             // we've read and parsed the command
        }

    }

	return 0;
}


// -------- INTERRUPT SERVICE ROUTINES --------


#pragma vector = USCI_A0_VECTOR             // interrupt vector for Rx interrupt
__interrupt void USCI_A0_ISR(void)
{
    RxByte = UART_Rx();                         // Get the new byte from the Rx buffer

    if(parsingMessage)
        UART_string("Currently parsing a byte!");
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
        cmdByte0 = dequeue(cb);
        cmdByte1 = dequeue(cb);
        ColorByte = dequeue(cb);
        dataByte0 = dequeue(cb);
        dataByte1 = dequeue(cb);
        dataByte2 = dequeue(cb);
        dataByte3 = dequeue(cb);
        ESCByte = dequeue(cb);

        // handle escape byte
        if(ESCByte & BIT3)
            dataByte0 = 255;
        if(ESCByte & BIT2)
            dataByte1 = 255;
        if(ESCByte & BIT1)
            dataByte2 = 255;
        if(ESCByte & BIT0)
            dataByte3 = 255;

        // combine UART bytes into 16 bit integer for X and Y step target positions
        dataWord01 = dataByte0 << 8;
        dataWord01 = dataWord01 + dataByte1;
        dataWord23 = dataByte2 << 8;
        dataWord23 = dataWord23 + dataByte3;

        // tell super loop a new message is received
        if(startByte == 255){
            newCommand = 1;
        }

        parsingMessage = 0;                     // signal we're done parsing a message
    }
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

                    P3OUT |= (BIT5 + BIT4);   // set direction to +ve & take step
                    XcurrentSteps++;        // increment current steps
                }
                // if above target position, move -ve steps to reach target
                else if(XcurrentSteps > XtargetSteps){
                    P3OUT &= ~BIT5;         // set direction -ve
                    P3OUT |= BIT4;          // take step
                    XcurrentSteps--;        // decrement current steps
                }

                break;
            case TB1IV_TBCCR2:
                // turn off X AXIS PUL+
                P3OUT &= ~BIT4;
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

                    P3OUT |= BIT7 + BIT6;   // set direction to +ve & take step
                    YcurrentSteps++;        // increment current steps
                }
                // if above target position, move -ve steps to reach target
                else if(YcurrentSteps > YtargetSteps){
                    P3OUT &= ~BIT7;         // set direction -ve
                    P3OUT |= BIT6;          // take step
                    YcurrentSteps--;        // decrement current steps
                }

                break;
            case TB2IV_TBCCR2:
                // turn off X AXIS PUL+
                P3OUT &= ~BIT6;
                break;
            default:
                break;
        }
    }

    TB2CCTL1 &= ~(CCIFG);                       // reset interrupt flag for TB1IFG
    TB2CCTL2 &= ~(CCIFG);                       // reset interrupt flag for TB1IFG
}
