#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

// defines and global variables
unsigned volatile char Rx = 0;
unsigned volatile char startByte;
unsigned volatile char dutyByte;
unsigned volatile char dirByte = 0;
unsigned volatile int parsingMessage = 0;
unsigned volatile int halfStepState = 0;
unsigned volatile int wholeStepState = 0;
unsigned volatile int stepMode = 1;         // 1 = whole stepping, 0 = half stepping

unsigned volatile int motorSpeed = 0;
unsigned volatile int stepSpeed = 65535;

#define BUFFER_SIZE 50

#define on          OUTMOD_7    /* PWM output mode: 7 - PWM reset/set */
#define off         OUTMOD_5    /* PWM output mode: 5 - Reset */

volatile CircularBuffer* cb;

// look up tables for stepper control
static const unsigned int wholeStepping[16] =
{
     1, 0, 0, 0,     // A1
     0, 0, 1, 0,     // A2
     0, 1, 0, 1,     // B1
     0, 0, 0, 1      // B2
};

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
    WDTCTL = WDTPW | WDTHOLD;   // stop watchdog timer

    cb = createCircularBuffer(BUFFER_SIZE); // create circular buffer at cb location

    // setup functions
    CSCTL0 = CSKEY;                             // unlocking clock
    CSCTL1 = DCORSEL;                           // set DCO to 16MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;          // ACLK = DCO, SMCLK = DCO, MCLK = DCO

    // -------- TB0 SETUP --------

    // setting up TB0 for A1 & A2
    TB0CTL |= TBSSEL__ACLK;                     // TB0 using ACLK
    TB0CTL |= ID__1;                            // TB0 with a divider of 1
    TB0CTL |= MC__UP;                           // TB0 in UP mode
    TB0CCR0 = 40000;                            // set UP counter to 40000

    // setting count for A1N2 to 10000
    TB0CCR1 = 10000;
    TB0CCTL1 = on;

    // setting count for A1N1 to 10000
    TB0CCR2 = 10000;
    TB0CCTL2 = on;

    // -------- TB1 SETUP --------

    // setting up TB1 for B1 & B2
    TB1CTL |= TBSSEL__ACLK;                     // TB1 using ACLK
    TB1CTL |= ID__1;                            // TB1 with divider of 1
    TB1CTL |= MC__UP;                           // TB1 in UP mode
    TB1CCR0 = 40000;                            // set UP counter to 40000

    // setting count for B1N2 to 10000
    TB1CCTL1 = on;
    TB1CCR1 = 10000;

    // setting count for B1N1 to 10000
    TB1CCTL2 = on;
    TB1CCR2 = 10000;

    // -------- TB2 SETUP --------

    // setting up TB2
    TB2CTL |= TBSSEL__ACLK;                     // TB2 using ACLK
    TB2CTL |= ID__1;                            // TB2 with divider of 1
    TB2CTL |= MC__UP;                           // TB2 in UP mode
    TB2CCR0 = 0xFFFF;                           // set UP counter to 0xFFFF (max)

    // setting PWM for DC motor
    TB2CCTL1 = on;
    TB2CCR1 = motorSpeed;

    TB2CCTL2 = on;                              // set TB2.2 to OUTMOD_7 (reset/set)
    TB2CCTL2 |= CCIE;                           // enable interrupt flag for CCR2
    TB2CCR2 = 10000;

    // -------- A1, A2, B1, B2 STEPPER PIN SETUP --------

    // P1.4 (A1N2) & P1.5 (A1N1) set to TB0.1 & TB0.2 respectively
    //P1DIR |= BIT4 + BIT5;
    P1SEL0 |= BIT4 + BIT5;

    // P3.4 (B1N2) & P3.5 (B1N1) set to TB1.1 & TB1.2 respectively
    //P3DIR |= BIT4 + BIT5;
    P3SEL0 |= BIT4 + BIT5;

    // P2.2 set to TB2.2
    P2DIR |= BIT2;
    P2SEL0 |= BIT2;

    // Global interrupt enable
    _EINT();

    while(1);

    return 0;
}


// -------- INTERRUPT SERVICE ROUTINES --------


#pragma vector = TIMER2_B1_VECTOR               // interrupt vector TB2.2 interrupt
__interrupt void USCI_A1_ISR(void)
{

    switch(TB2IV){

    case TB2IV_TBCCR2:

        // if half stepping, take a half step
        halfStep();

//        if(stepMode)
//            halfStep();
//        else
//            wholeStep();

        // increment the step counter
        TB2CCR2 = (TB2CCR2 + stepSpeed) % 65535;

        break;
    default:
        break;
    }



    TB2CCTL2 &= ~(CCIFG);                       // reset interrupt flag for TB2CCTL2
}




// take a half step on the stepper motor
void halfStep(){

//    TB0CCTL2 = halfStepping[halfStepState];         // set A1N1
//    TB0CCTL1 = halfStepping[halfStepState + 8];     // set A1N2
//    TB1CCTL2 = halfStepping[halfStepState + 16];    // set B1N1
//    TB1CCTL1 = halfStepping[halfStepState + 24];    // set B1N2

    halfStepState++;                                // increment halfStepState
    if(stepState)
        halfStepState++;                            // if in whole stepping mode, take 2 half steps

    // if halfStepState goes past the last state, reset to 0
    if(halfStepState > 7)
        halfStepState = 0;

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

// take a whole step on the stepper motor
void wholeStep(){

    TB0CCTL2 = wholeStepping[wholeStepState];       // set A1N1
    TB0CCTL1 = wholeStepping[wholeStepState + 4];   // set A1N2
    TB1CCTL2 = wholeStepping[wholeStepState + 8];   // set B1N1
    TB1CCTL1 = wholeStepping[wholeStepState + 12];  // set B1N2

    wholeStepState++;                               // increment wholeStepState

    // if wholeStepState goes past the last state, reset to 0
    if(wholeStepState > 3)
        wholeStepState = 0;
}
