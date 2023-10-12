#include <msp430.h> 

#define BUFFER_SIZE 50

typedef struct{

    char buffer[BUFFER_SIZE];
    int head;
    int tail;
    int full;

} buffer;

void initializeBuffer(buffer* cb){
    cb->head = 0;
    cb->tail = 0;
    cb->full = 0;
}

int isEmpty(const buffer *buffer{

}

void enqueue(buffer* cb, char data){


}


char dequeue(buffer* cb){

}

/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	




	return 0;
}
