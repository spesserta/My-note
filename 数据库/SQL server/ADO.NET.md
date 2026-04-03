### 一、初识ADO.NET

>ADO.NET是作为.NET平台优先采用的数据访问接口，它提供给了平台互用性和可伸缩的数据访问，可以让C#操作数据库

<img width="813" height="447" alt="image" src="https://github.com/user-attachments/assets/caae0fa9-7527-4482-94a5-6c9506e4b66e" />


### 二、使用SqlConnection对象创建**连接对象**

```c#
SqlConnection con = new SqlConnection("链接字符串"); 
```

>上面代码的链接字符串表示的是要链接的数据库的信息，可以在VS里的解决方案管理器里新建项，选择ADO.NET实体数据模型

<img width="1173" height="815" alt="image" src="https://github.com/user-attachments/assets/3742a57d-8634-4476-a86d-3fe46dae9b6b" />

<img width="782" height="751" alt="image" src="https://github.com/user-attachments/assets/312d1caa-e6b0-4c57-ad73-67d3adabe38d" />

<img width="777" height="751" alt="image" src="https://github.com/user-attachments/assets/3173b541-7139-4e48-b181-106415226b94" />

<img width="551" height="726" alt="image" src="https://github.com/user-attachments/assets/409d8c57-4d00-40ee-9fc6-23c54be5bf66" />

>选择好数据库后记得点击测试链接一下，如果测试没问题就可以得到一个链接字符串了，在高级按钮里面的就是了

<img width="705" height="899" alt="image" src="https://github.com/user-attachments/assets/12780abb-c48a-4c10-b914-6988b24819d7" />

<img width="522" height="608" alt="image" src="https://github.com/user-attachments/assets/65c9298b-aed1-4bd6-93b9-99c773c44c38" />

```c#
//创建一个Connection对象（类似于获取电话号码）
SqlConnection conn = new SqlConnection("Data Source=DESKTOP-VL906FO\\SQLEXPRESS;Initial Catalog=awa;Integrated Security=True;Trust Server Certificate=True");
//打开数据库连接（类似于拨号）
conn.Open();
//测试数据库连接状态
Console.WriteLine(conn.State);  //输出Open
```

### 三、使用SqlCommand执行sql命令

SqlCommand命令的执行分为3部分，第一就是设置好SQL语句，第二就是创建一个SqlCommand对象，第三就是执行SQL语句

```c#
//设置SQL语句
string sql = string.Format("SQL语句");
//创建一个SqlCommand对象
SqlCommand cmd = new SqlCommand(sql , conn);

//执行SQL语句（增删改）
cmd.ExecuteNonQuery();

//执行SQL语句（查），cmd.Execute会返回数据，需要用SqlDataReader类的对象来接收
SqlDataReader reader = cmd.ExecuteReader();
//reader.Read()读数据表里面的一行，再次Read就是读第二行，建议用循环语句
while(reader.Read())
{
    //reader.GetValue(0)表示读取该行的第一列那个数据，reader.GetValue(1)表示第二列，以此类推
    Console.WriteLine($"{reader.GetValue(0)}--{reader.GetValue(1)}");
}
```

### 四、参数化SQL命令

```c#
using System;
using System.Data;    //DataSet数据集
using System.Data.SqlClient;  //操作数据库的四个类

namespace SQLtest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-VL906FO\\SQLEXPRESS;Initial Catalog=awa;Integrated Security=True;Trust Server Certificate=True");
            conn.Open();

            Console.Write("输入用户名：");
            string UserName = Console.ReadLine();
            Console.Write("输入密码：");
            string pwd = Console.ReadLine();

            string sql = string.Format($"select * from test where UserName = '{UserName}' and PassWord = {pwd}");
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                Console.WriteLine("登陆成功！");
            }
            else
            {
                Console.WriteLine("登陆失败！");
            }

        }
    }
}
```
>如上图的登陆系统，遇到大佬的话容易被SQL注入攻击，就是在输入账号的时候直接输入SQL语句使得数据库误判从而登陆成功

<img width="482" height="269" alt="image" src="https://github.com/user-attachments/assets/637d64d7-e66c-4f2f-8efd-bf89bb298bc4" />


>此时可以通过参数化的SQL命令来解决


```c#
using System;
using System.Data;    //DataSet数据集
using System.Data.SqlClient;  //操作数据库的四个类

namespace SQLtest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-VL906FO\\SQLEXPRESS;Initial Catalog=awa;Integrated Security=True;Trust Server Certificate=True");
            conn.Open();

            Console.Write("输入用户名：");
            string UserName = Console.ReadLine();
            Console.Write("输入密码：");
            string pwd = Console.ReadLine();

            //Sql命令参数化，放弃拼接的方式
            string sql = "select * from test where UserName = @UserName and PassWord = @pwd";
           
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UserName",UserName); //往命令对象的参数集合中添加了一个参数对象代表账号
            cmd.Parameters.AddWithValue("@pwd",pwd);           //往命令对象的参数集合中添加了一个参数对象代表密码

            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                Console.WriteLine("登陆成功！");
            }
            else
            {
                Console.WriteLine("登陆失败！");
            }

        }
    }
}
```

>当然增删改查都能使用参数化SQL命令的方式

<img width="615" height="307" alt="image" src="https://github.com/user-attachments/assets/0f2ac6b2-6037-40dc-b379-bbdd1fd2c1bc" />





