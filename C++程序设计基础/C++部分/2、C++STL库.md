# 简介
>STL（Standard Template Library，标准模板库）是 C++ 的核心组成部分，提供了大量通用的数据结构和算法，极大提升开发效率。

# 一、STL 核心组件
>STL 主要包含 4 大核心组件，相互配合完成数据处理：


<img width="670" height="280" alt="image" src="https://github.com/user-attachments/assets/a233493d-00f5-4a46-901f-5c095b7eaaca" />


# 二、常用容器（附完整源码）
## 1. 顺序容器：vector（动态数组）
>核心特性：随机访问快、尾部增删快、中间增删慢，自动扩容。
```cpp

#include <iostream>
#include <vector>   // 引入vector头文件
#include <algorithm> // 算法头文件（sort/find等）
using namespace std;

// vector完整示例
int main() {
    // 1. 初始化
    vector<int> vec1;                  // 空vector
    vector<int> vec2(5, 10);           // 5个元素，每个值为10
    vector<int> vec3 = {1, 3, 2, 5, 4};// 初始化列表（C++11+）

    // 2. 常用操作
    vec1.push_back(1);  // 尾部添加元素
    vec1.push_back(2);
    vec1.push_back(3);

    cout << "vec1大小：" << vec1.size() << endl;          // 输出3
    cout << "vec1容量：" << vec1.capacity() << endl;      // 输出4（自动扩容）
    cout << "vec1第2个元素：" << vec1[1] << endl;         // 输出2（下标从0开始）
    cout << "vec1第2个元素（安全访问）：" << vec1.at(1) << endl; // 越界会抛异常

    // 3. 遍历方式
    // 方式1：下标遍历
    cout << "下标遍历vec3：";
    for (int i = 0; i < vec3.size(); i++) {
        cout << vec3[i] << " ";
    }
    cout << endl;

    // 方式2：迭代器遍历
    cout << "迭代器遍历vec3：";
    for (vector<int>::iterator it = vec3.begin(); it != vec3.end(); it++) {
        cout << *it << " ";
    }
    cout << endl;

    // 方式3：范围for（C++11+）
    cout << "范围for遍历vec3：";
    for (int num : vec3) {
        cout << num << " ";
    }
    cout << endl;

    // 4. 算法操作
    sort(vec3.begin(), vec3.end()); // 升序排序
    cout << "排序后vec3：";
    for (int num : vec3) cout << num << " "; // 输出1 2 3 4 5
    cout << endl;

    // 5. 删除元素
    vec1.pop_back(); // 删除尾部元素
    vec1.erase(vec1.begin() + 0); // 删除第一个元素
    cout << "删除后vec1：";
    for (int num : vec1) cout << num << " "; // 输出2
    cout << endl;

    // 6. 清空
    vec1.clear();
    cout << "清空后vec1是否为空：" << (vec1.empty() ? "是" : "否") << endl; // 输出是

    return 0;
}
```
## 2. 顺序容器：list（双向链表）
>核心特性：任意位置增删快、不支持随机访问、遍历只能用迭代器。
```cpp
运行
#include <iostream>
#include <list>
#include <algorithm>
using namespace std;

// list完整示例
int main() {
    list<int> lst = {5, 2, 8, 1};

    // 1. 常用操作
    lst.push_front(10); // 头部添加元素
    lst.push_back(20);  // 尾部添加元素
    lst.insert(++lst.begin(), 99); // 在第二个位置插入99

    cout << "list大小：" << lst.size() << endl; // 输出6

    // 2. 遍历（仅支持迭代器/范围for）
    cout << "遍历list：";
    for (auto it = lst.begin(); it != lst.end(); it++) {
        cout << *it << " "; // 输出10 99 5 2 8 1 20
    }
    cout << endl;

    // 3. 排序（list自带sort，不支持std::sort）
    lst.sort(); // 升序排序
    cout << "排序后list：";
    for (int num : lst) cout << num << " "; // 输出1 2 5 8 10 20 99
    cout << endl;

    // 4. 删除元素
    lst.pop_front(); // 删除头部
    lst.remove(8);   // 删除所有值为8的元素
    cout << "删除后list：";
    for (int num : lst) cout << num << " "; // 输出2 5 10 20 99
    cout << endl;

    return 0;
}
```
## 3. 关联容器：map（键值对，红黑树）
>核心特性：键唯一、自动按键升序排序、键值对存储（key-value），查找效率高。
```cpp

#include <iostream>
#include <map>
#include <string>
using namespace std;

// map完整示例
int main() {
    // 1. 初始化
    map<int, string> mp;
    // 2. 插入元素
    mp[1] = "张三";    // 方式1：下标插入（无则增，有则改）
    mp[2] = "李四";
    mp.insert({3, "王五"}); // 方式2：insert插入（避免覆盖）
    mp.insert(pair<int, string>(4, "赵六")); // 老式插入方式

    // 3. 遍历
    cout << "遍历map：" << endl;
    for (auto it = mp.begin(); it != mp.end(); it++) {
        // first是键，second是值
        cout << "键：" << it->first << "，值：" << it->second << endl;
    }

    // 4. 查找元素
    int key = 2;
    auto it = mp.find(key);
    if (it != mp.end()) {
        cout << "找到键" << key << "，值：" << it->second << endl; // 输出李四
    } else {
        cout << "未找到键" << key << endl;
    }

    // 5. 修改元素
    mp[2] = "李四（修改后）";
    cout << "修改后键2的值：" << mp[2] << endl;

    // 6. 删除元素
    mp.erase(3); // 根据键删除
    cout << "删除键3后，map大小：" << mp.size() << endl; // 输出3

    // 7. 清空
    mp.clear();
    cout << "清空后map是否为空：" << (mp.empty() ? "是" : "否") << endl;

    return 0;
}
```
## 4. 关联容器：unordered_map（哈希表）
>核心特性：键唯一、无序、查找 / 插入 / 删除平均 O (1)，比 map 更快（无排序开销）。
```cpp
运行
#include <iostream>
#include <unordered_map>
#include <string>
using namespace std;

// unordered_map完整示例
int main() {
    unordered_map<string, int> score_map;
    // 插入
    score_map["语文"] = 90;
    score_map["数学"] = 95;
    score_map["英语"] = 88;

    // 遍历（无序）
    cout << "unordered_map遍历（无序）：" << endl;
    for (auto& pair : score_map) {
        cout << pair.first << "：" << pair.second << endl;
    }

    // 查找
    string key = "数学";
    if (score_map.count(key)) { // count：存在返回1，不存在返回0
        cout << key << "成绩：" << score_map[key] << endl;
    }

    return 0;
}
```
## 5. 关联容器：set（集合，键唯一）
>核心特性：元素唯一、自动升序排序、无重复元素。
```cpp

#include <iostream>
#include <set>
using namespace std;

// set完整示例
int main() {
    set<int> s;
    // 插入（自动去重+排序）
    s.insert(5);
    s.insert(2);
    s.insert(5); // 重复元素，插入无效
    s.insert(8);

    cout << "set大小：" << s.size() << endl; // 输出3
    cout << "遍历set：";
    for (int num : s) cout << num << " "; // 输出2 5 8
    cout << endl;

    // 查找
    if (s.find(2) != s.end()) {
        cout << "找到元素2" << endl;
    }

    // 删除
    s.erase(5);
    cout << "删除5后：";
    for (int num : s) cout << num << " "; // 输出2 8
    cout << endl;

    return 0;
}
```
# 三、STL 算法（附源码）
>STL 算法均定义在<algorithm>头文件中，适用于所有支持迭代器的容器。
## 1. 常用基础算法
```cpp

#include <iostream>
#include <vector>
#include <algorithm>
#include <numeric> // 数值算法头文件
using namespace std;

// 算法示例
int main() {
    vector<int> vec = {3, 1, 4, 1, 5, 9, 2, 6};

    // 1. 排序：sort
    sort(vec.begin(), vec.end()); // 升序
    cout << "升序排序：";
    for (int num : vec) cout << num << " "; // 1 1 2 3 4 5 6 9
    cout << endl;

    sort(vec.begin(), vec.end(), greater<int>()); // 降序
    cout << "降序排序：";
    for (int num : vec) cout << num << " "; // 9 6 5 4 3 2 1 1
    cout << endl;

    // 2. 查找：find
    auto it = find(vec.begin(), vec.end(), 5);
    if (it != vec.end()) {
        cout << "找到5，索引：" << it - vec.begin() << endl; // 输出2
    }

    // 3. 计数：count
    int cnt = count(vec.begin(), vec.end(), 1);
    cout << "元素1的个数：" << cnt << endl; // 输出2

    // 4. 求和：accumulate（需<numeric>）
    int sum = accumulate(vec.begin(), vec.end(), 0); // 初始值0
    cout << "数组总和：" << sum << endl; // 输出31

    // 5. 反转：reverse
    reverse(vec.begin(), vec.end());
    cout << "反转后：";
    for (int num : vec) cout << num << " "; // 1 1 2 3 4 5 6 9
    cout << endl;

    // 6. 去重：unique（需先排序）
    sort(vec.begin(), vec.end());
    auto new_end = unique(vec.begin(), vec.end()); // 去重，返回新尾迭代器
    vec.erase(new_end, vec.end()); // 删除重复元素
    cout << "去重后：";
    for (int num : vec) cout << num << " "; // 1 2 3 4 5 6 9
    cout << endl;

    return 0;
}
```
## 2. 遍历算法：for_each
```cpp

#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

// 自定义函数：打印元素
void print_num(int num) {
    cout << num << " ";
}

// 仿函数：累加
struct AddNum {
    int sum = 0;
    void operator()(int num) {
        sum += num;
    }
};

int main() {
    vector<int> vec = {1,2,3,4,5};

    // 1. 用普通函数遍历
    cout << "for_each遍历：";
    for_each(vec.begin(), vec.end(), print_num); // 输出1 2 3 4 5
    cout << endl;

    // 2. 用仿函数累加
    AddNum add_obj = for_each(vec.begin(), vec.end(), AddNum());
    cout << "累加和：" << add_obj.sum << endl; // 输出15

    // 3. 用lambda表达式（C++11+，推荐）
    int total = 0;
    for_each(vec.begin(), vec.end(), [&total](int num) {
        total += num;
    });
    cout << "lambda累加和：" << total << endl; // 输出15

    return 0;
}
```
# 四、迭代器详解
迭代器统一了不同容器的遍历方式，核心类型如下：

<img width="603" height="335" alt="image" src="https://github.com/user-attachments/assets/7af593c5-09eb-4132-9764-c103f856c7f2" />


>迭代器常用方法
```cpp

#include <iostream>
#include <vector>
using namespace std;

int main() {
    vector<int> vec = {10,20,30,40,50};
    vector<int>::iterator it = vec.begin();

    cout << *it << endl;        // 解引用：10
    it++;                       // 后移：指向20
    cout << *it << endl;
    it += 2;                    // 随机访问：指向40
    cout << *it << endl;
    cout << it[1] << endl;      // 随机访问：50（等价*(it+1)）

    // 反向迭代器
    vector<int>::reverse_iterator rit = vec.rbegin();
    cout << *rit << endl;       // 50（反向开始，即最后一个元素）
    rit++;
    cout << *rit << endl;       // 40

    return 0;
}
```
