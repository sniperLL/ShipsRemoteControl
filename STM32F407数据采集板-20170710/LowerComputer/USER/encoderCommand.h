#ifndef _ENCODERCOMMAND_H_
#define _ENCODERCOMMAND_H_

//编码器数据清零
void EncoderClearCommand(void);
//编码器电机数据清零
void EncoderMotorClearCommand(void);
//返回编码器通道1到4的数据
void EncoderChannelOneToFourCommand(void);
//返回编码器通道5到8的数据
void EncoderChannelFiveToEightCommand(void);

#endif
