#include <msp430.h>

// MECH 423 Example code for configuring clocks and UARTs

int main(void)
{
    int i;

    WDTCTL = WDTPW | WDTHOLD; // stop watchdog timer
    // Configure clocks
    CSCTL0 = 0xA500; // Write password to modify CS registers
    CSCTL1 = DCOFSEL0 + DCOFSEL1; // DCO = 8 MHz
    CSCTL2 = SELM0 + SELM1 + SELA0 + SELA1 + SELS0 + SELS1; // MCLK = DCO, ACLK = DCO, SMCLK = DCO

    // Configure ports for UART
    P2SEL0 &= ~(BIT0 + BIT1);
    P2SEL1 |= BIT0 + BIT1;
    UCA0CTLW0 |= UCSWRST; // Put the UART in software reset
    UCA0CTLW0 |= UCSSEL0; // Run the UART using ACLK
    UCA0MCTLW = UCOS16 + UCBRF0 + 0x4900; // Baud rate = 9600 from an 8 MHz clock
    UCA0BRW = 52;
    UCA0CTLW0 &= ~UCSWRST; // release UART for operation
    UCA0IE |= UCRXIE; // Enable UART Rx interrupt

    // Global interrupt enable
    _EINT();

    while (1)
    {
        // Periodically transmit an "A" character
        while (!(UCA0IFG & UCTXIFG));
        UCA0TXBUF = 'A';
        for (i=0;i<20000;i++)
        _NOP();

    }


    return 0;
}

#pragma vector = USCI_A0_VECTOR
__interrupt void USCI_A0_ISR(void)
{
    unsigned char RxByte = 0;
    RxByte = UCA0RXBUF; // Get the new byte from the Rx buffer

    while (!(UCA0IFG & UCTXIFG)); // Wait until the previous Tx is finished
    UCA0TXBUF = RxByte; // Echo back the received byte

    while (!(UCA0IFG & UCTXIFG)); // Wait until the previous Tx is finished
    UCA0TXBUF = RxByte + 1; // Echo back the received byte + 1
}
