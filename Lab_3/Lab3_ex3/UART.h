/*
 * UART.h
 *
 *  Created on: Oct. 16, 2023
 *      Author: lukeg
 */

#ifndef UART_H_
#define UART_H_

void UARTSetup(void){
    // Configure ports P2.0 and P2.1 for UART Rx and Tx
    P2SEL0 &= ~(BIT0 + BIT1);
    P2SEL1 |= BIT0 + BIT1;

    UCA0CTLW0 |= UCSWRST; // Put the UART in software reset
    UCA0CTLW0 |= UCSSEL__ACLK; // Run the UART using ACLK
    UCA0MCTLW = UCOS16 + UCBRF0 + 0x4900; // Baud rate = 9600 from an 8 MHz clock
    UCA0BRW = 52;
    UCA0CTLW0 &= ~UCSWRST; // release UART for operation
    UCA0IE |= UCRXIE;       // Enable UART recieve interrupt
    //UCA0IE |= UCTXIE; // Enable UART transmit interrupt
}

/*
void UART_Tx(unsigned char TxByte){

    while (!(UCA0IFG & UCTXIFG));   // Wait for Tx to complete
    UCA0TXBUF = TxByte;             // Write the desired char to the buffer

}
*/

void UART_Tx_str(unsigned char str[]){
    unsigned int i = 0;

    for(i = 0; str[i] != '\0'; i++){
        UART_Tx(str[i]);
    }
}

// vector = USCI_A0_VECTOR

#endif /* UART_H_ */
