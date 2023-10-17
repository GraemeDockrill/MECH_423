#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

volatile int buttonState = 0;


/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
	// configuring clocks
    clockSetup();
    timerBSetup(7999);
    timerB_CCR1_Setup(0);             // setting up TB1.1 cycle to 500 Hz, 50% duty

    // setting up P3.4 as TB1.1 output
    P3DIR |= BIT4;                      // setting P3.4 as output
    P3SEL0 |= BIT4;                     // setting P3.4 as TB1.1

    // set P4.0 as a digital input
    P4DIR &= ~BIT0;

    // set up P4.0 as GPIO
    P4SEL0 &= ~BIT0;
    P4SEL1 &= ~BIT0;

    // set P4.1 as a digital input
    P4DIR &= ~BIT1;

    // set up P4.1 as GPIO
    P4SEL0 &= ~BIT1;
    P4SEL1 &= ~BIT1;

    // enable internal pull-up resistors for switch S1 (attached to pin P4.0)
    P4REN |= BIT0;
    P4OUT |= BIT0;              // set P4.0 to use pull-up resistor page 293

    // enable internal pull-up resistors for switch S2 (attached to pin P4.1)
    P4REN |= BIT1;
    P4OUT |= BIT1;              // set P4.1 to use pull-up resistor page 293

    // set P4.0 to get interrupted from rising edge & enable the interrupt
    P4IES |= BIT0;             // interrupt falling edge select
    P4IE |= BIT0;               // interrupt enable
    P4IES |= BIT1;             // interrupt falling edge select
    P4IE |= BIT1;               // interrupt enable

    // disable interrupt flag of P4.0,P4.1
    P4IFG &= ~BIT0;
    P4IFG &= ~BIT1;


    // Global interrupt enable
    _EINT();

    while(1);

	return 0;
}

// ISR
#pragma vector = PORT4_VECTOR;
__interrupt void buttonInterrupt(void){

    switch(P4IV){
    case P4IV_P4IFG0:
        buttonState = buttonState + 1;  // increment buttonState on S1 press
        P4IFG &= ~BIT0;         // reset interrupt flag
        break;
    case P4IV_P4IFG1:
        buttonState = buttonState - 1;  // decrement buttonState on S1 press
        P4IFG &= ~BIT1;         // reset interrupt flag
        break;
    }


    if (buttonState > 8)
        buttonState = 8;
    else if (buttonState < 0)
        buttonState = 0;

    switch(buttonState){
    case 0:
        timerB_CCR1_Setup(0);             // setting up TB1.1 cycle to 500 Hz, 0% duty
        break;
    case 1:
        timerB_CCR1_Setup(999);             // setting up TB1.1 cycle to 500 Hz, 1/8% duty
        break;
    case 2:
        timerB_CCR1_Setup(1999);             // setting up TB1.1 cycle to 500 Hz, 50% duty
        break;
    case 3:
        timerB_CCR1_Setup(2999);             // setting up TB1.1 cycle to 500 Hz, 50% duty
        break;
    case 4:
        timerB_CCR1_Setup(3999);             // setting up TB1.1 cycle to 500 Hz, 50% duty
        break;
    case 5:
        timerB_CCR1_Setup(4999);             // setting up TB1.1 cycle to 500 Hz, 50% duty
        break;
    case 6:
        timerB_CCR1_Setup(5999);             // setting up TB1.1 cycle to 500 Hz, 50% duty
        break;
    case 7:
        timerB_CCR1_Setup(6999);             // setting up TB1.1 cycle to 500 Hz, 50% duty
        break;
    case 8:
        timerB_CCR1_Setup(7999);             // setting up TB1.1 cycle to 500 Hz, 50% duty
        break;
    }

}

