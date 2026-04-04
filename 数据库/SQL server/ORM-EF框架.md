### 一、基本概念

>ORM表示对象关系映射，EF表示EntityFramework实体框架，是ADO.NET的面向对象封装。<br>
>其中ORM的对象关系映射中，数据库映射为其中的C#，数据表（一张表）映射为其中的类，记录（一行的信息）映射为其中的对象，字段（某行某列的信息）映射为其中的属性。EF框架就是典型的ORM框架。<br>
>举个例子，现在要准备一条记录，直接在C#中new一个对象就行了，然后直接对象打点来修改，无需写SQL语句。
```
XX xx = new XX();
db.Insert(xx);
db.Delete(xx);
```

现在数据库里有一张数据表如下：

<img width="397" height="164" alt="image" src="https://github.com/user-attachments/assets/5396e425-3c12-4a70-a73a-f0ca8785472f" />

此时我们需要再VS里面新建一个ADO.NET实体数据模型


<img width="1178" height="811" alt="image" src="https://github.com/user-attachments/assets/0e10c211-f65e-4f46-b3da-169b9103de11" />


<img width="785" height="746" alt="image" src="https://github.com/user-attachments/assets/38ab1f36-d646-4007-a40c-aae040bb8260" />


选中对应的数据表点完成

<img width="775" height="751" alt="image" src="https://github.com/user-attachments/assets/a2f10cd9-6f69-4012-bc7d-c4f0aebcf63d" />

此时VS里面就出现了对应的demx文件了！

<img width="1909" height="862" alt="image" src="https://github.com/user-attachments/assets/03777ec3-e092-42b1-9d74-59e6da600f3d" />


好的，现在就可以利用对象的方式来操作员工表了（注意可能要先安装 EF6 NuGet 包）

<img width="689" height="392" alt="image" src="https://github.com/user-attachments/assets/2177e6f7-0c9d-4b1c-b687-00b69c990acd" />


### 二、添加数据举例
```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            awaEntities db = new awaEntities();  //数据库名称叫awa
            //创建数据表中的一行（拿来插入的）
            test t = new test() { ID = 10, UserName = "010", PassWord = "123456", NickName = "笑笑", Sex = 0 };
            db.test.Add(t);  //将这一行t存入到数据集合中（还没放到数据库）
            db.SaveChanges(); //保存到SQL SERVER数据库
            Console.WriteLine("添加完毕");

        }
    }
}
```

<img width="382" height="192" alt="image" src="https://github.com/user-attachments/assets/36981bc3-dcb6-409d-a5a8-4e79f3ac20a4" />

可以发现成功添加



### 三、删除数据举例

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ORM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            awaEntities db = new awaEntities();  //数据库名称叫awa
            //使用FirstOrDefault来查找第一个满足条件的行，括号里的查找条件用lambda表达式
            test tDel =  db.test.FirstOrDefault(test => test.ID == 10);  //找ID=10的那行数据，返回值是test类型或者NULL类型
            if(tDel != null) //找到满足条件的了
            {
                db.test.Remove(tDel);
                Console.WriteLine("删除完毕");
            }
            else    //没找到就返回NULL
            {
                Console.WriteLine("查无此人");
            }
            db.SaveChanges(); //保存到数据库
            
        }
    }
}
```

### 四、修改数据举例

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ORM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            awaEntities db = new awaEntities();  //数据库名称叫awa
            test tUpdate = db.test.FirstOrDefault(test => test.ID == 1); //找到ID等于1的那行
            if(tUpdate != null)
            {
                tUpdate.UserName = "001";  //直接通过改对象属性的方式来修改数据表
                Console.WriteLine("修改成功");
            }
            else
            {
                Console.WriteLine("查无此人");
            }
            db.SaveChanges();
        }
    }
}
```

### 五、查询数据举例

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ORM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            awaEntities db = new awaEntities();  //数据库名称叫awa
            //基础查询-查询NickName = 小明的行
            List<test> tSelect =  db.test.Where(test => test.NickName == "小明").ToList();  //找到NickName=小明的行并且转为List类型
            foreach (test test in tSelect) 
            {
                Console.WriteLine($"{test.ID}  {test.UserName}  {test.NickName}  {test.Sex}");
            }
            //查询表中一共有多少人
            //SQL语句 select count(*) from test
            int num1 = db.test.Count();
            Console.WriteLine($"总人数：{num1}人");
            //查询男员工的人数
            //SQL语句 select count(*) from test where Sex = 1
            int num2 = db.test.Count(test => test.Sex == 1);
            Console.WriteLine($"男生有：{num2}人");
        }
    }
}

```

<img width="1023" height="465" alt="image" src="https://github.com/user-attachments/assets/80419f1c-3c81-4b86-bdb4-0aa5d7609f4e" />



### 六、实战案例

>好的我们现在需要做一个类似于淘宝商场的控制台应用，包括用户注册，商品信息修改、购物车管理三大模块<br>
>第一步我们在SQL SERVER中建数据库建数据表

```sql
--用户信息表
create table t_user
(
   account varchar(50) primary key , --账号
   [password] varchar(50) ,          --密码
   name varchar(50)                  --姓名
)
go
-- 插入两个注册用户
insert t_user values('001','123456','张三'),('002','123456','李四')

--商品信息表
create table t_goods
(
	gid int identity(1,1) primary key , --商品编号
	gname varchar(50),                  --商品名称
	[type] varchar(50),                 --商品类型
	price int,                          --价格
	sale  int                           --销量
)
insert t_goods values  --插入商品信息
('巧克力' , '零食' , 10 , 0),
('纯净水' , '饮料' ,  2 , 0)

create table t_shopCar
(
	id int identity(1,1) primary key ,
	account varchar(50) , --用户名称
	gid int ,             --价格
	gname varchar(50),    --商品名称
	price int,            --价格
	amount int ,          --购买数量
	[money] int           --小计金额
)
```

>接下来就需要分层了，先根据数据表信息简历Model层

<img width="434" height="213" alt="image" src="https://github.com/user-attachments/assets/72a03445-897c-426d-8687-d76d209ba298" />

```c#
namespace Model
{
    public class goods
    {
        public int GId { get; set; }
        public string GName { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public int Sale { get; set; }
    }
}
```
```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class shopCar
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public int GId { get; set; }
        public string GName { get; set; }
        public int Price { get; set; }
        public int Money { get; set; }
    }
}
```
```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class userInfo
    {
            public string account {  get; set; }
            public string password { get; set; }
            public string name { get; set; }
    }
}
```

>然后建立DBHelp类放在common层

<img width="407" height="149" alt="image" src="https://github.com/user-attachments/assets/2f20c5d0-b2d7-4c45-9183-c8c084921ad1" />


>然后是DAL层






































