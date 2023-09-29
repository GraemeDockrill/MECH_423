#include <msp430.h> 


/**
 * main.c
 */
int main(void)
{
    WDTCTL = WDTPW | WDTHOLD;   // stop watchdog timer

    // initial setup
    CSCTL0 = CSKEY; // give clock password

    // configure pins PJ.0, PJ.1, PJ.2, PJ.3, P3.4, P3.5, P3.6, P3.7 as digital outputs

    // set the P3.4 - P3.7 pins to outputs
    P3DIR |= (BIT4 | BIT5 | BIT6 | BIT7); // or equals the BITs makes them outputs

    // choose P3.4 - P3.7 pins to be general purpose I/O
    P3SEL0 &= ~(BIT4 | BIT5 | BIT6 | BIT7);
    P3SEL1 &= ~(BIT4 | BIT5 | BIT6 | BIT7);

    //set PJ.0-PJ.3 pins to be outputs
    PJDIR |= (BIT0 | BIT1 | BIT2 | BIT3);

    // choose PJ.0-PJ.3 pins to be general purpose I/O
    PJSEL0 &= ~(BIT0 | BIT1 | BIT2 | BIT3);
    PJSEL1 &= ~(BIT0 | BIT1 | BIT2 | BIT3);

    // setting output of LEDs to 10010011
    PJOUT |= (BIT0 | BIT3);
    PJOUT &= ~(BIT1 | BIT2);
    P3OUT |= (BIT6 | BIT7);
    P3OUT &= ~(BIT4 | BIT5);

    while(1){

        int i;
        // delay loop - do nothing
        for(i = 0; i < 30000; i++){
            _NOP();
        }

        // toggle LEDS 2,3,5,6
        PJOUT ^= (BIT1 | BIT2);
        P3OUT ^= (BIT4 | BIT5);
    }

    return 0;
}
