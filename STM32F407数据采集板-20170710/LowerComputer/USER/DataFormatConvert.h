#ifndef _DATAFORMATCONVERT_H_
#define _DATAFORMATCONVERT_H_

float  decimaltofloat(unsigned char byte0,  unsigned char byte1 , unsigned char byte2 , unsigned char byte3);
unsigned char floattodecimal(float y ,int i);
long FloatTohex(float HEX);
unsigned char decimaltohex(int y ,int i);

#endif

