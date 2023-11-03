/*
 * LED_Functions.h
 *
 *  Created on: Oct. 16, 2023
 *      Author: lukeg
 */

#ifndef LED_FUNCTIONS_H_
#define LED_FUNCTIONS_H_

void LEDSetup(void){
    /* This function sets all LEDs to output mode */
    PJDIR |= BIT0 + BIT1 + BIT2 + BIT3;
    P3DIR |= BIT4 + BIT5 + BIT6 + BIT7;
}



void LEDMod(unsigned char LEDByte){
    /* This function sets LED//2 if remainder == 0, and resets LED//2 if remainder == 1.
    */
    unsigned char LEDNum = LEDByte / 2;
    unsigned char LEDState = LEDByte % 2; // State = 0 -> set, State = 1 -> Reset

    if(LEDNum < 4){             // If LED < 4 -> PJOUT
        if(LEDState == 0)       // Turn ON
            PJOUT |= 1 << LEDNum;

        else                    // Turn OFF
            PJOUT &= ~(1 << LEDNum);
    }
    else{                       // If LED > 3 -> P3OUT
        if(LEDState == 0)
            P3OUT |= 1 << LEDNum;
        else
            P3OUT &= ~(1 << LEDNum);
    }
}

void LED_series(unsigned char LEDNum){
    /* This function sets all LED's up to LEDByte (LEDByte = 0 sets none, LEDByte = 1 sets only LED 1, etc.)  */

    P3OUT = 0;             // Turn off all upper LEDs

    if(LEDNum < 4){   // For LEDs 1, 2, 3, and 4:
        PJOUT = 0;
        while(LEDNum > 0){
            PJOUT |= 1 << LEDNum;
            LEDNum--;
        }
    }
    else{                   // For LED's 5, 6, 7, and 8:
        PJOUT = (0xFF);     // Turn on all lower LEDs
        while(LEDNum > 4){
            P3OUT |= 1 << LEDNum;
            LEDNum--;
        }

    }

}

#endif /* LED_FUNCTIONS_H_ */
