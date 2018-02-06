#include "stm32f4xx_conf.h"

#define INSRBSIZE 43
extern uint8_t insRxBuffer[INSRBSIZE];

void mainDelay_ms(unsigned int ms)                        // 延时子程序
{    
  unsigned int a,b; 
  for(a=ms;a>0;a--)
  for(b=123;b>0;b--);
}

int main(void)
{		
	ServoPwmPinInit();
	MPU6050Usart1Init(9600);	
	UpperUsart2Init(115200);	
	EncoderUsart3Init(115200);
	Adc3_Init();	
	InsUsart6Init(115200);
	EncoderClearCommand();	
	ServoPWMTimer1BaseInit();

	while(1)
	{
		if(insRxBuffer[42] == 0xff)
		{
			TIM_Cmd(TIM1,DISABLE);
			mainDelay_ms(1500);
			EncoderClearCommand();
			TIM_Cmd(TIM1,ENABLE);
		}
	} //等待中断产生
}
