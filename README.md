### 为什么要选计算机啊为什么要选计算机啊为什么要选寄蒜鸡啊為什麼要選寄蒜雞啊???!!!?!?!?!?!?!?!?

### 君乃初入太学，当习[C语言](C++程序设计基础/C语言部分)之术，汝速去！

```c
井inculde《studio。h》
lnt mian{}
【
paint("Halo word”):
remake O。
】
```
### 此乃何物？哈哈哈哈哈哈哈哈！笑煞我也！你写了个蛋？
### 何故发笑！汝能则试之？
```c
#include<stdio.h>
int main(){
char buf[4];
strcpy(buf, "Hello"); 
printf("%s", buf);   
return 0;
}
```
### “烫烫烫烫烫烫烫烫” 何故编绎器呼“烫”耶？

### 君既通C语，何不试炼[C++](C++程序设计基础/C++部分)之奇术？然观汝昔日之“蛋”作，恐此番亦难成器！

### C++者，C之进阶也，岂有畏难之理？且看吾试手——

```cpp
#include <iostream>
using namespace std;

class 动物 {
public:
    void 叫() {
        cout << "动物叫..." << endl;
    }
};

class 狗 : 动物 {  // 错误：未用 public 继承
public:
    void 叫() {
        cout << "汪汪汪！" << endl;
    }
};

int main() {
    狗 阿黄;
    阿黄.叫();
    return 0;
}
```
### 此乃“继承”之妙用也！
### 噫！汝之“狗”虽能叫，然若父类有protected成员，汝能访之乎？

```cpp
#include <iostream>
using namespace std;

class 动物 {
public:
    virtual void 叫() {  // 虚函数
        cout << "动物叫..." << endl;
    }
};

class 狗 : public 动物 {  // 正确：public 继承
public:
    void 叫() override {  // 重写虚函数
        cout << "汪汪汪！" << endl;
    }
};

int main() {
    动物* 动物指针 = new 狗();
    动物指针->叫();  // 多态调用
    delete 动物指针;
    return 0;
}

```

### 此乃“虚函数”与“多态”之正道，汝当谨记！
### 君今已臻大二之境，宜速习[数据结构](数据结构与算法)之学！救赎之道，就在其中！
### 同学不怕，数据结构，并非难事，先加入猛料，再大火收汁，摇匀脑浆，乃易如反掌耳
```
//脑部复杂度O(1)
//脑部复杂度O(logn)
//脑部复杂度O(n)
//脑部复杂度O(n2)
//脑部复杂度O(n3)
//脑部复杂度O(nn)
```
### 此真初学课程呼？真天书也！吾已红温！
### 既习之罔也，何不先看代码？以观其行呼？吾有一书，可救汝之水火！名曰[算法设计与分析](算法)





































































































