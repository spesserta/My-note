# 本章包含：
* algorithm头文件
* cmath头文件

# algorithm头文件函数
## 一、max()、min()、abs()函数
* max()：求两个数最大值
* min()：求两个数最小值
* abs()：求一个数的绝对值  //只能用于int型变量
```c++
#include<iostream>
#include<algorithm>
using namespace std;
int main() {
	int a = 3, b = 4;
	//求最大值
	int Max = max(a,b);
	//求最小值
	int Min = min(a,b);
	//求绝对值
	int Abs = abs(-3);
	cout << Max << Min << Abs;
	
	return 0;
 } 

```
## 二、交换函数：swap()
```c++
#include<iostream>
#include<algorithm>
using namespace std;
int main() {
	int a = 3, b = 4;
	swap(a,b);            //交换a,b的值
	cout << a << b;
	return 0;
 } 

```
## 三、翻转函数：reverse()
### 1、翻转整个数组
```c++
#include<iostream>
#include<algorithm>
using namespace std;
int main() {
	int a[5] = {11,22,33,44,55};
	reverse(a,a+5);
	for(int i = 0; i < 5; i++) 
		cout << a[i] << ' ';
	return 0;
 } 

```
### 2、翻转部分数组
```c++
#include<iostream>
#include<algorithm>
using namespace std;
int main() {
	int a[5] = {11,22,33,44,55};
	reverse(a+3,a+5);
	for(int i = 0; i < 5; i++) 
		cout << a[i] << ' ';
	return 0;
 } 

```
### 3、翻转整个容器
```c++
#include<iostream>
#include<algorithm>
#include<vector>
using namespace std;
int main() {
	vector<int>v;
	//输入： 
	for(int i = 0; i < 5; i++) 
		v.push_back(i);
	//输出： 
	reverse(v.begin(), v.end());
	for(int i = 0; i < v.size(); i++) {
		cout << v[i] << ' ';
	}
	return 0;
 } 

```
### 4、翻转部分容器
```c++
#include<iostream>
#include<algorithm>
#include<vector>
using namespace std;
int main() {
	vector<int>v;
	vector<int>::iterator it;
	//输入： 
	for(int i = 0; i < 5; i++) 
		v.push_back(i);
	//输出： 
	it = v.begin(); 
	reverse(it, it+3);
	for(int i = 0; i < v.size(); i++) {
		cout << v[i] << ' ';
	}
	return 0;
 } 

```
## 四、排序函数：sort()<br>
见排序部分。<br>

## 五、查找函数find()<br>
查找某数组指定区间x-y内是否有x，若有，则返回该位置的地址，若没有，则返回该数组第n+1个值的地址。<br>
数组中查找是否有某值：一定一定一定要满足代码中这两个条件。<br>
第一个条件是：p-a != 数组的长度。p是查找数值的地址，a是a[0]的地址。<br>
第二个条件是：*p == x; 也就是该地址指向的值等于我们要查找的值。<br>
最后输出p-a+1； p-a相当于x所在位置的地址-a[0]所在位置的地址， 但因为是从0开始算， 所以最后需要+1。<br>
### 1、对数组查找<br>
```c++
#include<iostream>
#include<algorithm>
using namespace std;
int main() {  
	int a[5] = {11,22,33,44,55}	
	
	int *p = find(a,a+5,33);				//定义指针，指向查找完成后返回的地址，5为a2数组长度 
  	if(((p-a) != 5) && (*p == x))		//若同时满足这两个条件，则查找成功，输出 
  		cout << (p-a+1);					//输出所在位置 
	return 0;
 } 

```
### 2、对容器查找<br>
```c++
#include<iostream>
#include<algorithm>
#include<vector>
using namespace std;
int main() {
	vector<int>v;
	vector<int>::iterator it, it1;
	//输入： 
	for(int i = 0; i < 5; i++) 
		v.push_back(i); 
	//查找 
	int size = v.size();					//第一步：求长度
	it = find(v.begin(), v.end(), 3);		//第二步：查找x在容器的位置，返回迭代器1
	it1 = v.begin();						//第三步：令迭代器2指向容器头 
	if(((it-it1)!=size)&&(*it==3))			//第四步：若同时满足这两个条件，则查找成功，输出 
		cout << (it-it1+1) << endl;			//输出所在位置 
	return 0;
 } 

```
## 六、二分查找函数：upper_bound()、lower_bound()<br>
### 1、upper_bound()：查找第一个大于x的值的位置<br>
### 2、lower_bound()：查找第一个大于等于x的值的位置<br>
### 例题：<br>
![image](https://github.com/spesserta/My-note/assets/138494873/cb61da51-96d3-4849-a9f3-6bd2304b6b8c)
```cpp
#include<iostream>
#include<algorithm>
using namespace std;
int num[1000010];
int main(){
	int n,m;
	cin>>n>>m;
	for(int i=1;i<=n;i++){
		cin>>num[i];
	}
	while(m--){
		int x;
		cin>>x;
		int index=lower_bound(num+1,num+n+1,x)-num; //由于返回的是一个地址，因此需要减去num
		if(num[index]==x){
			cout<<index<<" ";
		}else{
			cout<<-1<<" ";
		}
	}
	return 0;
}
```

## 七、填充函数：fill()<br>
在区间内填充某一个值。同样适用所有类型数组，容器。<br>
### 1、举例：在数组中未赋值的地方填充9999<br>
```c++
#include<iostream>
#include<algorithm>
using namespace std;
int main() {  
	int a[5] = {11,33,22};
	
	fill(a+3,a+5,9999);								
	
	for(int i = 0; i < 5; i++) 
		cout << a[i] << ' ';
	return 0;
 } 

```
## 八、查找某值出现的次数：count()<br>
### 1、在数组中查找x 在某区间出现的次数<br>
```c++
#include<iostream>
#include<algorithm>
using namespace std;
int main() {  
	int a[5] = {11,22,33,44,44};
	
	cout << count(a, a+5, 44);	
	
	return 0;
 } 

```
### 2、在容器中查找同理，只是需要用iterator迭代器或begin()、end()函数<br>


## 九、求最大公因数：__gcd()<br>
```c++
#include<iostream>
#include<algorithm>
using namespace std;
int main() {  
	int a = 12, b = 4;
	int Gcd = __gcd(a,b);  //注意gcd前有2个下划线
	cout << Gcd;
	return 0;
 } 
```

## 十、全排列：next_permutation()<br>
将给定区间的数组、容器全排列<br>
### 1、将给定区间的数组全排列<br>
```c++
#include<iostream>
#include<algorithm>
using namespace std;
int main() {  
	int a[3] = {1,2,3};
	do{
		cout<<a[0]<<a[1]<<a[2]<<endl; 
	}while(next_permutation(a,a+3));	//输出1、2、3的全排列 
	
	return 0;
 } 

```
输出：<br>
123<br>
132<br>
213<br>
231<br>
312<br>
321<br>
2、容器全排列同理：只不过将参数换成iterator迭代器或begin()、end()函数。<br>
<br>
## 十一、求交集、并集、差集：set_intersection()、set_union()、set_difference()<br>
### 1、求交集：<br>
（1）：将两个数组的交集赋给一个容器<br>
```c++
#include<algorithm>
#include<iostream>
#include<vector>
using namespace std;
int main() {
	int a[5] = {1,2,3,4,5}, b[5] = {1,2,33,44,55};
	vector<int>::iterator it;
	vector<int>v4;	
	set_intersection(a, a+5, b, b+5, inserter(v4,v4.begin()));
	for(int i = 0; i < v4.size(); i++) {
		cout << v4[i] << ' ';
	}
```
输出：1 2
（2）：将两个容器的交集赋给另一个容器<br>
```c++
#include<algorithm>
#include<iostream>
#include<vector>
using namespace std;
int main() {
	vector<int> v1, v2, v3;
	for(int i = 0; i < 5; i++) 
		v1.push_back(i);
	for(int i = 3; i < 8; i++) 
		v2.push_back(i);
	  
	set_intersection(v1.begin(), v1.end(), v2.begin(), v2.end(), inserter(v3,v3.begin()));  
	for(int i = 0; i < v3.size(); i++) {
		cout << v3[i] << ' ';
	}
	return 0;
} 

```
输出：3 4
### 2、求并集：<br>
（1）：将两个数组的并集赋给一个容器(为什么不能赋给数组呢？因为数组不能动态开辟，且inserter()函数中的参数必须是指向容器的迭代器。)<br>
```c++
#include<algorithm>
#include<iostream>
#include<vector>
using namespace std;
int main() {
	int a[5] = {1,2,3,4,5}, b[5] = {1,2,33,44,55};
	vector<int>::iterator it;
	vector<int>v4;	
	set_union(a, a+5, b, b+5, inserter(v4,v4.begin()));
	for(int i = 0; i < v4.size(); i++) {
		cout << v4[i] << ' ';
	}

```
输出：1 2 3 4 5 33 44 55<br>
（2）：将两个容器的并集赋给另一个容器：<br>
```c++
#include<algorithm>
#include<iostream>
#include<vector>
using namespace std;
int main() {
	vector<int> v1, v2, v3;
	for(int i = 0; i < 5; i++) 
		v1.push_back(i);
	for(int i = 3; i < 8; i++) 
		v2.push_back(i);
	  
	set_union(v1.begin(), v1.end(), v2.begin(), v2.end(), inserter(v3,v3.begin()));  
	for(int i = 0; i < v3.size(); i++) {
		cout << v3[i] << ' ';
	}
	return 0;
} 

```

输出：0 1 2 3 4 5 6 7<br>

### 3、差集完全同理<br>

注意：<br>
inserter(c,c.begin())为插入迭代器<br>
此函数接受第二个参数，这个参数必须是一个指向给定容器的迭代器。元素将被插入到给定迭代器所表示的元素之前。<br>

原文链接：https://blog.csdn.net/weixin_43899069/article/details/104450000
-----------------------------------------------------------------------------
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
