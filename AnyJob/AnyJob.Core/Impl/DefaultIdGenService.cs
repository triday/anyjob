using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IIdGenService))]
    public class DefaultIdGenService : IIdGenService
    {
        
        public string NewChildId(string parentId)
        {
            return Guid.NewGuid().ToString();
        }

        public string NewId()
        {
            return NewSequenceGuid().ToString();
        }

        private Guid NewSequenceGuid()
        {
            byte[] timestampBytes = BitConverter.GetBytes(DateTimeOffset.Now.Ticks);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }
            var byteArray = Guid.NewGuid().ToByteArray();
            Buffer.BlockCopy(timestampBytes, 0, byteArray, 0, timestampBytes.Length);
            return new Guid(byteArray);
        }
    }
}
