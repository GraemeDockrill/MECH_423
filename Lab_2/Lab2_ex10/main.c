#include <msp430.h> 
#include <msp_setup_functions.h>

// defines and global variables
unsigned volatile char Rx = 0;
unsigned volatile char sendData = 0;
unsigned volatile char startByte;
unsigned volatile char cmdByte;
unsigned volatile char dataByte1;
unsigned volatile char dataByte2;
unsigned volatile char escByte;
unsigned volatile int dataWord;
unsigned volatile int parsingMessage = 0;

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
