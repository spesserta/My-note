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



















