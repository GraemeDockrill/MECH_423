/*
 * TimerB.h
 *
 *  Created on: Oct. 16, 2023
 *      Author: lukeg
 */

#ifndef TIMERB_H_
#define TIMERB_H_

void TimerBSetup_PWMOut(void){
    // Set up timer B in "up-count" on SMCLK:
    TB1CTL |= TBSSEL__SMCLK;     // Set the timer to use ACLK
    TB1CTL |= MC__UP;       // Set the mode to "up-count" to CCR0
    TB1CTL |= ID__1;         // Set internal divider
    // Set outputs (AND ISRS???)
    TB1CCTL1 |= OUTMOD_7;    // PWM out -> toggle/reset?
    TB1CCTL2 |= OUTMOD_7;    // PWM out -> toggle/reset?

    TB1CCR0 = 1999;          // The timer will count to 2 000 before ISR -> 500 Hz toggle
    TB1CCR1 = 1000;         // The CCR will trigger at 1000 -> halfway through.
    TB1CCR2 = 500;         // The CCR will trigger at 1500 -> 25% remaining

    // Set pin 3.4 to output from TB1.1:
    P3DIR |= BIT4;
    P3SEL0 |= BIT4;

    // Set pin 3.5 to output from TB1.2:
    P3DIR |= BIT5;
    P3SEL0 |= BIT5;
}

void TimerBSetup_25Hz(void){
    // Set up timer B in "up-count" using SMCLK:
    TB1CTL |= TBSSEL__SMCLK;    // Set the timer to use SMCLK
    TB1CTL |= MC__UP;           // Set the mode to "up-count" to CCR0
    TB1CTL |= ID__1;            // Set internal divider
    TB1CCTL0 |= CCIE;           // Turn interrupt on
    TB1CTL &= ~TBIFG;           // Disable Flag

    CSCTL3 |= DIVS__8;              // Divides SMCLK source by 8 (Now 1 000 kHz)
    TB1CCR0 = 39999;                // The timer will count to 20 000 before ISR -> 25 Hz toggle
}

#endif /* TIMERB_H_ */
