#  一、串的定义

<img width="982" height="550" alt="image" src="https://github.com/user-attachments/assets/b21dd40b-2025-44e2-be6c-2d6b564de0f9" />


# 二、串和线性表的区别：
  
>相比于线性表，只是里面的元素被限制为字符。操作对象不再是单个元素，而是整个字串作为一个整体操作


<img width="889" height="560" alt="image" src="https://github.com/user-attachments/assets/feeed1f6-b222-4f65-ba4f-50928dad78b5" />



# 三、串的基本操作：

>赋值（给串初始化）、复制（串S复制给串T）、判空（判断空串）、串长（返回串的元素个数）、清空（变成空串）、销毁（空串+释放内存）、串连接（字符串的拼接）、求子串（返回第pos个字符开始长度为len的串）、定位操作（寻找子串在主串中的位置）、比较（比较字符串的大小，英文用ASCII，中文和其他语言用Unicode）


# 四：串的存储结构

```cpp
   #define MAX 255
 typedef struct{                             //顺序存储结构体
       char ch[MAX];
       int length;
 }SString;
```

<img width="1014" height="585" alt="image" src="https://github.com/user-attachments/assets/24409d33-e8ff-4987-97fb-7310c3c6fb76" />

```cpp
typedef struct{                               //顺序存储之动态数组存储
    char *ch;                                     //指向分配内存的基地址
    int length;
 }HString;
  Hstring S;                                       //使用方法
  S.ch=(char *)malloc(MAX*sizeof(char));
  S.length=0;

typedef struct StringNode{
   char ch[4];                                    //可以每个节点存多个字符提升存储密度
   struct StringNode *next;
}StringNode , *String;
```

# 五、串的基本操作

## 1、求pos开始长度为len的子串：
```cpp
bool SubString(SString &Sub,SString S,int pos,int len){
    if(pos+len-1>S.length)  return false;             //子串范围越界了
    for(int i=pos;i<lpos+len;i++)    
         Sub.ch[i-pos+1]=S.ch[i];                          //子串数据赋值给另一个串Sub
     Sub.length=len;                                           //Sub串长为len
     return true;
}
```
## 2、比较S和T，S>T返回值>0，S<T返回值<0，S=T返回值=0：
```cpp
int Strcompare(SString S,SString T){
    for(int i=1;i<=S.length&&i<=T.length;i++)
        if(S.ch[i]!=T.ch[i])                                     //有不相同的字符就根据ASCII码对比
           return S.ch[i]-T.ch[i];
            }
         return S.length-T.length;                        //所有字符都相同就长度长的串更大
 }
```
## 3、定位操作：在主串S中返回它的子串T第一次出现的位置，否则为0
```cpp
int Index(SString S,SString T){
    Int i=1;  n=StrLength(S),m=StrLength(T);
    SString temp;
    while(i<=n-m+1){                                      //循环次数为S的长度减去T的长度+1
       SubString(temp,S,i,m);                            //从S里面的第i个下标开始长度为T长度的子串，依次赋值到temp
       if(StrCompare(temp,T)!=0)  i++;            //对比temp和T是否相等
       else return i;
     }
     return 0;                                                     //匹配失败
}

```
## 4、字符串的模式匹配算法—字符串里面找出某一段内容相同的子串
```cpp
 //朴素模式匹配算法
 //(图6)主串中找出所有和它长度相等的子串一一对比，最多对比n-m+1次
 int Index(SString S,SString T){
   int i=1,j=1;
   while(i<=S.length && j<=T.length){    //保证比较的是对应位置的元素
         if(S.ch[i]==T.ch[j]){          //相等的话比较下一对元素
              i++;   j++; 
          } 
          else{                            
                i=i-j+2;            //不相等的话i指向下一个子串的第一个位置
                j=1;                   //j回到匹配子串的第一个位置
          }
     }
     if(j>T.length)   return i-T.length;   
                                       //匹配完j超过了T子串的长度说明所有元素都匹配，返回S子串当前的第一个位置
     else return 0;
}
最坏时间复杂度O(n*m)





//KMP算法
int Index_KMP(SString S,SString T,int next[]){
   int i=1,j=1;
   while(i<=S.length&&j<=T.length){
          if(j==0||S.ch[i]==T.ch[j]){
              i++;   j++;   
            }
            else   j=next[j];
     }
     if(j>T.length)
          return i-T.length;
      else 
           return 0;
}
最坏时间复杂度O(m+n)

```


























































































































































































































































































