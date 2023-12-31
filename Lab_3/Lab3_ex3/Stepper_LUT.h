/*
 * Stepper_LUT.h
 *
 *  Created on: Nov. OUTMOD_7, 2023
 *      Author: lukeg
 */

#ifndef STEPPER_LUT_H_
#define STEPPER_LUT_H_

#define OUTMOD_7               (7*0x20u)      /* PWM output mode: 7 - PWM reset/set */
#define OUTMOD_5               (5*0x20u)      /* PWM output mode: 5 - Reset */


// include everything that needs to be written to TBXCCTLX
static const unsigned int on = OUTMOD_7;
static const unsigned int off = OUTMOD_5;

// Whole Stepping LUT
static const unsigned int outputA1_WS[4] =
{
     on, off, off, off
};
static const unsigned int outputA2_WS[4] =
{
     off, off, on, off
};
static const unsigned int outputB1_WS[4] =
{
     off, on, off, off
};
static const unsigned int outputB2_WS[4] =
{
     off, off, off, on
};

// Half stepping LUT:
static const unsigned int outputA1_HS[8] =
{
     on, on, off, off, off, off, off, on
};
static const unsigned int outputA2_HS[8] =
{
     off, off, off, on, on, on, off, off
};
static const unsigned int outputB1_HS[8] =
{
     off, on, on, on, off, off, off, off
};
static const unsigned int outputB2_HS[8] =
{
     off, off, off, off, off, on, on, on
};



#endif /* STEPPER_LUT_H_ */
