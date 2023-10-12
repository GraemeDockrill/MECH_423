#include <msp430.h> 

// global variables for acceleration
unsigned volatile char Ax = 0;
unsigned volatile char Ay = 0;
unsigned volatile char Az = 0;
unsigned int accelerometerState = 0;


void clockSetup (void){
    // configuring clocks
    CSCTL0 = CSKEY;                             // unlocking clock
    CSCTL1 |= DCOFSEL1 + DCOFSEL0;              // set DCO to 8MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;          // ACLK = DCO, SMCLK = DCO, MCLK = DCO
    CSCTL3 = DIVS__16 + DIVM__16;               // SMCLK/16, MCLK/16
}

void timerBSetup (void){
    // setting up Timer B
    TB1CTL |= TBSSEL__SMCLK;            // TB1 using SMCLK
    TB1CTL |= ID__1;                    // TB1 with a CLK divider of 1
    TB1CTL |= MC__UP;                   // setting TB to up mode
    TB1CCTL0 |= CCIE;                   // enable timer B overflow flag

    TB1CCR0 = 19999;                    // set compare latch. period of timer B: SMCLK = 500kHz; 500kHz/1; 500kHz/25Hz = 20 cycles
}


void UART_Setup (void){
    // Configure P2.0 and P2.1 ports for UART
    P2SEL0 &= ~(BIT0 + BIT1);               // secondary module function UCA0RXD
    P2SEL1 |= BIT0 + BIT1;                  // secondary module function UCA0TXD
    UCA0CTLW0 |= UCSWRST;                   // Put the UART in software reset so can be modified
    UCA0CTLW0 |= UCSSEL0;                   // Run the UART using ACLK
    UCA0MCTLW = UCOS16 + UCBRF0 + 0x4900;   // Enable oversampling, Baud rate = 9600 from an 8 MHz clock (BRCLK) and from column UCBRSx
    UCA0BRW = 52;                           // Clock prescaler from Table 18-5 column UCBRx
    UCA0CTLW0 &= ~UCSWRST;                  // release UART for operation
    UCA0IE |= UCRXIE;                       // Enable UART Rx interrupt
}


void ADCSetup (void){
    // initial ADC configuration
    ADC10CTL0 &= ~ADC10ENC;                                 // disable ADC10
    ADC10CTL1 |= ADC10SSEL_1 + ADC10SHP + ADC10CONSEQ_0;    // select ACLK for ADC10_B, SAMPCON from sampling timer, single-channel single-conversion
    ADC10CTL0 |= ADC10ON;                                   // turn on the ADC
    ADC10CTL2 |= ADC10RES;                                  // set resolution to 10 bit
    ADC10CTL0 |= ADC10ENC;                                  // enable ADC10
}


void UART_Tx (unsigned char TxByte){
    while (!(UCA0IFG & UCTXIFG));       // wait until the previous Tx is finished
    UCA0TXBUF = TxByte;                 // send TxByte
}


char ADC_Read (void){
    unsigned char result;

    while(ADC10CTL1 & ADC10BUSY);       // wait for ADC to complete
    result = ADC10MEM0 >> 2;            // read converted memory and bit shift

    if (result >= 255)                  // if accelerometer data is 255, set to 254
        result = 254;

    return result;                      // return result
}


/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
	// set P3.0, P3.1, P3.2 as A12-A14
	P3SEL0 |= BIT0 + BIT1 + BIT2;
	P3SEL1 |= BIT0 + BIT1 + BIT2;

	// configuring P2.7 as output
	P2DIR |= BIT7;
	P2SEL1 &= ~BIT7;
	P2SEL0 &= ~BIT7;

	// set to output high to power accelerometer
	P2OUT |= BIT7;

	clockSetup();
	timerBSetup();
	ADCSetup();
	UART_Setup();

	// Global interrupt enable
	_EINT();

	while(1);

	return 0;
}


// ISRs------------------------------------------------------------------------------

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

    // set up ADC for Ay input
    ADC10CTL0 &= ~ADC10ENC;             // disable conversion
    ADC10MCTL0 = ADC10INCH_13;          // set up ADC to read A13 (Ay)
    ADC10CTL0 |= ADC10ENC + ADC10SC;    // enable/start conversion

    Ay = ADC_Read();                    // read ADC to Ay
    UART_Tx(Ay);                        // transmit Ay

    // set up ADC for Az input
    ADC10CTL0 &= ~ADC10ENC;             // disable conversion
    ADC10MCTL0 = ADC10INCH_14;          // set up ADC to read A14 (Az)
    ADC10CTL0 |= ADC10ENC + ADC10SC;    // enable/start conversion

    Az = ADC_Read();                    // read ADC to Az
    UART_Tx(Az);                        // transmit Az

    TB1CCTL0 &= ~CCIFG;                 // reset timer flag
}
