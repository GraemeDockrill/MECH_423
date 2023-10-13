#include <msp430.h> 

// defines and global variables
#define BUFFER_SIZE 50

unsigned volatile char data = 0;

// Setup Functions--------------------------------
void clockSetup (void){
    // configuring clocks
    CSCTL0 = CSKEY;                             // unlocking clock
    CSCTL1 |= DCOFSEL1 + DCOFSEL0;              // set DCO to 8MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;          // ACLK = DCO, SMCLK = DCO, MCLK = DCO
    CSCTL3 = DIVS__8 + DIVM__8;                // SMCLK/8, MCLK/8
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

void UART_Tx (unsigned char TxByte){
    while (!(UCA0IFG & UCTXIFG));       // wait until the previous Tx is finished
    UCA0TXBUF = TxByte;                 // send TxByte
}

char UART_Rx (void){
    unsigned char RxByte = 0;               // Create an unsigned character variable (8 bits)
    RxByte = UCA0RXBUF;                     // Get the new byte from the Rx buffer
    return RxByte;                          // return RxByte
}

void UART_string (char string[]){
    // loop through string character by character and send over serial
    int i;
    for(i = 0; string[i] != '\0'; i++)
        UART_Tx(string[i]);
}

typedef struct{

    char buffer[BUFFER_SIZE];
    int head;
    int tail;
    int full;

} CircularBuffer;

// sets up buffer
void initializeBuffer(CircularBuffer* cb){
    cb->head = 0;
    cb->tail = 0;
    cb->full = 0; // 1 = full
}

// checks if buffer is empty. returns cb->full
int isEmpty(const CircularBuffer *cb){
//    if(!cb->full && (cb->head == cb->tail))
//        return 1;
//    else
//        return 0;

    return cb->full;
}

// enqueues a char given and returns 1 if successful
int enqueue(CircularBuffer* cb, char data){
    if(!cb->full){
        cb->buffer[cb->head] = data;
        cb->head = (cb->head + 1) % BUFFER_SIZE;    // warp around to beginning if overflows
        if(cb->head == cb->tail)
            cb->full = 1;                           // if buffer full, set full to 1
        return 1;                                   // return 1 since successful
    }
    return 0;                                       // return 0 if queue full
}

// dequeues a char from the buffer. returns 1 if works, 0 otherwise
int dequeue(CircularBuffer* cb, char* data){
    // if buffer is not empty
    if(!isEmpty(cb)){
        *data = cb->buffer[cb->tail];               // stores tail in data
        cb->tail = (cb->tail + 1) % BUFFER_SIZE;     // advance tail and wrap around if overflow
        cb->full = 0;                               // buffer no longer full
        return 1;                                   // return 1 for success
    }
    return 0;                                       // return 0 for empty queue
}


volatile CircularBuffer cb;      // create buffer struct



/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
	initializeBuffer(&cb);  // initialize buffer at its memory location

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
    char Rx = UART_Rx();

    // if carriage return received over UART, dequeue and send dequeued
    if(Rx == 13){
        if(dequeue(&cb, &data))
            UART_Tx(data);                  // transmit dequeued byte
        else
            UART_string("Queue full!");     // error for empty queue
    }
    else
        // enqueue data from UART
        if(enqueue(&cb, data))
            _NOP();
        else
            UART_string("Queue full!");     // error for enqueuing full queue

}
