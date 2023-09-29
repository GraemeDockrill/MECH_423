#include <msp430.h> 


/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer

	// initial setup
	// set P4.0 as a digital input
	P4DIR &= ~BIT0;

	// set up P4.0 as GPIO
	P4SEL0 &= ~BIT0;
	P4SEL1 &= ~BIT0;

	// enable internal pull-up resistors for switch S1 (attached to pin P4.0)
	P4REN |= BIT0; // enable for low-high transition

	// set P4.0 to get interrupted from rising edge & enable the interrupt
	P4IES &= ~BIT0; // interrupt edge select
	P4IE |= BIT0; // interrupt enable

	// disable interrupt flag of P4.0
	P4IFG &= ~BIT0;

	// set up P3.7 as a digital output
	P3DIR |= BIT7;

	// set pin P3.7 to digital I/O
	//P3SEL0 &= ~BIT7;
	//P3SEL1 &= ~BIT7;

	// global interrupt enable
	_EINT();

	// infinite loop
	while(1);

	return 0;
}

// ISR
#pragma vector = PORT4_VECTOR;
__interrupt void buttonInterrupt(void){
    P3OUT ^= BIT7; // toggle LED
    P4IFG &= ~BIT0; // reset interrupt flag
}
