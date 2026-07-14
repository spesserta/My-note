一、SVN介绍

- 代码版本管理工具
- 它能记住每次的修改
- 查看所有的修改记录恢复到任何历史版本
- 恢复已经删除的文件

二、SVN跟Git比有什么优势

- 使用简单，上手快
- 目录级权限控制，企业安全必备（人事部和开发部看到的都不一样）
- 子目录Checkout，减少不必要的文件检出工。


三、SVN主要应用

开发人员用来做代码的版本管理用来存储一些重要的文件，比如合同公司内部文件共享，并且能按目录划分权限



四、安装SVN

先svnbucket.com上面注册一个号

- Windows系统推荐使用TortoiseSVN
- MAC系统推荐Cornerstone

以Windows系统为例，浏览器搜索乌龟SVN，选择对应的版本下载即可

<img width="1231" height="686" alt="image" src="https://github.com/user-attachments/assets/5c803939-759c-4206-8c2f-c0fb3f471d94" />


安装全部点击下一步就行了



五、SVN的基本操作

- 检出 checkout
- 新增 add
- 提交 commit
- 更新 update
- 历史记录

SVNBucket网站是模拟服务器的，然后乌龟SVN是客户端<br>
在项目列表中新建一个项目


<img width="566" height="698" alt="image" src="https://github.com/user-attachments/assets/396d8f68-0a25-404d-b98c-23189718372b" />


进去后复制右上角的地址，在下载安装好乌龟SVN后，桌面上新建一个文件夹后右键就能看到SVN的操作了：

<img width="634" height="587" alt="image" src="https://github.com/user-attachments/assets/b59e008f-aea6-4672-9c60-aa0ee132eb43" />

首先来一个SVN checkout检出，URL填写刚才复制好的，然后就会弹出登录界面，如果是公司里的话，就填写公司仓库给你的账号密码，现在是checkout刚才网站上的项目，就填写网站上注册的账号密码就行

<img width="599" height="611" alt="image" src="https://github.com/user-attachments/assets/19ea2a0d-d489-4ab2-8f44-48a4ce4e1f48" />

登录成功后填写URL，写好checkout目录

<img width="594" height="614" alt="image" src="https://github.com/user-attachments/assets/1ce148f1-ef99-461f-b73f-b193888c3893" />

<img width="862" height="446" alt="image" src="https://github.com/user-attachments/assets/09711725-5f31-4438-9c2f-8c32ded8f561" />

检出成功后文件夹旁边会出现一个绿色的勾，这个勾表示这个文件夹和网上的仓库数据是同步的，如果没有这个勾就重启下电脑


























































