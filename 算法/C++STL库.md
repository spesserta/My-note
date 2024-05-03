# 本章包括
* string容器
* vector容器
* set容器
* queue容器
* priority_queue容器
* stack容器
* map容器
* deque容器


# string库函数总结<br>
## 一、string的构造和拼接<br>
>string类函数，需要带上`#include<string>`函数头<br>
### 1、无参构造<br>
`string str;`
### 2、用等号初始化<br>
`string str2="Hello";`
### 3、用char *方式构造<br>
` string str3("你好");`
### 4、重复字符构造<br>
`  string str4(11, 's');  `                            //重复构造11次，每次用s填充
###  5、拷贝构造<br>
`  string str5(str2);     `                            //将str2的数据复制到str5里面
###  6、移动构造<br>
`  string str6(move(str2));       `                    //将str2的数据移动到str6里面并清空str2
### 7、用指定范围进行构造<br>
` string str7(str2,4);      `                          //从str2的第四个开始的所有字符复制到str7里面
` string str7(str2,4,3);    `                          //从str2的第四个开始，复制长度为3的字符
###  8、字符串的拼接<br>
`string str8=str2+"awkufgwi";`                         //str8为str2基础上再加上一串字符
`string str8+="wckaeugb"； `                            //str8在后面加上一串字符
```c++
#include<iostream>
#include<string> 
using namespace std;
int main()
{
	string str1;  //构造但是不赋值 
	string str2="hello"; //构造且赋值"hello"
	string str3("你好"); //char形式构造且赋值"你好"
	string str4(11,'s'); // 用11个s赋值
	string str5(str2); //拷贝构造赋值str2的内容
	string str6(move(str3));//str3赋值到str6并删除str3
	string str7(str1,4); //将str1索引为4开始的所有值都赋值进去 
	string str8(str1,4,6); //将str1索引为4到索引为6的元素赋值进去
	string str9=str1+str2+"wjdyv"; //拼接好后赋值进去 
	return 0;
}
```

## 二、string元素访问<br>
### 1、方括号[]访问具体元素，类似于数组访问<br>
`string str="Hellow World!";
str[2]='E'; //将str的第三个元素改成"E"`
### 2、at()访问具体元素，和方括号类似<br>
`string str="Hellow World!";`
`str.at(2)='E'; //将str的第三个元素改成"E"`
方括号at区别在与方括号不进行边界检查，超过数组长度后不会报错，但是at会报错<br>
### 3、front()和back()<br>
> 一个输出第一个字符，一个输出最后一个字符
`cout<<str.front()<<endl;
 cout<<str.back()<<endl;`
### 4、c_ctr()和data()<br>
>两者都是返回一个字符串的指针，即C方式的输出整句话<br>
`cout<<str.c_str()<<endl;`
>>应用：
```c++
#include<iostream>
using namespace std;
int main(){
	string str="Hello world!";
	cout<<str[0]<<endl;       
	cout<<str.at(1)<<endl;
	cout<<str.front()<<endl;
	cout<<str.back()<<endl;
	cout<<str.data()<<endl;
	cout<<str.c_str()<<endl;
	return 0;
}
```
![image](https://github.com/spesserta/My-algorithm-note/assets/138494873/eb662438-a3f0-4aa6-bab2-8cace91af93b)


## 三、容量操作<br>
### 1、`empty()`
>判断字符串是否为空，是则1不是则是0<br>
### 2、`size()和length()`
>两个都是返回当前的字节数，注意中文的字节数不等于字符数<br>
```c++
string str="sdajqweda";
cout<<str.length()<<endl;
cout<<str.size()<<endl;
```
### 3、max_size()
>代表这个字符串最大的容量<br>
`cout<<str.max_size()<<endl;`
### 4、resize()
`resize()`是重新设置字符串的字节长度，再加一个参数就是设置填充的字符（默认空格填充）<br>
`str.resize(20);`
`str.resize(20,'a');`
### 5、capacity()
>输出计算机给这个字符串已经分配的大小，一般编译器分配空间是按照一定的规律分配的<br>
`cout<<str.capacity()<<endl;`
### 6、reserve()和shrink_to_fit()
>前者是提前给字符串分配内存，分配100那size什么的不变，capacity变成100；后者是把没有用到的容量释放掉<br>
`str.reserve(100);`
>>应用：
```c++
#include<iostream>
using namespace std;
int main(){
	string str="Hello world!";
	cout<<str.empty()<<endl;  //输出判断是否为空的结果 
	cout<<str.size()<<endl;   //输出长度
	cout<<str.length()<<endl;	
	return 0;
}
```
![image](https://github.com/spesserta/My-algorithm-note/assets/138494873/f0a6a0a8-8da0-44e5-991d-6c1ff9f5138e)


## 四、string迭代器：迭代器就是为容器类提供一个专门遍历的接口
### 1、正向迭代器iterator
```c++
string str="Hellow world!";     
string::iterator it=str.begin();        //声明一个str的迭代器，将迭代器指向str首部
for(;it!=str.end();it++)                //从首部到尾部进行遍历
cout<<*it<<endl;
```
### 2、反向迭代器reverse_iterator
>如图上的那个，反向迭代器的自增是从后往前的
 ```c++
string str="dwajkhfgg";
string::reverse_iterator it=str.rbegin();  //声明一个str的反向迭代器，将迭代器指向str首部
for(;it!=str.rend();it++)                  //从首部到尾部进行遍历
```
>应用：
```c++
#include<iostream>
using namespace std;
int main(){
	string str="Hello world!";
	string::iterator it=str.begin(); //正向迭代器
	for(;it<=str.end();it++)  
	  cout<<*it;
	cout<<endl;
	string::reverse_iterator rit=str.rbegin(); //反向迭代器
	for(;rit<=str.rend();rit++)
	 cout<<*rit;	
	return 0;
}

```
## 五、字符串的比较操作
### 1、==
>判断两个字符串的内容是否相等，如果相等就返回1，否则就0
```c++
string str1="dawjv";
string str2="dwajy";
cout<<(str1==str2)<<endl;
```
### 2、compare()
>判断两个字符串，等于就是0，小于就是负数，大于就是正数，根据ASCII码来作为判断标准
```c++
string str1="dawjv";
string str2="dwajy";
string str3="dawjv";
cout<<str1.compare(str2)<<endl;
cout<<str1.compare(str3)<<endl;
```
### 3、starts_with()和end_with()(C++20)
>比较两者是否是以特定的内容为开头或者结尾，返回true或false
```c++
string str1="dawjv";
string str2="dwajy";
cout<<str1.starts_with()<<endl;
```
### 4、contains()(C++23)
>判断字符串里面是否包含指定的内容，有就返回true没有就false
```c++
string str="cesjkyg";
cout<<str.contains("ces")<<endl;
```
## 六、插入和删除
### 1、push_back()和pop_back()
>在现有的字符串的最后面增加或者删除一个字符
```c++
string str1 = "dawjv";
str1.pop_back();
str1.push_back('v');
```
### 2、append()
>和+=差不多，就是在现有的字符串后面追加内容
```c++
string str = "kaseueg";
str.append("wekegu");  //加上wekegu
str.append(10,'e');   //加上10个e
string str2 = "wekuag";
str.append(str2,3);   //str2里面的第三个开始所有加上去
str.append(str2, 3, 5); //str2里面第三个开始，加5个字母上去
```
### 3、insert()
>可以在字符串中的任意位置插入一个或者一串字符
```
string str = "wekuaeg";
str.insert(2,10,'a');  //在第二个位置上插入10个'a'
str.insert(2,"qweufg"); //在第二个位置上插入一个字符串指针
string str2 = "wdeku";
str.insert(2,str2); //在第二个位置上插入一个字符串str2
str.insert(2, str2,2); //在第二个位置上插入一个字符串str2,，从第二个开始所有
str.insert(2, str2, 2,3); //在第二个位置上插入一个字符串str2,，从第二个开始插入3个字符
```
### 4、clear()

清空字符串

### 5、erase()
清除字符串中指定位置的内容
```c++
string str = "wekuaeg";
str.erase(2,3);//索引为2开始，清除3个字符、
str.erase();  //清除所有字符
```

![image](https://github.com/spesserta/My-note/assets/138494873/2ea27164-cb66-41a2-93d6-529bbf4c2aa7)
```cpp
#include<bits/stdc++.h>
using namespace std;
int main() {
    string str;
    int s,i;
    cin>>str>>s;
    while(s) {
        for(i=0; str[i]<=str[i+1];) { //前边的数尽量小
            i++;
        }
        str.erase(i,1);
        s--;
    }
    while(str[0]=='0'&&str.size()>1) { //处理前导零，但要注意长度不能为0
        str.erase(0,1);
    }
    cout<<str;
    return 0;
}
```


## 七、替换与子串
### 1、replace()  //不可用
>替换指定位置的内容
```c++
string str = "qwedkugg";
string str1 = "你好";
string str2 = "kkkkkkk";
str.replace(2,2,str1);  //str中的索引为2的地方开始的2个字符替换成str1里面的内容
str.replace(2,2,str2,1,4); //str中索引为2的地方开始的2个字符替换成str1里面索引为1开始的4个字符
```
### 2、substr()
```c++
string str = "wekugwediug";
cout << str.substr()<<endl;  //默认输出str里面的所有字符
cout << str.substr(2) << endl;  //输出索引为2开始的所有字符
cout << str.substr(2,2) << endl; //输出索引为2开始的2个字符
```
## 八、string的查找
### 1、find()
>从前往后开始找，输出匹配的字符串中第一个字符的索引位置，没有找到就返回
```c++
string::npos，用if(str.find("wsug")==string::npos)来判断是否匹配成功
string str="wekugwdjhv";
string index=str.find("jhv");//index为str里面的jhv中j的索引位置
```
### 2、rfind()
>与前者的区别就是这个是从后往前找
```c++
string str="wekugwdjhv";
string index=str.rfind("jhv");//index为str里面的jhv中v的索引位置
```
## 九、string的其他操作
### 1、to_string() //不可用
>将一串整数或小数转换成string类型，从而可以进行拼接等操作
 ```c++
string str="dewqkh";
cout<<str+to_string(12.32142342);
```
### 2、stoi()、stol()、stoll()
>将string类型的数字转换成int、long、long long 型
```c++
string str="1238t";
cout<<stoi(str)<<endl;
```
### 3、hash
>计算出string对象对应的哈希值
```c++
string str="dwqugu";
string str1="akewrufg";
hash<string> hs;
cout<<hs(str)<<endl;  //输出str的哈希值
cout<<hs(str1)<<endl;  //输出str1的哈希值
```
>总结代码：
```c++
#include<iostream>
using namespace std;
int main(){
	string str="1239456789";
	str.pop_back();
	cout<<str<<endl;
	str.push_back('9');
	cout<<str<<endl;
	cout<<str.find('9')<<endl;	
	cout<<str.rfind('9')<<endl;
	str.erase(1,4);
	cout<<str<<endl;
	str.insert(1,4,'a');
	cout<<str<<endl;
	str.append("新增内容");
	cout<<str<<endl;
    cout<<str.substr()<<endl;
    cout<<str.substr(2)<<endl;
    cout<<str.substr(2,5)<<endl;
	return 0;
}
```
![image](https://github.com/spesserta/My-algorithm-note/assets/138494873/1dda0980-302c-4f2f-b4ed-ad8944103a83)



# 动态数组vector<br>
vector数组就是可以根据情况实时变动的，长度可以动态改变。使用它可以很方便的减少代码量。<br>
在使用vector数组时需要加上#include<vector>头文件。<br>
### 1、构造动态数组：<br>
无参构造：`vector<T> vec;` 里面T表示数据类型，vec是数组名，可以随便取一个。<br>
构造并赋值：` int n=10; vector<T> vec(n,1)`用n个1构造初始长度的数组。<br>
二维数组：`vector<int> b[1000]` 行是动态的，列是固定的为1000。<br>
### 2、插入元素：<br>
vector数组插入是通过push_back()在数组后面插入一个元素。<br>
例如：
```c++
#include<iostream>
#include<vector> 
using namespace std;
int main()
{
	vector<int> vec;
	vec.push_back(1);
	vec.push_back(2);
	vec.push_back(3);
	vec.push_back(4);
	return 0;
}
```
### 3、获取元素及其个数：<br>
通过`vec.front()`可以返回第一个元素的值。<br>
通过`vec.back()`可以返回最后一个元素的值。<br>
>注意：和`vec.begin(),vec.end()`不一样，begin和end用于返回第一个元素的迭代器，返回的是一个地址。<br>
通过`vec.size()`可以获取到元素个数。<br>
```c++
#include<iostream>
#include<vector> 
using namespace std;
int main()
{
	vector<int> vec;
	vec.push_back(1);
	vec.push_back(2);
	vec.push_back(3);
	vec.push_back(4);
	for(int i=0;i<vec.size();i++)  //输出数组元素
	 cout<<vec[i]<<endl;
	return 0;
}
```
### 4、访问数组<br>
和数组一样，可以用中括号+数组下标和at()函数进行访问，两个方法有一点区别。<br>
`cout<<vec[2]<<endl;`<br>
`cout<<vec.at(2)<<endl;`<br>

### 5、删除元素<br>
和插入一样，只能对尾部进行操作，通过`pop_back()`可以删除最后一个元素。<br>

### 6、清空元素<br>
只需要调用clear()函数即可清空，但是不会释放内存。<br>

### 7、除了可以存基本数据类型的数组，也能存结构体类型数组或者自定义数据类型的数组。<br>
```c++
#include<iostream>
#include<vector> 
using namespace std;
struct Student{
	string name;
	int age;
};
int main()
{
	vector<Student> vec;
	Student stu1,stu2;
	stu1.name="jack"; stu1.age=11;
	stu2.name="tomn"; stu2.age=13;
	vec.push_back(stu1);
	vec.push_back(stu2);
	cout<<vec[0].name<<vec[0].age<<endl;
	cout<<vec.at(1).name<<vec.at(1).age<<endl;
	return 0;
}
```
### 8、vector还能构造出动态的二维数组，即vector的vector<br>
```c++
#include<iostream>
#include<vector> 
using namespace std;
struct Student{
	string name;
	int age;
};
int main()
{
 	int n=5;
 	vector<vector<int>> vec2;
 	for(int i=0;i<n;i++){
 		vector<int> x(i+1,1);   //每次循环造出一个一维vector然后push进二维vector里面
 		vec2.push_back(x);
 	}
 	for(int i=0;i<n;i++){
 		for(int j=0;j<vec2[i].size();j++){
 			cout<<vec2[i][j]<<" ";   //访问和普通二维数组一样
 		}
 		cout<<endl;
 	}
	return 0;
}
```
### 9、遍历动态数组。<br>
>第一种方式：
```c++
for( int i=0;i<a.size();i++)
   cout<<a[i]<<endl;
```
>第二种方式：
```c++
for ( vector<int>::iterator it=a.begin() ; it!=a.end() ; it++ )
   cout<<*it<<endl;
```
>第三种方式：<br>
```c++
for ( auto it=a.begin() ; it!=a.end() ; it++ )
   cout<<*it<<endl;
```
### 10、删除指定元素<br>
erase(地址,个数)；即可

### 11、例题：<br>
![image](https://github.com/spesserta/My-algorithm-note/assets/138494873/c2bb2cb0-6f73-4940-b3ca-b63e0ab05874)
![image](https://github.com/spesserta/My-algorithm-note/assets/138494873/74fd0fd3-8315-47c8-9a6f-e40b67162472)
```c++
#include<iostream>
#include<vector>
using namespace std;

vector<string> city;      
vector<string> dig[1000];  //二维数组，一行包含了一个城市所有的快递单号

int Myfind(string s)  //判断城市名是否重复
{
    for(int i=0;i<city.size();i++)
    {
        if(city[i]==s) return i;    //重复的话返回重复城市名在city数组里面的下标
    }
    return -1;   //不重复返回-1
}
int main()
{

    int n;
    cin>>n;
    for(int i=0;i<n;i++)
    {
        string d,c;
        cin>>d>>c;  
        int flag=Myfind(c);
        if(flag==-1){  //不重复就在dig数组中创建新的行，在city数组中创建新的元素
            city.push_back(c);
            dig[city.size()-1].push_back(d);
        }
        else  dig[flag].push_back(d);  //城市名重复就将单号存入该城市的一维数组中
    }
    for(int i=0;i<city.size();i++)
    {
        cout<<city[i]<<" "<<dig[i].size()<<endl; 
        for(int j=0;j<dig[i].size();j++)
            cout<<dig[i][j]<<endl;
    }
}
```
# 集合set<br>
通俗来讲，就是由一些不重复的数据组成的，比如{1,2,3}是集合，{1，1，2，3}不是集合。<br>
使用set集合需要加上`include<set>`头文件。<br>
C++的set集合是通过红黑树来实现的，他会将元素自动排序并且保持元素的唯一性。<br>
### 1、插入元素insert()<br>
>通过insert("xxxx")可以插入一个新元素，如果原来已经有元素了，那原有集合就不发生任何改变。<br>
```c++
#include<iostream>
#include<set> 
using namespace std;
int main()
{
	set<string> country;
	country.insert("China"); //有一个元素了 
	country.insert("Ameria"); //2个元素 
	country.insert("France");//3个元素 
	country.insert("Japan");//4个元素 
	country.insert("China");//重复了，还是4个元素 
 	return 0;
}
```
### 2、删除元素erase()<br>
通过erase("xxx")可以删除集合里面的一个元素，原来没有这个元素的话就什么也不干。<br>
```c++
#include<iostream>
#include<set> 
using namespace std;
int main()
{
	set<string> country;
	country.insert("China"); 
	country.insert("Ameria"); 
	country.insert("France");
	country.insert("Japan");
	country.insert("China");
	country.erase("Japan");//删除了日本
	country.erase("France");//删除了法国 
 	return 0;
}
```
### 3、判断元素是否存在count()<br>
通过count("xxx")可以判断集合里面有没有xxx这个元素，有就返回1，没有就0.<br>
```c++
#include<iostream>
#include<set> 
using namespace std;
int main()
{
	set<string> country;
	country.insert("China"); 
	country.insert("Ameria"); 
	country.insert("France");
	country.insert("Japan");
	country.insert("China");
	cout<<country.count("Japan");//输出1
	cout<<country.count("meiguo");//输出0 
 	return 0;
}
```
### 4、遍历元素-用迭代器<br>
通过`set<string>::iterator it=country.begin()`来声明set的迭代器。begin()表示集合头部，end()表示集合尾部。与此同时还有
反向迭代器reverse_iterator也能用，rbegin()表示尾，rend()表示头<br>
正向迭代器：<br>
```c++
#include<iostream>
#include<set> 
using namespace std;
int main()
{
	set<string> country;
	country.insert("China"); 
	country.insert("Ameria"); 
	country.insert("France");
	country.insert("Japan");
	country.insert("China");
	for(set<string>::iterator it=country.begin();it!=country.end();it++){
		cout<<*it<<" ";
	}

}
```
反向迭代器：<br>
```c++
#include<iostream>
#include<set> 
using namespace std;
int main()
{
	set<string> country;
	country.insert("China"); 
	country.insert("Ameria"); 
	country.insert("France");
	country.insert("Japan");
	country.insert("China");
	for(set<string>::reverse_iterator it=country.rbegin();it!=country.rend();it++){
		cout<<*it<<" ";
	}

}
```
### 5、清空clear()<br>
调用`country.clear()`即可清空所有元素。<br>

### 6、时间复杂度统计：<br>
![image](https://github.com/spesserta/My-algorithm-note/assets/138494873/507a4b8b-09bc-46d2-bd72-1ced97ee4c36)




# 队列Queue<br>
>学过数据结构就知道，队列相当于商场排队的人，是先进先出后进后出，只能在首尾两端操作的线性表。<br>
### 1、定义一个队列：<br>
`queue<T> qu`<br>
### 2、入队和出队：<br>
入队：`qu.push()`<br>
出队：`qu.pop()`<br>
### 3、返回首尾的元素值：<br>
返回queue中第一个元素的引用：`qu.front()`<br>
返回queue中最后一个元素的引用：`qu.back()`<br>
### 4、返回队列中元素个数：<br>
`qu.size()`<br>
### 5、判断queue中有没有元素：<br>
`qu.empty()`有元素就返回0，没有元素就返回1.<br>
### 6、遍历队列的元素：<br>
>和其他类型不一样，queue不支持迭代器遍历，只能一边遍历一边出队。<br>
```c++
	while(V.size()){
		cout<<V.front()<<endl;  //输出元素
		V.pop();                //出队
	}
	while(NO.size()){
		cout<<NO.front()<<endl;
		NO.pop();
	}
```
### 7、例题：<br>
![image](https://github.com/spesserta/My-algorithm-note/assets/138494873/59edc1dd-59e0-4439-a56c-c398c2a358e6)
![image](https://github.com/spesserta/My-algorithm-note/assets/138494873/5e1af5fb-a190-4773-9b11-049d26552f01)
```c++
#include <iostream>
#include <queue>
using namespace std;

queue<string> V;  //VIP窗口
queue<string> N;  //普通窗口

int main()
{
    int M;
    cin>>M;
    while(M--) 
    {
        string op,name,type;  
        cin>>op;
        if(op=="IN")  //操作为in就入队
        {
            cin>>name>>type;

            if(type=="V")  //分成VIP窗口入队和普通窗口入队
                V.push(name);

            else
                N.push(name);
        }
        else   //操作为out就出队
        {
            cin>>type;
            if(type=="V")  //分成VIP窗口入队和普通窗口出队
                V.pop();
            else
                N.pop();
        }
    }
    while(V.size())  //函数体里面出队，当V队列没有元素时结束
    {
        cout<<V.front()<<endl;
        V.pop();
    }
    while(N.size())  //函数体里面出队，当N队列没有元素时结束
    {
        cout<<N.front()<<endl;
        N.pop();
    }
}
```

# 优先队列（堆）
优先队列相比于队列，它在元素插入时候就会对其进行排序，默认将大的数值放在队首（大顶堆）。<br>
使用优先队列需要加上`#include<queue>`头文件。<br>
### 1、基本操作
* 大顶堆定义为`priority_queue<int> qu; ` <br>
* 小顶堆定义为`priority_queue<int,vector<int>,greater<int>> qu2;`<br>
* 入队：`qu.push(type a);` <br>
* 出队：`qu.pop();` <br>
* 访问队头元素：`qu.top()` <br>
* 判空：`qu.empty()` <br>
* 返回队列元素个数：`qu.size();`<br>
![image](https://github.com/spesserta/My-note/assets/138494873/2fa180ee-d947-4978-9e1c-bd548268b9da)
### 2、自定义类型的优先队列
>优先队列相比于普通队列，它多了优先级比较，如果直接将自定义类型用作优先队列的类型，优先队列就不知道以什么规则来比较优先级，会报错。<br>
>因此需要自己重载比较运算符得到。<br>
```cpp
#include <iostream>
#include<queue>
using namespace std;
struct Node{
  int x;
  int y;	
  bool operator<(const Node &b)const{
  	//用x作为优先级比较的内容
  	//'>'为x小的优先级高，'<'是x大的优先级高 
    return this->x<b.x;  
	  
  }
}no;
int main()
{
  priority_queue<Node> que;
  que.push((Node){1,5});
  que.push((Node){2,3});
  que.push((Node){3,7});
  que.push((Node){0,0});
  cout<<que.top().x<<" "<<que.top().y<<endl; 
  return 0;
}
```
![image](https://github.com/spesserta/My-note/assets/138494873/d078dfce-886d-4d9d-b4b3-ce3709f5c2a2)

### 例题：<br>
![image](https://github.com/spesserta/My-note/assets/138494873/143b2087-431c-4e32-b460-4d292a6fa4ce)
```cpp
/*
整一个从小到大排序的优先队列
将所有数值push进去
while 直到size为1停下
pop2个，求和，将结果push进队列，cnt加上
*/
#include<bits/stdc++.h>
#include <queue>
using namespace std;
priority_queue<int,vector<int>,greater<int>> que;
int main() {
    int n;
    cin>>n;
    while(n--) {
        int t;
        cin>>t;
        que.push(t);
    }
    long long cnt=0;
    while(que.size()>1) {
        int a=que.top();
        que.pop();
        int b=que.top();
        que.pop();
        int c=a+b;
        cnt+=c;
        que.push(c);
    }
    cout << cnt;
    return 0;
}
```



# 栈stack<br>
加上`include<stack>`头文件，再定义栈`stack<T> s`即可定义了一个储存T类型的栈s。<br>
### 1、入栈和出栈和获取栈顶元素<br>
入栈：`push()`<br>
出栈：`pop()`<br>
获取栈顶元素：`top()`<br>
通过入栈和出栈都有“先进后出、后进先出”的规律。
```c++
#include<iostream>
#include<stack>
using namespace std;
int main(){
	stack<int> s;
	s.push(1);
	cout<<s.top()<<endl;
	s.push(2);
	cout<<s.top()<<endl;
	s.push(3); 
	cout<<s.top()<<endl;
	s.pop();
	cout<<s.top()<<endl;
	s.pop();
	cout<<s.top()<<endl;
	s.pop();
	cout<<s.top()<<endl;
	
	return 0;
}
```
![image](https://github.com/spesserta/My-algorithm-note/assets/138494873/55df2f71-908d-471e-b9e0-f1153624a090)

### 2、判空和栈的数据个数：<br>
判空：`empty()`<br>
获取元素个数：`size()`<br>
```c++
#include<iostream>
#include<stack>
using namespace std;
int main(){
	stack<string> s;
	s.push("小明");
	cout<<"栈顶元素："<<s.top()<<"栈的个数："<<s.size()<<endl;
	s.push("小红");
	cout<<"栈顶元素："<<s.top()<<"栈的个数："<<s.size()<<endl;
	s.push("小刚"); 
	cout<<"栈顶元素："<<s.top()<<"栈的个数："<<s.size()<<endl;
	s.pop();
	cout<<"栈顶元素："<<s.top()<<"栈的个数："<<s.size()<<endl;
	s.pop();
	cout<<"栈顶元素："<<s.top()<<"栈的个数："<<s.size()<<endl;
	cout<<"是否为空："<<s.empty()<<endl;;
	s.pop();
	cout<<"是否为空："<<s.empty()<<endl;;
	return 0;
}
```

# 映射表map<br>
映射就是一个元素对应另外一个元素，比如下图的对应关系：class("Tom")=1 ，class("Jone")=2 ，class("mary")=1.<br>
![image](https://github.com/spesserta/My-algorithm-note/assets/138494873/23394b60-2fba-4d8f-9eb6-20e6b5db703a)
<br>其中姓名集合称为关键字集合(key)，班级集合称为值集合(value)。<br>
类似于f(key)=value，一个key对应一个value<br>
注意：前提是集合，不能重复元素！并且需要加上`include<map>`头文件<br>
### 1、构造一个映射：<br>
格式：`map<T1,T2> m;`这样定义了一个名为m的T1为KEY的T2为VALUE的的一个映射，初始值m是空映射。<br>
例如：map<string,int> m构建了一个字符串到整数的映射，这样可以把一个整数和是字符串联系起来。<br>
1、插入一对映射insert()<br>
在插入之前需要定义一个头文件<utility>中的一个pair类型（可以看成两个成员first和second的结构体）。<br>
```c++
#include<iostream>
#include<map> 
#include<string>
#include<utility>
using namespace std;
int main()
{
	map<string,int> dict;
	dict.insert(make_pair("Tom",1));//{"Tom"和1绑定}
	dict.insert(make_pair("Jone",2));//{"Jone"和2绑定}
	dict.insert(make_pair("Jack",1));//{"Jack"和1绑定}
	dict.insert(make_pair("Mike",2));//{"Mike"和2绑定}
  //和set集合一样，如果现在再插入一个key为Mike的pair，不会发生任何变化
	return 0;
}
```
除此之外，还可以像数组一样`map[key]=value`来插入。<br>
### 2、输出数据（用方括号下标即可，方括号里面为key）<br>
```c++
#include<iostream>
#include<map> 
#include<string>
#include<utility>
using namespace std;
int main()
{
	map<string,int> dict;
	dict.insert(make_pair("Tom",1));
	dict.insert(make_pair("Jone",2));
	dict.insert(make_pair("Jack",1));
	dict.insert(make_pair("Mike",2));
	cout<<dict["Mary"]<<endl;  //集合里面没有这个，输出0 
	cout<<dict["Tom"]<<endl;   //key为Tom的value为1，输出1 
	return 0;
}
```
### 3、查找操作<br>
用count()即可，如果被映射过就返回1，没有的话返回0.<br>
```c++
m.count(key)：//由于map不包含重复的key，因此m.count(key)取值为0，或者1，表示是否包含。
m.find(key)：//返回迭代器，判断是否存在。
```

### 4、清空clear<br>
只需要调用clear()即可清空映射表。<br>

### 5、查看容量和判空：<br>
```c++
int map.size();//查询map中有多少对元素
bool empty();// 查询map是否为空
```

### 6、遍历操作：<br>
```c++
map<string, string>::iterator it;
for (it = mapSet.begin(); it != mapSet.end(); ++it)
{
    cout << "key" << it->first << endl;

    cout << "value" << it->second << endl;
}
```

### 7、例题：<br>
![image](https://github.com/spesserta/My-algorithm-note/assets/138494873/1e26fc6b-46f7-4210-97eb-9bcb6ff6f5f1)
![image](https://github.com/spesserta/My-algorithm-note/assets/138494873/a839c2a2-a51c-4984-9346-14ce863bca0d)
```c++
 #include <iostream>
 #include <map>
 using namespace std;
map<string,bool> mp;
int main ()
{
    int n;
    string ans="NO";    
    cin>>n;
    for(int i=0;i<n;i++)
    {
        string word;
        cin>>word;
        if(mp.count(word)){  //如果输入的单词已经有了
            ans=word;
            break;     //直接跳出循环，输入操作也中断了。
        }
        else mp[word]=1;    //没有的话将word值存入映射表中
    }
    cout<<ans<<endl;
}
```
### 8、斯诺登的密码
![image](https://github.com/spesserta/My-note/assets/138494873/647331f7-29fd-4a54-8ea2-41ab23584f5f)
>一串单词对应一个数字，用map非常好
```cpp
#include<bits/stdc++.h>
using namespace std;
map<string,int> q;
vector<int> vec;
int main()
{
	//对所有单词打表 
    q["one"]=1;q["two"]=2;q["three"]=3;q["four"]=4;q["five"]=5;
	q["six"]=6;q["seven"]=7;q["eight"]=8;q["nine"]=9;q["ten"]=10;
    q["eleven"]=11;q["twelve"]=12;q["thirteen"]=13;q["fourteen"]=14;
	q["fifteen"]=15;q["sixteen"]=16;q["seventeen"]=17;
	q["eighteen"]=18;q["nineteen"]=19;q["twenty"]=20;q["a"]=1;
	q["both"]=2;q["another"]=1;q["first"]=1;q["second"]=2;q["third"]=3;
	string s;
	for(int i=1;i<=6;i++){
		cin>>s;
		if(q.count(s)){ //如果字典里面有
		 int k=q[s]*q[s]%100;
		 if(k==0) continue;
		 vec.push_back(k);  //将数字插入  
		}
	} 
	if(vec.empty()){ //没有数字就输出0 
		cout<<0;
		return 0;
	} 
	sort(vec.begin(),vec.end());
	cout<<vec[0]; //首字母不需要输出前导0 
	for(int i=1;i<vec.size();i++){
		if(vec[i]<10) cout<<0; //输出前导0 
		cout<<vec[i];
	}
    return 0;
}
```

# 双端队列deque
一般的队列只能在两端操作，并且队头只能出队，队尾只能入队，但是双端队列的队头和队尾都可以进行出队和入队。除此之外，双端队列相比于一般队列还支持很多操作。<br>
### 创建一个双端队列
和上面的容器一样，都是`deque<数据类型> de;`即可。<br>

### 队头和队尾的操作
* `push_back()` 尾插
* `push_front()` 头插
* `pop_back()` 尾出
* `pop_front()` 头出
* 
### 判空和大小操作
* `size()` 返回队列的长度
* `empty()` 判断队列是否为空
* 
### 插入和删除特定位置的元素
>注意这个插入和删除位置使用地址表示的，需要用到迭代器变量，直接用数字下标是不行的。
>并且insert和erase里面参数表示的位置不是同一个位置。
* `insert(it,n)` 在迭代器位置中插入n
* `inser(it,x,n)` 在迭代器中插入x个元素，元素为n
* `erase(it,n)` 在迭代器位置中删除n
* `erase(it,it+n)` 删除it到it+n这一区间的元素

### 访问指定位置的元素
* `at(从0开始的下标)` 即可访问

### 2个deque互相交换元素
* `swap(deque)` 即可

### deque元素的遍历
```cpp
deque<数据类型>::iterator it=de.begin(); //声明一个deque的迭代器
deque<数据类型>::reverse_iterator rit=de.rbegin();  //声明一个deque的反向迭代器
for(;it!=de.end();it++){ //正向迭代
    cout<<de.at(i)<<" ";
 }
for(;it!=de.rend();it++){ //反向迭代
    cout<<de.at(i)<<" ";
 }
```
### 综合运用代码
```cpp
#include <bits/stdc++.h>
using namespace std;
deque<int> de;
void show(){
	cout<<endl<<endl;
	cout<<"当前元素个数："<<de.size()<<endl;
    cout<<"当前所有元素：";
    for(auto it=de.begin();it!=de.end();it++){
    	cout<<*it<<" ";
    }
}
int main() {
    
    //尾插3个元素 
    de.push_back(1);
    de.push_back(2);
    de.push_back(3);
    show();
    //头插3个元素
	de.push_front(-1);
	de.push_front(-2);
	de.push_front(-3); 
	show();
    //头删和尾删
	de.pop_back();
	de.pop_front();
    show();
    //插入和删除指定位置元素 
    deque<int>::iterator it=de.begin();
	de.insert(it+1,10); //it+n为第n+1个元素位置
    show();
    de.erase(it+1);     //注意it+n为第n+2个位置 
    show();
    de.insert(it+1,5,10); //从下标it+1开始插入5个元素 
    show();
    de.erase(it,it+6);  //删除从第2个到第7个所有元素 
    show();
	//访问指定元素
	cout<<endl<<endl<<"索引为1的元素是："<<de.at(1); 
    cout<<endl<<endl<<"索引为0的元素是："<<de.at(0);
    //交换2个deque的元素
	deque<int> de2={1,2,3,4,5,6}; 
	de2.swap(de); //de和de2交换 
	cout<<endl<<endl<<"交换后de的元素为：";
    for(int i=0;i<de.size();i++){
    	cout<<de.at(i)<<" ";
    }
    deque<int>::reverse_iterator rit=de.rbegin();
    return 0;
}
```
![image](https://github.com/spesserta/My-note/assets/138494873/700d7d7c-8fab-4fe5-8d39-921ecc8cb6a5)







