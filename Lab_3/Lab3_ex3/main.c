#include <msp430.h> 
#include <Stepper_LUT.h>
#include <UART.h>
#include <TimerB.h>
#include <../includes/msp_setup_functions.h>

/**
 * main.c
 */

volatile unsigned int state = 0;

int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
    // setup functions
    CSCTL0 = CSKEY;                             // unlocking clock
    CSCTL1 |= DCORSEL;
    CSCTL1 |= DCOFSEL_0;                        // set DCO to 16MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;          // ACLK = DCO, SMCLK = DCO, MCLK = DCO
    CSCTL3 = DIVM__8;                           // MCLK/8

    UARTSetup(); // This will be at 19200 baud for a 16 MHz ACLK

    // This sets up timer B1
    TB1CTL |= TBSSEL__ACLK;             // TB1 using SMCLK
    TB1CTL |= ID__1;                    // TB1 with a CLK divider of 1
    TB1CTL |= MC__CONTINUOUS;           // setting TB to up mode

    TB1CTL |= CNTL_0;                   // choose highest counter of 16bits 0xFFFFh

    TB1CTL |= MC__CONTINUOUS;

    // This sets up timer B0
    TB0CTL |= TBSSEL__ACLK;             // TB1 using SMCLK
    TB0CTL |= ID__1;                    // TB1 with a CLK divider of 1
    TB0CTL |= MC__CONTINUOUS;           // setting TB to up mode

    TB0CTL |= CNTL_0;                   // choose highest counter of 16bits 0xFFFFh

    TB0CTL |= MC__CONTINUOUS;



	// A1 is TB0.2
	// A2 is TB0.1
	// B1 is TB1.2
	// B2 is TB1.1

	return 0;
}