#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

// defines and global variables
unsigned char RxByte = 0;
unsigned char sendData = 0;

#define BUFFER_SIZE 50

volatile CircularBuffer* cb;


/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
	cb = createCircularBuffer(BUFFER_SIZE);

	// set up clock and UART
	clockSetup();
    UART_Setup();

	// Global interrupt enable
    _EINT();

	while(1);

	return 0;
}


#pragma vector = USCI_A0_VECTOR             // interrupt vector for Rx interrupt
__interrupt void USCI_A0_ISR(void)
{
    RxByte = UART_Rx();                         // get char from UART

    if(RxByte == 13){                           // if
        sendData = dequeue(cb);
        if(sendData == -1)                  // if dequeue failed do nothing
            _NOP();
        else
            UART_Tx(sendData);              // if dequeue successful, send dequeued char to UART
    }
    else
        enqueue(cb, RxByte);                    // enqueue received data from UART
}
