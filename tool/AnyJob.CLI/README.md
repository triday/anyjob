## AnyJob.CLI

如何安装
```shell
dotnet tool install -g AnyJob.CLI
```

如何使用



- 启动本地服务
```shell
anyjob start-provider {anyjob-packs-root-folder}
```
`{anyjob-packs-root-folder}` 需要符合anyjob的目录规范
 1. 根目录下存放pack目录
 2. pack目录下存放版本目录，版本是有1-3位数字组成，并由`.`连接
 3. 版本目录下面存放对应版本的文件 

例如以下目录结构

```shell
root
├─demopack
  ├─0.0.1
    ├─add.action
    ├─add.py
    ├─hello.action
    ├─hello.py
├─demopack2
  ├─0.0.1
    ├─add.action
    ├─add.py
    ├─hello.action
    ├─hello.py
```


- 注册provider
```shell
dotnet add-provider demopack http://xxx.xxx.xxx
```

- 本地执行action 
```shell
anyjob  run-local demopack.add '{"num1":2,"num2":7444}'
```
