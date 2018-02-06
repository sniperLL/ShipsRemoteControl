#include "stm32f4xx_conf.h"

typedef union
{
	unsigned char u8[4];
	float f32;
}t_F32;


typedef union
{
	unsigned char u8[4];
	int i32;
}t_I32;

float  decimaltofloat(unsigned char byte0,  unsigned char byte1 , unsigned char byte2 , unsigned char byte3)
{
	t_F32 t;
	
	t.u8[0] = byte3;
	t.u8[1] = byte2;
	t.u8[2] = byte1;
	t.u8[3] = byte0;
	
	return t.f32;
}

unsigned char floattodecimal(float y ,int i)
{
	t_F32 t;
	t.f32 = y;
	
	return t.u8[i];
}

long FloatTohex(float HEX)
{
	return *( long *)&HEX;
}


unsigned char decimaltohex(int y ,int i)
{
	t_I32 t;
	t.i32 = y;
	
	return t.u8[i];
}
