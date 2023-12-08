#include <msp430.h> 


/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer

	P3DIR |= BIT4;
	P3OUT |= BIT4;

	while(1){
	    P3OUT |= BIT4;
	}

	return 0;
}
