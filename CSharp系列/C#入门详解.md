
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






















