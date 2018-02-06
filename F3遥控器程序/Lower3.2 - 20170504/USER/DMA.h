#ifndef _DMA_H_H
#define _DMA_H_H

/* include ----------------------------------------------------*/
#include "stm32f10x.h"


/* Define parameters  -------------------------------------*/


/* Functions declaration ---------------------------------------*/
//DMA_CHx:DMA通道CHx
//cpar:外设地址
//cmar:存储器地址
//cndtr:数据传输量
//dir:数据传输方向(DMA_DIR_PeripheralDST : 从内存读取发送到外设 ; DMA_DIR_PeripheralSRC : 从外设读取发送到内存)
//mode:工作模式(DMA_Mode_Circular : 循环模式 ; DMA_Mode_Normal : 正常模式)
void MYDMA_Config(DMA_Channel_TypeDef* DMA_CHx, u32 cpar, u32 cmar, u16 cndtr, u32 dir, u32 mode);         //Config DMA1_CHx

void MYDMA_Enable(DMA_Channel_TypeDef* DMA_CHx);        //Enable DMA1_CHx













#endif



