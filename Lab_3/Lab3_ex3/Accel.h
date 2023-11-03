/*
 * Accel.h
 *
 *  Created on: Oct. 16, 2023
 *      Author: lukeg
 */

#ifndef ACCEL_H_
#define ACCEL_H_

void accelerometerSetup(void){
    // Output high to Pin 2.7 - Power to accelerometer
    P2DIR |= BIT7;
    P2OUT |= BIT7;

    // These pins connect from accelerometer output to the ADC
    P3SEL0 |= (BIT0 + BIT1 + BIT2);
    P3SEL1 |= (BIT0 + BIT1 + BIT2);

    // Below is the ADC setup:
    ADC10CTL0 &= ~ADC10ENC;

    ADC10CTL1 |= ADC10SHP + ADC10CONSEQ_0 + ADC10SSEL_1;     // Clock controls are also in ADC10CTL1
    ADC10CTL0 &= ~ADC10SHT_2;
    ADC10CTL0 |= (ADC10ON) | ADC10SHT_4;                    // Turn on ADC and set sample rate
    ADC10CTL2 |= ADC10RES;                                  // Set resolution to 10 bit:

    // Enable Conversion (THIS LOCKS ADC10CTLn and 10INCH_)
    ADC10CTL0 |= (ADC10ENC + ADC10SC);
}

unsigned char getADCResult(void)
{
    unsigned char result;
    while(ADC10CTL1 & ADC10BUSY)  // Wait for ADC to not be busy
        _NOP();

    result = ADC10MEM0 >> 2;        // Bit shift to make it 8 bits
    if(result == 255)              // Change result from 0xFF -> We can't have this value.
         result = 254;
    return result;
}

#endif /* ACCEL_H_ */
