# STL扩展
## 一、动态数组vector<br>
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
### 10、例题：<br>
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
## 二、集合set<br>
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

## 三、映射表map<br>
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

## 五、队列Queue<br>
>学过数据结构就知道，队列相当于商场排队的人，是先进先出后进后出，只能在首尾两端操作的线性表。<br>
1、定义一个队列：<br>
`queue<T> qu`<br>
2、入队和出队：<br>
入队：`qu.push()`<br>
出队：`qu.pop()`<br>
3、返回首尾的元素值：<br>
返回queue中第一个元素的引用：`qu.front()`<br>
返回queue中最后一个元素的引用：`qu.back()`<br>
4、返回队列中元素个数：<br>
`qu.size()`<br>
5、判断queue中有没有元素：<br>
`qu.empty()`有元素就返回0，没有元素就返回1.<br>
6、例题：<br>
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






