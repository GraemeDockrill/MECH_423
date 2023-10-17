#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

// globals
volatile unsigned int sum = 0;
volatile unsigned int avg = 0;
volatile unsigned int dutyControl = 0;
volatile unsigned char dequeuedData = 0;
unsigned volatile char Ax = 0;

#define BUFFER_SIZE 16

volatile CircularBuffer* cb;

/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
    cb = createCircularBuffer(BUFFER_SIZE); // create circular buffer at cb location

	// configuring stuff
    clockSetup();
    timerBSetup(25999);
    timerB_CCR1_Setup(0);               // setting up TB1.1 cycle to 500 Hz, 50% duty
    timerAOutputSetup(9999);           // setting up TA1.1 cycle to 25 Hz
    timerA_Overflow_Flag();
    ADCSetup();
    UART_Setup();

    // set P3.0 as A12
    P3SEL0 |= BIT0;
    P3SEL1 |= BIT0;

    // configuring P2.7 as output
    P2DIR |= BIT7;
    P2SEL1 &= ~BIT7;
    P2SEL0 &= ~BIT7;

    // set to output high to power accelerometer
    P2OUT |= BIT7;

    // setting up P3.4 as TB1.1 output
    P3DIR |= BIT4;                      // setting P3.4 as output
    P3SEL0 |= BIT4;                     // setting P3.4 as TB1.1

    // Global interrupt enable
    _EINT();

    while(1);

	return 0;
}


#pragma vector = TIMER1_A0_VECTOR       // interrupt vector for TA1 interrupt
__interrupt void TA1_ISR(void)
{
    //UART_Tx(255);                       // transmit start bit

    // set up ADC for Ax input
    ADC10CTL0 &= ~ADC10ENC;             // disable conversion
    ADC10MCTL0 = ADC10INCH_12;          // set up ADC to read A12 (Ax)
    ADC10CTL0 |= ADC10ENC + ADC10SC;    // enable/start conversion

    Ax = ADC_Read();                    // read ADC to Ax
    //UART_Tx(Ax);                        // transmit Ax

    sum = sum + Ax;                 // add new value to sum
    if (cb->count < 16)
        enqueue(cb, Ax);
    else if (cb->count >= 16){
        dequeuedData = dequeue(cb);     // dequeue end of buffer
        sum = sum - dequeuedData;       // subtract dequeued value from sum
        avg = sum / 16;                 // average buffer values
        enqueue(cb, Ax);                // enqueue new buffer value

        dutyControl = (avg-120) << 8;   // get duty cycle by subtracting off the 120 baseline

        timerB_CCR1_Setup(dutyControl); // change duty cycle of TB1.1 using avg of Ax values
    }


    TB1CCTL0 &= ~CCIFG;                 // reset timer flag
}
