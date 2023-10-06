#include <msp430.h> 

// MECH 423 Lab 2 Exercise 5

/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer

	// configuring clocks
	CSCTL0 = CSKEY;                     // unlocking clock
	CSCTL1 |= DCOFSEL1 + DCOFSEL0;      // set DCO to 8MHz
	CSCTL2 |= SELA0 + SELA1 + SELA2;    // ACLK = DCO
	CSCTL2 |= SELS0 + SELS1 + SELS2;    // SMCLK = DCO
	CSCTL2 |= SELM0 + SELM1 + SELM2;    // MCLK = DCO
	CSCTL3 |= DIVA0 + DIVA1;            // ACLK/8
	CSCTL3 |= DIVS0 + DIVS1;            // SMCLK/8
	CSCTL3 |= DIVM0 + DIVM1;            // MCLK/8

	// setting up Timer B
	TB1CTL |= TBSSEL1;                  // TB1 using SMCLK
	TB1CTL &= ~TBSSEL0;                 // TB1 using SMCLK
	TB1CTL &= ~(ID0 + ID1);             // TB1 with a CLK divider of 1
	TB1CTL |= MC0;                      // setting TB to up mode
	TB1CTL |= TBIE;                     // enable TB1CTL interrupt

	// setting TB1.1 cycle to 500Hz
	TB1CCTL1 |= OUTMOD2;                // set mode to OUTMODE 4 Toggle
	TB1CCTL1 &= ~(OUTMOD0 + OUTMOD1);   // set mode to OUTMODE 4 Toggle

	// setting TB1.2 cycle to 500Hz, 25% duty cycle
	TB1CCTL2 |= OUTMOD0 + OUTMOD1;      // set mode to Set/Reset
    TB1CCTL2 &= ~(OUTMOD2);             // set mode to Set/Reset

    // setting up square wave
	TB1CCR0 = 999;                      // setting compare latch TB1CL0 - CAN'T WRITE DIRECTLY TO TB1CL0
	TB1CCR1 = 499;                      // setting compare latch TB1CL1 - CAN'T WRITE DIRECTLY TO TB1CL0

	// setting up P3.4 as TB1.1 output
	P3DIR |= BIT4;                      // setting P3.4 as output
	P3SEL0 |= BIT4;                     // setting P3.4 as TB1.1
	P3SEL1 &= ~BIT4;                    // setting P3.4 as TB1.1

	// setting up P3.5 as TB1.2 output
	P3DIR |= BIT5;                      // setting P3.5 as output
	P3SEL0 |= BIT5;                     // setting P3.5 as TB1.2
	P3SEL1 &= ~BIT5;                    // setting P3.4 as TB1.2

	// Global interrupt enable
    _EINT();

	
	while(1);

	return 0;
}
