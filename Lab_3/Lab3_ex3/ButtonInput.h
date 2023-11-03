/*
 * ButtonInput.h
 *
 *  Created on: Oct. 16, 2023
 *      Author: lukeg
 */

#ifndef BUTTONINPUT_H_
#define BUTTONINPUT_H_

void buttonSetup(void){

    P4DIR &= ~(BIT0 + BIT1);    // Sets P4.0 and 4.1 to input direction:
    P4SEL0 &= ~(BIT0 + BIT1);
    P4SEL1 &= ~(BIT0 + BIT1);

    P4IES |= BIT0;              // Flag set on high->low transition for bit set
    P4IES &= BIT0;              // Flag set of low->high transition for bit reset

    P4IES |= BIT1;              // Flag set on high->low transition for bit set
    P4IES &= BIT1;              // Flag set of low->high transition for bit reset

    P4IE |= BIT0 + BIT1;        // Interrupt Enabled
    P4IFG = 0;                  // Interrupt flags cleared
}

// Vector: PORT4_VECTOR
// P4IV tells which pin has a flag.


#endif /* BUTTONINPUT_H_ */
