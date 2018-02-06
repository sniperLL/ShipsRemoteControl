/*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::;
*
*       Programs function: LED Initiation
*      Functions name: Init_LED(void)
*
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::;*/

#include  "Init_LED.h"

void Init_LED(void)
{       
        //Define structures for initiation
        GPIO_InitTypeDef GPIO_InitStrue;
        
        //Enable Tclock
        RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOG, ENABLE);
        
        //Config I/O ports,     Used I/O ports: PG14
        GPIO_InitStrue.GPIO_Pin = GPIO_Pin_14; 
        GPIO_InitStrue.GPIO_Speed = GPIO_Speed_50MHz;
        GPIO_InitStrue.GPIO_Mode = GPIO_Mode_Out_PP;        
        GPIO_Init(GPIOG, &GPIO_InitStrue);      //LED, D2
        
}



