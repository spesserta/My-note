### 一、WPF基本信息

WPF是微软推出的用于构建Windows桌面应用程序的UI框架。可以看成是构建Windows应用的一套强大的“工具箱”和“规则手册”。

##### 1、和Winform的区别 

在技术基础和渲染上：
- Winform是建立在古老的GDI+API之上，是基于CPU基于像素的绘图技术，意味着所有的UI都会绘制成一个像素矩阵，缩放的时候会导致模糊
- WPF是建立在DirectX上，这是利用GPU进行硬件加速的现代图形引擎，WPF将所有的UI元素都看成矢量图形，可以无限缩放不失真，适合复杂的UI和动画

在UI构建上：
- Winform采用命令式和事件驱动的方式，程序员拖放控件到设计器上构建UI，VS会自动生成初始化控件的C#代码，UI逻辑直接写在控件的代码后置文件中。
- WPF采用声明式的方式，UI使用XAML来定义，将UI设计和程序逻辑清晰的分离开来，负责设计的人可以处理XAML文件，开发者可以专注于C#逻辑，类似于Java的前后端分离。

在数据绑定上：
- Winform数据绑定是事后添加的功能，通常只能绑定简单的属性，并且需要大量手动代码来同步数据和UI
- WPF的数据绑定是核心部分，支持双向绑定、复杂路径绑定、数据模板、值转换器，这为实现MVVM提供了完美支持，极大的提高了应用程序的可测试性和可维护性

在视觉设计灵活性上：
- Winform控件的外观由操作系统的主题来决定，如果要设计一个好看的按钮，就必须自己重写方法，从头绘制，这个很繁琐
- WPF的控件外观和行为是分离的，任何控件的视觉外观都可以用ControlTemplate来完全重做，并且核心功能保持不变

在布局系统上：
- Winform的布局主要依靠绝对坐标（left、right、Top），辅以”停靠“和”锚定“来应对窗口大小的变化，创建复杂的流式布局很难
- WPF的布局由容器面板决定，不同的面板提供了不同的布局逻辑，使得构建能够自适应不同窗口大小和屏幕分辨率的动态布局变得非常的简单自然

### 二、MVVM

MVVM模式专门为WPF设计，完美的适配了WPF的数据绑定，包含Model（数据模型）、View（界面）、ViewModel（中间层业务逻辑）。相比于MVC的Model->Control->View 这个单向的过程，MVVM是Model<->ViewModel<->View这双向的过程。MVVM完全的实现了前后端分离，UI设计和后端逻辑互不干扰。ViewModel也不依赖UI可以单独的进行单元测试。

#### 1、INotifyPropertyChanged接口（后端数据通知UI）

该接口主要作用是：当数据源（如ViewModel或Model）中的属性值发生变化时，自动通知绑定的 UI 控件进行更新，而无需手动刷新界面。在ViewModel层中继承该接口，举例如下：

```c#
//假设我们有一个简单的用户信息界面，当后台数据中的UserName发生变化时，界面上的文本框会自动同步更新。
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class UserViewModel : INotifyPropertyChanged   //继承该接口
{
    //声明事件
    public event PropertyChangedEventHandler PropertyChanged;

    //封装触发方法，利用 CallerMemberName 自动获取调用者的属性名
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    //定义需要绑定的属性
    private string _userName;
    public string UserName
    {
        get => _userName;
        set
        {
            //只有当值真正发生改变时，才触发通知，避免无效刷新
            if (_userName != value) 
            {
                _userName = value;
                OnPropertyChanged(); // 无需传参，编译器自动填入 "UserName"
            }
        }
    }
}
```

在XAML界面中，只需要将控件绑定到这个属性，剩下的UI更新工作框架会自动完成：

```xml
<!-- 假设 Window 的 DataContext 已经设置为了 UserViewModel 实例 -->
<TextBox Text="{Binding UserName, UpdateSourceTrigger=PropertyChanged}" />
```

#### 2、ICommand接口（UI通知后端数据）


该接口是MVVM架构中用于处理用户交互（如按钮点击、菜单选择等）的核心接口。如果说INotifyPropertyChanged负责“数据变化时通知UI更新”，那么ICommand就负责“UI触发操作时，通知后台执行逻辑”。它的最大优势在于将 UI控件（View）与业务逻辑（ViewModel）彻底解耦，同时还能自动控制UI的状态（例如按钮的启用/禁用）。<br>

该接口主要包含三个成员：

- Execute(object parameter)：当用户触发操作（如点击按钮）时，执行的具体业务逻辑。
- CanExecute(object parameter)：判断当前命令是否可以执行。UI控件会根据它的返回值自动启用或禁用（例如返回false时按钮变灰）。
- CanExecuteChanged 事件：当决定命令能否执行的条件发生变化时，触发此事件通知 UI 重新查询CanExecute的状态。


实际例子如下：
```c#
//假设有一个简单的界面，包含一个显示数字的文本和一个“增加”按钮。当数字达到 10 时，按钮自动变灰禁用。
using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object parameter)
    {
        _execute(parameter);
    }

    // 当状态可能改变时，通知 UI 重新查询 CanExecute
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}
```
ViewModel层实现：
```c#
public class CounterViewModel : INotifyPropertyChanged
{
    private int _count;
    public int Count
    {
        get => _count;
        set { _count = value; OnPropertyChanged(); }
    }

    // 定义一个 ICommand 属性供 UI 绑定
    public ICommand IncrementCommand { get; }

    public CounterViewModel()
    {
        // 初始化命令：传入执行逻辑和判断逻辑
        IncrementCommand = new RelayCommand(
            execute: _ => Count++,
            canExecute: _ => Count < 10  // 当 Count >= 10 时，按钮自动禁用
        );
    }

    // INotifyPropertyChanged 的实现代码...
}
```
UI界面绑定：
```xml
<!-- 假设 DataContext 已设置为 CounterViewModel ,不需要写 Button.Click 事件，也不需要手动写 button.IsEnabled = false，只需绑定即可-->
<StackPanel>
    <TextBlock Text="{Binding Count}" FontSize="24" />
    <!-- 按钮的点击和启用/禁用状态完全由 Command 接管 -->
    <Button Content="增加" Command="{Binding IncrementCommand}" />
</StackPanel>
```

#### 3、MVVM实践中需要注意什么

- ViewModel尽量少些，不要包含UI代码
- 使用ObservableCollection自动通知集合变化
- 避免View层引入Model，通过ViewModel转换
- 用ValueConverter处理数据格式转换
- ViewModel之间要通信可以使用事件


#### 4、常见的MVVM框架

根据项目的复杂度来选择合适的框架，主流的框架包括以下：
- Prism：企业首选，功能全面
- MVVM Lite：轻量级，学起来简单
- Reactive UI：响应式编程


#### 5、MVVM开发流程

- 根据数据库来设计Model
- 创建ViewModel实现属性通知变更和业务逻辑
- 设计View，使用XAML语法和数据绑定
- 连接绑定，设置DataContext
- 测试验证效果





### 三、数据绑定

数据绑定有三个核心概念：

- 界面元素会自动关联数据对象
- 后端数据变化时界面实时更新
- 界面输入时自动更新数据

数据绑定可以不用手动更新UI界面，可以保持数据和界面的同步，也容易维护（界面和业务逻辑分离），也支持数据验证。

#### 1、绑定模式

- OneWay：数据到界面单向的绑定
- TwoWay：数据和界面之间双向的绑定
- OneTime：只绑定一次，只有初始化的时候才显示
- OneToSource：界面到数据的绑定（和OneWay路径相反）

<img width="557" height="211" alt="image" src="https://github.com/user-attachments/assets/9ea59e59-72ff-4de5-a5a1-178eeca52c1d" />


#### 2、ObserverableCollection类（自动通知集合变化）

ObservableCollection<T>是MVVM架构中专门用于集合数据绑定的核心类。如果说INotifyPropertyChanged负责通知UI更新“单个属性”，那么ObservableCollection就负责通知UI更新“整个列表”。<br>

普通的集合如List<T>在添加或删除元素时，UI是感知不到的。而ObservableCollection实现了INotifyCollectionChanged接口。当对它进行Add（添加）、Remove（删除）、Clear（清空）等操作时，它会自动触发事件，通知绑定的UI控件（如 ListBox、DataGrid）自动刷新界面，无需手动调用刷新方法。

```c#
public class ChatViewModel : INotifyPropertyChanged
{
    //使用ObservableCollection作为数据源
    public ObservableCollection<Device> Devices { get; set; } = new ObservableCollection<string>();

    public void AddDevice(Device device)
    {
        Devices.Add(device);
    }
    public void DelDevice(Device device)
    {
        Devices.Remove(device);
    }
}
```
```xml
<!--绑定-->
<ListBox ItemsSource="{Binding Devices}" />
```



#### 3、值转换器ValueConverter类


这个类的作用是：当数据源（ViewModel）的数据类型或格式与 UI 控件（View）的要求不一致时，在绑定过程中自动进行转换。<br>

在UI开发中，后台数据往往不能直接用于界面显示。例如，后台存储的是布尔值 true/false，但 UI 需要的是 Visible/Collapsed；或者后台存的是 DateTime，但界面需要显示为“2023年10月1日”。ValueConverter 就是用来处理这些转换逻辑的。通过以下两个方法：

- Convert：将后台数据转换为 UI 需要的格式（ViewModel → View）。
- ConvertBack：将 UI 上的值转换回后台数据格式（View → ViewModel，仅在双向绑定时需要）。


假设我们有一个“删除”按钮，当用户没有选中任何列表项时（后台状态为 false），按钮应该隐藏，选中后（后台状态为 true），按钮才显示。可以这么用：

```c#
//定义转换器
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

public class BoolToVisibilityConverter : IValueConverter
{
    // 将 bool转换为Visibility
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }

    // 将Visibility转换回 bool（如果不需要双向绑定，可直接抛出异常或返回 null）
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            return visibility == Visibility.Visible;
        }
        return false;
    }
}
```
```xml
//在UI层中直接使用这个封装好的类
<Window.Resources>
    <!-- 声明转换器 -->
    <local:BoolToVisibilityConverter x:Key="BoolToVisibilityConverter" />
</Window.Resources>

<!-- 绑定并应用转换器 -->
<Button Content="删除" 
        Visibility="{Binding IsItemSelected, Converter={StaticResource BoolToVisibilityConverter}}" />
```

这种极其常用的转换器，建议不要自己手写，微软已经封装好了现成的包可以调用。


#### 4、数据绑定的流程

- 设计模型数据，实现INotifyPropertyChanged接口
- 创建ViewModel包装数据模型数据
- 设置DataContext，关于View和ViewModel
- 编写绑定表达式，就是在XAML中设置Binding
- 测试验证



### 四、WPF的数据验证和错误处理机制


数据验证就是确保用户输入的数据符合程序规定的要求，防止用户乱输入东西导致系统崩溃，提高程序的健壮性。数据验证可以在UI层进行验证，也可以在ViewModel层进行验证。


#### 1、内置的数据验证

- 异常验证规则 (ExceptionValidationRule)：检查在更新绑定源属性时是否抛出了异常。如果源属性的 setter 或类型转换失败
- 数据错误验证规则 (DataErrorValidationRule)：检查实现了 IDataErrorInfo 接口的对象所引发的错误，允许在业务对象内部集中定义验证逻辑


使用举例如图：
<img width="668" height="252" alt="image" src="https://github.com/user-attachments/assets/00c28d46-3ffa-4c20-a6dd-0816f1cdabba" />


#### 2、自定义验证规则

可以通过继承ValidationRule类并重写Validate方法来创建自己的验证逻辑。这在需要复杂的、跨字段的验证时非常有用。


```c#
//创建一个 FutureDateRule，确保输入的日期是未来的日期
public class FutureDateRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (DateTime.TryParse(value.ToString(), out DateTime date))
        {
            if (DateTime.Now > date)
                return new ValidationResult(false, "请输入未来的日期。");
        }
        else
        {
            return new ValidationResult(false, "日期格式不正确。");
        }
        return ValidationResult.ValidResult;
    }
}
```
在XAML中这么使用：
```xml
<TextBox Text="{Binding Path=StartDate, UpdateSourceTrigger=PropertyChanged}">
    <TextBox.Text>
        <Binding Path="StartDate" UpdateSourceTrigger="PropertyChanged">
            <Binding.ValidationRules>
                <!-- 引入自定义的验证规则 -->
                <local:FutureDateRule />
            </Binding.ValidationRules>
        </Binding>
    </TextBox.Text>
</TextBox>
```


#### 3、IDataErrorInfo接口

之前学的 ValidationRule 是把验证规则写在 UI 层（XAML）中，那么 IDataErrorInfo 就是把验证逻辑下沉到后台数据层，让数据对象自己负责检查自己的数据是否合法。


该接口有2个核心成员：

- Error 属性：返回一个字符串，表示整个对象级别的错误（通常返回 null 或空字符串即可）。
- this[string columnName] 索引器：接收一个属性名称（字符串），返回该属性对应的错误信息。


假设有一个用户注册界面，要求年龄必须在 18 岁以上。第一步现在ViewModel中实现接口

```c#
public class UserViewModel : INotifyPropertyChanged, IDataErrorInfo
{
    private int _age;
    public int Age
    {
        get => _age;
        set { _age = value; OnPropertyChanged(); }
    }

    //整个对象的错误（通常不用，直接返回 null）
    public string Error => null;

    //单个属性的错误检查
    public string this[string columnName]
    {
        get
        {
            if (columnName == nameof(Age))
            {
                if (Age < 18)
                    return "年龄必须在18岁以上！";
            }
            return null; // 验证通过返回 null
        }
    }
}
```

第二步就在UI层 (XAML) 开启数据错误验证，只需要在绑定时加上ValidatesOnDataErrors=True即可

```xml
<TextBox Text="{Binding Age, UpdateSourceTrigger=PropertyChanged, ValidatesOnDataErrors=True}" />
```


#### 4、INotifyErrorInfo接口

INotifyDataErrorInfo是WPF比IDataErrorInfo更强大、更现代的数据验证接口。它最大的优势在于支持异步验证、支持单个属性返回多个错误信息，以及支持跨属性验证。

核心成员如下：

- HasErrors 属性：返回一个布尔值，指示当前实体是否包含任何验证错误。
- ErrorsChanged 事件：当某个属性的验证错误状态发生改变时触发，通知 UI 更新。
- GetErrors(string propertyName) 方法：接收属性名，返回该属性对应的错误集合（IEnumerable）。


假设我们在注册时，用户名不能为空，且长度必须大于3；同时，我们需要异步调用后端接口检查用户名是否已被占用。

```c#
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public class RegisterViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
{
    //使用字典集中管理错误信息
    private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    private string _userName;
    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged();
            ValidateUserName(); // 属性改变时触发验证
        }
    }

    //同步验证逻辑
    private void ValidateUserName()
    {
        ClearErrors(nameof(UserName));

        if (string.IsNullOrWhiteSpace(UserName))
            AddError(nameof(UserName), "用户名不能为空。");
        else if (UserName.Length < 3)
            AddError(nameof(UserName), "用户名长度不能少于3个字符。");
        
        //如果通过了基本验证，可以触发异步验证
        if (string.IsNullOrWhiteSpace(UserName) == false && UserName.Length >= 3)
        {
            _ = CheckUserNameAvailabilityAsync();
        }
    }

    //异步验证逻辑（例如调用数据库或API）
    private async Task CheckUserNameAvailabilityAsync()
    {
        // 模拟网络请求
        bool isTaken = await Task.Delay(1000).ContinueWith(_ => UserName == "admin");

        if (isTaken)
        {
            AddError(nameof(UserName), "该用户名已被占用，请更换。");
        }
    }

    #region INotifyDataErrorInfo 核心实现

    public bool HasErrors => _errors.Any();

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public IEnumerable GetErrors(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return _errors.Values.SelectMany(e => e); // 返回所有错误

        return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
    }

    #endregion

    #region 错误管理辅助方法

    private void AddError(string propertyName, string error)
    {
        if (!_errors.ContainsKey(propertyName))
            _errors[propertyName] = new List<string>();

        if (!_errors[propertyName].Contains(error))
        {
            _errors[propertyName].Add(error);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }

    private void ClearErrors(string propertyName)
    {
        if (_errors.Remove(propertyName))
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }

    #endregion

    // INotifyPropertyChanged 基础实现
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
```

使用 INotifyDataErrorInfo 时，XAML 绑定极其简洁。WPF 默认就会自动识别该接口，只需要正常绑定即可（或者显式设置 ValidatesOnNotifyDataErrors=True）

```xml
<TextBox Text="{Binding UserName, UpdateSourceTrigger=PropertyChanged}" />
```


<img width="823" height="296" alt="image" src="https://github.com/user-attachments/assets/2943e16d-41b8-4df6-b9f6-fdd7b71c005d" />


手写INotifyDataErrorInfo的字典管理和事件触发逻辑非常繁琐。在实际企业开发中，建议使用 Prism 框架提供的 ErrorsContainer<T> 类，或者 CommunityToolkit.Mvvm 中的 ObservableValidator 基类。它们已经封装好了底层的字典管理和异步验证逻辑，只需要专注于编写验证规则即可。


### 五、用户控件和自定义控件


#### 1、用户控件UserControl（搭积木）

用户控件本质是 XAML 和后台代码的组合封装。你像设计普通窗口一样，把现有的基础控件（如按钮、文本框、图片等）拖拽组合在一起，形成一个固定的界面模块，UI 结构是固定的，外部只能修改它暴露出来的普通属性（如颜色、文本），这种控件比较简单易用，但是外观上的设计会受到限制。

<img width="937" height="653" alt="image" src="https://github.com/user-attachments/assets/a9d93d78-781c-4c99-b97d-767d03d7c30f" />


假设需要做一个登录界面，用户控件就像是“搭积木”，可以把账号输入框、密码输入框和登录按钮组合在一起，封装成一个独立的模块。现将基础的 TextBox、PasswordBox 和 Button 组合成一个登录面板。

```xml
<UserControl x:Class="Module1.Views.UserControl1"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Border BorderBrush="LightGray" BorderThickness="1" CornerRadius="5" Padding="20">
        <StackPanel Width="300">
            <TextBlock Text="用户登录" FontSize="20" FontWeight="Bold" Margin="0,0,0,15" HorizontalAlignment="Center"/>

            <!-- 账号输入 -->
            <TextBlock Text="账号:" Margin="0,0,0,5"/>
            <TextBox Text="{Binding Username, UpdateSourceTrigger=PropertyChanged}" Height="30" Padding="5"/>

            <!-- 密码输入 -->
            <TextBlock Text="密码:" Margin="0,10,0,5"/>
            <!-- 注意：PasswordBox 不支持直接绑定，通常通过附加属性或事件传递-->
            <PasswordBox x:Name="PwdBox" Height="30" Padding="5"/>

            <!-- 登录按钮 -->
            <Button Content="登 录" 
                    Command="{Binding LoginCommand}" 
                    CommandParameter="{Binding ElementName=PwdBox}"
                    Height="35" Margin="0,20,0,0" Background="#0078D7" Foreground="White"/>
        </StackPanel>
    </Border>
</UserControl>
```


<img width="865" height="415" alt="image" src="https://github.com/user-attachments/assets/c7f31ffb-b878-4c3c-b87d-a0cc1bac2287" />

```c#
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Module1.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        //登录命令
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<object>(ExecuteLogin);
        }

        private void ExecuteLogin(object parameter)
        {
            //从命令参数中获取密码框的值
            if (parameter is PasswordBox pwdBox)
            {
                string password = pwdBox.Password;

                //模拟登录验证逻辑
                if (Username == "admin" && password == "123456")
                {
                    MessageBox.Show("登录成功！");
                    //触发事件通知主窗口进行页面跳转
                }
                else
                {
                    MessageBox.Show("账号或密码错误！");
                }
            }
        }

        //INotifyPropertyChanged 实现...
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
```


封装好之后，就可以像使用原生按钮一样，在任何地方轻松调用这个登录控件了。

```xml
<Window x:Class="YourNamespace.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:YourNamespace">
    
    <Window.DataContext>
        <local:LoginViewModel />
    </Window.DataContext>

    <Grid>
        <!-- 直接像搭积木一样把登录控件放进来 -->
        <local:LoginControl HorizontalAlignment="Center" VerticalAlignment="Center"/>
    </Grid>
</Window>
```


#### 2、自定义控件CustomCtrol（造零件）


该控件继承自 Control 类。它本身是“无皮肤”的，没有默认的 XAML 界面。它的逻辑与外观完全分离，必须依赖 ControlTemplate（控件模板）来定义视觉结构。开发者可以随意改变它的长相，而无需修改任何 C# 逻辑代码。这种控件十分的灵活但是上手难。



<img width="930" height="652" alt="image" src="https://github.com/user-attachments/assets/5c8302bd-8661-4dad-90c7-f0c19b153b30" />


同样以登录界面为例,先创建自定义控件类 (UserLogin.cs),这个类必须继承control,只定义数据（依赖属性）和行为（路由事件），不写任何UI布局代码.

```c#
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public class UserLogin : Control
{
    //静态构造函数：重写默认样式键，让控件去Generic.xaml中寻找外观
    static UserLogin()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(UserLogin), 
            new FrameworkPropertyMetadata(typeof(UserLogin)));
    }

    //定义依赖属性：账号
    public static readonly DependencyProperty UsernameProperty =
        DependencyProperty.Register("Username", typeof(string), typeof(UserLogin), new PropertyMetadata(string.Empty));

    public string Username
    {
        get => (string)GetValue(UsernameProperty);
        set => SetValue(UsernameProperty, value);
    }

    //定义依赖属性：密码
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string), typeof(UserLogin), new PropertyMetadata(string.Empty));

    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    //定义事件：当点击登录按钮时，向外部抛出事件
    public static readonly RoutedEvent LoginClickEvent =
        EventManager.RegisterRoutedEvent("LoginClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserLogin));

    public event RoutedEventHandler LoginClick
    {
        add => AddHandler(LoginClickEvent, value);
        remove => RemoveHandler(LoginClickEvent, value);
    }

    //模板应用：当控件模板被加载时，找到内部的按钮并绑定点击事件
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        if (GetTemplateChild("PART_LoginButton") is Button loginBtn)
        {
            loginBtn.Click -= LoginBtn_Click; // 避免重复绑定
            loginBtn.Click += LoginBtn_Click;
        }
    }

    private void LoginBtn_Click(object sender, RoutedEventArgs e)
    {
        // 触发事件，将控制权交给外部
        RaiseEvent(new RoutedEventArgs(LoginClickEvent, this));
    }
}
```

自定义控件的 UI 必须写在Generic.xaml 文件中。在这里通过 ControlTemplate 将 TextBox、PasswordBox 和 Button 组装起来，并使用 TemplateBinding 与后台的依赖属性进行双向绑定。


```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:local="clr-namespace:YourNamespace">

    <Style TargetType="{x:Type local:UserLogin}">
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="{x:Type local:UserLogin}">
                    <Border BorderBrush="LightGray" BorderThickness="1" CornerRadius="5" Padding="20">
                        <StackPanel Width="300">
                            <TextBlock Text="自定义控件登录" FontSize="20" FontWeight="Bold" Margin="0,0,0,15" HorizontalAlignment="Center"/>
                            
                            <!-- 账号输入：绑定到自定义控件的 Username 属性 -->
                            <TextBlock Text="账号:" Margin="0,0,0,5"/>
                            <TextBox Text="{Binding Username, RelativeSource={RelativeSource TemplatedParent}, UpdateSourceTrigger=PropertyChanged}" Height="30" Padding="5"/>
                            
                            <!-- 密码输入：绑定到自定义控件的 Password 属性 -->
                            <TextBlock Text="密码:" Margin="0,10,0,5"/>
                            <PasswordBox x:Name="PART_PasswordBox" Height="30" Padding="5"/>
                            
                            <!-- 登录按钮：通过 PART_ 命名约定在后台获取 -->
                            <Button x:Name="PART_LoginButton" Content="登 录" 
                                    Height="35" Margin="0,20,0,0" Background="#0078D7" Foreground="White"/>
                        </StackPanel>
                    </Border>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
    </Style>
</ResourceDictionary>
```

最后在主窗口内使用这个自定义控件就可以了

```xml
<Window x:Class="YourNamespace.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:YourNamespace">

    <Grid>
        <!-- 像使用原生控件一样使用自定义登录控件 -->
        <local:UserLogin x:Name="MyLoginControl" 
                         HorizontalAlignment="Center" 
                         VerticalAlignment="Center"
                         LoginClick="MyLoginControl_LoginClick"/>
    </Grid>
</Window>
```
```c#
// MainWindow.xaml.cs
private void MyLoginControl_LoginClick(object sender, RoutedEventArgs e)
{
    // 直接从自定义控件中读取绑定的属性
    string username = MyLoginControl.Username;
    string password = MyLoginControl.Password;

    if (username == "admin" && password == "123456")
    {
        MessageBox.Show("登录成功！");
    }
    else
    {
        MessageBox.Show("账号或密码错误！");
    }
}
```



### 六、布局


WPF的布局控件，本质上是用于管理子元素大小、位置和排列方式的容器类控件。它们不直接显示内容，而是决定内部子控件如何“摆放”和“自适应”，是实现响应式、动态界面的核心基础.

#### 1、stackPanel布局

这个布局类似于叠积木,可以设置为垂直排列和水平排列

```xml
<!--就像叠积木一样，可以设置为垂直排列和水平排列-->
<StackPanel Orientation="Vertical" Background="LightBlue">   
    <Button Content="button1" Height="30" Margin="10"/>
    <Button Content="button2" Height="30" Margin="10"/>
    <Button Content="button3" Height="30" Margin="10"/>
    <TextBox Text="文本框" Margin="10"/>
    <CheckBox Content="选择框" Margin="10"/>
</StackPanel>
```

<img width="900" height="507" alt="image" src="https://github.com/user-attachments/assets/e0f8206f-df9b-450f-86c6-5211cd0f1afa" />



<img width="1007" height="762" alt="image" src="https://github.com/user-attachments/assets/5a2a1963-389f-4114-a2d7-0caa4e455587" />


#### 2、Grid表格布局

这个布局类似于Excel表格,可以自定义行和列

```xml
<Grid>
    <Grid.RowDefinitions>   <!--行定义-->
        <RowDefinition Height="Auto" /> <!--第一行：自动高度-->
        <RowDefinition Height="*"/>     <!--第二行占用剩余所有空间-->
        <RowDefinition Height="50"/>    <!--第三行固定高度50-->
    </Grid.RowDefinitions>

    <Grid.ColumnDefinitions> <!--列定义-->
        <ColumnDefinition Width="100"/>  <!--第一列宽度100-->
        <ColumnDefinition Width="*"/>    <!--第二列占剩余空间（剩余1/3）-->
        <ColumnDefinition Width="2*"/>   <!--第三列占第二列的两倍空间（剩余2/3）-->
    </Grid.ColumnDefinitions>

    <!--标题栏-->
    <!--第一行第一列，列跨度为3-->
    <TextBlock Grid.Row="0" Grid.Column="0" Grid.ColumnSpan="3" 
               Text="设备监控系统" Background="LightBlue"
               FontSize="16" FontWeight="Bold"
               HorizontalAlignment="Center" Padding="10"/>
    
    <!--内嵌stackPanel做侧边栏-->
    <!--第2行第一列-->
    <StackPanel Grid.Row="1" Grid.Column="0" Background="LightGray">
        <Button Content="设备1" Margin="5"/>
        <Button Content="设备2" Margin="5"/>
        <Button Content="设备3" Margin="5"/>
    </StackPanel>
    
    <!--主要内容区-->
    <!--第二行第二列，列跨度为2-->
    <TextBox Grid.Row="1" Grid.Column="1" Grid.ColumnSpan="2"
             Text="红色区域这里是主要区域"
             Background="Red" Margin="5"/>
    
    <!--状态栏-->
    <!--第三行第1列，列跨度为3-->
    <TextBox Grid.Row="2" Grid.Column="0" Grid.ColumnSpan="3"
             Text="黄色区域这里是状态栏"
             Background="Yellow" />
</Grid>
```

<img width="874" height="495" alt="image" src="https://github.com/user-attachments/assets/87e258b5-f072-4c06-bff3-e57e65278e25" />


#### 3、DockPanel停靠布局

停靠布局就是将某个区域贴着某个方向,然后一个大区域自动填充剩余部分

```xml
<DockPanel LastChildFill="True">
    <!--停靠顶部的菜单-->
    <Menu DockPanel.Dock="Top" Background="LightBlue">
        <MenuItem Header="文件">   <!--文件按钮里面折叠了新建和打开-->
            <MenuItem Header="新建"/>
            <MenuItem Header="打开"/>
        </MenuItem>
        <MenuItem Header="编辑"/>
    </Menu>
    
    <!--停靠底部的菜单-->
    <StatusBar DockPanel.Dock="Bottom" Background="LightGreen">
        <StatusBarItem>
            <TextBox Text="状态：就绪"/>
        </StatusBarItem>
    </StatusBar>
    
    <!--停靠左侧的工具栏-->
    <StackPanel DockPanel.Dock="Left" Width="80" Background="LightYellow">
        <Button Content="工具1" Margin="5"/>
        <Button Content="工具2" Margin="5"/>
        <Button Content="工具3" Margin="5"/>
    </StackPanel>
    
    <!--主要内容区就自动填充剩余空间了-->
    <TextBox Text="主要区域"
             Background="Blue" TextWrapping="Wrap"/>

</DockPanel>
```

<img width="879" height="506" alt="image" src="https://github.com/user-attachments/assets/3b3cbbdb-85c4-492c-9c56-2e5eec6c62dc" />


#### 4、WarpPanel智能换行布局

效果和名字一样,从左往右,第一行满了就自动换行,像word文档一样

```xml
    <Grid>
        <WrapPanel Orientation="Horizontal">
            <Button Content="按钮1" Width="80" Height="30" Margin="5"/>
            <Button Content="按钮2" Width="80" Height="30" Margin="5"/>
            <Button Content="按钮3" Width="80" Height="30" Margin="5"/>
            <Button Content="按钮4" Width="80" Height="30" Margin="5"/>
            <Button Content="按钮5" Width="80" Height="30" Margin="5"/>
            <Button Content="按钮6" Width="80" Height="30" Margin="5"/>
            <Button Content="按钮7" Width="80" Height="30" Margin="5"/>
            <Button Content="按钮8" Width="80" Height="30" Margin="5"/>
            <Button Content="按钮9" Width="80" Height="30" Margin="5"/>
        </WrapPanel>
    </Grid>
```


<img width="928" height="511" alt="image" src="https://github.com/user-attachments/assets/d5d36423-1a2b-4ad9-94d2-d75782b1347c" />



#### 5、Canvas绝对定位布局


这个布局里的每一个空间都需要输入距离上下左右的相对距离.

```xml
<Canvas Background="AliceBlue">
    <!--比如按钮1举例左侧50远，距离顶部30远，距离宽度80远，距离高度30远-->
    <Button Content="按钮1" Canvas.Left="50" Canvas.Top="30" 
            Width="80" Height="30"/>
    <Button Content="按钮2" Canvas.Left="150" Canvas.Top="80" 
    Width="80" Height="30"/>
    <TextBox Text="文本框" Canvas.Left="80" Canvas.Top="150"
             Width="120" Height="25"/>
</Canvas>
```


<img width="893" height="479" alt="image" src="https://github.com/user-attachments/assets/6deb2b62-bda9-4ab8-bcd9-c23c17ed4923" />





### 七、基础控件

#### 1、Button按钮控件

```xml
<StackPanel>
    <!--基本按钮包括大小和边缘间隔-->
    <Button Content="基本按钮" Height="30" Margin="5"/>
    <!--带样式的按钮可以多加背景色、字体颜色、字体粗细度-->
    <Button Content="带样式的按钮" Background="Red" Foreground="Wheat" 
            FontWeight="Bold" Height="30" Margin="5"/>
    <!--带图标的按钮可以插入图标-->
    <Button Height="40" Margin="5">
        <StackPanel Orientation="Horizontal">
            <Image Source="play.png" Width="20" Height="20"/>
            <TextBlock Text="带图标的按钮" Margin="10,0,0,0"/>
        </StackPanel>
    </Button>
    <!--绑定点击事件的按钮-->
    <Button Content="绑定事件" Click="点击事件方法名" Margin="5"/>
</StackPanel>
```


<img width="846" height="484" alt="image" src="https://github.com/user-attachments/assets/6d7dc1c3-9dad-45fa-b161-304c93cf97d3" />



#### 2、TextBox和TextBlock文本显示和输入


```xml
<StackPanel Margin="10">
    <!--TextBlock用于显示文本，用户不能编辑-->
    <TextBlock Text="设备状态监控" FontSize="16" FontWeight="Bold"/>
    <TextBlock Text="当前状态：运行中" Foreground="Green" Margin="0,5,0,5"/>
    
    <!--TextBox用户可以编辑的文本框-->
    <TextBlock Text="IP地址" Margin="0,10,0,5"/>
    <TextBox x:Name="txtIpaddress" Text="192.168.0.0" Height="25"/>

    <TextBlock Text="端口号" Margin="0,10,0,5"/>
    <TextBox x:Name="txtPort" Text="503" Height="25"/>
    
    <!--多行文本框-->
    <TextBlock Text="日志信息" Margin="0,10,0,5"/>
    <TextBlock x:Name="txtLog" Height="80" TextWrapping="Wrap"
              VerticalAlignment="Center"/>
</StackPanel>
```

<img width="826" height="473" alt="image" src="https://github.com/user-attachments/assets/8b643a5d-89e0-4745-bf69-d04cabb9f087" />



#### 3、下拉选择框ComboBox

```xml
<StackPanel Margin="10">
    <TextBlock Text="选择设备" FontWeight="Bold"/>
    <ComboBox x:Name="cmbDevice" Height="25" Margin="0,5,0,10"
              SelectionChanged="cmbDevice_SelectionChanged">
        <ComboBoxItem Content="设备1"/>
        <ComboBoxItem Content="设备2"/>
        <ComboBoxItem Content="设备3"/>
        <ComboBoxItem Content="设备4"/>
    </ComboBox>
</StackPanel>
```

<img width="779" height="312" alt="image" src="https://github.com/user-attachments/assets/351c0e1f-431b-4bc9-826a-380e00516547" />



#### 4、CheckBox和RadioButton选择控件

```xml
<StackPanel Margin="10">
    <TextBlock Text="设备配置" FontSize="14" FontWeight="Bold" Margin="0,0,0,10"/>

    <!-- CheckBox: 多选框，可以同时选多个 -->
    <TextBlock Text="启用功能：" Margin="0,0,0,5"/>
    <CheckBox Content="自动启动" x:Name="chkAutoStart" Margin="5"/>
    <CheckBox Content="报警提示" x:Name="chkAlarm" Margin="5" IsChecked="True"/>
    <CheckBox Content="数据记录" x:Name="chkDataLog" Margin="5"/>

    <!-- RadioButton: 单选框，只能选一个 -->
    <TextBlock Text="运行模式：" Margin="0,15,0,5"/>
    <RadioButton Content="手动模式" x:Name="rdoManual" GroupName="Mode" Margin="5" IsChecked="True"/>
    <RadioButton Content="半自动模式" x:Name="rdoSemiAuto" GroupName="Mode" Margin="5"/>
    <RadioButton Content="全自动模式" x:Name="rdoFullAuto" GroupName="Mode" Margin="5"/>

    <Button Content="保存配置" Click="SaveConfig_Click" Margin="0,20,0,0" Height="30"/>
</StackPanel>
```


<img width="822" height="470" alt="image" src="https://github.com/user-attachments/assets/3c9d1a01-9a98-42f8-bf10-f184e51d9dde" />


#### 5、进度条ProgressBar

```xml
<StackPanel Margin="20" VerticalAlignment="Center">

    <TextBlock Text="下载进度：" FontSize="16" Margin="0,0,0,5"/>

    <!-- Maximum: 总进度 (例如 100%) -->
    <!-- Value: 当前进度 (例如 45%) -->
    <!-- Height: 设置高度让进度条更明显 -->
    <ProgressBar x:Name="myProgressBar"
             Minimum="0"
             Maximum="100"
             Value="45"
             Height="25"
             Foreground="#2196F3" />

    <!-- IsIndeterminate="True": 当不知道具体要多久完成时，进度条会显示循环滚动动画 -->
    <TextBlock Text="正在连接服务器..." Margin="0,20,0,5"/>
    <ProgressBar IsIndeterminate="True"
             Height="10"
             Margin="0,0,0,20"/>

    <!-- 带文本显示的进度条 (组合控件) -->
    <Grid>
        <!-- 底层放一个进度条 -->
        <ProgressBar Minimum="0" Maximum="100" Value="75" Height="30" />
        <!-- 上层放一个文本块，居中显示文字 -->
        <TextBlock Text="75%"
               HorizontalAlignment="Center"
               VerticalAlignment="Center"
               FontWeight="Bold"
               Foreground="White"/>
    </Grid>

</StackPanel>
```



<img width="840" height="472" alt="image" src="https://github.com/user-attachments/assets/fbcbac70-b24e-4ccc-91de-c30d92141898" />



### 八、Style样式


Style相当于一个属性设置的集合,设置好一个统一的属性后,可以运用到多个控件上.

#### 1、定义基本样式

```xml
<Window.Resources>
    <!-- 定义一个按钮样式 -->
    <Style x:Key="MyButtonStyle" TargetType="Button">
        <Setter Property="Background" Value="LightBlue"/>
        <Setter Property="Foreground" Value="DarkBlue"/>
        <Setter Property="FontSize" Value="14"/>
        <Setter Property="FontWeight" Value="Bold"/>
        <Setter Property="Width" Value="120"/>
        <Setter Property="Height" Value="35"/>
        <Setter Property="Margin" Value="5"/>
    </Style>
</Window.Resources>

<StackPanel Margin="20">
    <!-- 使用样式 -->
    <Button Content="按钮1" Style="{StaticResource MyButtonStyle}"/>
    <Button Content="按钮2" Style="{StaticResource MyButtonStyle}"/>
    <Button Content="按钮3" Style="{StaticResource MyButtonStyle}"/>
</StackPanel>
```


#### 2、样式的继承

继承就是在原有的样式中根据实际不同的情况新增样式.用BasedOn关键字来继承(类似于C#中的:)

```xml
<Window.Resources>
    <!--基础按钮样式-->
    <Style x:Key="BaseButtonStyle" TargetType="Button">
        <Setter Property="FontSize" Value="14"/>
        <Setter Property="FontWeight" Value="Bold"/>
        <Setter Property="Width" Value="120"/>
        <Setter Property="Height" Value="35"/>
        <Setter Property="Margin" Value="5"/>
    </Style>

    <!--成功按钮样式，继承基础样式-->
    <Style x:Key="SuccessButtonStyle" BasedOn="{StaticResource BaseButtonStyle}" TargetType="Button">
        <Setter Property="Background" Value="LightGreen"/>
        <Setter Property="Foreground" Value="DarkGreen"/>
        <Setter Property="BorderThickness" Value="2"/>
        <Setter Property="BorderBrush" Value="DarkGreen"/>
    </Style>

    <!--警告按钮样式，继承基础样式-->
    <Style x:Key="WarningButtonStyle" BasedOn="{StaticResource BaseButtonStyle}" TargetType="Button">
        <Setter Property="Background" Value="LightYellow"/>
        <Setter Property="Foreground" Value="DarkOrange"/>
        <Setter Property="BorderThickness" Value="2"/>
        <Setter Property="BorderBrush" Value="DarkOrange"/>
    </Style>

    <!--危险按钮样式，继承基础样式-->
    <Style x:Key="DangerButtonStyle" BasedOn="{StaticResource BaseButtonStyle}" TargetType="Button">
        <Setter Property="Background" Value="LightCoral"/>
        <Setter Property="Foreground" Value="DarkRed"/>
        <Setter Property="BorderThickness" Value="2"/>
        <Setter Property="BorderBrush" Value="DarkRed"/>
    </Style>
</Window.Resources>
```

#### 3、隐式样式

这种样式会自动应用到指定类型的所有控件上，无需手动设置 Style 属性.


```xml
<Window.Resources>
    <!-- 隐式样式：所有按钮都会应用这个样式 -->
    <Style TargetType="Button">
        <Setter Property="Background" Value="LightBlue"/>
        <Setter Property="Foreground" Value="DarkBlue"/>
        <Setter Property="FontSize" Value="14"/>
        <Setter Property="Margin" Value="5"/>
    </Style>
</Window.Resources>

<StackPanel Margin="20">
    <!-- 这些按钮会自动应用上面的隐式样式 -->
    <Button Content="按钮1"/>
    <Button Content="按钮2"/>
    <Button Content="按钮3"/>

    <!-- 如果某个按钮不想用隐式样式，可以显式设置 Style 为 {x:Null} -->
    <Button Content="特殊按钮" Style="{x:Null}" Background="Red" Foreground="White"/>
</StackPanel>
```


### 九、Template模板


样式是用来统一控件外观的，模板是则是用来完全重写控件外观的.可以保持界面的统一,实现很好的动画效果.主要的template有以下这些：

- DataTemplate：决定数据内容怎么展示,和UI无关
- ItemsPanelTemplate：决定容器的排列方式是横着排还是竖着排
- ControlTemplate：决定控件本身长什么样

DataTemplate如下
```xml
<!-- 假设有一个 Employee 类，包含 Name 和 Role 属性 -->
<Window.Resources>
    <!-- 定义数据模板：规定 Employee 对象在界面上如何呈现 -->
    <DataTemplate x:Key="EmployeeDataTemplate">
        <StackPanel Orientation="Horizontal" Margin="5">
            <!-- 名字加粗 -->
            <TextBlock Text="{Binding Name}" FontWeight="Bold" Foreground="#333"/>
            <TextBlock Text=" - " Margin="5,0"/>
            <!-- 职位灰色 -->
            <TextBlock Text="{Binding Role}" Foreground="Gray"/>
        </StackPanel>
    </DataTemplate>
</Window.Resources>

<!-- 使用：ListBox 会自动对集合中的每个对象应用这个模板 -->
<ListBox ItemTemplate="{StaticResource EmployeeDataTemplate}">
    <local:Employee Name="张三" Role="经理"/>
    <local:Employee Name="李四" Role="程序员"/>
</ListBox>
```
ItemsPanelTemplate如下
```xml
<Window.Resources>
    <!-- 接上面的 DataTemplate... -->

    <!-- 定义面板模板：让列表项横向排列，而不是默认的竖向 -->
    <ItemsPanelTemplate x:Key="HorizontalPanelTemplate">
        <StackPanel Orientation="Horizontal"/>
    </ItemsPanelTemplate>
</Window.Resources>

<!-- 使用：结合上面的 DataTemplate -->
<ListBox ItemTemplate="{StaticResource EmployeeDataTemplate}"
         ItemsPanel="{StaticResource HorizontalPanelTemplate}">
    <local:Employee Name="张三" Role="经理"/>
    <local:Employee Name="李四" Role="程序员"/>
    <local:Employee Name="王五" Role="设计师"/>
</ListBox>
```
ControlTemplate如下
```xml
<Window.Resources>
    <!-- 接上面的 DataTemplate 和 ItemsPanelTemplate... -->

    <!-- 定义控件模板：彻底改变 ListBox 的外观 -->
    <ControlTemplate x:Key="CustomListBoxTemplate" TargetType="ListBox">
        <!-- 这里的 Border 取代了 ListBox 默认的边框 -->
        <Border Background="Transparent" CornerRadius="10">
            <!-- ScrollViewer 提供滚动功能 -->
            <ScrollViewer CanContentScroll="True">
                <!-- ItemsPresenter 是一个占位符，表示“列表项将在这里渲染” -->
                <ItemsPresenter />
            </ScrollViewer>
        </Border>
    </ControlTemplate>

    <!-- 顺便定义一下选中时的样式（ItemContainerStyle），配合 ControlTemplate 使用效果更好 -->
    <Style x:Key="CustomListBoxItemStyle" TargetType="ListBoxItem">
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="ListBoxItem">
                    <Border x:Name="Bd" Padding="5" Margin="2" CornerRadius="5" Background="White">
                        <ContentPresenter /> <!-- 这里显示 DataTemplate 定义的内容 -->
                    </Border>
                    <ControlTemplate.Triggers>
                        <!-- 当被选中时，改变背景色 -->
                        <Trigger Property="IsSelected" Value="True">
                            <Setter TargetName="Bd" Property="Background" Value="#2196F3"/>
                            <Setter TargetName="Bd" Property="TextElement.Foreground" Value="White"/>
                        </Trigger>
                    </ControlTemplate.Triggers>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
    </Style>
</Window.Resources>

<!-- 最终成品：应用所有模板 -->
<ListBox ItemTemplate="{StaticResource EmployeeDataTemplate}"
         ItemsPanel="{StaticResource HorizontalPanelTemplate}"
         Template="{StaticResource CustomListBoxTemplate}"
         ItemContainerStyle="{StaticResource CustomListBoxItemStyle}">
    <local:Employee Name="张三" Role="经理"/>
    <local:Employee Name="李四" Role="程序员"/>
    <local:Employee Name="王五" Role="设计师"/>
</ListBox>
```


<img width="779" height="522" alt="image" src="https://github.com/user-attachments/assets/2a7d74d0-a54d-4990-9a11-ab7424f321dd" />


### 十、Trigger触发器

这东西就是一个条件响应的系统,当数据的变化达到某种条件的时候会触发执行这个,改变UI的样式和行为.这东西可以提供完美的视觉反馈效果.


- Property Trigger（属性触发器）：监听控件自身的依赖属性（如 IsMouseOver, IsPressed, IsEnabled）,当属性值改变时，自动应用样式。
- Data Trigger（数据触发器）：监听绑定的数据对象（ViewModel）的属性。当数据发生变化时，UI 自动响应。
- Event Trigger（事件触发器）：监听路由事件（如 MouseEnter, MouseLeave, Loaded）。它通常不直接修改属性，而是启动动画（Storyboard）


Property Trigger举例如下
```xml
<!-- 监听 IsMouseOver 属性 -->
<Trigger Property="IsMouseOver" Value="True">
    <Setter Property="Background" Value="#E0F7FA"/> <!-- 鼠标悬停时变浅蓝色 -->
    <Setter Property="BorderBrush" Value="#00BCD4"/>
</Trigger>
```

Data Trigger举例如下
```xml
<!-- 假设绑定的数据对象有一个 IsVIP 属性 -->
<DataTrigger Binding="{Binding IsVIP}" Value="True">
    <Setter Property="BorderBrush" Value="Gold"/>
    <Setter Property="BorderThickness" Value="3"/>
    <Setter Property="ToolTip" Value="尊贵的VIP用户"/>
</DataTrigger>
```

Event Trigger举例如下
```xml
<!-- 监听 MouseEnter 事件,鼠标移入时，扩大卡片大小 -->
<EventTrigger RoutedEvent="MouseEnter">
    <BeginStoryboard>
        <Storyboard>
            <!-- 在 0.2 秒内，将 ScaleX 和 ScaleY 平滑放大到 1.05 倍 -->
            <DoubleAnimation Storyboard.TargetProperty="(RenderTransform).(ScaleTransform.ScaleX)" To="1.05" Duration="0:0:0.2"/>
            <DoubleAnimation Storyboard.TargetProperty="(RenderTransform).(ScaleTransform.ScaleY)" To="1.05" Duration="0:0:0.2"/>
        </Storyboard>
    </BeginStoryboard>
</EventTrigger>
<!--鼠标移出时，恢复卡片大小 -->
<EventTrigger RoutedEvent="MouseLeave">
    <BeginStoryboard>
        <Storyboard>
            <DoubleAnimation Storyboard.TargetProperty="(RenderTransform).(ScaleTransform.ScaleX)" To="1" Duration="0:0:0.2"/>
            <DoubleAnimation Storyboard.TargetProperty="(RenderTransform).(ScaleTransform.ScaleY)" To="1" Duration="0:0:0.2"/>
        </Storyboard>
    </BeginStoryboard>
</EventTrigger>
```

组合举例:
```xml
<Window x:Class="WpfApp1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="WPF 触发器学习示例" Height="400" Width="600">

    <Window.Resources>
        <!-- 定义一个综合了三种触发器的卡片样式 -->
        <Style x:Key="InteractiveCardStyle" TargetType="Border">
            <!-- 默认状态：必须有 RenderTransform 才能做缩放动画 -->
            <Setter Property="RenderTransform">
                <Setter.Value>
                    <ScaleTransform ScaleX="1" ScaleY="1"/>
                </Setter.Value>
            </Setter>
            <Setter Property="Background" Value="White"/>
            <Setter Property="BorderBrush" Value="LightGray"/>
            <Setter Property="BorderThickness" Value="1"/>
            <Setter Property="CornerRadius" Value="8"/>
            <Setter Property="Padding" Value="20"/>
            <Setter Property="Margin" Value="10"/>

            <Style.Triggers>
                <!-- 1. Property Trigger: 鼠标悬停时改变颜色 -->
                <Trigger Property="IsMouseOver" Value="True">
                    <Setter Property="Background" Value="#FFF9C4"/>
                    <Setter Property="Cursor" Value="Hand"/>
                </Trigger>

                <!-- 2. Data Trigger: 当绑定的 IsVIP 为 True 时，改变边框 -->
                <!-- 注意：这里假设 DataContext 中有一个 IsVIP 属性 -->
                <DataTrigger Binding="{Binding IsVIP}" Value="True">
                    <Setter Property="BorderBrush" Value="Gold"/>
                    <Setter Property="BorderThickness" Value="3"/>
                </DataTrigger>

                <!-- 3. Event Trigger: 鼠标移入时，放大卡片 -->
                <EventTrigger RoutedEvent="MouseEnter">
                    <BeginStoryboard>
                        <Storyboard>
                            <DoubleAnimation Storyboard.TargetProperty="(RenderTransform).(ScaleTransform.ScaleX)" To="1.05" Duration="0:0:0.2"/>
                            <DoubleAnimation Storyboard.TargetProperty="(RenderTransform).(ScaleTransform.ScaleY)" To="1.05" Duration="0:0:0.2"/>
                        </Storyboard>
                    </BeginStoryboard>
                </EventTrigger>

                <!-- 3. Event Trigger: 鼠标移出时，恢复卡片大小 -->
                <EventTrigger RoutedEvent="MouseLeave">
                    <BeginStoryboard>
                        <Storyboard>
                            <DoubleAnimation Storyboard.TargetProperty="(RenderTransform).(ScaleTransform.ScaleX)" To="1" Duration="0:0:0.2"/>
                            <DoubleAnimation Storyboard.TargetProperty="(RenderTransform).(ScaleTransform.ScaleY)" To="1" Duration="0:0:0.2"/>
                        </Storyboard>
                    </BeginStoryboard>
                </EventTrigger>
            </Style.Triggers>
        </Style>
    </Window.Resources>

    <Grid Background="#F5F5F5">
        <StackPanel VerticalAlignment="Center" HorizontalAlignment="Center">

            <!-- 普通卡片 -->
            <Border Style="{StaticResource InteractiveCardStyle}" Width="300">
                <TextBlock Text="普通用户卡片" FontSize="18" HorizontalAlignment="Center"/>
            </Border>

            <!-- VIP 卡片 (通过后台代码设置 DataContext 触发 DataTrigger) -->
            <Border x:Name="VipCard" Style="{StaticResource InteractiveCardStyle}" Width="300">
                <TextBlock Text="VIP 尊贵卡片" FontSize="18" HorizontalAlignment="Center" Foreground="DarkOrange"/>
            </Border>

            <!-- 测试按钮：点击切换 VIP 状态，观察 DataTrigger 效果 -->
            <Button Content="切换 VIP 状态" Width="150" Height="35" Margin="0,20,0,0" Click="ToggleVip_Click"/>
        </StackPanel>
    </Grid>
</Window>
```

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows;

namespace WpfApp1 // 确保命名空间与你的项目一致
{
    //定义一个简单的数据类
    public class UserViewModel
    {
        public bool IsVIP { get; set; }
    }

    public partial class MainWindow : Window
    {
        private UserViewModel _user;

        public MainWindow()
        {
            InitializeComponent();

            //初始化数据并绑定到 VIP 卡片
            _user = new UserViewModel { IsVIP = false };
            VipCard.DataContext = _user;
        }

        //点击按钮切换数据，观察 UI 自动响应
        private void ToggleVip_Click(object sender, RoutedEventArgs e)
        {
            _user.IsVIP = !_user.IsVIP;

            // 注意：为了让 DataTrigger 实时生效，UserViewModel 最好实现 INotifyPropertyChanged
            // 这里为了演示简单，直接重新赋值 DataContext 强制刷新
            VipCard.DataContext = null;
            VipCard.DataContext = _user;
        }
    }
}
```

<img width="584" height="392" alt="image" src="https://github.com/user-attachments/assets/ae0ba9b8-9257-4a5f-980a-69e5a894142e" />



### 十一 Converter









