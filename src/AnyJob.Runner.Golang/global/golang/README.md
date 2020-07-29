
编译为插件
```shell
go build -buildmode=plugin -gcflags="all=-N -l" test.go
```