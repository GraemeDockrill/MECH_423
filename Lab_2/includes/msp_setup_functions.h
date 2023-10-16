/*
 * msp_setup_functions.h
 *
 *  Created on: Oct 15, 2023
 *      Author: graemedd.stu
 */

#ifndef MSP_SETUP_FUNCTIONS_H_
#define MSP_SETUP_FUNCTIONS_H_

// functions
    // clockSetup
    // timerBSetup
    // TimerB_CCR2_Setup
    // UART_Setup
    // UART_Tx
    // UART_Rx
    // UART_string
    // isFull
    // isEmpty
    // enqueue
    // dequeue

// buffer size of circular buffer
//#define BUFFER_SIZE 50




// SETUP FUNCTIONS --------------------------------

// -------------------------------- CLOCK SETUP ---------------------------------


// sets up ACLK, SMCLK, MCLK to run at 8 MHz on the DCO
void clockSetup (void){
    // configuring clocks
    CSCTL0 = CSKEY;                             // unlocking clock
    CSCTL1 |= DCOFSEL_3;                        // set DCO to 8MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;          // ACLK = DCO, SMCLK = DCO, MCLK = DCO
    CSCTL3 = DIVS__8 + DIVM__8;                 // SMCLK/8, MCLK/8
}


// ------------------------------- TIMER B SETUP --------------------------------


// sets up Timer B to run at 1 MHz and produce a 500 Hz 50% duty square wave
void timerBSetup (int CCR0_val){
    // setting up Timer B
    TB1CTL |= TBSSEL__SMCLK;            // TB1 using SMCLK
    TB1CTL |= ID__1;                    // TB1 with a CLK divider of 1
    TB1CTL |= MC__UP;                   // setting TB to up mode

    // setting up square wave (CCR0 should be 1999 for 500 Hz)
    TB1CCR0 = CCR0_val;                 // setting compare latch TB1CL0 - CAN'T WRITE DIRECTLY TO TB1CL0
}

// sets up CCR1 to create a 500 Hz 50% duty square wave
void timerB_CCR1_Setup (int CCR1_val){
    // setting TB1.1 cycle to 500Hz, 50% duty cycle
    TB1CCTL1 = OUTMOD_7;                // set mode to Reset/Set

    // (CCR1 should be 1999 for 500 Hz, 50%)
    TB1CCR1 = CCR1_val;                 // setting compare latch TB1CL1 - CAN'T WRITE DIRECTLY TO TB1CL0
}

// sets up CCR2 to create a 500 Hz 25% duty square wave
void timerB_CCR2_Setup (int CCR2_val){
    // setting TB1.2 cycle to 500Hz, 25% duty cycle
    TB1CCTL2 = OUTMOD_7;                // set mode to Reset/Set

    // (CCR2 should be 499 for 500 Hz, 25%)
    TB1CCR2 = CCR2_val;                 // setting compare latch TB1CL2 - CAN'T WRITE DIRECTLY TO TB1CL0
}


// -------------------- UART COMMUNICATION SETUP AND METHODS --------------------


// set UART communication according to code given by Dr. Ma
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

// transmit a message over UART
void UART_Tx (unsigned char TxByte){
    while (!(UCA0IFG & UCTXIFG));           // wait until the previous Tx is finished
    UCA0TXBUF = TxByte;                     // send TxByte
}

// read and return a received UART message
char UART_Rx (void){
    unsigned char RxByte = 0;               // Create an unsigned character variable (8 bits)
    RxByte = UCA0RXBUF;                     // Get the new byte from the Rx buffer
    return RxByte;                          // return RxByte
}

// send each character in a string over UART
void UART_string (unsigned char string[]){
    // loop through string character by character and send over serial
    int i;
    for(i = 0; string[i] != '\0'; i++)
        UART_Tx(string[i]);
}


// --------------------- CIRCULAR BUFFER STRUCT AND METHODS ---------------------


// struct for circular buffer
typedef struct{

    unsigned char* buffer;
    int size;
    int front;
    int rear;

} CircularBuffer;

// creates buffer in memory and returns pointer to it
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

// enqueues a given char. sends an error over UART if queue full
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

// dequeues a char from the buffer. returns the char if works, -1 otherwise
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


// ------------------------------------------------------------------------------


#endif /* MSP_SETUP_FUNCTIONS_H_ */
