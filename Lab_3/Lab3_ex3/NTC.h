/*
 * NTC.h
 *
 *  Created on: Oct. 16, 2023
 *      Author: lukeg
 */

#ifndef NTC_H_
#define NTC_H_

void NTCSetup(void){
    P2DIR |= BIT7;
    P2OUT |= BIT7;                                          // Power the NTC

    P1SEL0 |= BIT4;
    P1SEL1 |= BIT4;                                         // NTC Input to ADC

    ADC10CTL0 &= ~(ADC10ENC);                               // Disable the conversion

    ADC10CTL0 |= (ADC10ON) + ADC10SHT_12;                   // Turn on ADC and take a 2^(ADC10SHT_##) sample
    ADC10CTL1 |= ADC10SHP + ADC10CONSEQ_0 + ADC10SSEL_3;    // Clock controls are also in ADC10CTL1, this is using SMCLK
    ADC10CTL2 |= ADC10RES;                                  // Set resolution to 10 bit:
    ADC10MCTL0 = ADC10INCH_4;                               // Read from the NTC

    ADC10CTL0 |= (ADC10ENC + ADC10SC);                      // Enable Conversion

    //set up reference voltage
    REFCTL0 |= REFVSEL_2 + REFON;    //Ref voltage = 2.
}

unsigned char getADCResult(void)
{
    unsigned char result;
    while(ADC10CTL1 & ADC10BUSY)    // Wait for ADC to not be busy
        _NOP();

    result = ADC10MEM0 >> 2;        // Bit shift to make it 8 bits
    if(result == 255)              // Change result from 0xFF -> We can't have this value.
         result = 254;
    return result;
}

#endif /* NTC_H_ */
