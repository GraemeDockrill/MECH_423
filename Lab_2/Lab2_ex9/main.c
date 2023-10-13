#include <msp430.h> 

// defines and global variables
#define BUFFER_SIZE 50

unsigned char Rx = 0;
unsigned char sendData = 0;

// Setup Functions--------------------------------
void clockSetup (void){
    // configuring clocks
    CSCTL0 = CSKEY;                             // unlocking clock
    CSCTL1 |= DCOFSEL1 + DCOFSEL0;              // set DCO to 8MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;          // ACLK = DCO, SMCLK = DCO, MCLK = DCO
    CSCTL3 = DIVS__8 + DIVM__8;                 // SMCLK/8, MCLK/8
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
    while (!(UCA0IFG & UCTXIFG));           // wait until the previous Tx is finished
    UCA0TXBUF = TxByte;                     // send TxByte
}

char UART_Rx (void){
    unsigned char RxByte = 0;               // Create an unsigned character variable (8 bits)
    RxByte = UCA0RXBUF;                     // Get the new byte from the Rx buffer
    return RxByte;                          // return RxByte
}

void UART_string (unsigned char string[]){
    // loop through string character by character and send over serial
    int i;
    for(i = 0; string[i] != '\0'; i++)
        UART_Tx(string[i]);
}

typedef struct{

    unsigned char* buffer;
    int size;
    int front;
    int rear;

} CircularBuffer;

// sets up buffer
CircularBuffer* createCircularBuffer(int size){
    CircularBuffer* cb = (CircularBuffer*)malloc(sizeof(CircularBuffer));
    cb->buffer = (unsigned char*)malloc(size * sizeof(unsigned char));
    cb->size = size;
    cb->front = -1;
    cb->rear = -1;
    return cb;
}

// checks if buffer is full. returns 1 if full
int isFull(CircularBuffer* cb){
    return ((cb->rear + 1) % cb->size) == cb->front;
}

// checks if buffer is empty. returns 1 is empty
int isEmpty(CircularBuffer* cb){
    return cb->front == -1;
}

// enqueues a given char given. UARTs an error if queue full
void enqueue(CircularBuffer* cb, char data){
    if(isFull(cb)){
        UART_string("Buffer is full, cannot enqueue!");
        return;
    }
    if(isEmpty(cb))
        cb->front = cb->rear = 0;
    else{
        cb->rear = (cb->rear + 1) % cb->size;
    }
    cb->buffer[cb->rear] = data;
    return;
}

// dequeues a char from the buffer. returns data if works, -1 otherwise
char dequeue(CircularBuffer* cb){
    if(isEmpty(cb)){
        UART_string("Queue is empty, cannot dequeue!");
        return -1;
    }
    char data = cb->buffer[cb->front];
    if(cb->front == cb->rear)
        cb->front = cb->rear = -1;
    else
        cb->front = (cb->front + 1) % cb->size;

    return data;
}


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
    Rx = UART_Rx();                         // get char from UART

    if(Rx == 13){                           // if
        sendData = dequeue(cb);
        if(sendData == -1)                  // if dequeue failed do nothing
            _NOP();
        else
            UART_Tx(sendData);              // if dequeue successful, send dequeued char to UART
    }
    else
        enqueue(cb, Rx);                    // enqueue received data from UART
}
