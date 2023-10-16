#include <msp430.h>
#include <../includes/msp_setup_functions.h>

// defines and global variables
unsigned char RxByte = 0;

// MECH 423 Example code for configuring clocks and UARTs

int main(void)
{
    int i;

    WDTCTL = WDTPW | WDTHOLD; // stop watchdog timer

    // set up clock and UART
    clockSetup();
    UART_Setup();

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
        UART_Tx('a');

        for (i=0;i<20000;i++)
        _NOP();
    }

    return 0;
}

#pragma vector = USCI_A0_VECTOR             // interrupt vector for Rx interrupt
__interrupt void USCI_A0_ISR(void)
{
    RxByte = UART_Rx();                         // Get the new byte from the Rx buffer

    UART_Tx(RxByte);                            // Echo back the received byte
    UART_Tx(RxByte + 1);                        // Echo back the received byte + 1

    if(RxByte == 'j')                       // if received char is j, turn on LED
        PJOUT |= BIT0;
    if(RxByte == 'k')
        PJOUT &= ~BIT0;                     // if received char is k, turn off LED
}
