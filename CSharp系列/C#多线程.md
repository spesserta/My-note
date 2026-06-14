# 一、命名空间

```c#
// 多线程的命名空间
using System.Threading;       // 基础线程操作
using System.Threading.Tasks; // 核心
```
>说明：传统Thread类不经常用，引入System.Threading即可，日常开发优先用Task。


# 二、多线程核心内容

## 1、1. Task/async await

>上位机开发最常用的多线程方式，替代传统Thread，简洁易维护，避免界面卡死。

操作：
- 用Task.Run()开启后台线程（执行耗时操作，如通信、数据采集）
- 用async/await编写异步方法（方法前加async，耗时操作前加await）
- 任务等待：Wait()（同步等待）、WhenAll()（等待所有任务完成）、WhenAny()（等待任一任务完成）
- 禁忌：不要在async方法中同步死等（如Task.Wait()），会导致界面卡死

>上位机用途：
- 串口、网口、Modbus通信（不阻塞UI，保证界面流畅）
- PLC、传感器数据采集（后台循环采集，不影响界面操作）
- 日志保存、数据库读写（耗时操作后台执行）


```c#
// 异步采集数据方法（async/await）
private async void btnStartCollect_Click(object sender, EventArgs e)
{
    // 开启后台线程执行采集，不卡界面
    await Task.Run(() => 
    {
        // 模拟PLC数据采集（循环采集）
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(1000); // 模拟采集耗时
            var data = $"采集到数据：{i}";
            // 后续需跨线程更新UI（见下文2）
        }
    });
    MessageBox.Show("采集完成");
}
```

## 2. 跨线程访问UI控件
>核心原则：子线程（后台采集、通信线程）不能直接修改UI控件（如TextBox、Label），会报线程安全异常。<br>
>两种场景实现（对应上位机常用框架）：
### （1）WinForm

```c#
// 方法1：Control.Invoke（同步更新UI）
private void UpdateUIFromThread(string data)
{
    // 判断是否跨线程，是则调用Invoke
    if (txtData.InvokeRequired)
    {
        txtData.Invoke(new Action(() => 
        {
            txtData.Text += data + Environment.NewLine;
        }));
    }
    else
    {
        txtData.Text += data + Environment.NewLine;
    }
}

// 方法2：Control.BeginInvoke（异步更新UI，不阻塞子线程，推荐）
private void UpdateUIFromThreadAsync(string data)
{
    if (txtData.InvokeRequired)
    {
        txtData.BeginInvoke(new Action(() => 
        {
            txtData.Text += data + Environment.NewLine;
        }));
    }
    else
    {
        txtData.Text += data + Environment.NewLine;
    }
}
```

### （2）WPF

```c#
// 用Dispatcher.Invoke/BeginInvoke
private void UpdateUIFromThreadWPF(string data)
{
    // Dispatcher是WPF的UI线程调度器
    Application.Current.Dispatcher.BeginInvoke(new Action(() => 
    {
        txtData.Text += data + Environment.NewLine;
    }));
}
```

## 3. 线程安全
>场景：多线程同时读写同一个变量（如采集数据缓存、全局配置）、同时操作同一个资源（如日志文件、串口），会出现数据错乱、程序崩溃。

>必会解决方案：
### （1）lock关键字（常用）

```c#
// 定义一个锁对象（全局唯一，不能是值类型）
private readonly object _lockObj = new object();
private int _count = 0; // 共享变量

// 多线程同时调用此方法，不会出现数据错乱
private void AddCount()
{
    lock (_lockObj) // 锁定代码块，同一时间只有一个线程执行
    {
        _count++;
        Console.WriteLine($"当前计数：{_count}");
    }
}
```


### （2）线程安全集合（缓存采集数据必备）
>无需手动加锁，框架自带线程安全，上位机最常用ConcurrentQueue（队列，先进先出，适合缓存采集数据）：

```c#
using System.Collections.Concurrent; // 需额外引入

// 线程安全队列，用于缓存采集到的数据
private ConcurrentQueue<string> _dataQueue = new ConcurrentQueue<string>();

// 采集线程：入队
private void CollectData()
{
    _dataQueue.Enqueue("采集到的数据"); // 线程安全，无需lock
}

// 处理线程：出队
private void ProcessData()
{
    if (_dataQueue.TryDequeue(out string data))
    {
        // 处理数据
        UpdateUIFromThread(data);
    }
}
```

>相关概念：
- 竞态条件：多线程同时读写共享资源，导致数据结果不确定（如两个线程同时给_count加1，可能只加1次）。
- 死锁：两个线程互相等待对方释放锁，导致程序卡死（上位机开发中，避免嵌套lock即可基本规避）。


## 4. 线程取消
>场景：点击“停止采集”“断开连接”、关闭窗口时，安全退出后台线程（避免线程占用资源、程序无法正常关闭）。
>必会：CancellationToken（取消令牌）

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

## 5. 定时器（非严格线程，但依赖线程）
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




