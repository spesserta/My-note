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

检出成功后文件夹旁边会出现一个绿色的勾，这个勾表示这个文件夹和网上的仓库数据是同步的，如果没有这个勾就重启下电脑试试


<img width="663" height="121" alt="image" src="https://github.com/user-attachments/assets/88737c45-3e51-4567-9c22-ab47164f5e02" />

现在将需要同步到仓库的文件放入该文件夹中(add)：


<img width="852" height="368" alt="image" src="https://github.com/user-attachments/assets/ff664b83-4a03-4a2b-92ef-70342d295e8a" />




现在右键空白处点击SVN commit提交：

<img width="837" height="579" alt="image" src="https://github.com/user-attachments/assets/c1eb0242-40fa-4fc5-86bf-87409ffce1b9" />


选中需要提交的文件，点击OK提交就行了

<img width="1032" height="783" alt="image" src="https://github.com/user-attachments/assets/9ebac88f-1362-4aab-a111-993336fe5505" />


<img width="864" height="449" alt="image" src="https://github.com/user-attachments/assets/79e18bd5-4268-42d3-8de6-ac34549b9a1f" />


提交之后是可以看到这里的文件图标都有一个勾了，去仓库看看也可以发现已经同步到了，后续也可以在网站上看到提交和下载的记录

<img width="1899" height="540" alt="image" src="https://github.com/user-attachments/assets/49f766d6-5ae8-4423-ad0e-cd9b7bc5740b" />


假如后续修改本地的代码后要同步到SVN仓库中，就可以选择SVN update实时将修改结果同步到仓库中，这个修改的文件就会变成红色的感叹号，双击这个文件就可以看到变更的内容

<img width="749" height="237" alt="image" src="https://github.com/user-attachments/assets/ee680dbc-055b-413e-8136-bdba991165e5" />


<img width="1070" height="638" alt="image" src="https://github.com/user-attachments/assets/06b0924f-f4bf-439b-a41a-6da631d56494" />


<img width="970" height="576" alt="image" src="https://github.com/user-attachments/assets/6748c696-4bb5-4196-b8e3-e6638041d8f2" />


除此之外还可以看到提交的历史记录


<img width="422" height="584" alt="image" src="https://github.com/user-attachments/assets/74f33670-81f8-4df9-9247-e3c10098f21a" />



六、SVN的撤销和恢复

- 撤销本地修改：TortoiseSVN->Revert 就可以丢弃本地修改了
- 撤销已经提交的内容

1、撤销本地修改

右键文件就行了，就像下面这样

<img width="699" height="1033" alt="image" src="https://github.com/user-attachments/assets/7e5bca20-9c65-4ec9-bead-a4bde02f918a" />

2、撤销已经提交的代码

右键TortoiseSVN ==> show log 查看提交记录。<br>
选择我们需要回去的版本，右键选择Revert to this version，这样就回去了指定的版本。<br>
最后还需要commit下撤销后的代码到SVN仓库。<br>


<img width="1337" height="880" alt="image" src="https://github.com/user-attachments/assets/dfd6ec24-7ce8-4f25-b15a-fd4c4acc37b0" />

<img width="1343" height="875" alt="image" src="https://github.com/user-attachments/assets/d68705d0-6614-4696-aa03-b208582b9383" />



七、添加忽略文件和文件夹

有时候有些文件或者文件夹不想提交，那么就可以选择忽略它


<img width="1344" height="770" alt="image" src="https://github.com/user-attachments/assets/ad3ce2b1-6295-45d8-84db-1646deb8decc" />

忽略完提交就可以看到没有将忽略的文件交上去了<br>


撤销忽略也是同样的操作

<img width="1335" height="885" alt="image" src="https://github.com/user-attachments/assets/9f0b4a89-e97c-4133-bede-c1765438a2b5" />




八、解决冲突

什么情况容易发生冲突? 
- 多个人修改了同个文件的同一行
- 无法进行合并的二进制文件


怎么避免冲突?
- 经常update同步下他人的代码
- 二进制文件不要多个人同时操作




九、分支

什么时候需要开分支?
- 隔离线上版本和开发版本
- 大功能开发，不想影响到其他人，自己独立开个分支去开发


SVIN经典目录结构:
- trunk
- branches
- tags











