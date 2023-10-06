#include <msp430.h>

// MECH 423 Example code for configuring clocks and UARTs

int main(void)
{
    int i;

    WDTCTL = WDTPW | WDTHOLD; // stop watchdog timer

    // Configure clocks
    CSCTL0 = 0xA500;                        // Write password to modify CS registers
    CSCTL1 = DCOFSEL0 + DCOFSEL1;           // DCO = 8 MHz
    CSCTL2 = SELM0 + SELM1 + SELA0 + SELA1 + SELS0 + SELS1; // MCLK = DCO, ACLK = DCO, SMCLK = DCO

    // Configure P2.0 and P2.1 ports for UART
    P2SEL0 &= ~(BIT0 + BIT1);               // secondary module function UCA0RXD
    P2SEL1 |= BIT0 + BIT1;                  // secondary module function UCA0TXD
    UCA0CTLW0 |= UCSWRST;                   // Put the UART in software reset so can be modified
    UCA0CTLW0 |= UCSSEL0;                   // Run the UART using ACLK
    UCA0MCTLW = UCOS16 + UCBRF0 + 0x4900;   // Enable oversampling, Baud rate = 9600 from an 8 MHz clock (BRCLK) and from column UCBRSx
    UCA0BRW = 52;                           // Clock prescaler from Table 18-5 column UCBRx
    UCA0CTLW0 &= ~UCSWRST;                  // release UART for operation
    UCA0IE |= UCRXIE;                       // Enable UART Rx interrupt

    // Configure PJ.0 as an output
    PJDIR |= BIT0;                          // Set PJ.0 as an output
    PJSEL0 &= ~BIT0;                        // Configure PJ.0 as GPIO
    PJSEL1 &= ~BIT0;
    PJOUT &= ~BIT0;

    // Global interrupt enable
    _EINT();

    while (1)
    {
        // Periodically transmit an "A" character
        while (!(UCA0IFG & UCTXIFG));
        UCA0TXBUF = 'a';
        for (i=0;i<20000;i++)
        _NOP();

    }


    return 0;
}

#pragma vector = USCI_A0_VECTOR             // interrupt vector for Rx interrupt
__interrupt void USCI_A0_ISR(void)
{
    unsigned char RxByte = 0;               // Create an unsigned character variable (8 bits)
    RxByte = UCA0RXBUF;                     // Get the new byte from the Rx buffer

    while (!(UCA0IFG & UCTXIFG));           // Wait until the previous Tx is finished
    UCA0TXBUF = RxByte;                     // Echo back the received byte

    while (!(UCA0IFG & UCTXIFG));           // Wait until the previous Tx is finished
    UCA0TXBUF = RxByte + 1;                 // Echo back the received byte + 1

    if(RxByte == 'j')                       // if received char is j, turn on LED
        PJOUT |= BIT0;
    if(RxByte == 'k')
        PJOUT &= ~BIT0;                     // if received char is k, turn off LED
}
