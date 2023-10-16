#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

// global variable for temperature
unsigned volatile temperature = 0;


/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
	// configuring P2.7 as output
    P2DIR |= BIT7;
    P2OUT |= BIT7;              // set high to power NTC

	// set P1.4 as A4 to ADC
	P1SEL0 |= BIT4;
	P1SEL1 |= BIT4;

	// set up reference voltage
	REFCTL0 |= REFVSEL1 + REFON;

    clockSetup();
    timerBSetup(39999);
    timerB_Overflow_Flag();
    ADCSetup();
    UART_Setup();

    // additional ADC settings
    ADC10CTL0 &= ~ADC10ENC;                                 // disable ADC10
    ADC10MCTL0 = ADC10INCH_4;                               // set up ADC to read A4 (NTC temperature sensor)
    ADC10CTL0 |= ADC10SHT_8;                                // sample and hold of 256 clock cycles
    ADC10IE |= ADC10IE0;                                    // enable interrupt for successful conversion
    ADC10CTL0 |= ADC10ENC;                                  // enable ADC10

    // set up LEDs
    PJDIR |= BIT0 + BIT1 + BIT2 + BIT3;
    P3DIR |= BIT4 + BIT5 + BIT6 + BIT7;
    PJOUT |= BIT0;

    // Global interrupt enable
    _EINT();

    while(1);

	return 0;
}

#pragma vector = TIMER1_B0_VECTOR       // interrupt vector for TB1 interrupt
__interrupt void TB1_ISR(void)
{
    UART_Tx(255);                       // start byte

    // set up ADC for Ax input
    ADC10CTL0 |= ADC10ENC + ADC10SC;    // enable/start conversion

    temperature = ADC_Read();           // read ADC to Ax
    UART_Tx(temperature);               // transmit Ax

    ADC10CTL0 &= ~(ADC10ENC + ADC10SC); // disable conversion

    // LEDs on
    if(temperature < 42)
        PJOUT |= BIT1;
    if(temperature < 41)
            PJOUT |= BIT2;
    if(temperature < 40)
            PJOUT |= BIT3;
    if(temperature < 39)
            P3OUT |= BIT4;
    if(temperature < 38)
            P3OUT |= BIT5;
    if(temperature < 37)
            P3OUT |= BIT6;
    if(temperature < 36)
            P3OUT |= BIT7;
    // LEDs off
    if(temperature > 42)
            PJOUT &= ~BIT1;
    if(temperature > 41)
            PJOUT &= ~BIT2;
    if(temperature > 40)
            PJOUT &= ~BIT3;
    if(temperature > 39)
            P3OUT &= ~BIT4;
    if(temperature > 38)
            P3OUT &= ~BIT5;
    if(temperature > 37)
            P3OUT &= ~BIT6;
    if(temperature > 36)
            P3OUT &= ~BIT7;

    TB1CCTL0 &= ~CCIFG;                 // reset timer flag
}
