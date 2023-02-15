# 介绍

key-value文件存储，能将任何可序列化的数据按键值对的方式存储，简单易用，跨平台。

依赖Newtonsoft.Json。(附Nuget地址：[https://www.nuget.org/packages/Newtonsoft.Json](https://gitee.com/link?target=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FNewtonsoft.Json))



### 存储

```C#
// public enum Color
// {
//     Red,
//     Blue,
// }

// public class Person
// {
//     public string name;
//     public int age;
// }

// public struct Point
// {
//     public int x;
//     public int y;
// }

var file = new KVFile(@"D:\data.dat");
// 存储int数据
file.Set("IntValue", 1);
// 存储float数据
file.Set("FloatValue", 3.14f);
// 存储double数据
file.Set("DoubleValue", 3.14);
// 存储string数据
file.Set("StringValue", "Hello");
// 存储枚举
file.Set("EnumValue", Color.Red);
// 存储对象
file.Set("ObjectValue", new Person()
{
    name = "Nick",
    age = 18
});
// 存储结构体
file.Set("StructValue", new Point());
// 保存文件
file.Save();
```

### 读取

```C#
var file = KVFile.Open(@"D:\data.dat");
// 读取int数据
int intValue = file.Get<int>("IntValue");
// 读取float数据
float floatValue = file.Get<float>("FloatValue");
// 读取double数据
double doubleValue = file.Get<double>("DoubleValue");
// 读取string数据
string stringValue = file.Get<string>("StringValue");
// 读取枚举
Color enumValue = file.Get<Color>("EnumValue");
// 读取对象
Person objectValue = file.Get<Person>("ObjectValue");
// 读取结构体
Point structValue = file.Get<Point>("StructValue");
```

