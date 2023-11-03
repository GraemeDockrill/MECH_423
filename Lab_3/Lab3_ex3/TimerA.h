/*
 * TimerA.h
 *
 *  Created on: Oct. 16, 2023
 *      Author: lukeg
 */

#ifndef TIMERA_H_
#define TIMERA_H_

void TimerA0Capture_Setup(void){

    // Set up Timer A:
        TA0CTL |= TASSEL__ACLK + MC_2 + TACLR;    // Pick the ACLK, set to continuous mode, set the interrupt flag
        //TA0CCTL0 |= CCIS_0 + CM_1 +              // Set input select to A, set capture mode to rising edge
               // CAP + CCIE + SCS;         // turn on capture, turn on the interrupt, set synchronous capture, set synch. Cap. compare input
    //                                            ^Probably don't need to do the synchronous stuff?
        TA0CCTL1 |= CCIS_0 + CM_3 + CAP + SCS;
        //TA0CCTL2 |= CCIS_0 + CM_1 + CAP + CCIE + SCS;

        // Set 1.6 as input
        P1DIR &= ~BIT0;
        P1SEL0 |= BIT0;
        P1SEL1 &= ~BIT0;     // Want to capture the signal into TA0CCR1;


        TA0CCTL1 |= CCIE;      // Enable CCR1 interrupt
        TA0CCTL1 &= ~BIT0;     // Disable the ...CCR1 Interrupt flag
}

// vector = TIMER0_A0_VECTOR is for CCR0, not used for capture.
// vector = TIMER0_A1_VECTOR is the vector for capture.

#endif /* TIMERA_H_ */
