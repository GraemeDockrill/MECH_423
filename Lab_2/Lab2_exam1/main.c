#include <msp430.h> 
#include <../includes/msp_setup_functions.h>


/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
	// configuring clocks
    clockSetup();
    timerBSetup(499);
    timerB_CCR1_Setup(124);             // setting up TB1.1 cycle to 2000 Hz, 25% duty

    // setting up P3.4 as TB1.1 output
    P3DIR |= BIT4;                      // setting P3.4 as output
    P3SEL0 |= BIT4;                     // setting P3.4 as TB1.1

    // Global interrupt enable
    _EINT();

    while(1);

	return 0;
}
