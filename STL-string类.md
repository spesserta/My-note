# string类函数总结<br>
## 一、string的构造和拼接<br>
>string类函数，需要带上<string>函数头<br>
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
>两者都是返回一个字符串的指针<br>
`cout<<str.c_str()<<endl;`

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

## 五、字符串的比较操作
### 1、==
>判断两个字符串的内容是否相等，如果相等就返回true，否则就false
```c++
string str1="dawjv";
* string str2="dwajy";
* cout<<(str1==str2)<<endl;
```
### 2、compare()
>判断两个字符串，等于就是0，小于就是负数，大于就是正数，根据ASCII码来作为判断标准
```c++
string str1="dawjv";
* string str2="dwajy";
* cout<<str1.compare(str2)<<endl;
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
## 七、替换与子串
### 1、replace()
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
### 1、to_string()
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

