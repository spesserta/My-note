<img width="966" height="899" alt="image" src="https://github.com/user-attachments/assets/32f7f5ab-75be-4aee-886d-c99e398c702e" /><img width="976" height="161" alt="image" src="https://github.com/user-attachments/assets/c194e822-c717-42ab-94d6-0cffd9373e73" /><img width="966" height="51" alt="image" src="https://github.com/user-attachments/assets/c0719d95-f532-4f14-aecb-13b9d8e21826" /><img width="658" height="26" alt="image" src="https://github.com/user-attachments/assets/89c89318-9e24-4576-af2c-bf896e37d794" /><img width="966" height="51" alt="image" src="https://github.com/user-attachments/assets/b3527b74-7e6e-430d-981c-c746b4765289" /><img width="966" height="56" alt="image" src="https://github.com/user-attachments/assets/b3532a00-f188-44e1-b5af-691f525fce50" /><img width="547" height="28" alt="image" src="https://github.com/user-attachments/assets/a9f17a63-67dc-47c1-9da8-6f890a5309e8" /><img width="966" height="51" alt="image" src="https://github.com/user-attachments/assets/c37b7963-8bc4-4827-92e6-95ed1541f5dc" /><img width="782" height="26" alt="image" src="https://github.com/user-attachments/assets/08c4c076-1d0e-4d09-a955-e7d87c00f745" />
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

装箱和拆箱说白了可以总结成一句话: 把堆区看成一个箱子，拆箱是堆到栈 装箱是栈到堆 ，尽量避免装拆箱来提高性能。<br>

常量就是定义的时候必须赋值，然后赋值后不能修改。


<img width="776" height="617" alt="image" src="https://github.com/user-attachments/assets/4f4415a3-ff22-488b-85b8-e1dbd2c19cc5" />


# 四、方法

C#的方法是由C语言的函数发展过来的，它俩其实一样，只是叫法不同。C#的方法有以下几个特点：

- 方法不能写在类的外面（C++的函数写在类外叫做全局函数，C#不能这样）
- 方法是类的基本成员之一
- 使用方法是为了隐藏复杂的逻辑以及方便代码重用

### 1、方法的组成如下

<img width="1279" height="570" alt="image" src="https://github.com/user-attachments/assets/284f3d98-bd93-4645-8c1e-446b8819e477" />

### 2、方法的声明和调用

```c#
using System;
namespace C_test
{
internal class Program
    {
        static void Main(string[] args)
        {
            Calcular.GetCircleArea(10); //静态方法和类绑定
            Calcular calcular = new Calcular(); 
            calcular.GerCircleArea(10); //静态方法不和对象绑定，会报错
}
    }
    class Calcular  
    {
        public static double GetCircleArea(double r) //静态方法和类绑定
        {
            return Math.PI * r * r;
        }
    }
}
```

### 3、构造器

构造器就是C++里面的构造函数，狭义的构造器就是指“实例构造器”，当申明一个对象的时候，会给这个对象分配一个内存空间，然后自动调用构造器给对象的属性赋初始值或指定值。<br>

默认构造器：

```c#
using System;
namespace C_test
{
internal class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student(); //默认构造器在new一个对象的时候起作用，给对象的数据成员赋初始值
}
    }
    class Student  //该类里面没有构造器，将使用自带的默认构造器
    {
        public int Id;   
        public string Name;
    
    }
}
```
带参数的构造器和不带参数的构造器：
```c#
using System;
using System.Security.Cryptography.X509Certificates;
namespace C_test
{
internal class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student();     //将用不带参数的构造器
            Student student2 = new Student(121,"小明");  //将用待参数的构造器
            Console.WriteLine(student1.Id);
            Console.WriteLine(student1.Name);
            Console.WriteLine(student2.Id);
            Console.WriteLine(student2.Name);
}
    }
    class Student 
    {
        public Student()  //创建一个自定义不带参数的构造器，当new一个对象的时候没填信息，将会使用这个默认信息
        {
            this.Id = 1;  //this 就是表示这个函数中所指的变量
            this.Name="Default";
        }
        public Student(int id, string name) //创建一个自定义带参数的构造器，new一个对象的时候填信息就会用这个
        {
            Id = id;
            Name = name;
        }
        public int Id;   
        public string Name;
    
    }
}
```

### 4、方法的重载overload

方法重载说白了就是：方法名称相同，返回值或者形参是不同的，在调用方法的时候会在这些同名方法中一个一个的比较数据类型和形参数量。根据实际情况来调用同名不同操作的方法。

```c#
using System;
using System.Security.Cryptography.X509Certificates;
namespace C_test
{
internal class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            Console.WriteLine(calculator.Add(1, 2));    //调用第一个
            Console.WriteLine(calculator.Add(1D, 2D));  //调用第二个
            Console.WriteLine(calculator.Add(1, 2,3));  //调用第三个
}
    }
class Calculator 
    {
        //在调用Add方法的时候会在这些同名方法中一个一个的比较数据类型和形参数量
        public int Add(int a,int b)   
        {
            return a + b;
        }
        public double Add(double a, double b)
        {
            return a + b;
        }
        public int Add(int a, int b,int c)
        {
            return a + b + c;
        }
      
}
```

### 5、VS里面方法的Debug

如果要调试一段代码时，首先需要设置断点，然后观察方法调用时的call stack，然后利用好Step-in，step-over，step-out来往下调试，需要观察里面的局部变量的变化情况。<br>

断点就是程序运行到这里的时候会停下来的点，方便程序员观察。在VS中的代码左侧按F9就可以设置一个断点。

<img width="1913" height="1017" alt="image" src="https://github.com/user-attachments/assets/64e662d5-2be0-40de-ae50-f0e11c53b366" />

- Step-in方式的debug就是一行一行的执行，按F11可以实现
- Step-over方式的debug就是当前的方法直接跳过，一段一段的执行，这个可以按F10
- Step-out立即执行完当前所在函数的剩余代码，直接跳出到调用这个函数的上一级代码行并暂停。

<img width="765" height="310" alt="image" src="https://github.com/user-attachments/assets/6fb61bba-3d6a-41b4-ba95-26364fd88723" />



# 五、操作符

c#语句=操作符+表达式

### 1、操作符表

<img width="861" height="443" alt="image" src="https://github.com/user-attachments/assets/a5367bb8-100e-45ae-8fb9-3cc10be5e7eb" />

学过C语言的话，以上的运算符应该都很熟悉了。

### 2、自定义操作符举例

```c#
using System;
using System.Security.Cryptography.X509Certificates;
namespace C_test
{
internal class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person();
            Person person2 = new Person();
            person1.Name = "jack";
            person2.Name = "cherry";
            List<Person> nation = Person.Getmary(person1, person2); //建立国家链表的时候jack和cherry进入链表中并生了11个孩子
            foreach(var p in nation) //遍历链表
            {
                Console.WriteLine(p.Name);
            }
        }
    }
class Person
{
        public string Name;
        public static List<Person>Getmary(Person p1, Person p2)
        {
            List<Person> people = new List<Person>();  //建立链表
            people .Add(p1);  //将夫妻两人放入链表中
            people .Add(p2);
            for(int i = 0; i < 11; i++)  
            {
                Person child = new Person();  //夫妻生11个孩子
                child.Name = p1.Name + "&" + p2.Name + "s child";
                people.Add(child);
            }
            return people;
        }
    
 }
```

### 3、优先级与运算数据

<img width="861" height="443" alt="image" src="https://github.com/user-attachments/assets/ce8720eb-f6f1-4fc8-a562-6646042096fd" />

如图，操作符优先级从上往下依次下降，另外还需知道以下特点：

- 可以使用圆括号括起来的方式提升优先级
- 除了带赋值功能的操作符，同优先级的操作符是由左到右的顺序执行运算
- 同优先级的运算没有结合率，例如3+4+5得写成Add(3,Add(4,5));

```c#
int x=100;
x = 3 + 6 + 4; //从左向右算
int y = 100;
x += y;       //带有赋值功能
int z = 100;
x += y += z;  //从右往左，先执行y+z然后+x
```


### 4、基本操作符扫盲

- x.y表示成员访问符
- f(x)表示方法调用符
- a[x]表示访问数组的元素
- x++和x--表示后置的自增和自减
- new表示声明对象
- typeof帮助查看数据类型的基本结构（例如Type t = typeof(int)是获取int类型的各种信息）
- default帮助我们获得默认值（例如int x = default(int)，对应内存区域默认刷成0）
- checked和unchecked表示该数据类型有没有溢出
- delegate表示委托（目前过时了，被lamuba语法取代）
- sizeof返回类型所占用字节大小、箭头->表示访问结构体内指针指向的值。

<img width="1000" height="575" alt="image" src="https://github.com/user-attachments/assets/f518ec06-2b66-4997-b5c3-2701b14e818c" />

<img width="1610" height="725" alt="image" src="https://github.com/user-attachments/assets/7d789484-8a26-4d66-b81b-bb4013dd355b" />

### 5、一元操作符扫盲

- 首先&是取地址操作符
- *表示取值操作符，需要在unsafe{}代码快里面。
- + - 单目就是单纯的给数字加正负号
- ！表示逻辑非、~表示异或，就是将数字表示的二进制值都按位取反
- 前++--表示先自增自建后执行语句、(T)表示强制类型转换操作符、

```c#
int x = -100;  //负的
Console.WriteLine(-x); //负负得正
```

### 6、剩下的操作符扫盲

- 乘法、加法、移位操作符：无需多言，稍微难点的移位符就是将二进制数左移或者右移一位，左移相当于*2，右移相当于/2.
- 关系操作符：所有的关系符的运算结果都是bool类型的，数字之间是比较大小，字符之间是比较ASCII码的大小。
- 逻辑操作符：与或非无需多言，注意短路效应，例如x>y && a++>3的时候，如果x>y不成立的话，后面的就不会再看了，a++不会执行。
- 二进制操作符：按位与、按位或、按位异或这种都是操作二进制数的，例如x&y是两者二进制数相与。
- 条件表达式：这个是唯一一个三位运算符，例如x>y ? A : B ，这个相当于简化版的if else分支，如果x>y则执行A，如果x<=y则执行B
- 赋值和lambda表达式：注意带赋值符号则从右往左，lambda表达式后面再说。



# 六、类型转换

### 1、隐式类型转换

隐式类型转换是编译器自动转换，不损失精度，在不同类型的值计算的时候会进行。隐私类型转换是通过多态来实现的，多态可以理解为子类向父类的隐式类型转换。

<img width="698" height="381" alt="image" src="https://github.com/user-attachments/assets/77d50a25-496d-46e5-b782-151fd52f6ea8" />

```c#
using System;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
namespace C_test
{
internal class Program
    {
        static void Main(string[] args) 
        {
            Teacher teacher = new Teacher();
            Human human = teacher; //将teacher所存储的地址交给human（子类到父类的隐式转换）
            human.Eat();   //子类可以访问父类的方法
            human.Think(); //子类可以访问自己的方法
            human.teach();  //报错，子类不能访问自己子类的方法
        }
    }
class Animal   //动物类
    { 
        public void Eat()
        {
            Console.WriteLine("Eating....");
        }
    
    }
    class Human : Animal  //人类
    {
        public void Think()
        {
            Console.WriteLine("思考....");
        }
    }
    class Teacher : Human  //老师类
    {
       public void Teach()
        {
            Console.WriteLine("教学.....");
        }
    }
}
```

### 2、显式类型转换

程序员自己进行的强制转换，在对象或者变量前面加上括号就行了，使用显式类型转换代表程序员自愿承担精度丢失的后果。

<img width="1106" height="766" alt="image" src="https://github.com/user-attachments/assets/d9552b1b-fcd8-41cb-9926-5a788977703a" />


除了加括号，还有convert和toString和Parse这两种显式转换的方法。


<img width="1603" height="715" alt="image" src="https://github.com/user-attachments/assets/1c3e0c86-b063-45e1-a3f3-9348c567ffe2" />




# 七、表达式语句


<img width="1420" height="808" alt="image" src="https://github.com/user-attachments/assets/4b243767-60e2-40b0-ad02-6cb83e9fab34" />


一些重要的表达式举例如下：<br>

if-else语句：

```c#
// if形式
int score = 85;
if (score >= 60)
{
    Console.WriteLine("及格"); // 条件为true时执行
}
// if-else 形式（二选一）
if (score >= 90)
{
    Console.WriteLine("优秀");
}
else
{
    Console.WriteLine("非优秀"); // 条件为false时执行
}
// if-else if-else 形式（多选一）
if (score >= 90)
{
    Console.WriteLine("优秀");
}
else if (score >= 80)
{
    Console.WriteLine("良好");
}
else if (score >= 60)
{
    Console.WriteLine("及格");
}
else
{
    Console.WriteLine("不及格"); // 所有条件都不满足时执行
}
```

switch语句：
```c#
int day = 3;
switch (day)
{
    case 1:
        Console.WriteLine("周一");
        break; // 跳出switch，避免执行后续case
    case 2:
        Console.WriteLine("周二");
        break;
    case 3:
        Console.WriteLine("周三");
        break;
    default: // 所有case都不匹配时执行
        Console.WriteLine("无效日期");
        break;
}
```


for语句：

```c#
// 输出1-5
for (int i = 1; i <= 5; i++) // 初始化i=1 → 条件i<=5 → 执行循环体 → i++
{
    Console.WriteLine(i);
}
```

foreach迭代器：

```c#
string[] fruits = { "苹果", "香蕉", "橙子" };
// 遍历每一个元素
foreach (string fruit in fruits)
{
    Console.WriteLine(fruit);
}
```

while循环：
```c#
int count = 0;
// 条件为true时循环
while (count < 3)
{
    Console.WriteLine("循环中：" + count);
    count++; // 必须手动修改条件变量，否则死循环
}
```

Do-while循环：
```c#
int num = 5;
do
{
    Console.WriteLine("执行一次：" + num);
    num--;
} while (num > 10); // 条件为false，但循环体已执行1次
```

break和continue语句：
```c#
// break示例：输出1-3后终止
for (int i = 1; i <= 5; i++)
{
    if (i == 4) break;
    Console.WriteLine(i);
}
// continue示例：跳过偶数，只输出奇数
for (int i = 1; i <= 5; i++)
{
    if (i % 2 == 0) continue;
    Console.WriteLine(i);
}
```
异常处理语句try-catch-finally：
```c#
try
{
    // 可能抛出异常的代码（受保护的代码）
    int a = 10;
    int b = 0;
    int result = a / b; // 此处会抛出DivideByZeroException
    Console.WriteLine(result);
}
catch (DivideByZeroException ex) // 捕获指定类型的异常
{
    // 异常处理逻辑（如提示用户、记录日志）
    Console.WriteLine("错误：除数不能为0 → " + ex.Message);
}
catch (Exception ex) // 捕获所有其他异常（父类异常，放最后）
{
    Console.WriteLine("未知错误 → " + ex.Message);
}
finally
{
    // 无论是否发生异常，都会执行的代码（如释放资源：关闭文件、数据库连接）
    Console.WriteLine("finally块执行（资源清理）");
}
```

抛出异常语句throw
```c#
void CheckAge(int age)
{
    if (age < 0)
    {
        // 主动抛出异常
        throw new ArgumentException("年龄不能为负数");
    }
    Console.WriteLine("年龄合法：" + age);
}
// 调用方法（需用try-catch捕获）
try
{
    CheckAge(-5);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
```

# 八、字段和属性

字段其实就是类里面的成员变量，用于直接存储数据。通常定义为 private（私有），避免外部直接访问和修改，防止数据被随意篡改（封装原则）。根据是否用static修饰可以分为：静态只读字段和实例只读字段。

```c#
namespace C_test
{
internal class Program
    {
        static void Main(string[] args) 
        {
            Student student = new Student();
            student.age = 40;    
            student.name = "Test";
Student student2 = new Student();
            student2.age = 80;  
            student.name = "Test";
}
    }
class Student
    {
        public int age;        //实例字段
        public string name="default";  //给实例字段设置默认值
        public static int Amount; //静态字段
        public Student() //实例构造器（给实例字段用的构造器）
        {
            
        }
        static Student() //静态构造器（给静态字段用的，会在数据加载的时候执行而且只会执行一次）
        {
}
    }
}
```

属性是包裹字段的 “访问器”，提供受控的访问方式（读/写），是字段的 “对外接口”，包含 get（读取值）和 set（设置值）方法，可以在读写时添加逻辑（比如数据验证、日志记录）。

```c#
class Student
{
    private int age;        //实例字段
    public int GetAge()    //get属性获取值
    {
        return age;
    }
    public void SetAge(int Value) //set属性设置值
     {
            if(Value>=0 && Value < 120) //在属性内可以添加逻辑
            {
                this.age = Value;
            }
            else
            {
                throw new Exception("年龄输入错误！");
            }
    }
}
```

这样设置的话主函数访问就需要这么写：
```c#
Student student = new Student();
student.SetAge(20);
```

其实还有一个更好的方法，可以使用微软自己设置的set和get函数：
```c#
private int age;        //私有字段
public int Age //公有属性
{
       get { return this.age; }
       set   //set这没有输入的参数因为微软默认value是
       {
                if (value >= 0 && value <= 120) //value是局部关键字，用来接收输入的关键字，放外面就不是关键字了
                {
                    this.age = value;
                }
                else
                {
                    throw new Exception("年龄错误！");
                }
            }
        }
}
```


这样就又可以像之前那样打点访问了：
```c#
Student student = new Student();
student.Age = 20;
```

# 九、索引器

索引器（Indexer）是C#中一种特殊的类成员，允许你像访问数组一样访问对象的内部数据，本质是给类提供 “数组式” 的访问语法。可以把它理解为：给类定制一个 “[] 运算符”，让对象能通过 对象名[索引] 的方式读取/修改内部数据，而不用显式调用方法（比如 GetData(int index)/SetData(int index, value)）。


```c#
using System;
// 自定义一个简单的字符串容器类
class StringContainer
{
    // 内部存储数据的数组
    private string[] _strings = new string[3] { "张三", "李四", "王五" };
    // 定义索引器（核心）
    // 语法：public 返回值类型 this[参数类型 索引名] { get; set; }
    public string this[int index]
    {
        // 读取索引对应的值（get访问器）
        get
        {
            // 简单的边界检查（避免数组越界）
            if (index < 0 || index >= _strings.Length)
            {
                throw new ArgumentOutOfRangeException("index", "索引超出范围");
            }
            return _strings[index];
        }
        // 修改索引对应的值（set访问器）
        set
        {
            if (index < 0 || index >= _strings.Length)
            {
                throw new ArgumentOutOfRangeException("index", "索引超出范围");
            }
            // set访问器中，value是关键字，代表外部传入的赋值内容
            _strings[index] = value;
        }
    }
    // 可选：获取内部数组长度，方便外部遍历
    public int Length => _strings.Length;
}
// 测试索引器
class Program
{
    static void Main()
    {
        // 创建对象
        StringContainer container = new StringContainer();
        // 1. 读取索引器的值（像数组一样用[]）
        Console.WriteLine("读取索引0的值：" + container[0]); // 输出：张三
        Console.WriteLine("读取索引1的值：" + container[1]); // 输出：李四
        // 2. 修改索引器的值
        container[1] = "赵六";
        Console.WriteLine("修改后索引1的值：" + container[1]); // 输出：赵六
        // 3. 遍历（结合Length）
        Console.WriteLine("\n遍历所有元素：");
        for (int i = 0; i < container.Length; i++)
        {
            Console.WriteLine(container[i]);
        }
    }
}
```


# 十、参数

### 1、传值参数—值参数

场景 1：值参数 + 值类型（最基础） <br>
值类型变量：存储的是实际数据（比如int num = 10，变量直接存 10）。<br>
值参数传递：复制一份实际数据给方法，方法内修改副本，原变量完全不受影响。<br>

<img width="801" height="515" alt="image" src="https://github.com/user-attachments/assets/879f8198-52c6-41f2-acaf-5c195d12b0d8" />

<img width="786" height="429" alt="image" src="https://github.com/user-attachments/assets/f8b0c13f-afc0-46ba-bbcc-bdb38dce044c" />


场景 2：值参数 + 引用类型<br>
引用类型变量：存储的是 “对象在堆上的内存地址”（比如Person p = new Person()，变量 p 存的是对象的地址，不是对象本身）。<br>
值参数传递：复制的是 “地址的副本”—— 方法内通过副本地址修改对象的属性，会影响原对象；但修改 “副本地址本身”（比如重新 new），不会影响原变量的地址。<br>


<img width="1191" height="665" alt="image" src="https://github.com/user-attachments/assets/66d7eb95-5282-4233-84e6-f993641bbc75" />


<img width="779" height="715" alt="image" src="https://github.com/user-attachments/assets/0fe0f57b-b68f-4c67-84b2-f74caa1322ab" />


### 2、传地址—引用参数

场景 3：ref 参数 + 值类型 <br>
ref 传递：直接传递值类型变量的 “内存地址”，方法内修改的是原变量的实际数据，会同步影响原变量。<br>

<img width="1187" height="660" alt="image" src="https://github.com/user-attachments/assets/57ea3604-154e-4085-9d68-ea8bc9b5e800" />

<img width="783" height="425" alt="image" src="https://github.com/user-attachments/assets/6b5ffbca-0f83-4fd2-af02-28140a8d666d" />


场景 4：ref 参数 + 引用类型
ref 传递：直接传递引用类型变量的 “地址本身”（不是地址副本）—— 方法内无论是修改对象属性，还是重新 new 对象（修改地址），都会影响原变量。

<img width="1189" height="662" alt="image" src="https://github.com/user-attachments/assets/450ad2b8-d4a1-480b-bcfe-2cf004363242" />

<img width="779" height="678" alt="image" src="https://github.com/user-attachments/assets/de8c2741-7b69-4894-9944-540388a6e89b" />


### 3、输出参数

需要显式加 out 修饰符，专门用于方法返回多个值（弥补 C# 方法只能返回一个值的限制）。<br>
原理：和 ref 类似（传递地址），但要求更严格：<br>
实参可以不初始化（方法内必须给 out 参数赋值）；<br>
方法内部必须为 out 参数赋值，否则编译报错。<br>
场景：比如一个方法既要返回计算结果，又要返回是否成功。<br>

<img width="922" height="604" alt="image" src="https://github.com/user-attachments/assets/d622f26b-7a56-453d-964c-6e22cf409ad8" />


<img width="865" height="558" alt="image" src="https://github.com/user-attachments/assets/dd743773-047a-42fe-9b4f-408296fcdc67" />


<img width="804" height="567" alt="image" src="https://github.com/user-attachments/assets/31cc68dd-396b-4bc2-bf4d-9a97b9b583a1" />

<img width="777" height="488" alt="image" src="https://github.com/user-attachments/assets/eb15f032-6438-4d37-8a55-928764e34415" />


### 4. 参数数组（params 修饰符）

需要显式加 params 修饰符，允许方法接收数量可变的同类型参数。<br>
原理：编译器会自动把可变参数转换成数组，params 必须是方法的最后一个参数。<br>

<img width="722" height="215" alt="image" src="https://github.com/user-attachments/assets/47644145-78d6-49d1-8d45-4b133276dfdc" />


```c#
string str = "Tim;Tom;Amy;Lisa";
string[] strings = str.Split(';', ';',';');  //str.Split表示分离
foreach (var name in strings) {
    Console.WriteLine(name);
}
```

<img width="782" height="613" alt="image" src="https://github.com/user-attachments/assets/e03ff560-0dfa-4798-a608-c9900d73279b" />


### 5、具名参数

<img width="561" height="257" alt="image" src="https://github.com/user-attachments/assets/ddc81031-79f4-4336-a701-49cae78fae7c" />


### 6、可选参数

<img width="538" height="258" alt="image" src="https://github.com/user-attachments/assets/db38a889-4b3d-4303-b366-054895f2191f" />


### 7、扩展方法this参数

<img width="880" height="251" alt="image" src="https://github.com/user-attachments/assets/f590f36b-278e-4e04-bbcf-bd9664a0ca2a" />


### 8、使用场景总结

<img width="713" height="330" alt="image" src="https://github.com/user-attachments/assets/8f11c1fe-23db-45da-b36a-d1436795214b" />


# 十一、委托

委托可以比喻成一个装方法的箱子，箱子里装了很多个方法，当需要一批方法的时候，就调用这个委托就行了。


<img width="921" height="474" alt="image" src="https://github.com/user-attachments/assets/54849589-7c70-40e5-a9bc-d78fc4ddd043" />

### 1、C#自带委托

简单的委托使用方式包括Action和Func，方法的调用方式也包括直接调用（方法名）和间接调用（指针方式）。C#封装好了Action和Function两种委托，最多支持16个参数的模版，其中Action不带返回值而Function带返回值（返回值就是最后一个参数的类型），写法如下：


<img width="769" height="242" alt="image" src="https://github.com/user-attachments/assets/8774458e-5a5b-42c3-8550-da531d1db923" />


<img width="870" height="290" alt="image" src="https://github.com/user-attachments/assets/f8fa6434-ff46-472a-a399-eaac9c1b8e05" />


```C#
using System;
class Program
{
    static void Main()
    {
        Calculator calculator = new Calculator();
        Action action = new Action(calculator.report);
        calculator.report();               //直接调用calculator.report()方法
        action.Invoke();                   //使用委托间接调用calculator.report()方法
        action();                          //简化版的间接调用calculator.report()方法
        Func<int, int, int> func = new Func<int, int, int>(calculator.Add);  //使用Func委托间接调用calculator.Add()方法
        Func<int, int, int> func1 = new Func<int, int, int>(calculator.Sub); //使用Func委托间接调用calculator.Sub()方法
    }
}
class Calculator
{
    public void report()
    {
        Console.WriteLine("I have 3 methods");
    }
    public int Add(int a, int b)
    {
        int result = a + b;
        return result;
    }
    public int Sub(int a, int b)
    {
        return a - b;
    }
}
```

### 2、自定义委托

以上的Action和Func委托都是C#自带的委托，以下是自定义委托的声明：

<img width="942" height="534" alt="image" src="https://github.com/user-attachments/assets/12c1c57f-14cd-4db1-b970-2ba0915208e3" />


由此可见，声明的委托的返回值类型和参数类型必须和方法的返回值类型和参数类型相一致，参数名不用一致也行。

```c#
using System;

class Program
{
    static void Main()
    {
        Calculator calculator = new Calculator();  //使用委托来封装方法
        Calc calc1 = new Calc(calculator.Add);
        Calc calc2 = new Calc(calculator.Sub);
        Calc calc3 = new Calc(calculator.Multiply);
        Calc calc4 = new Calc(calculator.Divide);
        double a = 100;
        double b = 200;
        double c = 0;
        c = calc1.Invoke(a, b);  //使用委托间接调用
        Console.WriteLine(c);
        c = calc2.Invoke(a, b);
        Console.WriteLine(c);
        c = calc3(a, b);         
        Console.WriteLine(c);
        c = calc4(a, b);
        Console.WriteLine(c);
    }
}
public delegate double Calc(double x, double y); //委托是类，因此与类同级
class Calculator
{
    public double Add(double x, double y)
    {
        return x + y;
    }
    public double Sub(double x, double y)
    {
        return x - y;
    }
    public double Multiply(double x, double y)
    {
        return x * y;
    }
    public double Divide(double x, double y)
    {
        return x / y;
    }
}
```

### 3、多播委托


多播委托就是一个委托变量绑定多个方法，在调用委托的时候会见绑定的多个方法依次执行。


```c#
using System;
//定义一个无返回值的委托（多播委托常用无返回值）
public delegate void NotifyDelegate();
class Program
{
    static void Main()
    {
        //分别定义几个要执行的方法
        void SendEmail() => Console.WriteLine("发送邮件通知");
        void SendSMS() => Console.WriteLine("发送短信通知");
        void ShowPopup() => Console.WriteLine("弹出窗口提示");
        //多播委托用+=把多个方法绑定到一起
        NotifyDelegate notify = null; // 先初始化为空
        notify += SendEmail;   // 绑定第一个方法
        notify += SendSMS;     // 绑定第二个方法
        notify += ShowPopup;   // 绑定第三个方法
                               // 4. 调用一次委托，三个方法按顺序执行
        Console.WriteLine("=== 触发多播委托 ===");
        notify();
        //可以用-=解绑某个方法
        notify -= SendSMS;
        Console.WriteLine("\n=== 解绑短信后再次触发 ===");
        notify(); // 只执行邮件和弹窗
    }
}
```


<img width="543" height="271" alt="image" src="https://github.com/user-attachments/assets/985f13c0-277f-4dd7-bea0-60a27a1bdaa0" />


### 4、隐式异步调用委托

使用此方法可以让委托在后台干活，不会阻塞主线程。同步就相当于一个先后顺序（你做完我再做），异步相当于同时并行做。

```c#
using System;
using System.Threading;
//定义一个有返回值的委托
public delegate int CalcDelegate(int a, int b);
class Program
{
    static void Main()
    {
        //定义一个耗时的计算方法
        int SlowAdd(int a, int b)
        {
            Console.WriteLine("[$] 开始慢计算...（模拟耗时3秒）");
            Thread.Sleep(3000); // 模拟3秒耗时
            Console.WriteLine("[$] 慢计算完成！");
            return a + b;
        }
        CalcDelegate calc = SlowAdd;
        //同步调用（会卡住主线程）
        Console.WriteLine("=== 同步调用 ===");
        int syncResult = calc(1, 2);
        Console.WriteLine("同步结果：" + syncResult);
        Console.WriteLine("同步调用结束，主线程被卡住了\n");
        //隐式异步调用（BeginInvoke，不卡住主线程）
        Console.WriteLine("=== 异步调用 ===");
        var asyncResult = calc.BeginInvoke(3, 4, null, null); // 后台开始算
        Console.WriteLine("[*] 主线程继续做别的事...");
        //模拟主线程在干活
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"[*] 主线程在执行第 {i + 1} 步...");
            Thread.Sleep(1000);
        }
        //等待异步完成并获取结果
        int asyncResultValue = calc.EndInvoke(asyncResult);
        Console.WriteLine("异步结果：" + asyncResultValue);
        Console.WriteLine("异步调用结束");
    }
}
```

# 十二、事件

前面的委托可以比喻成一个装方法的箱子，箱子里装了很多个方法，当需要一批方法的时候，就调用这个委托就行了。事件Event相当于对委托进行权限控制后的东西。

<img width="918" height="466" alt="image" src="https://github.com/user-attachments/assets/e51a5fe4-59bb-4ab0-87a9-d900429d5cc8" />



委托存在一种风险就是它可以被直接赋值，就会导致其容器内的函数存在丢失的风险，假设我们delegate1容器内存在着数个函数而delegate2没有，用等号赋值完成后委托1的注册函数就会被清空（delegate1的地址指向了delegate2的地址）。


<img width="876" height="440" alt="image" src="https://github.com/user-attachments/assets/43b3ffdb-4121-4695-9fc0-19026de14ec7" />


而事件就解决了这种问题，事件的本质就是特殊的委托，赋值的权限设置成了Private，比委托更加安全。写法上就是多加了个event关键词。

<img width="809" height="275" alt="image" src="https://github.com/user-attachments/assets/48ea3b41-048c-490e-be2d-706c6a2336ec" />


# 十三、类

### 1、构造器和析构器

和C++的构造函数和析构函数是同一个东西，就是在类实例化的时候执行的函数以及类的对象释放内存的时候执行的函数。

<img width="1262" height="702" alt="image" src="https://github.com/user-attachments/assets/0e3db6dc-75b7-4c0d-9727-246177f3ad60" />

### 2、类的继承

类的继承就是子类在父类的基础上扩展功能，以及多个子类可以以一个父类为基础进行多样化扩展。一般来说基类和派生类是一对，父类和子类是一对。

<img width="1094" height="591" alt="image" src="https://github.com/user-attachments/assets/881c9275-7235-4de7-bb86-9d18f706b49d" />

```c#
using System;
class Program
{
    static void Main()
    {
        Type t = typeof(Car);  //t表示Car类
        Type tb = t.BaseType;  //Car类的基类是Vehicle
        Console.WriteLine(tb.FullName); //输出的是Vehicle
        Type tc = tb.BaseType;  //输出的是System.Object类
        Console.WriteLine(Car is Object); //输出的是true
        Object o1 = new Vehicle();   //父类类型变量可以引用子类类型的实例（多态）
        Object o2 = new Car();
    }
}
sealed class Test  //sealed修饰的类不能再派生类（断子绝孙）
{
}
class Vehicle : Object //Object类是所有继承类的源头
{
}
class Car : Vehicle  //Car类从Vehicle类派生而来，Car继承了Vehicle类
{

}
class Toy : Vehicle //一个子类只能有一个父类，一个父类可以有多个子类（孩子不能有多个爸，爸爸可以有多个孩子）
{
}
```

<img width="914" height="510" alt="image" src="https://github.com/user-attachments/assets/30dbff52-20b6-48f8-849f-e5da1813df74" />


子类对父类的继承是全盘继承的，在派生与继承的过程中进行的是扩展，类成员只可能是越来越多。不要贸然引入新的类成员，不然后期很难去掉了。横向扩展是对类成员个数的扩充，纵向成员是对类版本的更新或者重写。<br>

派生类对继承成员的访问，父类的成员访问修饰符决定了子类能不能访问：<br>

public：任何地方都能访问 ✔️<br>
protected：子类和同一个包内可以访问 ✔️<br>
private：子类完全不能访问 ❌<br>


```c#
using System;
public class Animal
{
    public string Name { get; set; } 
    protected int Age { get; set; }
    private string id { }
}

public class Dog : Animal
{
    public void ShowInfo()
    {
        Console.WriteLine(Name);   //public修饰的可以继承过来
        Console.WriteLine(Age);    //protected修饰的可以继承过来
        Console.WriteLine(id);     //private修饰的不能继承！（报错）
    }
}
```

构造器不可继承，父类的构造方法，子类不能直接继承，必须通过base()显式调用。

```c#
using System;
public class Animal
{
    //构造器
   public Animal(string name)
    {
        Name = name;
    }
    public string Name;
}

public class Cat : Animal
{
    //构造器不能被继承，必须调用父类的构造器，否则编译报错
    public Cat(string name) : base(name)
    {
        Name = name;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Cat cat = new Cat("小猫");  //会先调用父类Animal构造器再执行Cat构造器
        Console.WriteLine(cat.Name);  
    }
}
```

### 3、类的重写

类的继承相当于类的横向扩展，类成员会越来越多。而类的重写就是类的纵向扩展，表现在类的方法的行为改变，版本升高。<br>
类继承的例子：父类是「手机」，子类「智能手机」在手机基础上，新增了「拍照」「上网」功能。特点是父类功能不变，子类多了新功能

```c#
// 父类：手机
public class Phone
{
    public void Call() { Console.WriteLine("打电话"); }
}
// 子类：智能手机（横向扩展）
public class SmartPhone : Phone
{
    // 新增成员：拍照
    public void TakePhoto() { Console.WriteLine("拍照"); }
    // 新增成员：上网
    public void SurfInternet() { Console.WriteLine("上网"); }
}
```

类重写的例子：父类「手机」的「打电话」是按键拨号，子类「智能手机」把「打电话」改成了触屏拨号。子类修改父类已有成员的行为，用virtual+override实现。


```c#
// 父类：手机
public class Phone
{
    // 标记为virtual，允许子类重写
    public virtual void Call() { Console.WriteLine("按键拨号打电话"); }
}
// 子类：智能手机（纵向扩展/重写）
public class SmartPhone : Phone
{
    // 重写父类方法，改变行为
    public override void Call() { Console.WriteLine("触屏拨号打电话"); }
}
```

与此同时还有类的隐藏这一概念，子类用 new 关键字，把父类的同名成员 “藏起来”，不修改父类逻辑。父类「手机」有「Call」，子类「老人机」也定义了一个「Call」，只在老人机视角下生效。

```c#
public class Phone
{
    public void Call() { Console.WriteLine("通用打电话"); }
}
public class OldPhone : Phone
{
    // 隐藏父类的Call方法
    public new void Call() { Console.WriteLine("老人机大按键打电话"); }
}
```

特点：和重写不同，隐藏不遵循多态，只有用子类类型引用时才会调用子类版本。<br>

重写与隐藏的发生条件，必须同时满足 3 个条件：
- 是函数成员（方法、属性、事件等，不是字段）
- 子类对父类成员可见（不是 private）
- 方法签名一致（方法名 + 参数类型 + 数量完全相同）

### 4、类的多态

多态（Polymorphism） = 同一个行为，不同对象表现出不同形态。
当父类引用指向子类对象时，调用重写的方法，执行的是子类的实现，而非父类的实现。<br>
多态的 3 个必要条件（缺一不可）:

- 继承：必须有父类和子类的继承关系；
- 重写：子类必须用 override 重写父类的 virtual/abstract 方法；
- 父类引用指向子类对象：Parent p = new Child();

```c#
Phone p1 = new Phone();
Phone p2 = new SmartPhone(); // 父类引用指向子类对象
p1.Call(); // 输出：按键拨号打电话（看Phone对象）
p2.Call(); // 输出：触屏拨号打电话（看SmartPhone对象）
```

在实际代码中，抽象类实现多态更为常用：

```c#
using System;
using System.Numerics;
// 抽象父类：无默认实现，强制子类重写
public abstract class Animal
{
    public abstract void MakeSound(); // 抽象方法，必须被重写
}
public class Dog : Animal
{
    public override void MakeSound() => Console.WriteLine("汪汪汪");
}
public class Cat : Animal
{
    public override void MakeSound() => Console.WriteLine("喵喵喵");
}
// 测试
Animal a1 = new Dog();
Animal a2 = new Cat();
a1.MakeSound(); // 汪汪汪
a2.MakeSound(); // 喵喵喵
```

### 5、抽象类

抽象类是至少含有一个抽象方法的类，类需要用abstract修饰，与抽象类对应的就是具体类（平常用的就是）。注意abstract不能是private的，因为抽象方法要通过子类继承后实现；抽象方法不能带方法体；抽像类不能实例化；抽象方法在C++中叫纯虚函数。

```c#
abstract class Student()  //抽象类（里面至少一个抽象方法，也可以包含不抽象的方法）
{
    abstract public void Study() ;  //抽象方法（必须在抽象类里）
}
```

我们应该封装稳定不变、确定的成员，哪些不确定的有可能改变的成员就用抽象来修饰，后面的子类来实现。
下面的代码表示的就是经典的示例，不建议这么搞：

```c#
//错误示例
class Car
{
    public void Run()
    {
        Console.WriteLine("Car is Running");
    }
    public void Stop()
    {
        Console.WriteLine("Car is Stopped");
    }
}
class Truck
{
    public void Run()
    {
        Console.WriteLine("Truck is Running"); //不建议Truck和Car一样的方法名
    }
    public void Stop()
    {
        Console.WriteLine("Turck is Stopped");
    }
}
```

以上示例Truck和Car一样的方法名，当代码量上去后就会难以维护。此时可以新定义一个Vehicle类作为Car和Truck的父类，子类继承父类后改写对应的方法实现。

```c#
using System;
namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Vehicle car = new Car();
            car.Run();
            car.Stop();
            Vehicle truck = new Truck();
            truck.Run();
            truck.Stop(); //没有重写Stop将输出Vehicle的Stop方法
        }
    }
    //修改后
    abstract class Vehicle
    {
        abstract public void Run();  //抽象方法要用override重写
        public virtual void Stop()  //虚拟方法也嘚用override重写
        {
            Console.WriteLine("Stopped");
        }

    }
    class Car : Vehicle //继承了抽象类，就必须实现抽象方法，否则自己得是抽象方法
    {
        public override void Run()  //必须用override来实现抽象方法
        {
            Console.WriteLine("Car is Running");
        }
        public override void Stop()  //重写父类方法
        {
            Console.WriteLine("Car is stopped");
        }
    }
    class Truck : Vehicle//继承了抽象类，就必须实现抽象方法，否则自己得是抽象方法
    {
        public override void Run()//必须用override来实现抽象方法
        {
            Console.WriteLine("Truck is Running");
        }
        //没有重写Stop将输出Vehicle的Stop方法
    }
}
```

当然上面代码中Vehicle上面可以再订一个纯的抽象方法：


```c#
abstract class VehicleBase  //纯抽象类
{
    abstract public void Stop();
    abstract public void Run();
}
```

这种纯抽象类其实就是接口interface了

```c#
interface VehicleBase  //接口
{
    abstract public void Stop();
    abstract public void Run();
}
```

### 6、接口

接口是一个特殊的类，接口里面的方法全部都是抽象类。接口的引入可以有效的降低程序的耦合度。耦合度高的代码最大问题是 “牵一发而动全身”，而接口通过 “面向接口编程”，遵循了 “开闭原则”（对扩展开放、对修改关闭），这也是设计模式的核心思想。比如做一个支付功能，最初只支持微信支付，如果直接依赖微信支付的具体类，后续要加支付宝支付，代码改动会很大，耦合度极高；但用接口封装后就能彻底的解耦。
如以下接口实现的支付功能：

```C#
using System;
//定义支付接口：只规定“要做支付”这个行为，不关心具体怎么支付
public interface IPayment
{
    // 接口只定义方法签名，无实现
    bool Pay(decimal amount);
}
//微信支付实现类：实现接口的具体逻辑
public class WeChatPayment : IPayment
{
    public bool Pay(decimal amount)
    {
        Console.WriteLine($"微信支付{amount}元，扣减微信余额");
        return true;
    }
}
//支付宝支付实现类：新增实现类，完全不改动原有代码
public class AliPay : IPayment
{
    public bool Pay(decimal amount)
    {
        Console.WriteLine($"支付宝支付{amount}元，扣减支付宝余额");
        return true;
    }
}
//调用方（订单服务）：只依赖接口，不依赖具体支付类
public class OrderService
{
    // 接收接口类型参数，而非具体类
    public void ProcessPayment(IPayment payment, decimal amount)
    {
        // 调用方只关心“支付”这个行为，不关心是微信还是支付宝
        payment.Pay(amount);
    }
}
// 测试代码
class Program
{
    static void Main(string[] args)
    {
        OrderService orderService = new OrderService();
        // 用微信支付
        orderService.ProcessPayment(new WeChatPayment(), 100);
        // 改用支付宝支付：只改实例化的类，OrderService的代码一行不用动
        orderService.ProcessPayment(new AliPay(), 200);
        // 后续加银联支付：只新增一个实现IPayment的UnionPay类即可，完全不改动调用方
    }
}
```

没有接口时：如果 OrderService 直接依赖 WeChatPayment 类，要加支付宝支付，就得修改 OrderService 的代码（比如加 if-else 判断），调用方和实现方强耦合；<br>
有接口时：OrderService 只依赖 IPayment 接口，新增任何支付方式（银联、银行卡），只需要新增一个实现 IPayment 的类，调用方代码完全不用改 —— 这就是接口把 “调用逻辑” 和 “具体实现” 拆分开，降低了耦合度。<br>
在开发的时候我们应该尽可能的“松耦合”。当然也得遵循“接口隔离原则”：<br>

接口隔离原则：不让类实现不需要的接口方法，比如做打印机系统，要是把打印、扫描、复印都塞到一个接口里，普通打印机就得被迫实现扫描/复印方法（要么空实现要么抛异常），既臃肿又容易出错。

```c#
// 臃肿的接口：包含打印机、扫描仪、复印机的所有功能
public interface IAllInOneMachine
{
    // 打印机功能
    void Print(string content);
    // 扫描仪功能
    void Scan(string filePath);
    // 复印机功能
    void Copy(string content);
}
// 普通打印机：只需要打印功能，但被迫实现所有接口方法
public class SimplePrinter : IAllInOneMachine
{
    public void Print(string content)
    {
        Console.WriteLine($"打印：{content}");
    }
// 被迫实现不需要的方法，只能空实现/抛异常
    public void Scan(string filePath)
    {
        throw new NotSupportedException("普通打印机不支持扫描");
    }
public void Copy(string content)
    {
        throw new NotSupportedException("普通打印机不支持复印");
    }
}
// 测试：调用不需要的方法会报错，代码臃肿且易出问题
class Program
{
    static void Main(string[] args)
    {
        IAllInOneMachine printer = new SimplePrinter();
        printer.Print("简历"); // 正常
        printer.Scan("test.jpg"); // 报错：不支持扫描
    }
}
```

但把接口拆成 IPrintable、IScannable、ICopyable 三个专用接口，普通打印机只实现 IPrintable，一体机实现全部，这样每个类只依赖自己需要的接口，代码更简洁、易维护，也从编译层面避免了调用不存在的功能。

```c#
// 拆分后的专用接口：每个接口只包含单一功能
using System;

public interface IPrintable
{
    void Print(string content); // 仅打印功能
}
public interface IScannable
{
    void Scan(string filePath); // 仅扫描功能
}
public interface ICopyable
{
    void Copy(string content); // 仅复印功能
}
// 普通打印机：只实现打印接口，无需关心其他功能
public class SimplePrinter : IPrintable
{
    public void Print(string content)
    {
        Console.WriteLine($"打印：{content}");
    }
}
// 多功能一体机：实现所有接口（因为它确实需要这些功能）
public class AllInOnePrinter : IPrintable, IScannable, ICopyable
{
    public void Print(string content)
    {
        Console.WriteLine($"打印：{content}");
    }
    public void Scan(string filePath)
    {
        Console.WriteLine($"扫描文件到：{filePath}");
    }
    public void Copy(string content)
    {
        Console.WriteLine($"复印：{content}");
    }
}
// 测试：客户端只调用自己需要的接口方法，无冗余
class Program
{
    static void Main(string[] args)
    {
        // 普通打印机：只暴露打印功能
        IPrintable simplePrinter = new SimplePrinter();
        simplePrinter.Print("简历"); // 正常
                                   // 编译报错：IPrintable接口没有Scan方法，从根源避免误用
                                   // simplePrinter.Scan("test.jpg");
                                   // 一体机：可调用所有功能
        AllInOnePrinter allInOne = new AllInOnePrinter();
        allInOne.Print("合同");
        allInOne.Scan("doc.pdf");
        allInOne.Copy("身份证");
    }
}
```

但是不能玩的太过了，如果弄出一大堆单一接口，类接口的颗粒度就太小了，需要注意平衡。

### 7、反射和依赖注入
































