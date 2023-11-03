/*
 * CircularBuffer.h
 *
 *  Created on: Oct. 13, 2023
 *      Author: lukeg
 */

#ifndef CIRCULARBUFFER_H_
#define CIRCULARBUFFER_H_

typedef struct {
    unsigned char* buffer;
    int size;
    int front;
    int rear;
    int count;
} volatile CircularBuffer;

volatile CircularBuffer* createCircularBuffer(unsigned char size) {
    CircularBuffer* cb = (CircularBuffer*)malloc(sizeof(CircularBuffer));
    cb->buffer = (unsigned char*)malloc(size * sizeof(unsigned char));
    cb->size = size;            // This is the number of elements in the buffer
    cb->front = 0;              // This keeps track of
    cb->rear = 0;               // This keeps track of the
    cb->count = 0;              // This keeps track of how many elements are in there
    return cb;
}

void enqueue(CircularBuffer* cb, unsigned char data) {
    if (cb->count < cb->size) {
        cb->buffer[cb->rear] = data;
        cb->rear = (cb->rear + 1) % cb->size;
        cb->count++;
    }
    else{
        UART_Tx_str("Buffer Full!");
    }
}
unsigned char dequeue(CircularBuffer* cb) {
    if (cb->count > 0) {
        unsigned char data = cb->buffer[cb->front];
        cb->front = (cb->front + 1) % cb->size;
        cb->count--;
        return data;
    }
    else{
        UART_Tx_str("Buffer Empty!");
        return 255;
    }
}


#endif /* CIRCULARBUFFER_H_ */
