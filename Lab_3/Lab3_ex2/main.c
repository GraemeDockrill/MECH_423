#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

// defines and global variables
unsigned volatile char Rx = 0;
unsigned volatile char startByte;
unsigned volatile char dutyByte;
unsigned volatile char dirByte = 0;
unsigned volatile int parsingMessage = 0;

unsigned volatile int motorSpeed = 0;

#define BUFFER_SIZE 50

volatile CircularBuffer* cb;

/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
    cb = createCircularBuffer(BUFFER_SIZE); // create circular buffer at cb location

	// setup functions
	CSCTL0 = CSKEY;                             // unlocking clock
	CSCTL1 = DCORSEL;                           // set DCO to 16MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;          // ACLK = DCO, SMCLK = DCO, MCLK = DCO
    CSCTL3 |= DIVM__8;                          // MCLK/8, ACLK 16MHz
	timerBSetupContinuous();
	timerB_CCR0_Setup(0xFFFF);
	timerB_CCR1_Setup(motorSpeed);
	//timerB_CCR2_Setup(motorSpeed);
    UART1_Setup();                              // set up UART1 at 19200 Baud

	// 16bit precesion - 65535 decimal

	// set P3.4 and P3.5 as outputs for DRV8841 B1 & B2
	P3DIR |= BIT4 + BIT5;
	P3SEL0 |= BIT4 + BIT5;                     // setting P3.4 as TB1.1
	P3OUT &= ~BIT5;

	// Global interrupt enable
	_EINT();

	while(1);

	return 0;
}

#pragma vector = USCI_A1_VECTOR             // interrupt vector for Rx interrupt
__interrupt void USCI_A1_ISR(void)
{
    Rx = UART1_Rx();                         // get char from UART

    if(parsingMessage == 1)
        UART1_string("Currently parsing a byte!");
    // ensure you get the start byte first
    else if(cb->count == 0 && Rx == 255){
        enqueue(cb, Rx);                        // enqueue the start byte
    }
    else if(cb->count > 0){
        enqueue(cb, Rx);                        // enqueue the received byte
    }

    if(cb->count >= 3){
        parsingMessage = 1;                     // signal we're parsing a message
        startByte = dequeue(cb);
        dutyByte = dequeue(cb);
        dirByte = dequeue(cb);
        UART1_Tx(startByte);
        UART1_Tx(dutyByte);
        UART1_Tx(dirByte);

        if(startByte == 255){
            // turning duty cycle byte into motor speed
            motorSpeed = (65534.0 * dutyByte) / 100;

            // if dirByte = 1, CW, else if dirByte = 0, CCW
            if(dirByte == 1){
                TB1CCTL1 = OUTMOD_5;
                timerB_CCR2_Setup(motorSpeed);          // change motor speed
            }
            else if(dirByte == 0){
                TB1CCTL2 = OUTMOD_5;
                timerB_CCR1_Setup(motorSpeed);          // change motor speed
            }
        }

        parsingMessage = 0;                     // signal we're done parsing a message
    }

}
