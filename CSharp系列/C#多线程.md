# 一、多线程基本概念

```c#
// 多线程的命名空间
using System.Threading;       // 基础线程操作
using System.Threading.Tasks; // 核心
```
>说明：传统Thread类不经常用，引入System.Threading即可，日常开发优先用Task。

>多线程用途：
- 串口、网口、Modbus通信（不阻塞UI，保证界面流畅）
- PLC、传感器数据采集（后台循环采集，不影响界面操作）
- 日志保存、数据库读写（耗时操作后台执行）


# 二、多线程核心内容

## 1、Task

Task表示一个正在执行的线程，可以等待它完成、取它的返回值，或在它之后做别的事。

```c#
using System;
using System.Threading.Tasks;
class Program
{
    static void Main()
    {
        // Task.Run(lambda表达式): 开启一个后台线程
        Task t = Task.Run(() =>
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"后台工作中... {i}");
                Task.Delay(500).Wait(); // 模拟耗时
            }
        });

        Console.WriteLine("主线程继续干别的事...");
        t.Wait(); // 等待后台任务完成
        Console.WriteLine("任务结束");
    }
}
```

- Task.Run(() => { ... }) — 把一个 Lambda 丢到线程池去跑
- t.Wait() — 阻塞当前线程直到任务完成（实际上用await比较多）
- Task的TResult泛型版：Task<int> 表示任务完成后会返回一个int


## 2、异步async、await


async标记方法为异步方法，await等待一个Task完成但不阻塞当前线程。

```c#
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("开始做饭");

        // 异步煮饭，CookRiceAsync()在后台运行不阻塞主进程
        Task riceTask = CookRiceAsync();

        //煮饭的同时主线程在切菜
        Console.WriteLine("煮饭的同时切菜...");
        await Task.Delay(1500);
        Console.WriteLine("菜切好了");

        // 等饭煮好
        await riceTask;
        Console.WriteLine("开饭！");
    }

    static async Task CookRiceAsync()
    {
        Console.WriteLine("开始煮饭");
        await Task.Delay(2000); // 煮饭需要 2 秒，但不阻塞
        Console.WriteLine("饭煮好了");
    }
}
```

注意：
- async方法应返回Task或Task<T>，只有事件处理器（如按钮点击）可以返回 void
- await只能在async方法里用
- await后面跟Task对象
- async void尽量别用，因为异常无法捕获，调试困难



## 3、跨线程访问UI控件

UI控件由UI主线程创建，Windows消息机制规定：只有创建控件的线程才能操作它。后台线程（Task、Thread、Timer 回调）直接修改控件会抛异常<br>

#### winform中的跨线程访问

Invoke同步调用：
```c#
//假设后台线程需要更新textBox1的文本值
Task.Run(() =>
{
    int result = HeavyCalculation();

    //同步等待 UI 更新完成
    textBox1.Invoke(new Action(() =>
    {
        textBox1.Text = result.ToString();
    }));

    // 到这里 UI 已更新
});
```

BeginInvoke异步调用：
``c#
Task.Run(() =>
{
    int result = HeavyCalculation();

    // 异步丢给 UI 线程，不阻塞
    textBox1.BeginInvoke(new Action(() =>
    {
        textBox1.Text = result.ToString();
    }));
    // 不阻塞，继续执行
});
```

带返回值的Invoke
```c#
// 在 UI 线程取一个值，返回到后台线程
string text = (string)textBox1.Invoke(new Func<string>(() =>
{
    return textBox1.Text;
}));
```

Demo
```c#
private void btnStart_Click(object sender, EventArgs e)
{
    Task.Run(async () =>
    {
        for (int i = 1; i <= 10; i++)
        {
            await Task.Delay(500);

            // InvokeRequired 写法
            if (txtStatus.InvokeRequired)
            {
                txtStatus.Invoke(new Action(() =>
                {
                    txtStatus.Text = $"采集进度: {i * 10}%";
                }));
            }
            else
            {
                txtStatus.Text = $"采集进度: {i * 10}%";
            }
        }

        txtStatus.Invoke(new Action(() =>
        {
            txtStatus.Text = "采集完成";
        }));
    });
}
```


#### WPF中的跨线程访问

WPF 中，每个UI线程有一个 Dispatcher 对象，通过它把操作封送到UI线程执行。<br>

Dispatcher.Invoke同步调用：
```c#
// 后台线程
Task.Run(() =>
{
    int result = HeavyCalculation();

    // 等待 UI 线程执行完，才继续往下走
    Application.Current.Dispatcher.Invoke(() =>
    {
        txtResult.Text = result.ToString();
        progressBar.Value = 100;
    });

    // 到这里 UI 已经更新完了
    Log("更新完成");
});
```
Dispatcher.BeginInvoke异步调用：
```c#
// 后台线程
Task.Run(() =>
{
    int result = HeavyCalculation();

    // 丢给 UI 线程，自己不等，直接继续
    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
    {
        txtResult.Text = result.ToString();
    }));

    // 不等 UI 更新，直接执行下一行
    // （可能 UI 还没更新）
});
```

Dispatcher.InvokeAsync异步且可以await
```c#
// 后台线程里 await，等 UI 更新完再继续
await Application.Current.Dispatcher.InvokeAsync(() =>
{
    txtResult.Text = "更新完成";
});
// 到这里 UI 确实已更新
```


常见写法：
```c#
// 方式 A：全局 Application Dispatcher
Application.Current.Dispatcher.Invoke(() => { ... });

// 方式 B：控件的 Dispatcher（更推荐，不用判空）
txtMessage.Dispatcher.Invoke(() =>
{
    txtMessage.Text = "新消息";
});

// 方式 C：当前 UI 线程的 Dispatcher（在 Window/UserControl 里用）
this.Dispatcher.Invoke(() => { ... });
```


Demo：
```xml
<Window x:Class="WpfDemo.MainWindow" ...>
    <StackPanel>
        <TextBlock x:Name="txtStatus" FontSize="20"/>
        <Button Content="开始采集" Click="StartBtn_Click"/>
    </StackPanel>
</Window>
```

```c#
private void StartBtn_Click(object sender, RoutedEventArgs e)
{
    Task.Run(async () =>
    {
        for (int i = 1; i <= 10; i++)
        {
            //模拟采集数据
            await Task.Delay(500);

            //丢回UI线程更新
            txtStatus.Dispatcher.Invoke(() =>
            {
                txtStatus.Text = $"采集进度: {i * 10}%";
            });
        }

        txtStatus.Dispatcher.Invoke(() =>
        {
            txtStatus.Text = "采集完成";
        });
    });
}
```



## 4、线程安全lock

多线程同时读写同一个变量（如采集数据缓存、全局配置）、同时操作同一个资源（如日志文件、串口），会出现数据错乱、程序崩溃。

```c#
//定义一个锁对象（全局唯一，不能是值类型）
private readonly object _lockObj = new object();
private int _count = 0; // 共享变量

//多线程同时调用此方法，不会出现数据错乱
private void AddCount()
{
    lock (_lockObj) //lock锁定代码块，同一时间只有一个线程执行
    {
        _count++;
        Console.WriteLine($"当前计数：{_count}");
    }
}
```


## 5、线程取消
>场景：点击“停止采集”“断开连接”、关闭窗口时，安全退出后台线程（避免线程占用资源、程序无法正常关闭）。
>CancellationToken（取消令牌）

```c#
// 定义取消令牌源（全局，方便随时取消）
private CancellationTokenSource _cts;

// 开启采集线程（支持取消）
private async void btnStartCollect_Click(object sender, EventArgs e)
{
    _cts = new CancellationTokenSource();
    var token = _cts.Token;

    await Task.Run(() => 
    {
        // 循环采集，每次循环判断是否需要取消
        while (!token.IsCancellationRequested)
        {
            // 模拟采集数据
            var data = "采集到的数据";
            UpdateUIFromThread(data);
            Thread.Sleep(1000);
        }
        // 取消后执行的清理操作（如关闭串口、释放资源）
        Console.WriteLine("采集线程已安全退出");
    }, token);
}

// 停止采集（取消线程）
private void btnStopCollect_Click(object sender, EventArgs e)
{
    if (_cts != null && !_cts.IsCancellationRequested)
    {
        _cts.Cancel(); // 发送取消信号
        _cts.Dispose(); // 释放资源
    }
}
```

## 6、定时器（非严格线程，但依赖线程）
>用途：定时读取PLC、定时刷新界面曲线、定时保存日志、定时校验设备状态。
>必学两种定时器：
### （1）System.Timers.Timer（后台定时器，适合非UI操作）

```c#
using System.Timers; // 需引入

private Timer _dataTimer;

// 初始化定时器（定时1秒采集一次）
private void InitTimer()
{
    _dataTimer = new Timer(1000); // 间隔1000ms（1秒）
    _dataTimer.Elapsed += (s, e) => 
    {
        // 定时执行的操作（如采集PLC数据）
        var data = "定时采集的数据";
        UpdateUIFromThread(data);
    };
    _dataTimer.Start(); // 启动定时器
}

// 停止定时器
private void StopTimer()
{
    if (_dataTimer != null)
    {
        _dataTimer.Stop();
        _dataTimer.Dispose();
    }
}
```

### （2）DispatcherTimer（WPF专用，适合UI相关定时操作）
```c#
using System.Windows.Threading; // 需引入

private DispatcherTimer _uiTimer;

// 初始化WPF定时器（定时刷新UI）
private void InitUITimer()
{
    _uiTimer = new DispatcherTimer();
    _uiTimer.Interval = TimeSpan.FromSeconds(1);
    _uiTimer.Tick += (s, e) => 
    {
        // 直接更新UI，无需跨线程（DispatcherTimer运行在UI线程）
        txtTime.Text = DateTime.Now.ToString("HH:mm:ss");
    };
    _uiTimer.Start();
}
```




