﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    public class ServiceContainer : IServiceContainer, IDisposable
    {
        private Dictionary<Type, object> dic = new Dictionary<Type, object>();

        public T GetService<T>()
        {
            if (dic.TryGetValue(typeof(T), out object instance))
            {
                return (T)instance;
            }
            else
            {
                return default(T);
            }
        }

        public void RegisteType<T>(T service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));

            }
            dic[typeof(T)] = service;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~ServiceContainer() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }

        public void RegisteType<T>(Func<IServiceProvider, T> factory)
        {
            throw new NotImplementedException();
        }

        public T GetInstance<T>()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
