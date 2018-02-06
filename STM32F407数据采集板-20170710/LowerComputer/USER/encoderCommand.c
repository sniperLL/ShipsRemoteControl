#include "stm32f4xx_conf.h"
#include "string.h"

extern uint8_t data[8];  //返回通道1到4的命令
uint8_t clearCmd[8] = {0x5A,0x01,0x10,0x00,0x00,0x00,0xFF,0x6A};
uint8_t dataFiveToEightCmd[8] = {0x5A,0x01,0x02,0x00,0x00,0x00,0x00,0x5D};
uint8_t clearMotorCmd[8] = {0x5A,0x01,0x10,0x00,0x00,0x00,0x20,0x8B};

#define ENCODERTBSIZE 8

extern uint8_t encoderTxBuffer[ENCODERTBSIZE];//返回通道1-4数据的命令

//编码器数据清零
void EncoderClearCommand(void)
{
	//发送命令给编码器
	memcpy(encoderTxBuffer,clearCmd,ENCODERTBSIZE);
	DMA_Cmd(DMA1_Stream3 ,ENABLE);
}

//编码器电机数据清零
void EncoderMotorClearCommand(void)
{
	//发送命令给编码器
	memcpy(encoderTxBuffer,clearMotorCmd,ENCODERTBSIZE);
	DMA_Cmd(DMA1_Stream3 ,ENABLE);
}

//返回编码器通道1到4的数据
void EncoderChannelOneToFourCommand(void)
{
	//发送命令给编码器
	memcpy(encoderTxBuffer,data,ENCODERTBSIZE);
	DMA_Cmd(DMA1_Stream3 ,ENABLE);
}
//返回编码器通道5到8的数据
void EncoderChannelFiveToEightCommand(void)
{
	//发送命令给编码器
	memcpy(encoderTxBuffer,dataFiveToEightCmd,ENCODERTBSIZE);
	DMA_Cmd(DMA1_Stream3 ,ENABLE);
}
