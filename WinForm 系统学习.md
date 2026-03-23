# WinForm 系统学习完整笔记

> **适用人群**：零基础/想系统梳理WinForm、目标做桌面小工具/工控上位机的开发者 <br>
> **开发环境**：Visual Studio 2022 \+ \.NET Framework 4\.7\.2/4\.8（稳定兼容，工控首选）<br>
> **学习原则**：先基础后实战、边学边敲代码、拒绝零散知识点

---

# 第一部分：前置基础

WinForm基于C\#开发，跳过基础直接学控件会寸步难行，重点攻克**语法核心\+委托事件**（WinForm灵魂）

## 1\.1 C\# 核心基础

- **基础语法**：变量/数据类型（值类型\+引用类型）、运算符、分支（if/switch）、循环（for/while/foreach）、数组

- **面向对象**：类、对象、字段、属性、方法、构造函数、封装、继承、多态

- **集合类型（高频使用）**：List\&lt;T\&gt;（动态数组）、Dictionary\&lt;K,V\&gt;（键值对）、DataTable（表格数据）

- **字符串处理**：拼接、截取、格式化、空值判断（string\.IsNullOrEmpty）

- **异常处理**：try\-catch\-finally、throw抛异常，避免程序闪退

## 1\.2 委托与事件

- **委托**：方法的容器，相当于函数指针，用于方法传递、回调

- **事件**：基于委托的特殊类型，是控件交互的核心（按钮点击、窗体加载都是事件）

- **语法示例**：
`// 声明委托
public delegate void MyDelegate\(string msg\);
// 声明事件
public event MyDelegate MyEvent;
// 触发事件
MyEvent?\.Invoke\(\&\#34;Hello WinForm\&\#34;\);`

- **核心作用**：实现窗体传值、跨线程通信、解耦代码

## 1\.3 环境搭建与项目创建

1. 安装VS2022，勾选**\.NET桌面开发**工作负载

2. 新建项目：选择**Windows Forms App \(\.NET Framework\)**，命名后创建

3. 项目结构解析：
        

    - **Form1\.cs**：窗体后台代码（逻辑实现）

    - **Form1\.Designer\.cs**：设计器自动生成代码（控件布局、属性）

    - **Program\.cs**：程序入口，Main方法启动窗体

---

# 第二部分：WinForm 核心基础

## 2\.1 窗体（Form）基础

窗体是所有控件的容器，掌握属性、事件、生命周期是基础

- **常用属性**：Text（标题）、Size（大小）、StartPosition（启动位置）、BackColor（背景色）、Icon（图标）、MaximizeBox（最大化按钮）

- **常用事件**：
        

    - Load：窗体加载时触发（初始化数据、控件）

    - FormClosing：窗体关闭前触发（提示保存、退出确认）

    - FormClosed：窗体关闭后触发（资源释放）

- **实操技巧**：禁止窗体重复打开、设置窗体默认尺寸、模态/非模态窗体切换

## 2\.2 常用控件大全（按优先级排序，拒绝盲目学）

控件是WinForm的核心，只学**高频实用控件**，小众控件用到再查

### 2\.2\.1 基础输入/展示控件

- **Label**：文本展示，设置Text、Font、ForeColor属性

- **TextBox**：文本输入，常用属性：Multiline（多行）、PasswordChar（密码框）、ReadOnly（只读）

- **Button**：按钮，核心事件Click（点击触发逻辑）

- **CheckBox**：复选框，Checked属性判断是否选中

- **RadioButton**：单选框，同容器内互斥

- **ComboBox**：下拉框，绑定数据源、获取选中项

### 2\.2\.2 列表/表格控件（上位机必备）

- **ListBox**：列表展示，添加/删除项、获取选中项

- **DataGridView**：表格控件（重中之重）
        

    - 手动添加列、绑定DataTable数据源

    - 设置列宽、行高、只读、选中行获取数据

    - 常用事件：CellClick、CellDoubleClick

### 2\.2\.3 容器控件（界面布局）

- **Panel**：通用容器，分组控件

- **GroupBox**：带标题容器，分类展示控件

- **TabControl**：分页控件，多标签切换界面

### 2\.2\.4 功能控件（实用工具）

- **Timer**：定时器，Interval（间隔时间，毫秒）、Tick事件（定时执行逻辑）

- **ProgressBar**：进度条，Value属性设置进度

- **OpenFileDialog/SaveFileDialog**：文件选择/保存对话框

- **MessageBox**：消息提示框（提示、警告、错误、确认）

## 2\.3 控件操作核心技巧

1. **拖拽\+代码结合**：设计器拖拽布局，后台代码动态修改属性/绑定事件

2. **动态创建控件**：代码new对象，设置属性后添加到窗体Controls集合

3. **控件取值赋值**：TextBox\.Text、ComboBox\.SelectedItem、DataGridView\.Rows\[i\]\.Cells\[j\]\.Value

## 2\.4 界面布局（自适应窗口，告别错位）

- **Anchor（锚点）**：控件绑定窗体边缘，拉伸窗体时自动适配

- **Dock（停靠）**：控件填充父容器（Top/Bottom/Left/Right/Fill）

- **TableLayoutPanel/FlowLayoutPanel**：流式/表格布局，批量排列控件

- **禁忌**：固定像素定位，高分屏/拉伸窗体后控件错位

## 2\.5 多窗体操作

- **窗体显示**：
        

    - Show\(\)：非模态窗体，可切换操作多个窗体

    - ShowDialog\(\)：模态窗体，阻塞当前窗体，必须关闭才能操作

- **窗体传值（3种常用方式）**：
        

    1. 构造函数传值（打开窗体时传参）

    2. 公共属性/方法传值

    3. 委托事件传值（推荐，解耦）

- **关闭窗体**：Close\(\)、Hide\(\)（隐藏不销毁）

---

# 第三部分：WinForm 进阶核心

## 3\.1 跨线程操作UI（上位机必学，解决闪退报错）

WinForm UI控件只能在创建线程（主线程）操作，子线程直接修改控件会触发**跨线程调用异常**

- **解决方案**：Invoke/BeginInvoke（同步/异步调用主线程）

- **实操代码**：
`// 跨线程更新TextBox
private void UpdateUI\(string msg\)
\{
    if \(textBox1\.InvokeRequired\)
    \{
        // 委托回调
        textBox1\.Invoke\(new Action\&lt;string\&gt;\(UpdateUI\), msg\);
    \}
    else
    \{
        textBox1\.AppendText\(msg \+ Environment\.NewLine\);
    \}
\}`

- 简化方案：C\# 4\.0\+ 可使用async/await简化异步操作

## 3\.2 数据存储与交互

### 3\.2\.1 配置文件读写

- App\.config：存储固定配置（IP、端口、参数）

- 读写方法：ConfigurationManager类读取appSettings节点

### 3\.2\.2 本地文件操作

- Txt日志：File\.AppendAllText写入运行日志

- JSON/XML：序列化对象，保存用户配置、历史数据

### 3\.2\.3 数据库操作（轻量级首选）

- 推荐数据库：SQLite（无需安装、单文件）、Access

- 操作方式：ADO\.NET（原生SqlConnection、SqlCommand）

- 核心步骤：连接数据库→执行SQL（增删改查）→关闭连接

- 数据绑定：DataTable绑定DataGridView，实现表格展示

## 3\.3 自定义控件（UserControl）

- 作用：封装重复控件组合，实现代码复用

- 场景：自定义参数面板、数据展示模块、工控仪表盘

- 操作：新建UserControl→拖拽控件→封装属性方法→主窗体调用

## 3\.4 程序优化与异常处理

- 全局异常捕获：Application\.ThreadException \+ AppDomain\.CurrentDomain\.UnhandledException

- 资源释放：using语句释放数据库连接、文件流、串口对象

- 内存优化：及时销毁无用对象、避免控件频繁创建

---


