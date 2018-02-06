using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_RudderUpper
{
    class FilterPro
    {
        public const int N = 10;

        public FilterPro()
        {
 
        }

        public static float MedianAverageFilteringAlgorithm(float[] value_buf)
        {
            int i,j,count;
            float sum=0;
            float temp;
            float[] arrary_temp = new float[10] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
            for(i=0;i<N;i++)
            {
                arrary_temp[i] = value_buf[i];   //把数组中的值给临时数组.对临时数组进行排序.
            }
            
            for (j=0;j<N-1;j++)   //冒泡排序算法
            {
                for (i=0;i<N-j;i++)
                {
                    if (arrary_temp[i]>arrary_temp[i+1])     
                    {
                        temp = arrary_temp[i];
                        arrary_temp[i] = arrary_temp[i+1]; 
                        arrary_temp[i+1] = temp;
                    }
                }
            }
    
            for(count=1;count<N-1;count++)             //这里做了修改
                sum += arrary_temp[count];            //去掉最大值和最小值

            return (float)(sum/(N-2));
        }
    }
}
