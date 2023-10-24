#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
	clockSetup();

	timerBSetup(19999);
	timerB_CCR1_Setup(9999);

	P3DIR |= BIT4;
	P3OUT |= BIT4;

	P1DIR |= BIT0;
	P1OUT &= ~BIT0;

	P1DIR |= BIT4 + BIT5;

//	P1SEL1 |= BIT4 + BIT5;
//	P1SEL0 |= BIT4 + BIT5;

	P1OUT |= BIT4;
	P1OUT &= ~BIT5;

	while(1){
	    int i = 0;
	    for(i = 0; i < 30000; i++)
	        _NOP();

	    P3OUT ^= BIT4;
	}


	return 0;
}
