#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

// global variables for acceleration
unsigned volatile char Ax = 0;

/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
	// set P3.0 as A12
    P3SEL0 |= BIT0;
    P3SEL1 |= BIT0;

    // configuring P2.7 as output
    P2DIR |= BIT7;
    P2SEL1 &= ~BIT7;
    P2SEL0 &= ~BIT7;

    // set to output high to power accelerometer
    P2OUT |= BIT7;

    // configure Pin 3.4 as GPIO
    P3DIR |= BIT4; // setting pin 3.4 as an output

    // set up stuff
    clockSetup();
    timerBSetup(9999);
    timerB_Overflow_Flag();
    ADCSetup();
    UART_Setup();

    // Global interrupt enable
    _EINT();

    while(1);

	return 0;
}


#pragma vector = TIMER1_B0_VECTOR       // interrupt vector for TB1 interrupt
__interrupt void TB1_ISR(void)
{
    UART_Tx(255);                       // transmit start bit

    // set up ADC for Ax input
    ADC10CTL0 &= ~ADC10ENC;             // disable conversion
    ADC10MCTL0 = ADC10INCH_12;          // set up ADC to read A12 (Ax)
    ADC10CTL0 |= ADC10ENC + ADC10SC;    // enable/start conversion

    Ax = ADC_Read();                    // read ADC to Ax
    UART_Tx(Ax);                        // transmit Ax

    P3OUT ^= BIT4;                      // toggle LED


    TB1CCTL0 &= ~CCIFG;                 // reset timer flag
}
