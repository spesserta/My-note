# STL扩展
## 一、动态数组vector<br>
vector数组就是可以根据情况实时变动的，长度可以动态改变。使用它可以很方便的减少代码量。<br>
在使用vector数组时需要加上#include<vector>头文件。<br>
### 1、构造动态数组：<br>
无参构造：`vector<T> vec;` 里面T表示数据类型，vec是数组名，可以随便取一个。<br>
构造并赋值：` int n=10; vector<T> vec(n,1)`用n个1构造初始长度的数组。<br>
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
### 3、获取元素个数：<br>
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
## 二、集合set<br>
通俗来讲，就是由一些不重复的数据组成的，比如{1,2,3}是集合，{1，1，2，3}不是集合。<br>
使用set集合需要加上`include<set>`头文件。<br>
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

## 三、映射表map<br>
映射就是一个元素对应另外一个元素，比如下图的对应关系：class("Tom")=1 ，class("Jone")=2 ，class("mary")=1.<br>
![image](https://github.com/spesserta/My-algorithm-note/assets/138494873/23394b60-2fba-4d8f-9eb6-20e6b5db703a)
<br>其中姓名集合称为关键字集合(key)，班级集合称为值集合(value)。<br>
注意：前提是集合，不能重复元素！并且需要加上`include<map>`头文件<br>
### 1、构造一个映射：<br>
格式：`map<T1,T2> m;`这样定义了一个名为m的T1类型到T2类型的一个映射，初始值m是空映射。<br>
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
### 2、输出数据（用方括号下标即可）<br>
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
### 3、判断关键字是否被映射过<br>
用count()即可，如果被映射过就返回1，没有的话返回0.<br>
```c++
cout<<dict["Mary"];  //输出0
cout<<dict["Tom"];   //输出1

```

### 4、清空clear<br>
只需要调用clear()即可清空映射表。<br>

## 四、栈stack<br>
加上`include<stack>`头文件，再定义栈`stack<T> s`即可定义了一个储存T类型的栈s。<br>
1、入栈和出栈和获取栈顶元素<br>
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

2、判空和栈的数据个数：<br>
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











