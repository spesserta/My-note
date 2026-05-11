



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


### 7、数据类型

一个C#类型中所包含的信息有:
- 存储此类型变量所需的内存空间大小
- 此类型的值可表示的最大、最小值范围:此类型所包含的成员(如方法、属性、事件等)
- 此类型由何基类派生而来
- 程序运行的时候，此类型的变量在分配在内存的什么位置
- 此类型所允许的操作(运算)

数据类型所需的空间大小以及表示范围的参考图如下，如果想深究可以学习《计算机组成原理》。

<img width="700" height="365" alt="image" src="https://github.com/user-attachments/assets/1795cd37-e768-4708-81e9-ff53b251f9fd" />

<img width="619" height="170" alt="image" src="https://github.com/user-attachments/assets/0419c65d-9583-4553-bbb7-a2941f0a665d" />

数据类型类型本质上可以用一个类来定义，例如int对应着一个特殊的类，所以包括方法、属性、事件等成员, 也可以用上继承多态等。

```c#
internal class Program
{
   static void Main(string[] args)
   {
      Type thisType= typeof(int);   //Type表示类型，变量用来获取int的数据类型
      Console.WriteLine(thisType.Name);  //输出int型的类型名字
      Console.WriteLine(thisType.BaseType.FullName);  //输出int型的父类全名
      Console.WriteLine(thisType.BaseType.FullName.FullName);  //输出int型的父类的父类全名（报错因为往上没有父类了）
   }
}
```

数据类型也具有允许该类的操作（运算），例如int类型支持单目自增++

```c#
int age = 10;
age++;
```

一个程序的静态的东西是装在硬盘上的，动态的东西是装在内存上的。内存里也包含堆区和栈区，栈区主要是放调用的方法的，内存小速度快，堆主要放实际数据的，栈满了叫爆栈，堆满了叫内存泄露。

<img width="854" height="534" alt="image" src="https://github.com/user-attachments/assets/36fe9561-20a3-474a-be69-e1609d837760" />

```c#
using System;

class Program
{
    // 自定义引用类型（类）
    class Person // 引用类型，实际数据存在堆上
    {
        public int Age; // 值类型，作为引用类型的成员，也存在堆上
    }

    static void Main()
    {
        // 1. 值类型（int）— 直接存在栈上
        int num = 10;

        // 2. 引用类型（Person）— 分两部分：
        //    - 变量person（引用/指针）存在栈上
        //    - new Person()的实际对象存在堆上
        Person person = new Person();
        person.Age = 20;

        // 3. 方法调用—方法的参数/局部变量入栈，方法结束后出栈
        ChangeValue(num);
        ChangePerson(person);

        Console.WriteLine("num = " + num);         // 输出 10（栈上值未被修改）
        Console.WriteLine("person.Age = " + person.Age); // 输出 30（堆上数据被修改）
    }

    static void ChangeValue(int value)
    {
        value = 100; // 修改的是栈上的副本，不影响原变量
    }

    static void ChangePerson(Person p)
    {
        p.Age = 30; // p是栈上的引用，指向堆上的对象，修改的是堆上的实际数据
    }
}
```

面试题：C#有哪些数据类型

<img width="656" height="487" alt="image" src="https://github.com/user-attachments/assets/5d0fb9c8-4293-4956-8cf0-ad4f94a17237" />

- 值类型（int、struct、enum 等）是存值本身，数据存放在栈中，赋值的时候采用复制的方式，可以进一步分为类、接口、委托
- 引用类型（class、string、数组、委托等）存地址，数据存放在堆中，赋值的时候采用复制地址的方式，可以进一步分为：结构体、枚举

还有特殊的指针类型（unsafe 用）和可空类型（int?），分别用于底层操作和值类型存 null。比如 string 虽然是引用类型，但它是 “不可变的”—— 修改 string 时其实是创建新对象，所以行为像值类型；

### 8、变量、对象与内存

变量从表面上来看是用来存储数据的，准确点说：变量是以变量名所对应的内存地址为起点、以其数据类型所要求的存储空间为长度的一块内存区域。<br>

变量的7种类型：静态变量、实例变量（成员变量/字段）、数组元素、值参数、引用参数、输出形参、局部变量。<br>

声明格式：[可选修饰符] 类型 变量名; <br>

值类型和引用类型：


<img width="787" height="528" alt="image" src="https://github.com/user-attachments/assets/ae9d4df6-7f9b-4cb6-a1aa-6cd8429a0863" />


<img width="874" height="628" alt="image" src="https://github.com/user-attachments/assets/ad0a72da-6074-4a08-b0bb-147ea599ad60" />

>最后需要注意：局部变量是在栈空间里的，局部变量那种的值类型需要有明确的默认值（没有就报错）。引用的默认值是0

<img width="853" height="427" alt="image" src="https://github.com/user-attachments/assets/9bb2a4d5-307d-45c5-8a5a-b98eb3182974" />

<img width="868" height="590" alt="image" src="https://github.com/user-attachments/assets/f44be0a0-65c3-4a33-8313-a34ab96c3e0a" />


### 9、装箱和拆箱、常量

装箱和拆箱说白了可以总结成一句话:  拆箱：堆到栈 装箱：栈到堆 ，尽量避免装拆箱来提高性能。<br>

常量就是定义的时候必须赋值，然后赋值后不能修改。


<img width="776" height="617" alt="image" src="https://github.com/user-attachments/assets/4f4415a3-ff22-488b-85b8-e1dbd2c19cc5" />






































