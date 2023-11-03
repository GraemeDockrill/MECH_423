#include <msp430.h> 
#include <Stepper_LUT.h>
#include <UART.h>
#include <TimerB.h>
//#include <CircularBuffer.h>
#include <../includes/msp_setup_functions.h>

#define STEPPER_DUTY_CYCLE 0x3FFF

/**
 * main.c
 */

// defines and global variables
volatile unsigned int state = 0;

unsigned volatile char Rx = 0;
unsigned volatile char startByte;
unsigned volatile char dutyByte;
unsigned volatile char dirByte = 0;
unsigned volatile int parsingMessage = 0;

unsigned volatile int motorSpeed = 0;

#define BUFFER_SIZE 50

volatile CircularBuffer* cb;

int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
    // setup functions
    CSCTL0 = CSKEY;                             // unlocking clock
    CSCTL1 |= DCORSEL;
    CSCTL1 |= DCOFSEL_0;                        // set DCO to 16MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;          // ACLK = DCO, SMCLK = DCO, MCLK = DCO
    CSCTL3 = DIVM__8;                           // MCLK/8

    UARTSetup(); // This will be at 19200 baud for a 16 MHz ACLK

    // This sets up timer B2
    TB2CTL |= TBSSEL__ACLK;             // TB1 using SMCLK
    TB2CTL |= ID__1;                    // TB1 with a CLK divider of 1
    TB2CTL |= MC__CONTINUOUS;           // setting TB to up mode

    TB2CTL |= CNTL_0;                   // choose highest counter of 16bits 0xFFFFh

    TB2CTL |= MC__CONTINUOUS;

    // This sets up timer B1
    TB1CTL |= TBSSEL__ACLK;             // TB1 using SMCLK
    TB1CTL |= ID__1;                    // TB1 with a CLK divider of 1
    TB1CTL |= MC__CONTINUOUS;           // setting TB to up mode

    TB1CTL |= CNTL_0;                   // choose highest counter of 16bits 0xFFFFh

    TB1CTL |= MC__CONTINUOUS;

    // This sets up timer B0
    TB0CTL |= TBSSEL__ACLK;             // TB1 using SMCLK
    TB0CTL |= ID__1;                    // TB1 with a CLK divider of 1
    TB0CTL |= MC__CONTINUOUS;           // setting TB to up mode

    TB0CTL |= CNTL_0;                   // choose highest counter of 16bits 0xFFFFh

    TB0CTL |= MC__CONTINUOUS;


    // Set all timers to STEPPER_DUTY_CYCLE (currently 25%):
    TB0CCR1 = STEPPER_DUTY_CYCLE;
    TB0CCR2 = STEPPER_DUTY_CYCLE;

    TB1CCR1 = STEPPER_DUTY_CYCLE;
    TB1CCR2 = STEPPER_DUTY_CYCLE;

    // We will use TB2CCR1 to control the speed
    TB2CCR1 = motorSpeed;

    // Enable interrupts
    TB2CCTL1 |= CCIE;
    _EINT();

    while(1){
        // A1 is TB0.2
        TB0CCTL2 = outputA1_WS[state];
        // A2 is TB0.1
        TB0CCTL1 = outputA2_WS[state];
        // B1 is TB1.2
        TB1CCTL2 = outputB1_WS[state];
        // B2 is TB1.1
        TB1CCTL2 = outputB2_WS[state];

        // only clear flags after states are set.
        TB2CCTL1 &= (~CCIFG);
    }




	return 0;
}

#pragma vector = TIMER2_B1_VECTOR             // interrupt vector for TB1 -> Control the state machine.
__interrupt void TB2_ISR(void)
{
    // for whole stepping:
    if(dirByte){
        if(state < 4)
            state++;
        else
            state = 0;
    }
    else{
        if(state < 1)
            state = 3;
        else
            state--;
    }
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
            TB2CCR1 = (motorSpeed);             // changing motor speed

            // if dirByte = 1, CW, else if dirByte = 0, CCW
            if(dirByte == 1){
                P3OUT &= ~BIT7;
                P3OUT |= BIT6;
            }
            else if(dirByte == 0){
                P3OUT &= ~BIT6;
                P3OUT |= BIT7;
            }
        }

        parsingMessage = 0;                     // signal we're done parsing a message
    }

}
