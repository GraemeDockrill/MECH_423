#include <msp430.h> 
#include <../includes/msp_setup_functions.h>

// MECH 423 Lab 2 Exercise 6

volatile unsigned int rising_edge;
volatile unsigned int falling_edge;
volatile unsigned int edge_state = 0;
volatile unsigned int difference = 0;


/**
 * main.c
 */
int main(void)
{
    // EXERCISE 5 CODE-------------------

    WDTCTL = WDTPW | WDTHOLD;   // stop watchdog timer

    // setting up clock and timer B
    clockSetup();
    timerBSetup(1999);
    timerB_CCR1_Setup(999);
    timerB_CCR2_Setup(499);

    // setting up P3.4 as TB1.1 output
    P3DIR |= BIT4;                      // setting P3.4 as output
    P3SEL0 |= BIT4;                     // setting P3.4 as TB1.1

    // setting up P3.5 as TB1.2 output
    P3DIR |= BIT5;                      // setting P3.5 as output
    P3SEL0 |= BIT5;                     // setting P3.5 as TB1.2

    // EXERCISE 6 CODE-------------------

    // set up Timer A
    timerAInputSetup();

    // setting up P1.2 as timer A input
    P1DIR &= BIT2;
    P1SEL0 |= BIT2;
    P1SEL1 &= ~BIT2;

    // Global interrupt enable
    _EINT();

    while(1);

    return 0;
}


#pragma vector = TIMER1_A1_VECTOR       // interrupt vector for TA1 interrupt
__interrupt void TA1_ISR(void)
{
    switch (TA1IV)
    {
        case TA1IV_TACCR1:
        {
            if(edge_state == 0){
                rising_edge = TA1CCR1;
                edge_state = 1;
                TA1CCTL1 = CM_2 + CCIS_0 + CAP + SCS + CCIE;    // change timer A1.1 to trigger on falling edge

            }
            else if (edge_state == 1){
                falling_edge = TA1CCR1;
                edge_state = 0;
                TA1CCTL1 = CM_1 + CCIS_0 + CAP + SCS + CCIE;    // change timer A1.1 to trigger on rising edge

                if (falling_edge - rising_edge > 0)
                    difference = falling_edge - rising_edge;

            }

            TA0CTL &= ~TAIFG;               // reset timer A interrupt flag
            break;
        }
    }

}
