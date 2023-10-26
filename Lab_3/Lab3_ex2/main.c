#include <msp430.h> 
#include <../includes/msp_setup_functions.h>


/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
	// setup functions
	CSCTL0 = CSKEY;                             // unlocking clock
	CSCTL1 |= DCORSEL;
    CSCTL1 |= DCOFSEL_0;                        // set DCO to 16MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;          // ACLK = DCO, SMCLK = DCO, MCLK = DCO
    CSCTL3 = DIVM__8;                           // MCLK/8
	timerBSetupContinuous();
	TB1CTL |= MC__CONTINUOUS;
	timerB_CCR0_Setup(0xFFFF);
	timerB_CCR1_Setup(59999);

	// 16bit precesion - 65535 decimal

	// set P3.4 and P3.5 as outputs for DRV8841
	P3DIR |= BIT4 + BIT5;
	P3SEL0 |= BIT4;                     // setting P3.4 as TB1.1
	P3OUT &= ~BIT5;
//	P3OUT &= ~BIT4;

	// Global interrupt enable
	_EINT();

	while(1);

	return 0;
}

