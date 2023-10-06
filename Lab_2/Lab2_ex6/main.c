#include <msp430.h> 

// MECH 423 Lab 2 Exercise 6


unsigned int Timer = 0;

/**
 * main.c
 */
int main(void)
{
    // EXERCISE 5 CODE-------------------

    WDTCTL = WDTPW | WDTHOLD;   // stop watchdog timer

    // configuring clocks
    CSCTL0 = CSKEY;                     // unlocking clock
    CSCTL1 |= DCOFSEL1 + DCOFSEL0;      // set DCO to 8MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;  // ACLK = DCO, SMCLK = DCO, MCLK = DCO
    CSCTL3 = DIVA_4 + DIVS_4 + DIVM_4;  // ACLK/16, SMCLK/16, MCLK/16

    // setting up Timer B
    TB1CTL |= TBSSEL1;                  // TB1 using SMCLK
    TB1CTL &= ~TBSSEL0;                 // TB1 using SMCLK
    TB1CTL &= ~(ID0 + ID1);             // TB1 with a CLK divider of 1
    TB1CTL |= MC0;                      // setting TB to up mode
    TB1CTL |= TBIE;                     // enable TB1CTL interrupt

    // setting TB1.1 & TB1.2 out mode
    TB1CCTL1 = OUTMOD_7;                // set mode to Reset/Set
    TB1CCTL2 = OUTMOD_7;                // set mode to Reset/Set

    // setting up square wave (TB1.1 500Hz) & (TB1.2 500Hz 25% duty cycle)
    TB1CCR0 = 999;                      // setting compare latch TB1CL0 - CAN'T WRITE DIRECTLY TO TB1CL0
    TB1CCR1 = 499;                      // setting compare latch TB1CL1 - CAN'T WRITE DIRECTLY TO TB1CL0
    TB1CCR2 = 249;                      // setting compare latch TB1CL2 - CAN'T WRITE DIRECTLY TO TB1CL0

    // setting up P3.4 as TB1.1 output
    P3DIR |= BIT4;                      // setting P3.4 as output
    P3SEL0 |= BIT4;                     // setting P3.4 as TB1.1

    // setting up P3.5 as TB1.2 output
    P3DIR |= BIT5;                      // setting P3.5 as output
    P3SEL0 |= BIT5;                     // setting P3.5 as TB1.2

    // EXERCISE 6 CODE-------------------

    // setting up Timer A
    TA0CTL |= TASSEL_2 + ID_0
              + MC_1 + TAIE;          // TA1 using SMCLK, CLK divider of 1, set to up mode, enabing interrupt
    TA0CCTL0 |= CCIS_0 + CM_3 + CAP
              + CCIE + SCS + SCCI;      // capture input from CCI0A, capture on rising and falling edges, capture mode, interrupt enabled, synchronous capture,

    // setting up P1.6 for input Timer A
    P1DIR &= ~BIT6;
    P1SEL0 |= BIT6;
    P1SEL1 |= BIT6;

    // Global interrupt enable
    _EINT();


    while(1);

    return 0;
}


#pragma vector = TIMER0_A0_VECTOR       // interrupt vector for TA0 interrupt
__interrupt void TA0_ISR(void)
{
    if(TA0CCTL0 & CCI){
        TA0R = 0x0000;                  // reset counter to 0
    }
    else{
        Timer = TA0CCR1;                   // store current time in Timer variable
    }
    TA0CCTL0 &= ~CCIFG;                 // reset interrupt flag
    TA0CTL &= ~TAIFG;
}
