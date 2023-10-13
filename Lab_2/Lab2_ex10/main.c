#include <msp430.h> 

// defines and global variables
#define BUFFER_SIZE 50

unsigned volatile char Rx = 0;
unsigned volatile char sendData = 0;
unsigned volatile char startByte;
unsigned volatile char cmdByte;
unsigned volatile char dataByte1;
unsigned volatile char dataByte2;
unsigned volatile char escByte;
unsigned volatile int dataWord;
unsigned volatile int parsingMessage = 0;

// Setup Functions--------------------------------
void clockSetup (void){
    // configuring clocks
    CSCTL0 = CSKEY;                             // unlocking clock
    CSCTL1 |= DCOFSEL1 + DCOFSEL0;              // set DCO to 8MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;          // ACLK = DCO, SMCLK = DCO, MCLK = DCO
    CSCTL3 = DIVS__8 + DIVM__8;                 // SMCLK/8, MCLK/8
}

void timerBSetup (void){
    // setting up Timer B
    TB1CTL |= TBSSEL__SMCLK;            // TB1 using SMCLK
    TB1CTL |= ID__8;                   // TB1 with a CLK divider of 16
    TB1CTL |= MC__UP;                   // setting TB to up mode

    // setting TB1.1 cycle to 500Hz
    TB1CCTL1 = OUTMOD_7;                // set mode to Reset/Set

    // setting up square wave
    TB1CCR0 = 999;                      // setting compare latch TB1CL0 - CAN'T WRITE DIRECTLY TO TB1CL0
    TB1CCR1 = 499;                      // setting compare latch TB1CL1 - CAN'T WRITE DIRECTLY TO TB1CL0
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
    int count;

} CircularBuffer;

// sets up buffer
CircularBuffer* createCircularBuffer(int size){
    CircularBuffer* cb = (CircularBuffer*)malloc(sizeof(CircularBuffer));
    cb->buffer = (unsigned char*)malloc(size * sizeof(unsigned char));
    cb->size = size;
    cb->front = -1;
    cb->rear = -1;
    cb->count = 0;
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
    cb->count = cb->count + 1;
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

    cb->count = cb->count - 1;
    return data;
}


volatile CircularBuffer* cb;


/**
 * main.c
 */
int main(void)
{
    WDTCTL = WDTPW | WDTHOLD;   // stop watchdog timer

    cb = createCircularBuffer(BUFFER_SIZE); // create circular buffer at cb location

    // set up LED1 - by default GPIO
    PJDIR |= BIT0;

    // setting up P3.4 as TB1.1 output
    P3DIR |= BIT4;                      // setting P3.4 as output
    P3SEL0 |= BIT4;                     // setting P3.4 as TB1.1

    // set up clock and UART
    clockSetup();
    timerBSetup();
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

    if(parsingMessage == 1)
        UART_string("Currently parsing a byte!");
    else{
        enqueue(cb, Rx);                        // enqueue the received byte
    }

    if(cb->count >= 5){
        parsingMessage = 1;                     // signal we're parsing a message
        startByte = dequeue(cb);
        cmdByte = dequeue(cb);
        dataByte1 = dequeue(cb);
        dataByte2 = dequeue(cb);
        escByte = dequeue(cb);

        // if in frequency select mode (cmdByte == 1)
        if(cmdByte == 1){
            if(escByte == 1)                    // switch dataByte2 back
                dataByte2 = 255;
            else if(escByte == 2)               // switch dataByte1 back
                dataByte1 = 255;
            else if(escByte == 3){              // switch both dataBytes back
                dataByte2 = 255;
                dataByte1 = 255;
            }
            dataWord = (dataByte1 << 8) + dataByte2;    // combine both dataBytes into one

            TB1CCR0 = dataWord - 1;
            TB1CCR1 = (dataWord/2) - 1;

        }
        else if(cmdByte == 2)                   // switch LED1 on (cmdByte == 2)
            PJOUT |= BIT0;
        else if(cmdByte == 3)                   // switch LED1 off (cmdByte == 3)
            PJOUT &= ~BIT0;

        parsingMessage = 0;                     // signal we're done parsing a message
    }

}
