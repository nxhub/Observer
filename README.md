## 概述
封装观察者模式的主题和观察者对象，如果你对使用事件充满恐惧，那么欢迎使用。

## 安装
```xml
<PackageReference Include="NXHub.Extensions.Observer" Version="0.0.1" />
```

## 使用
```cs
var observable = new Observable<Message>();

new Observer(observable);

observable.NotifyObservers();
```
