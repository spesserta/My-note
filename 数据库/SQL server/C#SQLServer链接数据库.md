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








































