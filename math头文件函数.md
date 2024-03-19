# math头文件函数<br>
C语言提供了很多实用的数学函数，如果要使用，需要在程序开头加上math.h头文件。下面是几个比较常用的数学函数，需要掌握。<br>
* 1.fabs(double x)/abs(int x)对变量x取绝对值。<br>
* 2.floor(double x)对变量x向下取整，如floor(5.2)=5。ceil(double x)对变量x向上取整，如ceil(5.2)=6。<br>
* 3.pow(double/int r,double/int p)用于计算r^p。<br>
* 4.sqrt(double x)用于返回变量x的算术平方根。<br>
* 5.log(double x)用于返回变量下以自然对数为底的对数。<br>
* 6.round(double x)用于将变量x四舍五入，返回类型也是double，需进行取整。如round(3.55)=4.000000。<br>
* 7.sin(double x),cos(double x)和tan(double x)用于返回变量的正弦值、余弦值和正切值，参数要求是弧度制。<br>
* 8.asin(double x),acos(double x)和atan(double x)分别返回变量x的反正弦值、反余弦值和反正切值，示例如下：<br>
```c++
#include<bits/stdc++.h>
#include<math.h>
const double pi=acos(-1.0);
int main()
{
   double db1=sin(pi*45/180);
   double db2=cos(pi*45/180);
   double db3=tan(pi*45/180);
   double db4=asin(1);
   double db5=acos(-1.0);
   double db6=atan(0);
   cout<<db1<<" "<<db2<<" "<<db3<<endl;
   cout<<db4<<" "<<db5<<" "<<db6<<endl;
   return 0;
   }

```








原文链接：https://blog.csdn.net/zhimingdaye/article/details/105767644
