#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

// defines and global variables
unsigned volatile char Rx = 0;
unsigned volatile char startByte;
unsigned volatile char cmdByte0;
unsigned volatile char cmdByte1;
unsigned volatile char stepSpeedByte;
unsigned volatile char dutyByte;
unsigned volatile int parsingMessage = 0;
unsigned volatile int halfStepState = 0;
unsigned volatile int stepMode = 1;         // 1 = whole stepping, 0 = half stepping
unsigned volatile int stepDir = 1;          // 1 = CW, 0 = CCW
unsigned volatile int stopStep = 0;

unsigned volatile int motorSpeed = 0;
unsigned volatile int stepSpeed = 0;

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

    // setup functions
    CSCTL0 = CSKEY;                             // unlocking clock
    CSCTL1 = DCORSEL;                           // set DCO to 16MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;          // ACLK = DCO, SMCLK = DCO, MCLK = DCO


    // -------- TB0 SETUP STEPPING --------

    // setting up TB0 for stepper stepping
    TB0CTL |= TBSSEL__ACLK;                     // TB0 using ACLK
    TB0CTL |= ID__2;                            // TB0 with a divider of 1
    TB0CTL |= MC__UP;                            // TB0 in UP mode
    TB0CCR0 = stepSpeed;                        // set UP counter to 40000
    TB0CCTL0 |= CCIE;                           // enable interrupt for TB0


    // -------- TB1 SETUP STEPPER PWM --------

    // setting up TB1 PWM to P1.4, P1.5, P3.4, P3.5
    TB1CTL |= TBSSEL__ACLK;                     // TB1 using ACLK
    TB1CTL |= ID__1;                            // TB1 with divider of 1
    TB1CTL |= MC__UP;                           // TB1 in UP mode
    TB1CCR0 = 10000;                            // set UP counter to 40000

    // setting duty cycle for PMW to 25%
    TB1CCTL1 = OUTMOD_7;                        // set up reset/set mode
    TB1CCR1 = 0;                                // turn on at 0
    TB1CCTL1 |= CCIE;                           // enable interrupt flag

    TB1CCR2 = 2500;                            // turn off at 25%
    TB1CCTL2 |= CCIE;                           // enable interrupt flag


    // -------- TB2 SETUP DC MOTOR --------

    // setting up TB2 for DC motor
    TB2CTL |= TBSSEL__ACLK;                     // TB2 using ACLK
    TB2CTL |= ID__1;                            // TB2 with divider of 1
    TB2CTL |= MC__UP;                           // TB2 in UP mode
    TB2CCR0 = 0xFFFF;                           // set UP counter to 0xFFFF (max)

    // setting PWM for DC motor
    TB2CCTL2 = OUTMOD_7;
    TB2CCR2 = motorSpeed;


    // -------- UART SETUP --------
    UART1_Setup();                              // set up UART1 at 19200 Baud


    // -------- P2.2 DC MOTOR OUTPUT PWM --------

    P2DIR |= BIT2;
    P2SEL0 |= BIT2;                             // setting P2.2 as TB2.2

    // set P3.6 and P3.7 as outputs for AIN2 and AIN1 on DC motor driver
    P3DIR |= BIT6 + BIT7;
    P3OUT &= ~BIT6;
    P3OUT |= BIT7;


    // -------- A1, A2, B1, B2 STEPPER PIN SETUP --------

    // P1.4 (A1N2) & P1.5 (A1N1) set to TB0.1 & TB0.2 respectively
    P1DIR &= ~(BIT4 + BIT5);
    P1OUT |= BIT4 + BIT5;

    // P3.4 (B1N2) & P3.5 (B1N1) set to TB1.1 & TB1.2 respectively
    P3DIR &= ~(BIT4 + BIT5);
    P3OUT |= BIT4 + BIT5;

    // Global interrupt enable
    _EINT();

    while(1);

	return 0;
}


// -------- INTERRUPT SERVICE ROUTINES --------


#pragma vector = USCI_A1_VECTOR             // interrupt vector for Rx interrupt
__interrupt void USCI_A1_ISR(void)
{
    Rx = UART1_Rx();                         // get char from UART

    if(parsingMessage == 1)
        UART1_string("Currently parsing a byte!");
    // ensure you get the start byte first
    else if(cb->count == 0 && Rx == 255){
        enqueue(cb, Rx);                        // enqueue the start byte
    }
    else if(cb->count > 0){
        enqueue(cb, Rx);                        // enqueue the received byte
    }

    if(cb->count >= 5){
        parsingMessage = 1;                     // signal we're parsing a message
        startByte = dequeue(cb);
        cmdByte0 = dequeue(cb);
        cmdByte1 = dequeue(cb);
        stepSpeedByte = dequeue(cb);
        dutyByte = dequeue(cb);
        UART1_Tx(startByte);
        UART1_Tx(cmdByte0);
        UART1_Tx(cmdByte1);
        UART1_Tx(stepSpeedByte);
        UART1_Tx(dutyByte);

        if(startByte == 255){
            // turning duty cycle byte into motor speed
            motorSpeed = (65534.0 * dutyByte) / 100;
            TB2CCR2 = motorSpeed;               // changing motor speed

            if(stepSpeedByte == 0)
                stopStep = 1;
            else
                stopStep = 0;

            stepSpeed = -(50535 * stepSpeedByte) / 100 + 65535;
            TB0CCR0 = stepSpeed;                // changing stepper motor speed

            // stepper motor control based on cmdByte0
            switch(cmdByte0){
                case 0:                             // whole step CW
                    stepMode = 1;
                    incrementStep();
                    TB0CCTL0 &= ~CCIE;              // disable interrupt for TB0
                    break;
                case 1:                             // whole step CCW
                    stepMode = 1;
                    decrementStep();
                    TB0CCTL0 &= ~CCIE;              // disable interrupt for TB0
                    break;
                case 2:                             // half step CW
                    stepMode = 0;
                    incrementStep();
                    TB0CCTL0 &= ~CCIE;              // disable interrupt for TB0
                    break;
                case 3:                             // half step CCW
                    stepMode = 0;
                    decrementStep();
                    TB0CCTL0 &= ~CCIE;              // disable interrupt for TB0
                    break;
                case 4:                             // continuous whole step CW
                    stepMode = 1;
                    stepDir = 1;
                    TB0CCTL0 |= CCIE;               // enable interrupt for TB0
                    break;
                case 5:                             // continuous whole step CCW
                    stepMode = 1;
                    stepDir = 0;
                    TB0CCTL0 |= CCIE;               // enable interrupt for TB0
                    break;
                case 6:                             // continuous half step CW
                    stepMode = 0;
                    stepDir = 1;
                    TB0CCTL0 |= CCIE;               // enable interrupt for TB0
                    break;
                case 7:                             // continuous half step CCW
                    stepMode = 0;
                    stepDir = 0;
                    TB0CCTL0 |= CCIE;               // enable interrupt for TB0
                    break;
                default:
                    break;
            }

            updateCoils();                      // update stepper coils based on new command

            // DC motor control based on cmdByte1
            switch(cmdByte1){
                case 0:                             // CW DC motor
                    P3OUT &= ~BIT7;
                    P3OUT |= BIT6;
                    break;
                case 1:                             // CCW DC motor
                    P3OUT &= ~BIT6;
                    P3OUT |= BIT7;
                    break;
                default:
                    break;
            }


        }

        parsingMessage = 0;                     // signal we're done parsing a message
    }

}


#pragma vector = TIMER0_B0_VECTOR               // interrupt vector for changing stepper state
__interrupt void STEPPER_STEP_ISR(void)
{
    if(stopStep != 1){
        if(stepDir)
            incrementStep();
        else
            decrementStep();
    }

    TB0CCTL0 &= ~(CCIFG);                       // reset interrupt flag for TB0IFG
}


#pragma vector = TIMER1_B1_VECTOR               // interrupt vector TB1 PWM interrupt
__interrupt void STEPPER_PWM_ISR(void)
{
    switch(TB1IV){
        case TB1IV_TBCCR1:
            updateCoils();                      // update stepper coils based on look up table
            break;
        case TB1IV_TBCCR2:
            P1DIR &= ~BIT5;                     // reset A1N1
            P1DIR &= ~BIT4;                     // reset A1N2
            P3DIR &= ~BIT5;                     // reset B1N1
            P3DIR &= ~BIT4;                     // reset BIN2
            break;
        default:
            break;
    }

    TB1CCTL1 &= ~(CCIFG);                       // reset interrupt flag for TB1IFG
}


// update stepper coils based on halfStepState
void updateCoils(){

    if(halfStepping[halfStepState])
        P1DIR |= BIT5;                              // set A1N1
    else
        P1DIR &= ~BIT5;                             // reset A1N1
    if(halfStepping[halfStepState + 8])
        P1DIR |= BIT4;                              // set A1N2
    else
        P1DIR &= ~BIT4;                             // reset A1N2
    if(halfStepping[halfStepState + 16])
        P3DIR |= BIT5;                              // set BIN1
    else
        P3DIR &= ~BIT5;                             // reset B1N1
    if(halfStepping[halfStepState + 24])
        P3DIR |= BIT4;                              // set BIN2
    else
        P3DIR &= ~BIT4;                             // reset BIN2
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
