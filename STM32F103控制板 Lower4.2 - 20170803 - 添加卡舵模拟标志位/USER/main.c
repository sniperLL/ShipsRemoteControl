/* include -------------------------------------------------------------*/

#include "stm32f10x.h"
#include "Init_PWM.h"
#include "Delay_ms.h"
#include "Init_LED.h"
#include "Init_USART.h"
#include "Init_TIMER.h"
#include "dma.h"
#include "Ctrl_RUDDER.h"
#include "ADC.h"

typedef union
{
	float angleset_float;
	u8 angleset_u8[4];
}angleset;


/**********************************************************************************************************
**
** Programs function��Lower computer control steering engines 
**                                      and datas reception and transmission through USART1/2 DMA
**
**********************************************************************************************************/
/* define --------------------------------------------------------------*/

/* Define parameters-------------------------------------------------------------*/
//extern u8 Uart1_Buffer[UART1_BUF_SIZE];     //Receive data buffer size
extern u8 UART3_Buffer[UART3_BUF_SIZE];
extern u8 Uart2_Buffer[UART2_BUF_SIZE];     //Send data buffer size
extern u8 ReceiveBuf[UART2_BUF_SIZE];	//Receive data temp buffer area
extern u8 LastReceiveBuf[UART2_BUF_SIZE];
//extern u8 SendBuf[UART3_BUF_SIZE];           //Send data temp buffer area
extern float DutyfactorSet_LEFT_UP;
extern float DutyfactorSet_RIGHT_UP;
extern float DutyfactorSet_LEFT_DOWN;
extern float DutyfactorSet_RIGHT_DOWN;
extern float DutyfactorSet_HORIZ_LEFT;
extern float DutyfactorSet_HORIZ_RIGHT;
extern float DutyfactorSet_MOTOR;
float AngelSet_LEFT_UP_Buf = AngelSet_LEFT_UP_RESET;
float AngelSet_RIGHT_UP_Buf = AngelSet_RIGHT_UP_RESET;
float AngelSet_LEFT_DOWN_Buf = AngelSet_LEFT_DOWN_RESET;
float AngelSet_RIGHT_DOWN_Buf = AngelSet_RIGHT_DOWN_RESET;
float AngelSet_HORIZ_LEFT_Buf = AngelSet_HORIZ_LEFT_RESET;
float AngelSet_HORIZ_RIGHT_Buf = AngelSet_HORIZ_RIGHT_RESET;
float SpeedSet_MOTOR_Buf = SpeedSet_MOTOR_RESET;
extern u8 Warning_Flag;       //Safe:0x00; Danger:0xFF
angleset _LEFT_UP;
angleset _RIGHT_UP;
angleset _LEFT_DOWN;
angleset _RIGHT_DOWN;
angleset _HORIZ_LEFT;
angleset _HORIZ_RIGHT;
angleset _MOTOR;
/**************************************************************************
**
** Functions name��main()
**
**************************************************************************/

int main()
{
	SystemInit();
	Init_LED();
	Init_PWM(DutyfactorSet_LEFT_UP, 
                          DutyfactorSet_RIGHT_UP, 
                          DutyfactorSet_LEFT_DOWN, 
                          DutyfactorSet_RIGHT_DOWN, 
                          DutyfactorSet_HORIZ_LEFT, 
                          DutyfactorSet_HORIZ_RIGHT,
                          DutyfactorSet_MOTOR);
	Init_USART1();
	Init_USART2();
	Init_USART3();
	Init_TIMER2();
	Init_ADC1();
        
/*------------------------------- USART1 DMA Configuration ------------------------------------*/
////DMA1_Channel5-----Receive from USART1 to Uart1_Buffer[UART1_BUF_SIZE] by circular mode
//MYDMA_Config(DMA1_Channel5, (u32)&USART1->DR, (u32)Uart1_Buffer, UART1_BUF_SIZE, DMA_DIR_PeripheralSRC, DMA_Mode_Circular);
//USART_DMACmd(USART1, USART_DMAReq_Rx, ENABLE);          //Enable USART1 DMA reception
//MYDMA_Enable(DMA1_Channel5);            //Start DMA reception
	
/*------------------------------- USART2 DMA Configuration ------------------------------------*/
	//DMA1_Channel6-----Receive from USART2 to Uart1_Buffer[UART2_BUF_SIZE] by circular mode
	MYDMA_Config(DMA1_Channel6, (u32)&USART2->DR, (u32)Uart2_Buffer, UART2_BUF_SIZE, DMA_DIR_PeripheralSRC, DMA_Mode_Circular);
	USART_DMACmd(USART2, USART_DMAReq_Rx, ENABLE);          //Enable USART2 DMA reception
	MYDMA_Enable(DMA1_Channel6);            //Start DMA reception
        
	GPIO_SetBits(GPIOG, GPIO_Pin_14);
	
	GPIO_SetBits(GPIOC, GPIO_Pin_9);
	GPIO_ResetBits(GPIOC, GPIO_Pin_10);
	
	while(1)
	{
                if (DMA_GetFlagStatus(DMA1_FLAG_TC6) != RESET)  //Judge if Channel6 complete reception
                {
                        int i = 0;
												int j = 0;
                        DMA_ClearFlag(DMA1_FLAG_TC6);   //Clear Channel6 complete reception flag
                        for (i = 0; i < UART2_BUF_SIZE; i++)
                                        ReceiveBuf[i] = Uart2_Buffer[i];
                        if (ReceiveBuf[0] == 0x5A && ReceiveBuf[1] == 0x01 && ReceiveBuf[31] == 0x33)
                        {
                                for (i = 0; i < UART2_BUF_SIZE; i++)
                                        LastReceiveBuf[i] = ReceiveBuf[i]; 
                        }
                        else
                        {
                                for (i = 0; i < UART2_BUF_SIZE; i++)
                                        ReceiveBuf[i] = LastReceiveBuf[i];
                        }
												
						for(i = 0, j = 2; i < 4; i++,j++)
						{
							_LEFT_UP.angleset_u8[i] = ReceiveBuf[j];
						}
						AngelSet_LEFT_UP_Buf = _LEFT_UP.angleset_float;
						for(i = 0, j = 6; i < 4; i++,j++)
						{
							_RIGHT_UP.angleset_u8[i] = ReceiveBuf[j];
						}
						AngelSet_RIGHT_UP_Buf = _RIGHT_UP.angleset_float;
						for(i = 0, j = 10; i < 4; i++,j++)
						{
							_LEFT_DOWN.angleset_u8[i] = ReceiveBuf[j];
						}
						AngelSet_LEFT_DOWN_Buf = _LEFT_DOWN.angleset_float;
						for(i = 0, j = 14; i < 4; i++,j++)
						{
							_RIGHT_DOWN.angleset_u8[i] = ReceiveBuf[j];
						}
						AngelSet_RIGHT_DOWN_Buf = _RIGHT_DOWN.angleset_float;
						for(i = 0, j = 18; i < 4; i++,j++)
						{
							_HORIZ_LEFT.angleset_u8[i] = ReceiveBuf[j];
						}
						AngelSet_HORIZ_LEFT_Buf = _HORIZ_LEFT.angleset_float;
						for(i = 0, j = 22; i < 4; i++,j++)
						{
							_HORIZ_RIGHT.angleset_u8[i] = ReceiveBuf[j];
						}
						AngelSet_HORIZ_RIGHT_Buf = _HORIZ_RIGHT.angleset_float;
						for(i = 0, j = 26; i < 4; i++,j++)
						{
							_MOTOR.angleset_u8[i] = ReceiveBuf[j];
						}
						if(ReceiveBuf[32] == 0xFF)
						{
							GPIO_SetBits(GPIOC, GPIO_Pin_10);
							GPIO_ResetBits(GPIOC, GPIO_Pin_9);
						}
						else
						{
							GPIO_SetBits(GPIOC, GPIO_Pin_9);
							GPIO_ResetBits(GPIOC, GPIO_Pin_10);
						}
						SpeedSet_MOTOR_Buf = _MOTOR.angleset_float;
//                        AngelSet_LEFT_UP_Buf = ReceiveBuf[2] << (8*3) | ReceiveBuf[3] << (8*2) | ReceiveBuf[4] << (8*1) | ReceiveBuf[5]; 
//                        AngelSet_RIGHT_UP_Buf = ReceiveBuf[6] << (8*3) | ReceiveBuf[7] << (8*2) | ReceiveBuf[8] << (8*1) | ReceiveBuf[9]; 
//                        AngelSet_LEFT_DOWN_Buf = ReceiveBuf[10] << (8*3) | ReceiveBuf[11] << (8*2) | ReceiveBuf[12] << (8*1) | ReceiveBuf[13]; 
//                        AngelSet_RIGHT_DOWN_Buf = ReceiveBuf[14] << (8*3) | ReceiveBuf[15] << (8*2) | ReceiveBuf[16] << (8*1) | ReceiveBuf[17]; 
//                        AngelSet_HORIZ_LEFT_Buf = ReceiveBuf[18] << (8*3) | ReceiveBuf[19] << (8*2) | ReceiveBuf[20] << (8*1) | ReceiveBuf[21]; 
//                        AngelSet_HORIZ_RIGHT_Buf = ReceiveBuf[22] << (8*3) | ReceiveBuf[23] << (8*2) | ReceiveBuf[24] << (8*1) | ReceiveBuf[25]; 
//                        SpeedSet_MOTOR_Buf = ReceiveBuf[26] << (8*3) | ReceiveBuf[27] << (8*2) | ReceiveBuf[28] << (8*1) | ReceiveBuf[29]; 
						if(ReceiveBuf[30] == 0xFF)
						{
							TIM_Cmd(TIM2, DISABLE); //close timer2
							TIM_SetCompare1(TIM4, DutyfactorM);
							TIM_SetCompare2(TIM4, DutyfactorM);
							TIM_SetCompare3(TIM4, DutyfactorM);
							TIM_SetCompare4(TIM4, DutyfactorM);
							TIM_SetCompare1(TIM3, DutyfactorM);
							TIM_SetCompare2(TIM3, DutyfactorM);
							TIM_SetCompare3(TIM3, DutyfactorMo);
							TIM_Cmd(TIM2, ENABLE); //enable timer2
						}		

                }
				UART3_Buffer[41] = Warning_Flag;
                UART3_Buffer[42] = ReceiveBuf[30];    //Reset encoder flag
//              ReceiveBuf[30] =  0x00;
	}
        
}







