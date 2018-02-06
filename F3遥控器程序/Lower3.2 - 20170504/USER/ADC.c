/*----------------------------------------- include --------------------------------------------------*/
#include "ADC.h"
#include "Ctrl_RUDDER.h"
#include "Init_USART.h"

/*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::;
*
*       Programs function: ADC acquisite signal to figure out whether cabin has been flooded or not.
*       Functions name: Init_ADC1(void);         ADC1 Initiation
*                                     ADC1_2_IRQHandler();       Interrupt service function
*                       
*
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::;*/
/* Define parameters -------------------------------------------------*/
u8 Warning_Flag = 0x00;       //Safe: 0x00; Danger: 0xFF

/*-------------------------------------- Init_ADC() -----------------------------------------------------------*/
void Init_ADC1(void)
{
        //Define structures for initiation
        GPIO_InitTypeDef GPIO_InitStructure;
        ADC_InitTypeDef ADC_InitStructure;
        NVIC_InitTypeDef NVIC_InitStructure;
        
        //Enable clock
        RCC_APB2PeriphClockCmd(RCC_APB2Periph_ADC1 | RCC_APB2Periph_GPIOA, ENABLE);
        
        //Config ADC1_Channel1's I/O port to PA1
        GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AIN;
        GPIO_InitStructure.GPIO_Pin = GPIO_Pin_1;
        GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
        GPIO_Init(GPIOA, &GPIO_InitStructure);
        
        //Config frequency division factor of ADC1, 6, (Frequency of APB2 is 72MHz)
        RCC_ADCCLKConfig(RCC_PCLK2_Div6);
        
        //Reset ADC1
        ADC_DeInit(ADC1);
        
        //Initial parameters of ADC
        ADC_InitStructure.ADC_ContinuousConvMode = ENABLE;
        ADC_InitStructure.ADC_DataAlign = ADC_DataAlign_Right;
        ADC_InitStructure.ADC_ExternalTrigConv = ADC_ExternalTrigConv_None;
        ADC_InitStructure.ADC_Mode = ADC_Mode_Independent;
        ADC_InitStructure.ADC_NbrOfChannel = 1;
        ADC_InitStructure.ADC_ScanConvMode = DISABLE;
        ADC_Init(ADC1, &ADC_InitStructure);
        
        //Config priority of interruption
        NVIC_InitStructure.NVIC_IRQChannel = ADC1_2_IRQn;
        NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
        NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
        NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;
        NVIC_Init(&NVIC_InitStructure);
        
        NVIC_PriorityGroupConfig(NVIC_PriorityGroup_2);
        
        //Config EOC Interruption
        ADC_ITConfig(ADC1, ADC_IT_EOC, ENABLE);
        
        //Enable ADC1
        ADC_Cmd(ADC1, ENABLE);
        
        //Calibration ADC1
        ADC_ResetCalibration(ADC1);     //Enable reset calibration
        while(ADC_GetCalibrationStatus(ADC1));         //Wait the end of reset calibration
        ADC_StartCalibration(ADC1);     //Start ADC calibration
        while(ADC_GetCalibrationStatus(ADC1));          //Wait the end of calibration
        
        //Config regular channel parameters
        ADC_RegularChannelConfig(ADC1, ADC_Channel_1, 1, ADC_SampleTime_239Cycles5);
        
        //Enable software convertion
        ADC_SoftwareStartConvCmd(ADC1, ENABLE);
}


/*-------------------------------------- ADC1_2_IRQHandler() -----------------------------------------------------------*/
void ADC1_2_IRQHandler()
{
        u16 temp = ADC_GetConversionValue(ADC1);
        ADC_ClearITPendingBit(ADC1, ADC_IT_EOC);        //Clear interrupt pending bit
        if(temp > 500) //4096 = 3.3V; 1241 = 1V; when ADC's input value more than 1V, consider that the cabin has been flooded
        {
                Warning_Flag = 0xFF;
        }
				else
				{
					Warning_Flag = 0x00;
				}
				ADC_SoftwareStartConvCmd(ADC1, ENABLE);
}

