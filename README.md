# [导表工具](https://github.com/git4paulqu/Excel2Unity)

### 支持的类型
| c#                 |    Excel |
| ------------------ | -------: |
| int                |      int |
| float              |    float |
| bool               |     bool |
| string             |   string |
| Vector2            |  vector2 |
| Vector3            |  vector3 |
| Color              |    color |
| List<T>            |      [T] |
| Dictionary<T1, T2> | [T1, T2] |
### 配置Excel规则

1. float类型支持扩展，例如float5, 值为12345/10000, float3,值为12345/1000
2. List最多支持两层嵌套，eg:[[int]] 等于c#中的类型List<List<int>>, 注意目前支持扩展的的类型，最多支持一层嵌套,例如[Vector3]
3. Dictionary中的key为int/string, value类型如果是List, 那面List子类型只支持基本int/float/string/bool, value类型不支持字典
4. 忽略类型以@(config文件配置)开头的列数据
5，第0列为Key, 类型为Int

### 读取规则
`T data = DataTable.DataTableManager.Instance.TryGetValue<T>(id);`

数据类型为partical，方便扩展。
可参看unity工程。
工具目录：tool/Excel2Unity.exe
（若出现错误，请确认conig中配置正确）