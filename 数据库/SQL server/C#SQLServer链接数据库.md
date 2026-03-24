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

































































