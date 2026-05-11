



# 一、Hello World


```c#
using System;   //使用系统封装好的System命名空间

namespace CTest  //该cs文件里包含命名空间CTest
{
    public class Program  // 命名空间CTest内部定义Program类为主类（包含主方法Main的类）
    {
        static void Main(string[] args)  //Main是主方法，是程序的入口
        {
            //调用System命名空间里的Console类里的WriteLine方法
            //输出“Hello World!”
            System.Console.WriteLine("Hello World!");  
        }
    }
}
```

例如上面输出Hello World的案例，可以引申出类和命名空间的定义


### 1、类和命名空间

- 类（class）是构成程序的主体
- 命名空间（namespace）是以树形结构来组织类，可以看成多个类的集合体

>注意相同的类名，功能不一定相同，因为他们可能在不同的命名空间内。不同命名空间里的同一类名表示不同的类


### 2、类库

类库相当于别人写好的“工具箱”，里面的类不能直接运行，专门拿来调用的，以此来实现代码的复用和模块化。<br>
例如ADO.NET就是一个系统封装好的类库，通过引入这个类库，调用这个类库里面的方法来实现对数据库的连接操作。<br>

<img width="1920" height="1025" alt="image" src="https://github.com/user-attachments/assets/df2c1bd0-1410-4268-9f1c-e2fb697b2485" />


# 二、类与对象

### 1、类与对象概念和声明
类是对现实事物进行抽象的结果，现实的事物中包含着“实体”以及“动作”，这正好对应着类的“属性”和“方法”。<br>
对象是类的“实例化”，对应着现实世界的单个具体事物。<br>
例如现实世界的“人”类可以抽象成Person类，一个Person类可以包含“身高、体重、姓名、性别等”属性，以及“吃饭、睡觉等”方法。
```c#
Person 小明 = new Person();
```
上面的代码就是将Person类进行实例化，得出“小明”这个对象，这就对应着现实世界的单个具体事物。<br>


类和对象的声明如下：

```c#
using System;  

namespace CTest  
{
    public class Program  
    {
        static void Main(string[] args)  
        {
            Person xiaomin = new Person();  //定义一个Person类的xiaomin对象
            xiaomin.name = "Test";
            xiaomin.sex = true; 
            xiaomin.age = 20;
            xiaomin.Eat();  //输出“Test在吃东西”
            xiaomin.Sleep(); //输出“Test在睡觉”
        }
    }
    public class Person   //声明“人”类
    {
        public string name;  //姓名
        public bool sex;     //性别
        public int age;      //年龄

        public void Eat()  //吃饭方法
        {
            Console.WriteLine($"{name}在吃东西");
        }
        public void Sleep()  //睡觉方法
        {
            Console.WriteLine($"{name}在睡觉");
        }
    }
}
```

### 2、类的三大成员

类的三大成员分别是属性、方法、事件。

- 属性：用来存储数据的，表示类或者对象当前的状态
- 方法：相当于C语言的“函数”，表示类或者对象能做什么
- 事件：C#特有的机制，用来表示类或者对象通知其他类或者对象的机制

>注意有些特殊的类和对象也有侧重点，比如Winform里面的Entity Framework类重属性，一个固定的Button按键这种就是几乎只有属性。也有重方法的，例如Math里面都是一些数学计算的方法。

<img width="868" height="556" alt="image" src="https://github.com/user-attachments/assets/0ae4a5e5-1dbb-4ba1-b6b7-a5f1fbddef9c" />

>也有侧重事件的类，例如WPF里面的Timer类

<img width="1920" height="1038" alt="image" src="https://github.com/user-attachments/assets/ca759207-8dbc-4946-b535-83270ec7eb4b" />


### 3、静态成员和实例成员

- 静态（用static修饰的）成员在语义上表示它是“类的成员”
- 实例（不用static修饰的）成员在语义上表示它是“对象的成员”
- 绑定（Binding）指的是编译器把一个成员与类或者对象关联起来

>换一个角度看，静态Static成员表示一种普遍现象，可以看成“类的成员”，例如人类这个类的最大年龄是150。实例在语义上表示它是“对象的成员”，比如小明的年龄为18岁。

```c#
class Person
{
  static uint MAX_Age = 150 ;  //表示一种普遍现象，人类的最大年龄是150，所有对象的MAX_Age == 150
  uint Age ;  //表示对象的成员，具体Age值由对应的对象所决定，不同对象的Age也可能不同
}
```

### 4、访问修饰符

- public：公有权限，任何地方都能用
- private：私有权限，只有自己类内部能用
- protected：保护权限，自己类+子类能用

>私有的自己用，保护的父子用，公开的随便用

在实际写代码中要注意成员字段尽量private，用public属性包裹，要给子类继承复用的，用protected，对外暴露接口、方法，用public


```c#
public class Person
{
    // 私有字段（private）外部绝对不能直接访问！
    private string _name;    // 姓名
    private int _age;        // 年龄

    // 公共属性（public）用来安全地访问私有字段
    public string Name
    {
        get { return _name; }    // 读取
        set { _name = value; }   // 修改
    }
    public int Age
    {
        get { return _age; }
        set
        {
            // 规范好处：可以加逻辑验证！
            if (value >= 0 && value <= 150)
                _age = value;
            // 年龄非法时不赋值，保证数据安全
        }
    }

    // 受保护成员（protected）只有自己+子类能访问
    protected string IdCard { get; set; } // 身份证，子类可用，外部不能用

    // 公共方法（public）对外提供功能
    public void SayHello()
    {
        Console.WriteLine($"大家好，我是 {Name}，今年 {Age} 岁");
    }

    // 5. 私有方法（private）内部工具方法，外部完全看不见
    private void CheckHealth()
    {
        // 内部逻辑，外部无权调用
    }
}
```

# 三、构成C#的基本元素

一个C#程序由以下几部分够成：

- 关键字
- 操作符
- 标识符
- 标点符号
- 文本
- 注释与空白

### 1、关键字

关键字是人类文字里挑出来能表达逻辑的那几个单词，每个单词都有自己独特的逻辑功能.C#的关键字有这些：

<img width="721" height="783" alt="image" src="https://github.com/user-attachments/assets/3c0a712c-8952-42db-a924-3db0472caeec" />

### 2、操作符（运算符）

操作符是变量之间用来运算的，分为算术运算符和逻辑运算符。按照作用的数据个数又可以分为单目运费符、双目运算符、三目运算符。

<img width="692" height="964" alt="image" src="https://github.com/user-attachments/assets/bdf19d88-f8fa-4363-a15d-76e3e1a205cf" />


### 3、标识符

标识符是程序员自己取的名字，可以表示变量名、类名、对象名等，合法的标识符由字母数字下划线组成（数字不能开头），不能和关键字冲突。标识符取名的时候建议采用驼峰命名法。

```c#
//规范的命名法
int StudentId;
int StuentAge;
int Student_Id;
int Stuent_Age;
```

### 4、标点符号

标点符号不参与运算，只代表一个符号，例如;号、,号等

### 5、文本（变量值）

```c#
//int是数据类型、StudentId是标识符、2127152681是文本
int StudentId = 2127152681;
int StuentAge = 20;
```

### 6、注释和空白

注释符和C语言是一样的：//和/*   */



























