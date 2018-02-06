#ifndef _Init_USART_H_H
#define _Init_USART_H_H

#define UART1_BUF_SIZE 41       //Receive data buffer size from INS
#define UART2_BUF_SIZE 37        //Receive data buffer size from Upper
#define UART3_BUF_SIZE 43       //Send data buffer size to next Lower

/* include ----------------------------------------------------*/
#include "stm32f10x.h"


/* Define parameters ------------------------------------------*/


/* Functions declaration -----------------------------------------*/
void Init_USART1(void); //USART1 configuration

void Init_USART2(void); //USART2 configuration

void Init_USART3(void); //USART3 configuration










#endif



