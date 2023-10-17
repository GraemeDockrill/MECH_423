#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

volatile unsigned int dutyCycle = 0;
volatile unsigned int state = 0;
unsigned volatile char Rx = 0;
unsigned volatile char sendData = 0;
unsigned volatile char startByte;
unsigned volatile char cmdByte;
unsigned volatile char dataByte;
unsigned volatile int parsingMessage = 0;
unsigned volatile int oscillationPeriod = 1000;

#define BUFFER_SIZE 50

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
    timerBSetup(39999);
    timerB_CCR1_Setup(0);               // setting up TB1.1 cycle to 500 Hz, 50% duty
    UART_Setup();

    // setting up P3.4 as TB1.1 output
    P3DIR |= BIT4;                      // setting P3.4 as output
    P3SEL0 |= BIT4;                     // setting P3.4 as TB1.1

    // Global interrupt enable
    _EINT();

    while(1){
        int i;
        // delay loop - do nothing
        for(i = 0; i < 30000; i++){
            _NOP();__delay_cycles(2);
        }

        switch(state){
        case 0: // getting brighter
            dutyCycle = dutyCycle + 1000;
            break;
        case 1: // dimming
            dutyCycle = dutyCycle - 1000;
            break;
        case 2: // dark orb
            dutyCycle = 0;
            break;
        }

        if (dutyCycle >= 39 * oscillationPeriod)
            state = 1;              // start counting down
        else if (dutyCycle < 0)
            state = 0;              // start counting up

        timerB_CCR1_Setup(dutyCycle);
    }

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

    if(cb->count >= 3){
        parsingMessage = 1;                     // signal we're parsing a message
        startByte = dequeue(cb);
        cmdByte = dequeue(cb);
        dataByte = dequeue(cb);

        if (startByte == 255){ // make sure start byte is 255
            // if in frequency select mode (cmdByte == 1)
            if(cmdByte == 1){
                state = 0;
                dutyCycle = 0;
            }
            else if(cmdByte == 2){                   // orb goes from bright to dark
                state = 1;
                dutyCycle = 39 * oscillationPeriod;
            }
            else if(cmdByte == 4)                   // switch orb off
                state = 2;

            oscillationPeriod = dataByte << 2;

            timerBSetup(40 * oscillationPeriod);    // changing oscillation period of PWM
        }

        parsingMessage = 0;                     // signal we're done parsing a message
    }

}
