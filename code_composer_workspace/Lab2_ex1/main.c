#include <msp430.h> 


/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
	// initial setup
	CSCTL0 = CSKEY; // give clock password

	// configuring dco
	// CSCTL1 |= 0x0006;
	CSCTL1 |= DCOFSEL_3;

	// select SMCLK source
	CSCTL2 |= SELM__DCOCLK;

	// set up with a divider of 32
	CSCTL3 |= DIVS__32;

	// configure Pin 3.4 as output
	P3DIR |= BIT4;
	P3SEL1 |= BIT4;
	P3SEL0 |= BIT4;


	while(1){
	    _NOP();
	}

	return 0;
}
