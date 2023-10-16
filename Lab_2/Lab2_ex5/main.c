#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

// MECH 423 Lab 2 Exercise 5

/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer

	// configuring clocks
	clockSetup();
	timerBSetup(1999);
	timerB_CCR1_Setup(999);             // setting up TB1.1 cycle to 500 Hz, 50% duty
	timerB_CCR2_Setup(499);             // setting up TB1.2 cycle to 500 Hz, 25% duty

	// setting up P3.4 as TB1.1 output
	P3DIR |= BIT4;                      // setting P3.4 as output
	P3SEL0 |= BIT4;                     // setting P3.4 as TB1.1

	// setting up P3.5 as TB1.2 output
	P3DIR |= BIT5;                      // setting P3.5 as output
	P3SEL0 |= BIT5;                     // setting P3.5 as TB1.2

	// Global interrupt enable
    _EINT();
	
	while(1);

	return 0;
}
