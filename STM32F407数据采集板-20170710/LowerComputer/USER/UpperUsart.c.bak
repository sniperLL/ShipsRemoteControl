#include "stm32f4xx_conf.h"
#include "string.h"
#include <sys.h>

uint8_t data[8] = {0x5A,0x01,0x01,0x00,0x00,0x00,0x00,0x5C};  //测试数据

#define UPPERTBSIZE 69
#define UPPERRBSIZE 4
#define ENCODERTBSIZE 8
#define ENCODERRBSIZE 20
#define INSRBSIZE 43
#define MPU6050_RX_BUF 33

uint8_t upperTxBuffer[UPPERTBSIZE] = {0x07};
uint8_t upperRxBuffer[UPPERRBSIZE] = {0x00};
uint8_t encoderTxBuffer[ENCODERTBSIZE] = {0x05};//返回通道1-4数据的命令
uint8_t encoderRxBuffer[ENCODERRBSIZE] = {0x06};//接收通道1-4数据的命令
uint8_t insRxBuffer[INSRBSIZE] = {0x00}; //惯导数据接收以及复位标志位
uint8_t  MPU6050ReceiveBuff[MPU6050_RX_BUF] = {0x00};   //接收缓冲  

NVIC_InitTypeDef NVIC_InitStructure ;   
GPIO_InitTypeDef GPIO_InitStructure;    
USART_InitTypeDef USART_InitStructure;  
USART_ClockInitTypeDef USART_ClockInitStructure;
DMA_InitTypeDef DMA_InitStructure;  

/******************************************
**函数名称：MPU6050Usart1Init
**函数参数：baudRate 波特率
**函数作用：初始化与MPU6050通讯的串口Usart1
**硬件引脚：TX--PA9  RX--PA10
******************************************/
void MPU6050Usart1Init(int baudRate)
{
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_USART1, ENABLE);  
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_DMA2, ENABLE); 
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOA, ENABLE);  

	GPIO_PinAFConfig(GPIOA,GPIO_PinSource9,GPIO_AF_USART1);
	GPIO_PinAFConfig(GPIOA,GPIO_PinSource10,GPIO_AF_USART1);	
	//PA9(TX)设置成复用推挽输出
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_9; 
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_UP;
	GPIO_Init(GPIOA, &GPIO_InitStructure);
//	//PA10(RX)设置成浮空输入
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_10;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_Init(GPIOA, &GPIO_InitStructure);
	//USART1设置 115200 8 1 0 NONE
	USART_InitStructure.USART_BaudRate = baudRate;
	USART_InitStructure.USART_WordLength = USART_WordLength_8b;
	USART_InitStructure.USART_StopBits = USART_StopBits_1;
	USART_InitStructure.USART_Parity = USART_Parity_No ;
	USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;
	USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;
	USART_Init(USART1,&USART_InitStructure);
	//****************************配置UART1接收  
	DMA_DeInit(DMA2_Stream5);  
	while (DMA_GetCmdStatus(DMA2_Stream5) != DISABLE);//等待DMA可配置   
	/* 配置 DMA Stream */  
	DMA_InitStructure.DMA_Channel = DMA_Channel_4;  //通道选择  
	DMA_InitStructure.DMA_PeripheralBaseAddr = (u32)&USART1->DR;//DMA外设地址  
	DMA_InitStructure.DMA_Memory0BaseAddr = (u32)MPU6050ReceiveBuff;//DMA 存储器0地址  
	DMA_InitStructure.DMA_DIR = DMA_DIR_PeripheralToMemory ;//外设到存储器模式  
	DMA_InitStructure.DMA_BufferSize = MPU6050_RX_BUF;//数据传输量   
	DMA_InitStructure.DMA_PeripheralInc = DMA_PeripheralInc_Disable;//外设非增量模式  
	DMA_InitStructure.DMA_MemoryInc = DMA_MemoryInc_Enable;//存储器增量模式  
	DMA_InitStructure.DMA_PeripheralDataSize = DMA_PeripheralDataSize_Byte;//外设数据长度:8位  
	DMA_InitStructure.DMA_MemoryDataSize = DMA_MemoryDataSize_Byte;//存储器数据长度:8位  
	DMA_InitStructure.DMA_Mode = DMA_Mode_Circular;// 使用普通模式   
	DMA_InitStructure.DMA_Priority = DMA_Priority_Medium;//中等优先级  
	DMA_InitStructure.DMA_FIFOMode = DMA_FIFOMode_Disable;           
	DMA_InitStructure.DMA_FIFOThreshold = DMA_FIFOThreshold_Full;  
	DMA_InitStructure.DMA_MemoryBurst = DMA_MemoryBurst_Single;//存储器突发单次传输  
	DMA_InitStructure.DMA_PeripheralBurst = DMA_PeripheralBurst_Single;//外设突发单次传输  
	DMA_Init(DMA2_Stream5, &DMA_InitStructure);//初始化DMA Stream  
	
	NVIC_InitStructure.NVIC_IRQChannel = USART1_IRQn;//串口1中断通道  
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority=3;//抢占优先级3  
	NVIC_InitStructure.NVIC_IRQChannelSubPriority =3;       //子优先级3  
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;         //IRQ通道使能  
	NVIC_Init(&NVIC_InitStructure); //根据指定的参数初始化VIC寄存器、
	
	USART_ITConfig(USART1, USART_IT_IDLE, ENABLE);//开启相关中断
	
	USART_DMACmd(USART1,USART_DMAReq_Rx,ENABLE);  //使能串口1的DMA接收  
	USART_Cmd(USART1, ENABLE);  //使能串口1
	DMA_Cmd(DMA2_Stream5, ENABLE);  //开启DMA传输
}

void USART1_IRQHandler(void)  
{  
    u16 data;
  
    if(USART_GetITStatus(USART1,USART_IT_IDLE) != RESET)  
    {  
			DMA_Cmd(DMA2_Stream5, DISABLE); //关闭DMA,防止处理其间有数据  

			data = USART1->SR;  
			data = USART1->DR;  
				 
			DMA_ClearFlag(DMA2_Stream5,DMA_FLAG_TCIF5 | DMA_FLAG_FEIF5 | DMA_FLAG_DMEIF5 | DMA_FLAG_TEIF5 | DMA_FLAG_HTIF5);//清除DMA2_Steam7传输完成标志  
			DMA_SetCurrDataCounter(DMA2_Stream5, MPU6050_RX_BUF);  
			DMA_Cmd(DMA2_Stream5, ENABLE);     //打开DMA, 
		}
}

/******************************************
**函数名称：UpperUsart2Init
**函数参数：baudRate 波特率
**函数作用：初始化与上位机通讯的串口Usart2
**硬件引脚：TX--PD5  RX--PD6 
******************************************/
void UpperUsart2Init(int baudRate)
{
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_USART2, ENABLE);  
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_DMA1, ENABLE); 
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOA, ENABLE);  

	GPIO_PinAFConfig(GPIOA,GPIO_PinSource2,GPIO_AF_USART2);
//	GPIO_PinAFConfig(GPIOA,GPIO_PinSource3,GPIO_AF_USART2);	
	//PD5(TX)设置成复用推挽输出
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_2; //| GPIO_Pin_9;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_UP;
	GPIO_Init(GPIOA, &GPIO_InitStructure);
//	//PD6(RX)设置成浮空输入
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_3;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_Init(GPIOA, &GPIO_InitStructure); 	
	
	// USART2_DMA_RX DMA1_Stream5 DMA_Channel_4 
  DMA_DeInit(DMA1_Stream5);
  DMA_InitStructure.DMA_Channel = DMA_Channel_4;
  DMA_InitStructure.DMA_PeripheralBaseAddr = (uint32_t)&(USART2->DR);
  DMA_InitStructure.DMA_Memory0BaseAddr = (uint32_t)&upperRxBuffer;
  DMA_InitStructure.DMA_DIR = DMA_DIR_PeripheralToMemory;
  DMA_InitStructure.DMA_BufferSize = UPPERRBSIZE;
  DMA_InitStructure.DMA_PeripheralInc = DMA_PeripheralInc_Disable;
  DMA_InitStructure.DMA_MemoryInc = DMA_MemoryInc_Enable;
  DMA_InitStructure.DMA_PeripheralDataSize = DMA_PeripheralDataSize_Byte;
  DMA_InitStructure.DMA_MemoryDataSize = DMA_MemoryDataSize_Byte;
  DMA_InitStructure.DMA_Mode = DMA_Mode_Circular;//如果是Normal只能接受一次，故采用循环模式
  DMA_InitStructure.DMA_Priority = DMA_Priority_High;
  DMA_InitStructure.DMA_FIFOMode = DMA_FIFOMode_Disable;         
  DMA_InitStructure.DMA_FIFOThreshold = DMA_FIFOThreshold_HalfFull;
  DMA_InitStructure.DMA_MemoryBurst = DMA_MemoryBurst_Single;
  DMA_InitStructure.DMA_PeripheralBurst = DMA_PeripheralBurst_Single;
  DMA_Init(DMA1_Stream5, &DMA_InitStructure);
	
	// USART2_DMA_TX DMA1_Stream6 DMA_Channel_4 
	DMA_DeInit(DMA1_Stream6);
  DMA_InitStructure.DMA_Channel = DMA_Channel_4;
	DMA_InitStructure.DMA_PeripheralBaseAddr = (uint32_t) &(USART2->DR);
	DMA_InitStructure.DMA_Memory0BaseAddr = (uint32_t)&upperTxBuffer;
	DMA_InitStructure.DMA_DIR = DMA_DIR_MemoryToPeripheral;
	DMA_InitStructure.DMA_BufferSize = UPPERTBSIZE;
	DMA_InitStructure.DMA_PeripheralInc = DMA_PeripheralInc_Disable;
	DMA_InitStructure.DMA_MemoryInc = DMA_MemoryInc_Enable;
	DMA_InitStructure.DMA_PeripheralDataSize = DMA_PeripheralDataSize_Byte;
	DMA_InitStructure.DMA_MemoryDataSize = DMA_MemoryDataSize_Byte;
	DMA_InitStructure.DMA_Mode =  DMA_Mode_Normal;   //DMA_Mode_Circular;
	DMA_InitStructure.DMA_Priority = DMA_Priority_High;
  DMA_InitStructure.DMA_FIFOMode = DMA_FIFOMode_Disable;         
  DMA_InitStructure.DMA_FIFOThreshold = DMA_FIFOThreshold_HalfFull;
  DMA_InitStructure.DMA_MemoryBurst = DMA_MemoryBurst_Single;
  DMA_InitStructure.DMA_PeripheralBurst = DMA_PeripheralBurst_Single;
	DMA_Init(DMA1_Stream6, &DMA_InitStructure);

	//USART2设置 115200 8 1 0 NONE
	USART_InitStructure.USART_BaudRate = baudRate;
	USART_InitStructure.USART_WordLength = USART_WordLength_8b;
	USART_InitStructure.USART_StopBits = USART_StopBits_1;
	USART_InitStructure.USART_Parity = USART_Parity_No ;
	USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;
	USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;
	USART_Init(USART2,&USART_InitStructure);
	
	// Configure one bit for preemption priority 
  NVIC_PriorityGroupConfig(NVIC_PriorityGroup_0);
  // Enable DMA1_Stream5 Interrupt 
  NVIC_InitStructure.NVIC_IRQChannel = DMA1_Stream5_IRQn;
  NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
  NVIC_InitStructure.NVIC_IRQChannelSubPriority = 4;
  NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
  NVIC_Init(&NVIC_InitStructure);
  // Enable DMA1_Stream6 Interrupt 
  NVIC_InitStructure.NVIC_IRQChannel = DMA1_Stream6_IRQn;
  NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
  NVIC_InitStructure.NVIC_IRQChannelSubPriority = 6;
  NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
  NVIC_Init(&NVIC_InitStructure);
  // Enable USART2 Interrupt 
  NVIC_InitStructure.NVIC_IRQChannel = USART2_IRQn;
  NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
  NVIC_InitStructure.NVIC_IRQChannelSubPriority = 5;
  NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
  NVIC_Init(&NVIC_InitStructure);	
	
	//开启串口、DMA和串口总线空闲中断
	DMA_Cmd(DMA1_Stream5,ENABLE);
	DMA_Cmd(DMA1_Stream6,DISABLE);
	USART_DMACmd(USART2,USART_DMAReq_Tx,ENABLE);
	USART_DMACmd(USART2,USART_DMAReq_Rx,ENABLE);  
	DMA_ITConfig(DMA1_Stream5, DMA_IT_TC, ENABLE);
	DMA_ITConfig(DMA1_Stream6, DMA_IT_TC, ENABLE);
	DMA_ClearITPendingBit(DMA1_Stream5, DMA_IT_TCIF5); 
	DMA_ClearITPendingBit(DMA1_Stream6, DMA_IT_TCIF6);  	
	USART_ITConfig(USART2,USART_IT_IDLE,ENABLE); 
//	USART_ITConfig(USART2, USART_IT_RXNE, ENABLE);
	USART_Cmd(USART2, ENABLE);	
}

/******************************************
**函数名称：UpperUsart3Init
**函数参数：baudRate 波特率
**函数作用：初始化与编码器通讯的串口Usart3
**硬件引脚：TX--PD8  RX--PD9 
******************************************/
void EncoderUsart3Init(int baudRate)
{
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_USART3, ENABLE);  
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_DMA1, ENABLE); 
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOD, ENABLE); 

	GPIO_PinAFConfig(GPIOD,GPIO_PinSource8,GPIO_AF_USART3);
	GPIO_PinAFConfig(GPIOD,GPIO_PinSource9,GPIO_AF_USART3);	
	//PD8(TX)设置成复用推挽输出
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_8; //| GPIO_Pin_9;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_UP;
	GPIO_Init(GPIOD, &GPIO_InitStructure);
//	//PD9(RX)设置成浮空输入
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_9;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_Init(GPIOD, &GPIO_InitStructure); 
	
	// USART3_DMA_RX DMA1_Stream1 DMA_Channel_4 
  DMA_DeInit(DMA1_Stream1);
  DMA_InitStructure.DMA_Channel = DMA_Channel_4;
  DMA_InitStructure.DMA_PeripheralBaseAddr = (uint32_t)&(USART3->DR);
  DMA_InitStructure.DMA_Memory0BaseAddr = (uint32_t)&encoderRxBuffer;
  DMA_InitStructure.DMA_DIR = DMA_DIR_PeripheralToMemory;
  DMA_InitStructure.DMA_BufferSize = ENCODERRBSIZE;
  DMA_InitStructure.DMA_PeripheralInc = DMA_PeripheralInc_Disable;
  DMA_InitStructure.DMA_MemoryInc = DMA_MemoryInc_Enable;
  DMA_InitStructure.DMA_PeripheralDataSize = DMA_PeripheralDataSize_Byte;
  DMA_InitStructure.DMA_MemoryDataSize = DMA_MemoryDataSize_Byte;
  DMA_InitStructure.DMA_Mode = DMA_Mode_Circular;//如果是Normal只能接受一次，故采用循环模式
  DMA_InitStructure.DMA_Priority = DMA_Priority_High;
  DMA_InitStructure.DMA_FIFOMode = DMA_FIFOMode_Disable;         
  DMA_InitStructure.DMA_FIFOThreshold = DMA_FIFOThreshold_HalfFull;
  DMA_InitStructure.DMA_MemoryBurst = DMA_MemoryBurst_Single;
  DMA_InitStructure.DMA_PeripheralBurst = DMA_PeripheralBurst_Single;
  DMA_Init(DMA1_Stream1, &DMA_InitStructure);
	
	//USART3设置 115200 8 1 0 NONE
	USART_InitStructure.USART_BaudRate = baudRate;
	USART_InitStructure.USART_WordLength = USART_WordLength_8b;
	USART_InitStructure.USART_StopBits = USART_StopBits_1;
	USART_InitStructure.USART_Parity = USART_Parity_No;
	USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;
	USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;
	USART_Init(USART3,&USART_InitStructure);
	
	// USART3_DMA_TX DMA1_Stream3 DMA_Channel_4 
	DMA_DeInit(DMA1_Stream3);
  DMA_InitStructure.DMA_Channel = DMA_Channel_4;
	DMA_InitStructure.DMA_PeripheralBaseAddr = (uint32_t) &(USART3->DR);
	DMA_InitStructure.DMA_Memory0BaseAddr = (uint32_t)&encoderTxBuffer;
	DMA_InitStructure.DMA_DIR = DMA_DIR_MemoryToPeripheral;
	DMA_InitStructure.DMA_BufferSize = ENCODERTBSIZE;
	DMA_InitStructure.DMA_PeripheralInc = DMA_PeripheralInc_Disable;
	DMA_InitStructure.DMA_MemoryInc = DMA_MemoryInc_Enable;
	DMA_InitStructure.DMA_PeripheralDataSize = DMA_PeripheralDataSize_Byte;
	DMA_InitStructure.DMA_MemoryDataSize = DMA_MemoryDataSize_Byte;
	DMA_InitStructure.DMA_Mode =  DMA_Mode_Normal;   //DMA_Mode_Circular;
	DMA_InitStructure.DMA_Priority = DMA_Priority_High;
  DMA_InitStructure.DMA_FIFOMode = DMA_FIFOMode_Disable;         
  DMA_InitStructure.DMA_FIFOThreshold = DMA_FIFOThreshold_HalfFull;
  DMA_InitStructure.DMA_MemoryBurst = DMA_MemoryBurst_Single;
  DMA_InitStructure.DMA_PeripheralBurst = DMA_PeripheralBurst_Single;
	DMA_Init(DMA1_Stream3, &DMA_InitStructure);  
	
	// Configure one bit for preemption priority 
  NVIC_PriorityGroupConfig(NVIC_PriorityGroup_0);
  // Enable DMA1_Stream1 Interrupt 
  NVIC_InitStructure.NVIC_IRQChannel = DMA1_Stream1_IRQn;
  NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
  NVIC_InitStructure.NVIC_IRQChannelSubPriority = 2;
  NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
  NVIC_Init(&NVIC_InitStructure);
  // Enable DMA1_Stream3 Interrupt 
  NVIC_InitStructure.NVIC_IRQChannel = DMA1_Stream3_IRQn;
  NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
  NVIC_InitStructure.NVIC_IRQChannelSubPriority = 1;
  NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
  NVIC_Init(&NVIC_InitStructure);
  // Enable USART3 Interrupt 
  NVIC_InitStructure.NVIC_IRQChannel = USART3_IRQn;
  NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
  NVIC_InitStructure.NVIC_IRQChannelSubPriority = 3;
  NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
  NVIC_Init(&NVIC_InitStructure);
	
	//开启串口、DMA和串口总线空闲中断
	DMA_Cmd(DMA1_Stream1 ,ENABLE);
	DMA_Cmd(DMA1_Stream3 ,DISABLE);
	USART_DMACmd(USART3, USART_DMAReq_Tx,ENABLE);
	USART_DMACmd(USART3,USART_DMAReq_Rx,ENABLE);  
//	DMA_ITConfig(DMA1_Stream1, DMA_IT_TC, ENABLE);
	DMA_ITConfig(DMA1_Stream3, DMA_IT_TC, ENABLE);
//	DMA_ClearITPendingBit(DMA1_Stream1, DMA_IT_TCIF1); 
	DMA_ClearITPendingBit(DMA1_Stream3, DMA_IT_TCIF3);  	
	USART_ITConfig(USART3,USART_IT_IDLE,ENABLE); 
//	USART_ITConfig(USART3, USART_IT_RXNE, ENABLE);
	USART_Cmd(USART3, ENABLE);	
}

/******************************************
**函数名称：InsUsart6Init
**函数参数：baudRate 波特率
**函数作用：初始化与惯导通讯的串口Usart2 接收惯导数据
**硬件引脚：TX--PC6  RX--PC7 
******************************************/
void InsUsart6Init(int baudRate)
{
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_USART6, ENABLE);  
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_DMA2, ENABLE); 
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOC, ENABLE);  

//	GPIO_PinAFConfig(GPIOC,GPIO_PinSource6,GPIO_AF_USART6);
	GPIO_PinAFConfig(GPIOC,GPIO_PinSource7,GPIO_AF_USART6);	
//	//PD8(TX)设置成复用推挽输出
//	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_6;
//	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
//	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
//	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
//	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_UP;
//	GPIO_Init(GPIOC, &GPIO_InitStructure);
//	//PC9(RX)设置成浮空输入
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_7;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_Init(GPIOC, &GPIO_InitStructure); 	
	
	// USART6_DMA_RX DMA2_Stream2 DMA_Channel_5 
  DMA_DeInit(DMA2_Stream2);
  DMA_InitStructure.DMA_Channel = DMA_Channel_5;
  DMA_InitStructure.DMA_PeripheralBaseAddr = (uint32_t)&(USART6->DR);
  DMA_InitStructure.DMA_Memory0BaseAddr = (uint32_t)&insRxBuffer;
  DMA_InitStructure.DMA_DIR = DMA_DIR_PeripheralToMemory;
  DMA_InitStructure.DMA_BufferSize = INSRBSIZE;
  DMA_InitStructure.DMA_PeripheralInc = DMA_PeripheralInc_Disable;
  DMA_InitStructure.DMA_MemoryInc = DMA_MemoryInc_Enable;
  DMA_InitStructure.DMA_PeripheralDataSize = DMA_PeripheralDataSize_Byte;
  DMA_InitStructure.DMA_MemoryDataSize = DMA_MemoryDataSize_Byte;
  DMA_InitStructure.DMA_Mode = DMA_Mode_Circular;
  DMA_InitStructure.DMA_Priority = DMA_Priority_High;
  DMA_InitStructure.DMA_FIFOMode = DMA_FIFOMode_Disable;         
  DMA_InitStructure.DMA_FIFOThreshold = DMA_FIFOThreshold_HalfFull;
  DMA_InitStructure.DMA_MemoryBurst = DMA_MemoryBurst_Single;
  DMA_InitStructure.DMA_PeripheralBurst = DMA_PeripheralBurst_Single;
  DMA_Init(DMA2_Stream2, &DMA_InitStructure);

	//USART6设置 115200 8 1 0 NONE
	USART_InitStructure.USART_BaudRate = baudRate;
	USART_InitStructure.USART_WordLength = USART_WordLength_8b;
	USART_InitStructure.USART_StopBits = USART_StopBits_1;
	USART_InitStructure.USART_Parity = USART_Parity_No ;
	USART_InitStructure.USART_Mode = USART_Mode_Rx; //| USART_Mode_Tx;
	USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;
	USART_Init(USART6,&USART_InitStructure);
	
	// Configure one bit for preemption priority 
  NVIC_PriorityGroupConfig(NVIC_PriorityGroup_0);
  // Enable USART6 Interrupt 
  NVIC_InitStructure.NVIC_IRQChannel = USART6_IRQn;
  NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
  NVIC_InitStructure.NVIC_IRQChannelSubPriority = 7;
  NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
  NVIC_Init(&NVIC_InitStructure);	
	
	//开启串口、DMA和串口总线空闲中断
	DMA_Cmd(DMA2_Stream2,ENABLE);
	USART_DMACmd(USART6,USART_DMAReq_Rx,ENABLE);   	
	USART_ITConfig(USART6,USART_IT_IDLE,ENABLE); 
	USART_Cmd(USART6, ENABLE);	
}

/******************************************
**函数名称：DMA1_Stream1_IRQHandler
**函数参数：无
**函数作用：串口3 DMA接受完成时发送数据给ENCODER命令其返回编码器数据
******************************************/
//void DMA1_Stream1_IRQHandler(void) //UART3_RX
//{
//	if(SET ==  DMA_GetITStatus(DMA1_Stream1, DMA_IT_TCIF1))
//	{ 
//		DMA_Cmd(DMA1_Stream1,DISABLE);
//		DMA_ClearFlag(DMA1_Stream1,DMA_FLAG_TCIF1);
//		DMA_Cmd(DMA1_Stream1,ENABLE);	
//		
//		DMA_SetCurrDataCounter(DMA1_Stream3,ENCODERTBSIZE); 
//		memcpy(encoderTxBuffer,data,ENCODERTBSIZE);		
//		DMA_Cmd(DMA1_Stream3 ,ENABLE);		
//	}
//}

/******************************************
**函数名称：DMA1_Stream3_IRQHandler
**函数参数：无
**函数作用：串口3发送完成时中断入口函数，关闭传输通道并且清除标志
******************************************/
void DMA1_Stream3_IRQHandler(void) //UART3_TX
{
	if(SET == DMA_GetITStatus(DMA1_Stream3, DMA_IT_TCIF3))
	{
		DMA_Cmd(DMA1_Stream3,DISABLE);
		DMA_ClearFlag(DMA1_Stream3, DMA_FLAG_TCIF3);
	}
}

/******************************************
**函数名称：DMA1_Stream5_IRQHandler
**函数参数：无
**函数作用：串口2 DMA接受完成时发送数据给上位机
******************************************/
void DMA1_Stream5_IRQHandler(void)
{
	if(SET ==  DMA_GetITStatus(DMA1_Stream5, DMA_IT_TCIF5))
	{ 
		DMA_Cmd(DMA1_Stream5,DISABLE);
		DMA_ClearFlag(DMA1_Stream5,DMA_FLAG_TCIF5);
		DMA_Cmd(DMA1_Stream5,ENABLE);	
		
		DMA_SetCurrDataCounter(DMA1_Stream6,UPPERTBSIZE); 
		memcpy(upperTxBuffer,data,UPPERTBSIZE);		
		DMA_Cmd(DMA1_Stream6,ENABLE);		
	}
}

/******************************************
**函数名称：DMA1_Stream6_IRQHandler
**函数参数：无
**函数作用：串口2发送完成时中断入口函数，关闭传输通道并且清除标志
******************************************/
void DMA1_Stream6_IRQHandler(void) //UART2_TX
{
	if(SET == DMA_GetITStatus(DMA1_Stream6,DMA_IT_TCIF6))
	{
		DMA_Cmd(DMA1_Stream6,DISABLE);
		DMA_ClearFlag(DMA1_Stream6, DMA_FLAG_TCIF6);
	}
}

//串口3总线空闲中断可以用于接收DMA数据  编码器数据
void USART3_IRQHandler(void)
{
	if(USART_GetITStatus(USART3,USART_IT_IDLE) != RESET)
	{		
		uint32_t temp = 0;
		temp = USART3->SR;  
		temp = USART3->DR;
		DMA_Cmd(DMA1_Stream1,DISABLE);
		temp = ENCODERRBSIZE - DMA_GetCurrDataCounter(DMA1_Stream1);
		DMA_SetCurrDataCounter(DMA1_Stream1,ENCODERRBSIZE);
		DMA_Cmd(DMA1_Stream1,ENABLE); 

//		memcpy(upperTxBuffer,encoderRxBuffer,ENCODERRBSIZE);
//		DMA_Cmd(DMA1_Stream6 ,ENABLE);
	}
}

//串口6总线空闲中断可以用于接收DMA数据  惯导数据和复位标志位
void USART6_IRQHandler(void)
{
	if(USART_GetITStatus(USART6,USART_IT_IDLE) != RESET)
	{		
		uint32_t temp = 0;
		temp = USART6->SR;
		temp = USART6->DR;
		DMA_Cmd(DMA2_Stream2,DISABLE);
		temp = INSRBSIZE - DMA_GetCurrDataCounter(DMA2_Stream2);
		DMA_SetCurrDataCounter(DMA2_Stream2,INSRBSIZE);
		DMA_Cmd(DMA2_Stream2,ENABLE);

//		memcpy(upperTxBuffer,encoderRxBuffer,ENCODERRBSIZE);
//		DMA_Cmd(DMA1_Stream6 ,ENABLE);
	}
}
//不使用DMA时的传输方式	
//	unsigned char ch;  
//	if(USART_GetITStatus(USART3,  USART_IT_RXNE) != RESET)
//   {
//    /* Read one byte from the  receive data register */
//			ch =  (USART_ReceiveData(USART3));
//			USART_SendData(USART3,ch);
//	 }
//}

