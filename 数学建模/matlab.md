## 一、编辑器和命令行窗口
刚开始的话界面是只有命令行窗口的，该窗口可以直接和用户交互，类似于控制台。而编辑器是相当于一个代码的编译器。<br>
想打开编辑器就通过主页->脚本即可。<br>
在脚本写完代码就可以ctr+s建即可保存。<br>
* `%% `和`% `注释
* `clc` 清除命令窗口
* `clear all` 清除右侧工作区
* `disp()`输出某变量
![image](https://github.com/spesserta/My-note/assets/138494873/da23e232-46ef-4f76-af90-ecce189483e0)

## 二、变量命名
* 区分大小写
* 长度不超过63位
* 字母数字下划线组成，必须字母开头
* 字符串变量用单引号

## 三、数据类型
运算符：
* 加 ：+
* 减 ：-
* 乘 ：*
* 除 ：/
* 取模：mod(a,b)
![image](https://github.com/spesserta/My-note/assets/138494873/1ceeb178-2e76-4505-86c3-9cf50842b633)
  
## 四、字符和字符串 
* s='awfwe' abs(s) 
* abs(s) //返回ASCII码

![image](https://github.com/spesserta/My-note/assets/138494873/a21cfeef-0792-46a9-aa2f-ecc52102d70e)

* char(97)  //返回ASCII码对应的字母
![image](https://github.com/spesserta/My-note/assets/138494873/c1eeda64-47cb-403a-8355-6527b2246349)

* num2str(312) //数字字符串
![image](https://github.com/spesserta/My-note/assets/138494873/6013eaf4-e07b-4f0d-9afc-491338437ae6)

  
## 五、矩阵构造
* a={1 2 3} //单行矩阵
* b={1;2;3} //单列矩阵

![image](https://github.com/spesserta/My-note/assets/138494873/144f3578-78a4-4b22-b6c2-ea04a8824e4b)

* c={1,2,3;4,5,6;} //二维矩阵行+列（逗号分割列，分号分割行）
* d=a' //将a矩阵转置

![image](https://github.com/spesserta/My-note/assets/138494873/e4307f0e-e5f6-4920-8012-3b08c970268a)

* e=a(:) //将矩阵拉长（竖着拉的）

![image](https://github.com/spesserta/My-note/assets/138494873/9fcd8d0c-630d-4ef9-ad48-3e8a063ad21e)

* f=inv(a) //求逆矩阵（前提是正方形矩阵）
* g=zeros(10,5,3) //创建3维矩阵，10行5列
![image](https://github.com/spesserta/My-note/assets/138494873/5c389a44-0822-4ecb-b8db-d43dfe48bcfa)


## 六、随机数
* rand(m,n)生成均匀分布的m行n列的均匀分布的伪随机数
* rand(m,n,'double')生成指定精度的均匀分布的伪随机数
![image](https://github.com/spesserta/My-note/assets/138494873/1abf005b-e522-4993-8d30-16e6877c1a97)
* rand(RandStream,m,n)利用指定的RandStream生成为随机数
* randn生成标准正态分布的伪随机数（均值为0，方差为1）
* randi生成均匀分布的伪随机整数
* randi(Max)在开区间（0，Max）生成均匀分布的伪随机整数
* randi(Max,m,n)在开区间（0，Nax）生成m*n型随机矩阵
* r=randi([iMin,iMax],m,n)在开区间（iMin,iMax）生成m*n型随机矩阵
![image-1](https://github.com/spesserta/My-note/assets/138494873/19a10f9c-7de4-4e78-84f2-d9e473c7b0d2)


## 七、元胞数组
元胞数组就是每格都可容纳一片区域的数组，数组之间的数据类型可以不相同。<br>
* A=cell(1,5) 生成一个1*5的元胞数组
* A{2}=eye(3) 元胞数组中第二个格子里面生成一个3*3且对角线为1的矩阵
* A{5}=magic(5) 第5个格子中生成一个5*5的幻方（行+列+对角线值相等）
![image-2](https://github.com/spesserta/My-note/assets/138494873/06b127a3-b80b-4022-b9bb-e233fabe0623)

![image-3](https://github.com/spesserta/My-note/assets/138494873/64897e6b-5545-4a33-83e9-ffa809ed801e)


## 八、结构体
books=struct('name',{{'lalalalalla','widhqwiug'}},'price',[20,40])
创建一个名为books的结构体，里面包含name和price
![image-4](https://github.com/spesserta/My-note/assets/138494873/c28a4b78-3210-4ac8-abb1-f7908a1c341d)

books.name 输出books结构体中的name里面的所有内容
books.name(1) 输出books结构体中name里面的第一个内容
![image-5](https://github.com/spesserta/My-note/assets/138494873/53f283f7-755a-43a9-a3bf-ca098675e63a)


## 九、矩阵操作
### 1、构造矩阵
* A=[1 2 3 4 5 6 7 8] 创建了一个名为A的一个数组（元胞结构是花括号）
* B=1:3:9 创建了一个从1到9的，步长为2的数组
![image-6](https://github.com/spesserta/My-note/assets/138494873/778f6664-e78f-4ce9-9fa6-fd8500d035c2)

* C=repmat(B,3,1) 重复B 横着的3次，竖着的1次
![image-7](https://github.com/spesserta/My-note/assets/138494873/40a3736b-17cc-41e0-90b7-de7de9578ea0)

* D=ones(2,4)  生成一个2乘4的单位矩阵
![image-8](https://github.com/spesserta/My-note/assets/138494873/60fec29d-c248-4b1c-a367-404bba9c83ec)

### 2、矩阵的四则运算
就像一般的数字的四则运算进行即可。<br>
![image-9](https://github.com/spesserta/My-note/assets/138494873/c85126c1-4f6c-4d47-9495-9df17a8b7557)

![image-10](https://github.com/spesserta/My-note/assets/138494873/5d8b2003-e050-47e2-83ef-dd8a69efaa12)

![image-11](https://github.com/spesserta/My-note/assets/138494873/fb99c175-5acd-4fd9-900b-6ad95aa7ce0b)


>注意：A.*B表示矩阵的对应项相乘，A*B表示A和B矩阵一般相乘,A./B也是如此

### 3、矩阵的下标
* A=magic(5) 生成一个5*5的幻方矩阵
* B=A(2,3) 在幻方A中取一个2行3列的元素
* C=A(3,:) 在幻方A中取出所有3行的数字
* D=A(:,4) 在幻方A中取出所有4列的数字
![image-12](https://github.com/spesserta/My-note/assets/138494873/cc7a7a64-609f-4b46-a958-54383b59ed9a)

* [m,n]=find(A>20) 找出在幻方A中大于20的索引值(m行n列)


## 十、三大结构
#### 1、顺序结构
按照逻辑顺序输出即可
#### 2、循环结构：for循环和while循环
```python
for 循环变量 = 起始值：步长：终点值
    语句1...语句n
end 

while 循环变量的范围
    语句1...语句n
end
```
例如：求1到100的和
```python
sum=0;
for i=1:100 %步长不写的话默认是1
 sum=sum+i ;
end
disp(sum);
```
```python
sum=0;
i=1;
while i<=100
  sum=sum+i;
end
disp(sum);
```
![image-13](https://github.com/spesserta/My-note/assets/138494873/80546b12-6c24-488f-84e4-9dfff185e481)


#### 3、选择结构
```python
if 判断条件
满足条件就执行该段
end

if 判断条件
满足条件就执行该段
else
不满足就执行该段
end
```

```python
a=100; b=20;
if a>b
 disp(a);
else
 disp(b);
end
```
![image-14](https://github.com/spesserta/My-note/assets/138494873/c7f9cdb3-6b87-412c-96bb-70cb2841d729)









