#include <msp430.h> 


/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
	// initial setup
	CSCTL0 = CSKEY; // give clock password

	// configuring DCO to run at 8 MHz
	CSCTL1 |= DCOFSEL_3;

	// select SMCLK source
	CSCTL2 |= SELS__DCOCLK;

	// set up with a divider of 32
	CSCTL3 |= DIVS__32;

	// configure Pin 3.4 as output
	P3DIR |= BIT4; // setting pin 3.4 as an output
	// choosing tertiary module function (SMCLK) for pin 3.4
	P3SEL1 |= BIT4; // setting only BIT4 of P3SEL1 register to 1
	P3SEL0 |= BIT4; // setting only BIT4 of P3SEL0 register to 1


	while(1){
	    _NOP();
	}

	return 0;
}
