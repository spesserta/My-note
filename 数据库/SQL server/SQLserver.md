# 一、创建数据库
>一般写法
```sql
--两个减号表示注释
--创建数据库
create database db_test --数据库名为db_test
on --数据文件
(
	name='db_test',  --数据文件的逻辑名称
	filename='D:\DATA\db_test.mdf', --数据文件的物理路径和名称
	size=5MB, --数据文件的初始大小
    maxsize=100MB, --数据文件最大不超过100M
	filegrowth=2MB --数据文件的增长大小，可以写大小也可以写百分比，当初始大小满了后就按照这个速度来增长
)
log on --日志文件
(
	name='db_test_log', --日志文件的逻辑名称
	filename='D:\DATA\db_test.ldf',--日志文件的物理路径和名称
	size=5MB, --日志文件的文件的初始大小
    maxsize=100MB, --日志文件最大不超过100M
	filegrowth=2MB --日志文件的增长大小
)
```
>简写：文件存储、初始大小等全部采用默认值
```sql
create database db_test1
```
# 二、删除数据库
```sql
drop database 数据库名称
```

# 三、创建数据表
>建表必须建在数据库中
```sql
--切换数据库到db_test，即在该数据库里面建表
use db_test 
--创建表的基本语法
create table 表名
(
  --字段1 数据类型,
  --字段2 数据类型,
  .....
  --字段n 数据类型
)
```
>例如我要建立一个部门表、职级表、员工表
```sql
create table Department --创建部门表
(
   --部门编号，整型，设置为主键（只能有一个主键，推荐不重复的值作为主键）
   --identity(1,1):自动增长，初始值为1，增长步长为2
   DeId int primary key identity(1,2),
   --部门名称，varchar类型，长度为50，添加该数据时不能为空值
   DeName varchar(20) not null,
   --部门描述,text长文本类型
   DeRemark text
)

create table [Rank] --创建职级表
(
  RankId int primary key identity,
  RankNa varchar(50) not null,
  RanlRemark text
)

create table People --创建职级表
(
  DeId int references Department(DeId),--部门外键，表示在这里使用部门表的主键
  RankId int references [Rank](RankId) not null,--职级外键，表示在这里使用职级表的主键
  PeId int primary key identity, --id
  PeNa varchar(50) not null,   --姓名
  PeSex nvarchar(1) check(PeSex='男' or PeSex='女') , --性别
  --添加了一个check约束，规定输入值只能是男或者女
  PeRemark text default('此人没添加描述'), --描述
  --添加了一个default表示默认值，没有输入数据就默认显示这个
  PeBirth datetime not null, --出生日期
  --还有个smalldatetime表示最近几百年的日期，占用内存更小
  PeSalary decimal(12,2) check(PeSalary>=1000 and PeSalary<=10000000) --工资
  --12,2表示总长12，小数点精确到2位，工资范围约束到1000到10000000
  PePhone varchar(20) unique not null , --电话
  --unique表示不能重复的约束
  PeAdress varchar(300),  --家庭地址
  PeAddTime smalldatetime default(getdate())--加入公司的时间，没写就是默认现在的时间
  
)


```
### 常用数据类型：
* char 定长，char(10)表示无论数据是否到10字节，都占10字节大小
* varchar 变长，varchar(10)表示最多占用10个字节
* text 长文本，字多的推荐此类型
* char、varchar、text前面加上n表示存储unicode字符，对中文友好，例如varchar(100)可存100字母和50汉字，则nvarchar(100)就是100字母100汉字
* bigint/int/smallint/tinyint 表示整型，从大到小
* float/real 表示浮点数

# 四、修改表的结构
## 1、添加列
```sql
alter table 表名 add 新列名 数据类型
--例如给员工表添加一个邮箱
alter table People add PeopleMail varchar(200),
```
## 2、删除列
```sql
alter table 表名 drop column 列名
--删除表中邮箱一列
alter table People drop column PeopleMail
```
## 3、修改列
```sql
alter table 表名 alter column 列名 数据类型
--修改地址varchar(300)为varchar(200)
alter table People alter column PeopleAddress varchar(200)
```
# 五、约束的添加和删除
## 1、删除约束
```sql
alter table 表名 drop constraint 约束名
--删除一个月薪的约束
alter table People drop constraint check
```
## 2、添加约束
```sql
alter table 表名 add constraint 约束名
--添加工资字段的约束，工资最低100，最高100000
alter table People add constraint check(PeSalary>=100 and PeSalary<=100000)
```
>不同的约束有不同的语法
## 3、添加约束的约束种类
* null/not null空值约束
* unique唯一约束
* primary key主键约束
* foreign kty外键约束
* check 范围约束
* default 默认值约束

# 六、数据的插入
```sql

--语法
insert into 表名 (列1、列2、... 列n)
values('值1','值2', ... '值n')

--现在要向部门插入一个数据
insert into Department(DeName,DeId)
values('市场部门','111')

--再插入一个
insert into Department(DeName,DeId)
values('销售部门','222')

--一次性插入多行数据
insert into Department(DeName,Deramark)
select '测试部门','112' union
select '产品部门','123' union
select '实时部门','124'

--向职级表插入数据
insert into [Rank](RankName,RankRemark)
values('初级','13212')
insert into [Rank](RankName,RankRemark)
values('中级','13213432')
insert into [Rank](RankName,RankRemark)
values('高级','132114312')

--注意：如果先前设置好了约束，插入时违背了约束的规定就会报错，无法插入
```

# 七、修改和删除数据
## 1、修改数据
```sql

--语法
update 表名 set 字段1=值1,字段2=值2 
where 条件(修改所有数据就不需要加)

--假设要做一个工资的调整，每个人工资加100
update People set PeopleSalary = PeopleSalary+100

--现在单独给一个人加100，那个人叫小红
update People set PeopleSalary = PeopleSalary+100
where PeopleName = '小红'

--将软件部门人员，给工资低于2000的改成2000
update People set PeopleSalary = 2000
where DeName = '软件部门' and PeopleSalary < 2000

--修改刘备的工资是以前的2倍，并且把地址改成北京
update People set PeopleSalary = PeopleSalary * 2, PeopleAddress='北京'
where DeName='刘备'

```
## 2、删除数据
--语法
```sql

--语法
delete from 表名
where 条件（删除所有就不加）

--删除员工表所有记录
delete from People
--删除市场部门里面工资大于10000的人
delete from People 
where DeName='市场部' and PeopleSalary>10000

--除了delete删除，还有drop和truncate删除
--drop删除表对象，删除最彻底
drop table People

--truncate只是清空数据，表还是在的
truncate table People

```
>truncate和delete的区别:前者是清空所有数据，不能有条件。delete既可以删除所有数据，也可以删除符合条件的数据，假设表中编号为1,2,3,4,5,6，使用turncate时编号不变，delete的话编号就永远不存在了，变成了7,8,9,10等

# 八、基本查询
## 1、查询所有列和所有行
```sql
--*星号代表所有的意思 , from代表查询东西的来源
select *from 表名
```
## 2、查询指定列
```sql

select 需要查询的元素
from 查询数据的来源表

--例如我要查询指定列即姓名、性别、生日、月薪、电话
select PeName,PeSex,PeBirth,PeSalary,PePhone
from People

--当然也可以对每列的列名起一个中文别名，注意这样只是改变了显示的列名
--只需要在每一个需要查询的列名后面写上别名即可
select PeName 姓名,PeSex 性别,PeBirth 生日,PeSalary 工资,PePhone 电话
from People

--当然也可以用distinct对特定元素进行去重
--例如我要查询员工所在城市（不需要显示重复的城市名）
select distinct(PeAddress)
from People

--当然可以尝试看一下改变元素后的状态（不改变当前数据库里的数据）
--假设准备加百分之20的公资，看一下加完工资后的样子
select PeSalary*1.2 加薪后工资,PeName,PeSalary 原始工资
from People

```

# 九、条件查询

## 1、sql中常用运算符

* = 等于
* != 不等于
* > < >= <= 大于小于大于等于和小于等于
* IS NULL 比较为空
* IS NOT NULL 比较不为空
* in 比较是否在其中
* like 模糊查询
* between ... and ... 是否介于两者之间
* and 逻辑与
* or 逻辑或
* not 逻辑非
* asc 升序排序
* desc 降序排序

## 2、条件查询
```sql

--查询性别为女的员工信息
select *from People
where PeopleSex='女'

--查询工资大于100元小于500元的员工信息
select *from People
where PropleSalry > 100 and PeopleSalary < 500
--还可以这样写
select *from People
where PeopleSalary between 100 and 500

--查询性别女并且工资大于100的员工信息
select *from People
where PropleSex='女' and PeopleSalary >100

--查询月薪大于等于100的员工或者小于等于100的女员工
select *from People
where PeopleSalary>100 or(PeopleSalary<100 and PeopleSex='女')

--查询出生年月在1980-1-1之后的女员工
select *from People
where PeopleSex='女' and PeopleBirth>='1980-1-1'

--查询地址在武汉或者北京的员工信息
select *from People
where PeopleAddress ='北京' or PeopleAddress = '武汉'
--还可以这样写
select *from People
where PeopleAddress in('武汉','北京')

--查询所有员工的信息，根据工资进行降序排序
select *from  People order by PeopleSalary desc

--查询所有员工信息根据名字长度进行降序排序
select *from People order by len(PeopleName) desc

--查询工资最高的5个人信息
select top 5 * from People order by PeopleSalary desc

--查询工资最高的10%的员工信息
select top 10 percent * from People order by PeopleSalary desc

--查询地址没有填写的员工信息
select *from People
where PeopleAddress is null

--查询地址填写了的员工信息
select *from People
where PeopleAddress is not null

--查询30到40岁之间，并且工资在100到500之间的员工信息
select *from People
where (year(gerdate()-year(PeopleBirth)>=30 and year(gerdate()-year(PeopleBirth)<=40)
and (PeopleSalary between 100 and 500)

--查询出工资比赵云高的人的信息
select *from People
where PeopleSalary > ( select PeopleSalary from People where PeopleName='赵云')

```

# 模糊查询

## 1、模糊查询的关键词
* '%' 代表0个字符、1个或者多个字符
* '_' 代表有且仅有一个字符
* '[]' 代表匹配范围内
* '[^]' 代表匹配不在范围内

## 2、模糊查询

```sql
--查询出姓刘的员工信息
select *from People
where PeopleName like '刘%'

--查询出名字中含有‘小’的员工信息
select *from People
where PeopleName like '%小%'

--查询出名字中含有‘小’ 或者‘大’的员工信息
select *from People
where PeopleName like '%小%' or PeopleName like '%大%'

--查询姓刘的员工，名字只有2个字
select *from People
where PeopleName like '刘_'

--查询电话号码为182开头，第四为好像为7或者8，最后一个号码为5
select *from People
where PeoplePhone like '138[7,8]%5'

--查询电话号码为182开头，第四为好像为7或者8，最后一个号码为不是2和3
select *from People
where PeoplePhone like '138[7,8]%[^2,3]'


```

# 聚合函数查询

## 1、常用关键字
* count 求数量
* max 求最大值
* min 求最小值
* sum 求和
* avg 求平均值

## 2、聚合查询
```sql

-求员工的总人数
select count(*) from People

-求最高工资
select max(PeopleSalary) 最高工资 from People

-求最低工资
select min(PeopleSalary) 最低工资 from People

-求工资之和
select sum(PeopleSalary) from People

--求工资平均值
select avg(PeopleSalary) from People 

--求上面所有，并在一行显示
select count(*) 人数，max(PeopleSalary) 最高工资,min(PeopleSalary) 最低工资,sum(PeopleSalary),avg(PeopleSalary) from People
where PeopleAddress='武汉'

--求工资比平均工资高的信息
select * from People
where PeopleSalary > (select round(avg(PeopleSalary),2) 平均工资 from People)

```

# 分组查询

1、关键词
* group by 分组

2、分组查询
```sql

--根据地区进行分组
select PeopleAddress 地区, count(*) 员工人数 , sum(PeopleSalary) 工资总和,avg(PeopleSalary) from People
group by PeopleAddress

--根据地区进行分组，1985年及其以后不参与统计
select PeopleAddress 地区, count(*) 员工人数 , sum(PeopleSalary) 工资总和,avg(PeopleSalary) from People
where PeopleBirth < '1985-1-1'  -注意where在group by前面
group by PeopleAddress

--根据地区进行分组，1985年及其以后不参与统计，筛选出员工人数至少在2人及其以上的记录
select PeopleAddress 地区, count(*) 员工人数 , sum(PeopleSalary) 工资总和,avg(PeopleSalary) from People
where PeopleBirth < '1985-1-1'  -注意where在group by前面
group by PeopleAddress
having  count(*) >= 2  -注意having在group by 后边

```

# 多表查询

## 1、笛卡尔乘积
```sql
select *from Department
select *from People
--笛卡尔乘积的结果是将People所有记录和Department所有记录依次进行排列组合取得的结果
select *from Department ,People
```

## 2、简单的多条查询
```sql
--查询员工信息并显示部门名称
select *from People ,Department
where People.DepartmentId = Department.DepartmentId

--查询员工信息，显示部门名称和职级名称
select *from People,Department,[Rank]
where People.DepartmentId = Department.DepartmentId and People.RankId=[RankId].RankId
```

## 3、内连接查询
```sql
--查询员工信息，显示部门名称和职级名称
select *from People
inner join Department on People.DepartmentId = Department.DepartmentId
inner join [Rank] on People.RankId=[Rank].RankId
``` 
>简单多表查询和内连接查询的共同特点：不符合主外键关系的数据不会被显示出来

# 子查询
















