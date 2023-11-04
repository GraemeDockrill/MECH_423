#include <msp430.h> 
#include <Stepper_LUT.h>
#include <UART.h>
#include <TimerB.h>
//#include <CircularBuffer.h>
#include <../includes/msp_setup_functions.h>

#define STEPPER_DUTY_CYCLE 0x3FFF // 3FFF is 25% of FFFF; Might need to be BFFD (75% of FFFF)

/**
 * main.c
 */

// defines and global variables
volatile unsigned int state = 0;

unsigned volatile char Rx = 0;

unsigned volatile char startByte;
unsigned volatile char cmdByte0 = 4;
unsigned volatile char cmdByte1 = 0;
unsigned volatile char stepSpeed;
unsigned volatile char dutyCycle;

unsigned volatile int parsingMessage = 0;

unsigned volatile int motorSpeed = 1000;

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
    TB2CTL |= MC__UP;           // setting TB to up mode

    TB2CTL |= CNTL_0;                   // choose highest counter of 16bits 0xFFFFh

    // This sets up timer B1
    TB1CTL |= TBSSEL__ACLK;             // TB1 using SMCLK
    TB1CTL |= ID__1;                    // TB1 with a CLK divider of 1
    TB1CTL |= MC__UP;           // setting TB to up mode

    // This sets up timer B0
    TB0CTL |= TBSSEL__ACLK;             // TB1 using SMCLK
    TB0CTL |= ID__1;                    // TB1 with a CLK divider of 1
    TB0CTL |= MC__UP;                   // setting TB to up mode


    // Set all timers to STEPPER_DUTY_CYCLE (currently 25%):
    TB0CCR1 = STEPPER_DUTY_CYCLE;
    TB0CCR2 = STEPPER_DUTY_CYCLE;

    TB1CCR1 = STEPPER_DUTY_CYCLE;
    TB1CCR2 = STEPPER_DUTY_CYCLE;

    // We will use TB2CCR1 to control the speed
    TB2CCR0 = motorSpeed;

    // Enable interrupts
    TB2CCTL0 |= CCIE;
    _EINT();

    while(1){
        /*This section sets the outputs based on the states.*/
        switch(cmdByte0){
        case 4: // WS CW
            // A1 is TB0.2
            TB0CCTL2 = outputA1_WS[state];
            // A2 is TB0.1
            TB0CCTL1 = outputA2_WS[state];
            // B1 is TB1.2
            TB1CCTL2 = outputB1_WS[state];
            // B2 is TB1.1
            TB1CCTL2 = outputB2_WS[state];
            break;
        case 5:
            // A1 is TB0.2
            TB0CCTL2 = outputA1_WS[state];
            // A2 is TB0.1
            TB0CCTL1 = outputA2_WS[state];
            // B1 is TB1.2
            TB1CCTL2 = outputB1_WS[state];
            // B2 is TB1.1
            TB1CCTL2 = outputB2_WS[state];
            break;
        case 6: // HS CW
        case 7: // HS CCW
            TB0CCTL2 = outputA1_HS[state];
            TB0CCTL1 = outputA2_HS[state];
            TB1CCTL2 = outputB1_HS[state];
            TB1CCTL2 = outputB2_HS[state];
            break;
        default: // otherwise, _NOP();
            _NOP();
            break;
        }


        // only clear flags after states are set.
        TB2CCTL1 &= (~CCIFG);
    }




	return 0;
}

#pragma vector = TIMER2_B0_VECTOR             // interrupt vector for TB1 -> Control the state machine.
__interrupt void TA2_ISR(void)
{
    // for whole stepping:
    switch(cmdByte0){
        case 4:
            if(state < 4)
                state++;
            else
                state = 0;
            break;
        case 5:
            if(state < 1)
                state = 3;
            else
                state--;
            break;
        case 6:
            if(state < 8)
                state++;
            else
                state = 0;
            break;
        case 7:
            if(state < 1)
                state = 7;
            else
                state--;
            break;
        default:
            _NOP();
            break;

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

    if(cb->count >= 5){
        parsingMessage = 1;                     // signal we're parsing a message
        startByte = dequeue(cb);
        cmdByte0 = dequeue(cb);
        cmdByte1 = dequeue(cb);
        stepSpeed = dequeue(cb);
        dutyCycle = dequeue(cb);

        UART1_Tx(startByte);
        UART1_Tx(cmdByte0);
        UART1_Tx(cmdByte1);
        UART1_Tx(stepSpeed);
        UART1_Tx(dutyCycle);

        if(startByte == 255){
            // turning duty cycle byte into motor speed
            motorSpeed = (65534.0 * dutyCycle) / 100; // I don't love this. It's IO bound so maybe not a big deal.

            switch(cmdByte0){
                case 0:             // if cmdByte = 0, single whole step CW
                    TB2CCTL0 &= ~(CCIE);

                    if(state < 4)
                        state++;
                    else
                        state = 0;

                    // A1 is TB0.2
                    TB0CCTL2 = outputA1_WS[state];
                    // A2 is TB0.1
                    TB0CCTL1 = outputA2_WS[state];
                    // B1 is TB1.2
                    TB1CCTL2 = outputB1_WS[state];
                    // B2 is TB1.1
                    TB1CCTL2 = outputB2_WS[state];
                    break;

                case 1: // Whole step CCW
                    TB2CCTL0 &= ~(CCIE);

                    if(state < 1)
                        state = 3;
                    else
                        state--;

                    TB0CCTL2 = outputA1_WS[state];
                    TB0CCTL1 = outputA2_WS[state];
                    TB1CCTL2 = outputB1_WS[state];
                    TB1CCTL2 = outputB2_WS[state];
                    break;

                case 2: // Half step CW
                    TB2CCTL0 &= ~(CCIE);

                    if(state < 8)
                        state++;
                    else
                        state = 0;

                    TB0CCTL2 = outputA1_HS[state];
                    TB0CCTL1 = outputA2_HS[state];
                    TB1CCTL2 = outputB1_HS[state];
                    TB1CCTL2 = outputB2_HS[state];
                    break;

                case 3: // Half step CCW
                    TB2CCTL0 &= ~(CCIE);

                    if(state < 1)
                        state = 7;
                    else
                        state--;

                    TB0CCTL2 = outputA1_HS[state];
                    TB0CCTL1 = outputA2_HS[state];
                    TB1CCTL2 = outputB1_HS[state];
                    TB1CCTL2 = outputB2_HS[state];
                    break;

                case 4: // Whole step CW at speed = stepSpeed/4
                    TB2CCR0 |= stepSpeed;
                    TB2CCTL0 |= CCIE;
                    break;

                case 5: // Whole step CCW
                    TB2CCR0 |= stepSpeed;
                    TB2CCTL0 |= CCIE;
                    break;

                case 6:
                    TB2CCR0 |= stepSpeed;
                    TB2CCTL0 |= CCIE;
                    break;

                case 7:
                    TB2CCR0 |= stepSpeed;
                    TB2CCTL0 |= CCIE;
                    break;
                default:
                    _NOP();
                    break;


            }
        }

        parsingMessage = 0;                     // signal we're done parsing a message
    }

}
