/*----------------------------------------- include --------------------------------------------------*/
#include "DMA.h"

/*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::;
*
*       Programs function: Receive and send datas through serial ports by DMA
*       Functions name: void MYDMA_Config(DMA_Channel_TypeDef* DMA_CHx, u32 cpar, u32 cmar, u16 cndtr);         //DMA1_CHx configuration
*                                     void MYDMA_Enable(DMA_Channel_TypeDef* DMA_CHx);        //Enable DMA1_CHx
* 
*
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::;*/
/* Define parameters-------------------------------------------------*/
u16 DMA1_MEN_LEN;       //Save length of datas that sent or received everytime


/*-------------------------------------- MYDMA_Config() -----------------------------------------------------------*/
//DMA_CHx:DMA_Channel_CHx
//cpar:外设地址
//cmar:存储器地址
//cndtr:数据传输量
//dir:数据传输方向(DMA_DIR_PeripheralDST : 从内存读取发送到外设 ; DMA_DIR_PeripheralSRC : 从外设读取发送到内存)
//mode:work mode(DMA_Mode_Circular; DMA_Mode_Normal)

void MYDMA_Config(DMA_Channel_TypeDef* DMA_CHx, u32 cpar, u32 cmar, u16 cndtr, u32 dir, u32 mode)         //Config DMA1_CHx
{
        //Define structures for initiation
        DMA_InitTypeDef DMA_InitStructure;
        
        //Enable clock
        RCC_AHBPeriphClockCmd(RCC_AHBPeriph_DMA1, ENABLE);
        
        DMA1_MEN_LEN = cndtr;    //保存DMA每次数据传送长度
        
        DMA_DeInit(DMA_CHx);      //将DMA的通道1寄存器重设为缺省值
        
        //DMA Initiation Configuration
        DMA_InitStructure.DMA_BufferSize = cndtr;      //DMA通道的DMA缓存的大小
        DMA_InitStructure.DMA_DIR = dir;      //数据传输方向
        DMA_InitStructure.DMA_M2M = DMA_M2M_Disable;             //DMA通道x没有设置为内存到内存传输
        DMA_InitStructure.DMA_MemoryBaseAddr = cmar;            //DMA内存基地址
        DMA_InitStructure.DMA_MemoryDataSize = DMA_MemoryDataSize_Byte;          //数据宽度为8位
        DMA_InitStructure.DMA_MemoryInc = DMA_MemoryInc_Enable;         //内存地址寄存器递增
        DMA_InitStructure.DMA_Mode = mode;            //工作模式
        DMA_InitStructure.DMA_PeripheralBaseAddr = cpar;         //DMA外设基地址
        DMA_InitStructure.DMA_PeripheralDataSize = DMA_PeripheralDataSize_Byte;          //数据宽度为8位
        DMA_InitStructure.DMA_PeripheralInc = DMA_PeripheralInc_Disable;        //外设地址寄存器不变
        DMA_InitStructure.DMA_Priority = DMA_Priority_Medium;   //DMA_Channel has medium priority 
        
        DMA_Init(DMA_CHx, &DMA_InitStructure);
}

/*---------------------------------------- MYDMA_Enable() ----------------------------------------------------------*/
//Start DMA transmission or reception
void MYDMA_Enable(DMA_Channel_TypeDef* DMA_CHx)        //Enable DMA1_CHx
{
        DMA_Cmd(DMA_CHx, DISABLE);
        DMA_SetCurrDataCounter(DMA_CHx, DMA1_MEN_LEN);
        DMA_Cmd(DMA_CHx, ENABLE);
        
}






