#ifndef __SBUS_H__
#define __SBUS_H__

#include "stdint.h"

void uart_recv_cb(uint8_t ch);
void timer_cb(void);
int sbus_get(int ch_num);


#endif /*__SBUS_H__*/


