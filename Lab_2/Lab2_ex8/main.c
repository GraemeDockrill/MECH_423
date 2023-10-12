#include <msp430.h> 

// global variable for temperature
unsigned volatile temperature = 0;

void clockSetup (void){
    // configuring clocks
    CSCTL0 = CSKEY;                             // unlocking clock
    CSCTL1 |= DCOFSEL1 + DCOFSEL0;              // set DCO to 8MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;          // ACLK = DCO, SMCLK = DCO, MCLK = DCO
    CSCTL3 = DIVS__8 + DIVM__8;                // SMCLK/8, MCLK/8
}

void timerBSetup (void){
    // setting up Timer B
    TB1CTL |= TBSSEL__SMCLK;            // TB1 using SMCLK
    TB1CTL |= ID__2;                    // TB1 with a CLK divider of 2
    TB1CTL |= MC__UP;                   // setting TB to up mode
    TB1CCTL0 |= CCIE;                   // enable timer B overflow flag

    TB1CCR0 = 19999;                    // set compare latch. period of timer B: SMCLK = 1MHz; 1MHz/2; 500kHz/25Hz = 20 cycles
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
    ADC10CTL0 |= ADC10ON + ADC10SHT_8;                      // turn on the ADC, sample and hold of 256 clock cycles
    ADC10CTL1 |= ADC10SSEL_3 + ADC10SHP + ADC10CONSEQ_0;    // select SMCLK for ADC10_B, SAMPCON from sampling timer, single-channel single-conversion
    ADC10MCTL0 = ADC10INCH_4;                               // set up ADC to read A4 (NTC temperature sensor)
    ADC10CTL2 |= ADC10RES;                                  // set resolution to 10 bit
    ADC10CTL0 |= ADC10ENC;                                  // enable ADC10
    ADC10IE |= ADC10IE0;
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
	
	// configuring P2.7 as output
    P2DIR |= BIT7;
    P2OUT |= BIT7;              // set high to power NTC

	// set P1.4 as A4 to ADC
	P1SEL0 |= BIT4;
	P1SEL1 |= BIT4;

	// set up reference voltage
	REFCTL0 |= REFVSEL1 + REFON;

    clockSetup();
    timerBSetup();
    ADCSetup();
    UART_Setup();

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
