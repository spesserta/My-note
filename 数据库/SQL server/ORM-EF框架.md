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


好的，现在就可以利用对象的方式来操作员工表了




























































