#include <msp430.h> 

// global variables for acceleration
unsigned volatile int result = 0;
unsigned volatile char Ax = 0;
unsigned volatile char Ay = 0;
unsigned volatile char Az = 0;

unsigned int accelerometerState = 0;

/**
 * main.c
 */
int main(void)
{
	WDTCTL = WDTPW | WDTHOLD;	// stop watchdog timer
	
	// set P3.0, P3.1, P3.2 as A12-A14
	P3SEL0 |= BIT0 + BIT1 + BIT2;
	P3SEL1 |= BIT0 + BIT1 + BIT2;

	// configuring P2.7 as output
	P2DIR |= BIT7;
	P2SEL1 &= ~BIT7;
	P2SEL0 &= ~BIT7;

	// set to output high to power accelerometer
	P2OUT |= BIT7;

	// initial ADC configuration
	ADC10CTL0 |= ADC10ON;               // turn on the ADC
	ADC10CTL1 |= ADC10SSEL_3;           // select SMCLK for ADC10_B
	ADC10CTL2 &= ~ADC10RES;             // set resolution to 8 bit

	// set up ADC10IE interrupt
	ADC10IE |= ADC10IE0;

	// read from A12

	// Global interrupt enable
	_EINT();

	while(1){
	    // switch case for reading data from accelerometer
//        switch(state){
//            case 0:
//                // set up ADC to read port A12
//                // enable conversion
//                // convert
//                // disable  conversion
//                // store ADC10MEM0->Ax
//                break;
//            case 1:
//                // set up ADC to read port A13
//                // enable conversion
//                // convert
//                // disable  conversion
//                // store ADC10MEM0->Ay
//                break;
//            case 2:
//                // set up ADC to read port A14
//                // enable conversion
//                // convert
//                // disable  conversion
//                // store ADC10MEM0->Az
//                break;
//            default:
//                break;
//        }
	}

	return 0;
}



#pragma vector = ADC10_VECTOR       // interrupt vector for ADC10 interrupt
__interrupt void ADC10_Conversion_ISR(void)
{
    if(ADC10IFG0){
        switch (accelerometerState)
            {
                case 0:
                    result = ADC10MEM0;                 // read converted ADC memory
                    result >> 8;                        // bit shift 8 bits
                    Ax = result;
                    if (Ax >= 255)
                        Ax = 254;

                    // set up ADC for next input
                    ADC10CTL0 &= ~ADC10ENC;         // disable conversion
                    ADC10MCTL0 = ADC10INCH_13;      // set up ADC to read A13 (Ay)
                    ADC10CTL0 |= ADC10ENC + ADC10SC;// enable/start conversion

                    accelerometerState = 1;

                    break;
                case 1:
                    result = ADC10MEM0;                 // read converted ADC memory
                    result >> 8;                        // bit shift 8 bits
                    Ay = result;
                    if (Ay >= 255)
                        Ay = 254;

                    // set up ADC for next input
                    ADC10CTL0 &= ~ADC10ENC;         // disable conversion
                    ADC10MCTL0 = ADC10INCH_14;      // set up ADC to read A14 (Az)
                    ADC10CTL0 |= ADC10ENC + ADC10SC;// enable/start conversion

                    accelerometerState = 2;                    // bit shift 8 bits

                    break;
                case 2:
                    result = ADC10MEM0;                 // read converted ADC memory
                    result >> 8;                        // bit shift 8 bits
                    Az = result;
                    if (Az >= 255)
                        Az = 254;

                    // set up ADC for next input
                    ADC10CTL0 &= ~ADC10ENC;         // disable conversion
                    ADC10MCTL0 = ADC10INCH_12;      // set up ADC to read A12 (Ax)
                    ADC10CTL0 |= ADC10ENC + ADC10SC;// enable/start conversion

                    accelerometerState = 0;                 // bit shift 8 bits

                    break;
                default:
                    break;
            }
    }
}



//#pragma vector = ADC10_VECTOR       // interrupt vector for TA0 interrupt
//__interrupt void ADC10_Conversion_ISR(void)
//{
//
//
//
//
//}
//}
