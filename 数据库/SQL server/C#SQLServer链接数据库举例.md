# 一、数据的存储

>数据的本地存储一般是存放在硬盘上的，可以通过存放在txt文档、excel文件等地方来实现。<br>
>将数据存放在单独的文件，此时写好的C#程序、Winform、WPF程序都可以独立访问到。<br>
>SQLsever可以充当一个可执行文件和数据本地存放位置的“中介”，只需要使用SQL语句，即可以实现数据的存储。
>使用SQLsever可以提升安全性（需要登陆）、也支持对外的接口、也有更高的性能优化（优化文件IO）、定时服务（例如定时扫描等）、提供视图等优点。

<img width="460" height="275" alt="image" src="https://github.com/user-attachments/assets/04636452-b9ea-46c9-802c-f46c4b350c60" />


# 二、SQLSever的安装

>一般正常的公司都自带SQL数据库，只需要给程序员提供对应的网络接口就可以了，不用自己下载，本次下载是为了学习用。
>安装教程如下：

https://www.bilibili.com/video/BV1gzFQzmECU/?spm_id_from=333.337.search-card.all.click&vd_source=946b49660240cf7b9f1a826fe53670be

>打开SQL Sever，里面的身份验证主要包括windows身份验证和SQL Sever身份验证，前者本机使用足够了，后者是支持远程服务器连接的那种。

<img width="608" height="738" alt="image" src="https://github.com/user-attachments/assets/19c1434f-de99-4fa9-b830-b5f277408d77" />


>现在我们新建一个属于自己的数据库：

<img width="427" height="375" alt="image" src="https://github.com/user-attachments/assets/a86b42ec-395b-40ec-9fa8-2d891c9e7763" />

>建立好后可以发现对应的文件夹里多出了两个文件（mdf数据库文件和ldf数据库操作日志文件）

<img width="784" height="83" alt="image" src="https://github.com/user-attachments/assets/b517c34f-62b2-4746-979d-76fdddd3c046" />


>现在新建查询，输入指令新建一个test表

```sql
use awa
create table test
(
	UserName nvarchar(100)  not null,  
	PassWord nvarchar(100)  not null,
	NickName nvarchar(100)  not null,
	ID nvarchar(100) not null
)

```


>ok啊现在test1表里有4列了，现在给这4列分别插入信息

```sql
use awa
insert into test(UserName, PassWord , NickName , ID)
values('admin','admin','管理员','001')
.....
```


>在选择前1000行的时候就能看到这个表了，在编辑前200行那个选项里也可以设置表的一些属性

<img width="1226" height="618" alt="image" src="https://github.com/user-attachments/assets/c28a53d0-f9d7-4107-a426-9914d167968a" />

<img width="1029" height="523" alt="image" src="https://github.com/user-attachments/assets/5194ad2d-459e-4def-980c-d0038042714d" />

>本质上这个和Excel表是一样的

<img width="599" height="204" alt="image" src="https://github.com/user-attachments/assets/19f9add4-4d6e-4f94-81e5-b79b4a4ce397" />


>如果我们在编辑前200行里面修改数值，鼠标移动到本行其他地方可以看到一个红色感叹号，这是因为可视化软件这边更新了但是数据库的数据没更新，如果点击其他行，那么就解决了。

<img width="1097" height="512" alt="image" src="https://github.com/user-attachments/assets/3500078b-7132-4a25-b8a4-b4232a00d20a" />

>在点击的过程中是将表格里变更过的数据转化为对应的SQL语句然后修改数据库，当然可以在新建查询这里直接输入SQL语句进行修改


<img width="857" height="497" alt="image" src="https://github.com/user-attachments/assets/c2f6bcc8-af44-41eb-828a-4a3595882024" />



# 三、SQL Sever常用语句

>在本文件夹里叫SQL Sever的md格式文件里面。以下是最精炼的语句：<br>

>增加
```sql
insert into 表名(列1, 列2, 列3) VALUES (值1, 值2, 值3);

insert into test(UserName, PassWord, NickName) 
values ('张三', '123456', '小三');
```

>删除
```sql
delete from 表名 WHERE 条件;

delete from test WHERE UserName='张三';
```

>修改
```sql
update 表名 set 列1=新值, 列2=新值 WHERE 条件;

update test set PassWord='654321' WHERE UserName='张三';

```


>查找
```sql
-- 查询所有列
select * from 表名;

-- 查询指定列
select 列名1, 列名2 from 表名;

-- 带条件查询
select * from 表名 WHERE 条件;

select * from test where UserName='张三';
```





# 四、数据库的连接

>首先新建一个.NET framework的项目（注意.NET core框架的需要自己引入jar包）<br>

>现在将C#连接上SQLSERVER并且将对应的数据表输出

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 引入SQL Server数据库操作核心命名空间
using System.Data.SqlClient;
// 引入数据集合（DataSet/DataTable）相关命名空间
using System.Data;

namespace SQLtest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. 创建SqlConnection对象，用于建立与SQL Server的连接
            SqlConnection conn = new SqlConnection();
            
            // 2. 设置数据库连接字符串（核心参数说明）
            // Server=(local)\SQLEXPRESS ：指定要连接的SQL Server实例名称
            // Database=awa            ：指定要操作的目标数据库名称
            // Trusted_Connection=true ：使用Windows身份验证（无需输入账号密码）
            // @符号：避免反斜杠\被转义，保证连接字符串格式正确
            conn.ConnectionString = @"Server=(local)\SQLEXPRESS;Database=awa;Trusted_Connection=true;";
            
            // 3. 打开数据库连接（真正建立程序与数据库的通信链路）
            conn.Open();

            // 4. 创建SqlCommand对象，用于执行SQL语句或存储过程
            SqlCommand cmd = new SqlCommand();
            
            // 5. 将SqlCommand关联到已打开的数据库连接
            cmd.Connection = conn;
            
            // 6. 设置要执行的SQL查询语句：查询test表中的所有列数据
            cmd.CommandText = "select * from test";

            // 7. 创建SqlDataAdapter对象（数据适配器）
            // 作用：作为DataSet和SQL Server之间的桥梁，负责读取数据
            SqlDataAdapter adapter = new SqlDataAdapter();
            
            // 8. 将SqlCommand赋值给适配器的SelectCommand属性
            // 表示适配器要执行这个查询命令来获取数据
            adapter.SelectCommand = cmd;
            
            // 9. 创建DataSet对象（内存中的临时数据库）
            // 作用：离线存储从数据库读取的表数据，无需保持数据库连接
            DataSet ds = new DataSet();
            
            // 10. 执行查询并填充DataSet：适配器自动执行SQL语句，将结果存入ds
            adapter.Fill(ds);

            // 11. 关闭数据库连接（释放数据库资源，避免连接泄露）
            conn.Close();

            // 12. 从DataSet中获取第一个数据表（因为只执行了一个查询，所以索引为0）
            // 这个表就是test表的查询结果集
            DataTable table =  ds.Tables[0];

            // 13. 遍历DataTable中的每一行数据，展示查询结果
            // table.Rows.Count：获取数据表的总行数
            for (int i = 0; i < table.Rows.Count; i++) 
            {
                // 14. 读取当前行（第i行）的指定列数据并格式化输出
                // table.Rows[i]["列名"]：通过列名获取当前行对应列的值
                // UserName/PassWord/NickName：test表中的列名
                Console.WriteLine($"{table.Rows[i]["UserName"]} {table.Rows[i]["PassWord"]} {table.Rows[i]["NickName"]}");
            }
        }
    }
}
```

<img width="425" height="181" alt="image" src="https://github.com/user-attachments/assets/60d1dd97-6d33-458c-bcb8-50c6b395e200" />

>调用的过程很麻烦但是过程很固定，所以需要牢记一下<br>


>接下来是C#对数据库进行增删改的固定语句

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 引入SQL Server数据库操作核心命名空间
using System.Data.SqlClient;
// 引入数据集合（DataSet/DataTable）相关命名空间
using System.Data;

namespace SQLtest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. 创建SqlConnection对象，用于建立与SQL Server的连接
            SqlConnection conn = new SqlConnection();

            // 2. 设置数据库连接字符串（核心参数说明）
            // Server=(local)\SQLEXPRESS ：指定要连接的SQL Server实例名称
            // Database=awa            ：指定要操作的目标数据库名称
            // Trusted_Connection=true ：使用Windows身份验证（无需输入账号密码）
            // @符号：避免反斜杠\被转义，保证连接字符串格式正确
            conn.ConnectionString = @"Server=(local)\SQLEXPRESS;Database=awa;Trusted_Connection=true;";
            // 打开数据库连接
            conn.Open();
            //设置需要新增的参数
            string UserName = "新增Name";
            string PassWord = "新增pwd";
            string NickName = "新增nickname";
            string ID = "新增id";
            //设置SQL语句
            string insertSQL = $"insert into test values('{UserName}','{PassWord}','{NickName}','{ID}')";
            //插进去，第一个参数 是写好的SQL语句，第二个是conn
            SqlCommand cmd = new SqlCommand(insertSQL, conn); 
            //执行SQL语句并返回受影响的行数
            int count = cmd.ExecuteNonQuery(); 
            //用完关闭数据库是好习惯
            conn.Close();
            Console.WriteLine($"执行新增语句，受影响的行数是：{count}");
        }
    }
}
```



# 五、SQL的封装

>C#连接数据库的操作非常重复固定，适合封装成一个单独的方法

```sql
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 引入SQL Server数据库操作核心命名空间
using System.Data.SqlClient;
// 引入数据集合（DataSet/DataTable）相关命名空间
using System.Data;

namespace SQLtest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //设置SQL语句(增加)
            string UserName = "111";
            string PassWord = "111";
            string NickName = "111";
            string ID = "111";
            string insertSQL = $"insert into test values('{UserName}','{PassWord}','{NickName}','{ID}')";
            int count1 = EditData(insertSQL);
            if (count1 <= 0)
            {
                Console.WriteLine("新增数据失败！");
            }
            else
            {
                Console.WriteLine($"新增数据成功！新增了{count1}行");
            }



            //设置SQL语句（删除）
                string deleteSQL = "delete from test where UserName = '111'";
            int count2 = EditData(deleteSQL);
            if (count2 <= 0)
            {
                Console.WriteLine("删除数据失败！");
            }
            else
            {
                Console.WriteLine($"删除数据成功！删除了{count2}行");
            }


            //设置SQL语句（修改）
            string editSQL = "update test set UserName = '2111', PassWord='9855' where UserName = '211'";
            int count3 = EditData(editSQL);
            if (count3 <= 0)
            {
                Console.WriteLine("修改数据失败！");
            }
            else
            {
                Console.WriteLine($"修改数据成功！修改了{count3}行");
            }


            //设置SQL语句（查找）
            string searchSQL = "select * from test";
            DataTable dt = SelectData(searchSQL);
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine($"{dt.Rows[i]["UserName"]}  {dt.Rows[i]["PassWord"]}  {dt.Rows[i]["NickName"]}  {dt.Rows[i]["ID"]}");
            }
            

        }
        public static int EditData(string sql)  //外部传入sql语句（增删改）
        {
            //连接数据库
            SqlConnection conn = new SqlConnection();
            //创建连接对象，指定数据库位置
            conn.ConnectionString = @"Server = (local)\SQLEXPRESS ; Database=awa ; Trusted_Connection=true";
            conn.Open();
            //执行SQL语句
            SqlCommand cmd = new SqlCommand(sql, conn);
            int count = cmd.ExecuteNonQuery();
            //关闭数据库返回影响行数
            conn.Close();
            return count;

        }
        public static DataTable SelectData(string sql) //外部传入sql语句（查询）返回值是表格类型
        {
            //连接数据库
            SqlConnection conn = new SqlConnection();
            //创建连接对象，指定数据库位置
            conn.ConnectionString = @"Server = (local)\SQLEXPRESS ; Database=awa ; Trusted_Connection=true";
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            conn.Close();

            DataTable table = ds.Tables[0];
            return table;

        }
    }
}
```

>根据封装好的两个方法，可以尝试在做一个登陆验证的系统

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 引入SQL Server数据库操作核心命名空间
using System.Data.SqlClient;
// 引入数据集合（DataSet/DataTable）相关命名空间
using System.Data;

namespace SQLtest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //打印系统基本信息
            Console.WriteLine("欢迎访问**系统");
            Console.WriteLine("请在下方输入你的用户名和密码");
            
            Console.Write("请输入用户名：");
            string inputUserName = Console.ReadLine();
            Console.Write("请输入密码：");
            string inputPassWord = Console.ReadLine();

            //将用户的输入和数据库中的数据进行比对
            string sql = "select * from test";
            DataTable dt = SelectData(sql);

            bool isTrue = false;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string UserName = dt.Rows[i]["UserName"].ToString();
                string Password = dt.Rows[i]["Password"].ToString();
                if (inputUserName == UserName && inputPassWord == Password)
                {
                    Console.WriteLine("输入正确！");
                    isTrue= true;
                    break;
                }
            }
            if (!isTrue)
            {
                Console.WriteLine("输入错误！");
                Console.ReadKey();
                return ;
            }
            Console.ReadKey();


        }
        public static int EditData(string sql)  //外部传入sql语句（增删改）
        {
            //连接数据库
            SqlConnection conn = new SqlConnection();
            //创建连接对象，指定数据库位置
            conn.ConnectionString = @"Server = (local)\SQLEXPRESS ; Database=awa ; Trusted_Connection=true";
            conn.Open();
            //执行SQL语句
            SqlCommand cmd = new SqlCommand(sql, conn);
            int count = cmd.ExecuteNonQuery();
            //关闭数据库返回影响行数
            conn.Close();
            return count;

        }
        public static DataTable SelectData(string sql) //外部传入sql语句（查询）返回值是表格类型
        {
            //连接数据库
            SqlConnection conn = new SqlConnection();
            //创建连接对象，指定数据库位置
            conn.ConnectionString = @"Server = (local)\SQLEXPRESS ; Database=awa ; Trusted_Connection=true";
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            conn.Close();

            DataTable table = ds.Tables[0];
            return table;

        }
    }
}
```

>上面的代码逻辑是select *将所有数据传过来然后和用户输入的数据一一匹配（如果数据库过大读取过来就占很大开销），还有一种逻辑就是直接将输入的数据拼成SQL语句查询有没有搜到，搜到了一行就表明输入正确。

```sql
static void Main(string[] args)
{
    //打印系统基本信息
    Console.WriteLine("欢迎访问**系统");
    Console.WriteLine("请在下方输入你的用户名和密码");
    
    Console.Write("请输入用户名：");
    string inputUserName = Console.ReadLine();
    Console.Write("请输入密码：");
    string inputPassWord = Console.ReadLine();

    //将用户的输入和数据库中的数据进行比对
    string sql = $"select * from test where UserName = '{inputUserName}' and PassWord = '{inputPassWord}'";
    DataTable dt = SelectData(sql);
    if(dt.Rows.Count > 0 )
    {
        Console.WriteLine("用户名和密码正确！");
    }
    else
    {
        Console.WriteLine("用户名和密码不正确！");
    }
   
    Console.ReadKey();

}
```
>注意了where这里必须有个参数是主键，否则查出多个相同的数据那就冲突了<br>
>登录完后假设还需要显示出所有用户的用户名和昵称，并且输入密码失败后需要重新输入（用死循环），代码修改如下：

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 引入SQL Server数据库操作核心命名空间
using System.Data.SqlClient;
// 引入数据集合（DataSet/DataTable）相关命名空间
using System.Data;

namespace SQLtest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //打印系统基本信息
            Console.WriteLine("欢迎访问**系统");
            Console.WriteLine("请在下方输入你的用户名和密码");

            string inputUserName;
            string inputPassWord;
            string sql;
            DataTable dt;
            while (true)
            {
                Console.Write("请输入用户名：");
                inputUserName = Console.ReadLine();
                Console.Write("请输入密码：");
                inputPassWord = Console.ReadLine();
                //将用户的输入和数据库中的数据进行比对
                sql = $"select * from test where UserName = '{inputUserName}' and PassWord = '{inputPassWord}'";
                dt = SelectData(sql);
                if (dt.Rows.Count > 0)
                {
                    Console.WriteLine("用户名和密码正确！");
                    break;
                }
                else
                {
                    Console.WriteLine("用户名和密码不正确！");
                }
            }
            Console.WriteLine($"欢迎回来，{dt.Rows[0]["UserName"]}先生！");

            string sqlAllusers = "select * from test";
            DataTable tableAllUsers = SelectData(sqlAllusers);
            Console.WriteLine("用户名  昵称");
            for(int i = 0; i < tableAllUsers.Rows.Count; i++)
            {
                Console.WriteLine($"{tableAllUsers.Rows[i]["UserName"]}  {tableAllUsers.Rows[i]["NickName"]}");
            }



        }
        public static int EditData(string sql)  //外部传入sql语句（增删改）
        {
            //连接数据库
            SqlConnection conn = new SqlConnection();
            //创建连接对象，指定数据库位置
            conn.ConnectionString = @"Server = (local)\SQLEXPRESS ; Database=awa ; Trusted_Connection=true";
            conn.Open();
            //执行SQL语句
            SqlCommand cmd = new SqlCommand(sql, conn);
            int count = cmd.ExecuteNonQuery();
            //关闭数据库返回影响行数
            conn.Close();
            return count;

        }
        public static DataTable SelectData(string sql) //外部传入sql语句（查询）返回值是表格类型
        {
            //连接数据库
            SqlConnection conn = new SqlConnection();
            //创建连接对象，指定数据库位置
            conn.ConnectionString = @"Server = (local)\SQLEXPRESS ; Database=awa ; Trusted_Connection=true";
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            conn.Close();

            DataTable table = ds.Tables[0];
            return table;

        }
    }
}
```

>现在需求是增加一列性别，就需要在SQL SERVER中手动增加一列，并且在用户登陆完成的时候检测性别是否为空，空的话就提醒录入性别。

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 引入SQL Server数据库操作核心命名空间
using System.Data.SqlClient;
// 引入数据集合（DataSet/DataTable）相关命名空间
using System.Data;

namespace SQLtest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //打印系统基本信息
            Console.WriteLine("欢迎访问**系统");
            Console.WriteLine("请在下方输入你的用户名和密码");

            string inputUserName;
            string inputPassWord;
            string sql;
            DataTable dt;
            while (true)
            {
                Console.Write("请输入用户名：");
                inputUserName = Console.ReadLine();
                Console.Write("请输入密码：");
                inputPassWord = Console.ReadLine();
                //将用户的输入和数据库中的数据进行比对
                sql = $"select * from test where UserName = '{inputUserName}' and PassWord = '{inputPassWord}'";
                dt = SelectData(sql);
                if (dt.Rows.Count > 0)
                {
                    Console.WriteLine("用户名和密码正确！");
                    break;
                }
                else
                {
                    Console.WriteLine("用户名和密码不正确！");
                }
            }
            Console.WriteLine($"欢迎回来，{dt.Rows[0]["UserName"]}先生！");

            //判断用户的性别是否更新
            string gender = dt.Rows[0]["性别"].ToString();
            if(string.IsNullOrEmpty(gender))
            {
                Console.WriteLine($"尊敬的{dt.Rows[0]["NickName"]},您的性别是空，需要您输入性别：");
                Console.WriteLine("请在下方输入：");
                Console.WriteLine("性别选项：1男   2女");
                string inputGender = Console.ReadLine();
                if (inputGender == "1") gender = "男";
                else if (inputGender == "2") gender = "女";
                else Console.WriteLine("你傻逼吧，让你输入1和2");
            }

            //将输入的性别更新到数据库（修改操作）
            string UpdateGenderSQL = $"update test set 性别= '{gender}' where UserName = '{dt.Rows[0]["UserName"]}'";
            int ii = EditData(UpdateGenderSQL);
            if (ii <= 0)
            {
                Console.WriteLine("更新性别失败！本次不收集了下次再收集");
            }
            else
            {
                Console.WriteLine("更新性别成功！");
            }

            //显示所有用户的信息
            string sqlAllusers = "select * from test";
            DataTable tableAllUsers = SelectData(sqlAllusers);
            Console.WriteLine("用户名  昵称");
            for(int i = 0; i < tableAllUsers.Rows.Count; i++)
            {
                Console.WriteLine($"{tableAllUsers.Rows[i]["UserName"]}  {tableAllUsers.Rows[i]["性别"]}");
            }



        }
        public static int EditData(string sql)  //外部传入sql语句（增删改）
        {
            //连接数据库
            SqlConnection conn = new SqlConnection();
            //创建连接对象，指定数据库位置
            conn.ConnectionString = @"Server = (local)\SQLEXPRESS ; Database=awa ; Trusted_Connection=true";
            conn.Open();
            //执行SQL语句
            SqlCommand cmd = new SqlCommand(sql, conn);
            int count = cmd.ExecuteNonQuery();
            //关闭数据库返回影响行数
            conn.Close();
            return count;

        }
        public static DataTable SelectData(string sql) //外部传入sql语句（查询）返回值是表格类型
        {
            //连接数据库
            SqlConnection conn = new SqlConnection();
            //创建连接对象，指定数据库位置
            conn.ConnectionString = @"Server = (local)\SQLEXPRESS ; Database=awa ; Trusted_Connection=true";
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            conn.Close();

            DataTable table = ds.Tables[0];
            return table;

        }
    }
}
```

>在实际开发中，数据库里面的密码需要加密，不然一被入侵就明文了，可以使用MD5加密运算，在输入密码的时候会对其进行一个散列值的计算从而解密



>还有一种多语言的情形，可以通过选择中英文+if判断实现

```c#
//最开始的时候进行语言选择
Console.WriteLine("1：中文  2：英文");
string inputLang=Console.ReadLine();
int lang = 1;
if(inputLang=="2")  lang = 2;   //输入是2的时候就切换成英文模式
```
```c#
//显示所有用户的信息
            string sqlAllusers = "select * from test";
            DataTable tableAllUsers = SelectData(sqlAllusers);
            Console.WriteLine("用户名 昵称 性别");
            for (int i = 0; i < tableAllUsers.Rows.Count; i++)
            {
                string gender = tableAllUsers.Rows[i]["性别"].ToString();
                if (lang == 2)
                {
                    if (gender == "男") 
                        gender = "Male";
                    else if (gender == "女") 
                        gender = "Female";
                }
                Console.WriteLine($"{tableAllUsers.Rows[i]["UserName"]}  {tableAllUsers.Rows[i]["NickName"]}  {gender}");
            }
```

>当然也可以在数据库中将“男女”用数字1和数字2表示，然后读取过来再判断语言即可，修改一下输入性别的逻辑就行

```c#
 //判断用户的性别是否更新
 string genderForSQL = dt.Rows[0]["性别"].ToString();
 if (string.IsNullOrEmpty(genderForSQL))
 {
     Console.WriteLine($"尊敬的{dt.Rows[0]["NickName"]},您的性别是空，需要您输入性别：");
     Console.WriteLine("请在下方输入：");
     Console.WriteLine("性别选项：1男   2女");
     string inputGender = Console.ReadLine();
     if (inputGender == "1") genderForSQL = "1";
     else if (inputGender == "2") genderForSQL = "2";
     else Console.WriteLine("你傻逼吧，让你输入1和2");
 }
```

<img width="477" height="171" alt="image" src="https://github.com/user-attachments/assets/712c041c-ff77-447d-9670-cf1a34353061" />

```c#
            //显示所有用户的信息
            string sqlAllusers = "select * from test";
            DataTable tableAllUsers = SelectData(sqlAllusers);
            Console.WriteLine("用户名 昵称 性别");
            for (int i = 0; i < tableAllUsers.Rows.Count; i++)
            {
                string gender1 = tableAllUsers.Rows[i]["性别"].ToString().Trim(); //注意要Trim一下删掉多余空格
                if (lang == 1)
                {
                    if (gender1 == "1") 
                        gender1 = "男";
                    else if (gender1 == "2") 
                        gender1 = "女";
                }
                if (lang == 2)
                {
                    if (gender1 == "1")
                        gender1 = "Male";
                    else if (gender1 == "2")
                        gender1 = "Female";
                }
                Console.WriteLine($"{tableAllUsers.Rows[i]["UserName"]}  {tableAllUsers.Rows[i]["NickName"]}  {gender1}");
            }
```


>但是以上方法在后期新增语言的时候就麻烦了，如果改成配置文件的形式，就可以解决这个问题

<img width="876" height="515" alt="image" src="https://github.com/user-attachments/assets/9d845b8e-083c-4133-893c-d8a14ccec1fe" />


```c#
infoHelper infoM = new infoHelper();
//最开始的时候进行语言选择
Console.WriteLine("1：中文  2：英文");
string inputLang=Console.ReadLine();
int lang = 1;
if(inputLang=="2")  lang = 2;   //输入是2的时候就切换成英文模式

if (lang == 1)
{
    infoM.Gender1 = "男";
    infoM.Gender2 = "女";
}
else if (lang == 2)  
{
    infoM.Gender1 = "Male";
    infoM.Gender2 = "Female";
}
```

```c#
//显示所有用户的信息
string sqlAllusers = "select * from test";
DataTable tableAllUsers = SelectData(sqlAllusers);
Console.WriteLine("用户名 昵称 性别");
for (int i = 0; i < tableAllUsers.Rows.Count; i++)
{
    string gender1 = tableAllUsers.Rows[i]["性别"].ToString().Trim(); //注意要Trim一下删掉多余空格
    if (gender1 == "1")
    {
        gender1 = infoM.Gender1;
    }
    else if (gender1 == "2")
    {
        gender1 = infoM.Gender2;
    }

    Console.WriteLine($"{tableAllUsers.Rows[i]["UserName"]}  {tableAllUsers.Rows[i]["NickName"]}  {gender1}");
}
```
这样的话如果要新增语言，那么直接改上面的if lang == ?的这个逻辑就行 

<img width="920" height="516" alt="image" src="https://github.com/user-attachments/assets/d3f04fce-c244-4012-988a-fde44eb894ba" />

```c#
////文件1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLtest
{
    public class infoHelper  //用来支持多语言的类（注意要public）
    {
        public static string Gender1 {  get; set; }  //男
        public static string Gender2 { get;  set; }  //女

        public static string Infor1 {  get; set; }  //语句1
        public static string Infor2 {  get; set; }  //语句2
        public static string Infor3 { get; set; }   //语句3

    }
}
////文件2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 引入SQL Server数据库操作核心命名空间
using System.Data.SqlClient;
// 引入数据集合（DataSet/DataTable）相关命名空间
using System.Data;
using System.Runtime.InteropServices;

namespace SQLtest
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //最开始的时候进行语言选择
            Console.WriteLine("1：中文  2：英文");
            string inputLang=Console.ReadLine();
            int lang = 1;
            if(inputLang=="2")  lang = 2;   //输入是2的时候就切换成英文模式

            if (lang == 1)  //简体中文
            {
                infoHelper.Gender1 = "男";
                infoHelper.Gender2 = "女";
                infoHelper.Infor1 = "欢迎访问**系统";
            }
            else if (lang == 2)  //英文
            {
                infoHelper.Gender1 = "Male";
                infoHelper.Gender2 = "Female";
                infoHelper.Infor1 = "Welcome to ** system!";
            }else if (lang == 3)  //繁体中文
            {
                infoHelper.Gender1 = "男（繁体）";
                infoHelper.Gender2 = "女（繁体）";
                infoHelper.Infor1 = "欢迎访问**系统（繁体）";
            }
 

            //打印系统基本信息
            Console.WriteLine(infoHelper.Infor1);   //将写死的中文可以用变量换掉
            Console.WriteLine("请在下方输入你的用户名和密码");

            string inputUserName;
            string inputPassWord;
            string sql;
            DataTable dt;
            while (true)
            {
                Console.Write("请输入用户名：");
                inputUserName = Console.ReadLine();
                Console.Write("请输入密码：");
                inputPassWord = Console.ReadLine();
                //将用户的输入和数据库中的数据进行比对
                sql = $"select * from test where UserName = '{inputUserName}' and PassWord = '{inputPassWord}'";
                dt = SelectData(sql);
                if (dt.Rows.Count > 0)
                {
                    Console.WriteLine("用户名和密码正确！");
                    break;
                }
                else
                {
                    Console.WriteLine("用户名和密码不正确！");
                }
            }
            Console.WriteLine($"欢迎回来，{dt.Rows[0]["UserName"]}先生！");

            //判断用户的性别是否更新
            string genderForSQL = dt.Rows[0]["性别"].ToString();
            if (string.IsNullOrEmpty(genderForSQL))
            {
                Console.WriteLine($"尊敬的{dt.Rows[0]["NickName"]},您的性别是空，需要您输入性别：");
                Console.WriteLine("请在下方输入：");
                Console.WriteLine("性别选项：1男   2女");
                string inputGender = Console.ReadLine();
                if (inputGender == "1") genderForSQL = "1";
                else if (inputGender == "2") genderForSQL = "2";
                else Console.WriteLine("你傻逼吧，让你输入1和2");
            }

            //将输入的性别更新到数据库（修改操作）
            string UpdateGenderSQL = $"update test set 性别= '{genderForSQL}' where UserName = '{dt.Rows[0]["UserName"]}'";
            int ii = EditData(UpdateGenderSQL);
            if (ii <= 0)
            {
                Console.WriteLine("更新性别失败！本次不收集了下次再收集");
            }
            else
            {
                Console.WriteLine("更新性别成功！");
            }

            //显示所有用户的信息
            string sqlAllusers = "select * from test";
            DataTable tableAllUsers = SelectData(sqlAllusers);
            Console.WriteLine("用户名 昵称 性别");
            for (int i = 0; i < tableAllUsers.Rows.Count; i++)
            {
                string gender1 = tableAllUsers.Rows[i]["性别"].ToString().Trim(); //注意要Trim一下删掉多余空格
                if (gender1 == "1")
                {
                    gender1 = infoHelper.Gender1;
                }
                else if (gender1 == "2")
                {
                    gender1 = infoHelper.Gender2;
                }

                Console.WriteLine($"{tableAllUsers.Rows[i]["UserName"]}  {tableAllUsers.Rows[i]["NickName"]}  {gender1}");
            }



        }
        public static int EditData(string sql)  //外部传入sql语句（增删改）
        {
            //连接数据库
            SqlConnection conn = new SqlConnection();
            //创建连接对象，指定数据库位置
            conn.ConnectionString = @"Server = (local)\SQLEXPRESS ; Database=awa ; Trusted_Connection=true";
            conn.Open();
            //执行SQL语句
            SqlCommand cmd = new SqlCommand(sql, conn);
            int count = cmd.ExecuteNonQuery();
            //关闭数据库返回影响行数
            conn.Close();
            return count;

        }
        public static DataTable SelectData(string sql) //外部传入sql语句（查询）返回值是表格类型
        {
            //连接数据库
            SqlConnection conn = new SqlConnection();
            //创建连接对象，指定数据库位置
            conn.ConnectionString = @"Server = (local)\SQLEXPRESS ; Database=awa ; Trusted_Connection=true";
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            conn.Close();

            DataTable table = ds.Tables[0];
            return table;

        }
    }
}
```

>综上所述，可以将里面的语句用Infor变量全部替换掉 awa


<img width="1513" height="883" alt="image" src="https://github.com/user-attachments/assets/58d6c914-64fb-49c1-a045-452d0ac2aa21" />


>现在又有新需求了，需要加一个成绩，但是呢一个人的成绩它也不是固定不变的，需要再加上对应的时间，先建表

<img width="726" height="466" alt="image" src="https://github.com/user-attachments/assets/cfb21c36-db0c-444a-84cb-508bbac62e8b" />


>建表之后呢在对应的C#语句中添加一个对应的UserScoreTable类，类中存放调用数据库时调出来的数据，再添加一个UserScoreOperation类来存放对数据库的操作方法

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLtest
{
    internal class UserScoreTable
    {
        public string UserName { get; set; }
        public float Chinese { get; set; }   //为了方便修改成绩，使用float类型
        public float Maths { get; set; }
        public float English { get; set; }
        public DateTime Datetime { get; set; }

    }
}
```
```c#
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 引入SQL Server数据库操作核心命名空间
using System.Data.SqlClient;
// 引入数据集合（DataSet/DataTable）相关命名空间
using System.Data;

namespace SQLtest
{
    public class UserScoreOperation
    {
        public List<UserScoreTable> GetUserScoreByUserName(string userName)  //通过用户名来查询成绩
        {
            string sqlUserScores = $"select * from score where UserName = '{userName}'";
            DataTable UserSores = SQLHelp.SelectData(sqlUserScores);

            //转换成集合对象
            List<UserScoreTable> users = new List<UserScoreTable>();
            for (int i = 0; i < UserSores.Rows.Count; i++)
            {
                UserScoreTable model = UserScoreOperation.DataRowToUserScoreModel(UserSores.Rows[i]);
                users.Add(model);
            }
            return users;
        }

        internal static UserScoreTable DataRowToUserScoreModel(DataRow currentRow)
        {
            UserScoreTable model = new UserScoreTable();
            model.UserName = currentRow["UserName"].ToString();
            model.Chinese = float.Parse(currentRow["语文成绩"].ToString());
            model.Maths = float.Parse(currentRow["数学成绩"].ToString());
            model.English = float.Parse(currentRow["英语成绩"].ToString());
            model.Datetime = DateTime.Parse(currentRow["时间"].ToString());
            return model;

        }
    }

}

```

































