#include "stm32f4xx_conf.h"
#include "string.h"
#include "delay.h"

#define ENCODERTBSIZE 8
#define ENCODERRBSIZE 20
#define UPPERTBSIZE 69
#define INSRBSIZE 200
#define MPU6050_RX_BUF 33

extern uint8_t data[8];
extern uint8_t encoderTxBuffer[ENCODERTBSIZE];
extern uint8_t encoderRxBuffer[ENCODERRBSIZE];
extern uint8_t upperTxBuffer[UPPERTBSIZE];
extern uint8_t insRxBuffer[INSRBSIZE];
extern uint8_t  MPU6050ReceiveBuff[MPU6050_RX_BUF];
extern u16 ADCConvertedValue;

uint8_t allChannelData[UPPERTBSIZE] = {0x00};
uint8_t channelZeroToThreeData[ENCODERRBSIZE] = {0x00};
uint8_t encoderRxBufferClear[ENCODERRBSIZE] = {0x00};

TIM_TimeBaseInitTypeDef TIM_TimeBaseInitStrecture;
extern NVIC_InitTypeDef NVIC_InitStructure;
extern GPIO_InitTypeDef GPIO_InitStructure;


void ddelay_ms(unsigned int ms)                        // 延时子程序
{    
  unsigned int a,b; 
  for(a=ms;a>0;a--)
  for(b=123;b>0;b--);
}

/****************************************************
*函 数 名:ServoPWMTimer1BaseInit
*函数功能:初始化定时器1 定时周期1ms
*形    参:无
*返 回 值:无
****************************************************/
void ServoPWMTimer1BaseInit(void)
{
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_TIM1,ENABLE);

	NVIC_InitStructure.NVIC_IRQChannel = TIM1_UP_TIM10_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 1;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;
	NVIC_Init(&NVIC_InitStructure);

	TIM_TimeBaseInitStrecture.TIM_Period = 1000 - 1;
	TIM_TimeBaseInitStrecture.TIM_Prescaler = 1680 - 1;
	TIM_TimeBaseInitStrecture.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBaseInitStrecture.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseInitStrecture.TIM_RepetitionCounter = 0;
	TIM_TimeBaseInit(TIM1,&TIM_TimeBaseInitStrecture);

	TIM_ClearFlag(TIM1,TIM_FLAG_Update);
	TIM_ITConfig(TIM1,TIM_IT_Update,ENABLE);
	TIM_Cmd(TIM1,ENABLE);
}

/****************************************************
*函 数 名:TIM1_UP_TIM10_IRQHandler
*函数功能:定时器1的中断服务函数 产生PWM
*形    参:无
*返 回 值:无
*****************************************************/
static unsigned int num;

void TIM1_UP_TIM10_IRQHandler(void)
{
	num++;
	TIM_ClearFlag(TIM1,TIM_FLAG_Update);

	if(num == 3)
	{		
		EncoderChannelOneToFourCommand();
		GPIO_ResetBits(GPIOF,GPIO_Pin_9);
		GPIO_ResetBits(GPIOC,GPIO_Pin_9);
		//编码器1-4通道数据
		if(encoderRxBuffer[0] == 0x5A && encoderRxBuffer[2] == 0x02)
		{
			memcpy(allChannelData,encoderRxBuffer,ENCODERRBSIZE);
		}
	}
	else if(num == 5)
	{
		num = 0;		
		EncoderChannelFiveToEightCommand();
		GPIO_SetBits(GPIOF,GPIO_Pin_9);
		GPIO_SetBits(GPIOC,GPIO_Pin_9);
		if(encoderRxBuffer[0] == 0x5A && encoderRxBuffer[2] == 0x01)
		{
			//编码器5-8通道数据
			for(int i = 20;i < 40 ;i++)
			{
				allChannelData[i] = encoderRxBuffer[i - 20];
			}
		}
		//惯导数据
		if(insRxBuffer[0] == 0xFF && insRxBuffer[40] == 0x33)
		{
			for(int i = 44;i < 56 ;i++)
				allChannelData[i] = insRxBuffer[i - 34];
			allChannelData[56] = insRxBuffer[41];
			allChannelData[57] = insRxBuffer[42];
		}
		GetDepth();
		//MPU6050 数据
		if(MPU6050ReceiveBuff[0] == 0x55 && MPU6050ReceiveBuff[1] == 0x51)
		{
			for(int i = 58;i < 69 ;i++)
				allChannelData[i] = MPU6050ReceiveBuff[i - 58];
		}
		memcpy(upperTxBuffer,allChannelData,UPPERTBSIZE);
		DMA_Cmd(DMA1_Stream6 ,ENABLE);
	}	
}

void ServoPwmPinInit(void)
{
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOF, ENABLE);
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOC, ENABLE);
	
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_9;
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_OUT;
  GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
  GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;
  GPIO_Init(GPIOF, &GPIO_InitStructure);
	GPIO_SetBits(GPIOF,GPIO_Pin_9);//起始置高电位
	
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_0;
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_OUT;
  GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
  GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;
  GPIO_Init(GPIOC, &GPIO_InitStructure);
	GPIO_SetBits(GPIOC,GPIO_Pin_9);//起始置高电位
}
