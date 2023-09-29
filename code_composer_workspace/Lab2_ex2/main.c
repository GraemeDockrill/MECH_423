#include <msp430.h> 


/**
 * main.c
 */
int main(void)
{
    WDTCTL = WDTPW | WDTHOLD;   // stop watchdog timer

    // initial setup
    CSCTL0 = CSKEY; // give clock password

    // configuring DCO to run at 8 MHz
    // CSCTL1 |= 0x0006;
    CSCTL1 |= DCOFSEL_3;

    // select SMCLK source
    //CSCTL2 |= SELM__DCOCLK; // this was MCLK
    CSCTL2 |= SELS__DCOCLK;

    // set up with a divider of 32
    CSCTL3 |= DIVS__32;

    // configure Pin 3.4 as output - go to family guide pg 314 and look for PxDIR
    //P3DIR |= BIT4 | BIT5; // the | BIT5 is for further exercises of blinking multiple LEDs
    //P3SEL1 &= ~(BIT4 | BIT5);
    //P3SEL0 &= ~(BIT4 | BIT5);

    // configure Pin 3.4 as output
    P3DIR |= BIT4; // setting pin 3.4 as an output
    // choosing GPIO for pin 3.4
    P3SEL1 |= BIT4; // setting only BIT4 of P3SEL1 register to 0
    P3SEL0 |= BIT4; // setting only BIT4 of P3SEL0 register to 0


    while(1){
        _NOP();
    }

    return 0;
}
