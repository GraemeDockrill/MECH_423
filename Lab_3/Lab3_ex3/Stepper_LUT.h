/*
 * Stepper_LUT.h
 *
 *  Created on: Nov. 1, 2023
 *      Author: lukeg
 */

#ifndef STEPPER_LUT_H_
#define STEPPER_LUT_H_

// Whole Stepping LUT
static const unsigned int outputA1_WS[4] =
{
     1, 0, 0, 0
};
static const unsigned int outputA2_WS[4] =
{
     0, 0, 1, 0
};
static const unsigned int outputB1_WS[4] =
{
     0, 1, 0, 0
};
static const unsigned int outputB2_WS[4] =
{
     0, 0, 0, 1
};

// Half stepping LUT:
static const unsigned int outputA1_HS[8] =
{
     1, 1, 0, 0, 0, 0, 0, 1
};
static const unsigned int outputA2_HS[8] =
{
     0, 0, 0, 1, 1, 1, 0, 0
};
static const unsigned int outputB1_HS[8] =
{
     0, 1, 1, 1, 0, 0, 0, 0
};
static const unsigned int outputB2_HS[8] =
{
     0, 0, 0, 0, 0, 1, 1, 1
};



#endif /* STEPPER_LUT_H_ */
