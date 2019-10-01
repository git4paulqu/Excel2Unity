//=====================================================
// - FileName:      BaseReader.cs
// - Created:       qutong
// - UserName:      2018/11/21 13:28:30
// - Email:         
// - Description:   配置文件基类
// - 
//======================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace DataTable
{
    public interface IDataReader
    {
        string Name { get; }
    }

    public class BaseReader<TValue> : IDataReader
    {
        public TValue TryGetValue(int key) {
            TValue data;
            _map_datas.TryGetValue(key, out data);
            return data;
        }

        public bool ContainsKey(int key) {
            return _map_datas.ContainsKey(key);
        }

        protected void Load(string path) {
            byte[] buffer = LoadBuffer(path);
            int bufferLength = buffer.Length;
            int curPos = 0;

            int index = 0;
            while (curPos < bufferLength)
            {
                int key = 0;
                TValue data = default(TValue);
                try
                {
                    key = BitConverter.ToInt32(buffer, curPos);
                    curPos += sizeof(int);

                    int dataSize = BitConverter.ToInt32(buffer, curPos);
                    curPos += sizeof(int);
                   
                    byte[] dataBinary = new byte[dataSize];
                    Buffer.BlockCopy(buffer, curPos, dataBinary, 0, dataSize);
                    curPos += dataSize;

                    using (MemoryStream ms = new MemoryStream(dataBinary))
                    {
                        data = ProtoBuf.Serializer.Deserialize<TValue>(ms);
                    }
                   
                    _map_datas.Add(key, data);
                    OnConstructor(data);
                    index++;
                }
                catch (Exception e)
                {
                    throw new Exception (string.Format("serialize data:{0} is error, key is {1}, index:{2}, buffer:{3} error:{4}", typeof(TValue).ToString(), key.ToString(), index, bufferLength, e));
                }
            }
        }

        protected virtual void OnConstructor(TValue data) { }

        // replace your load function
        private byte[] LoadBuffer(string path) {
            TextAsset textAsset = Resources.Load<TextAsset>(string.Format("Data/{0}", path));
            if (null == textAsset || null == textAsset.bytes)
                throw new Exception(string.Format("the data:{0} bytes file is not exist.", path));
            
            return textAsset.bytes;
        }

        public Dictionary<int, TValue> Data { get { return _map_datas; } }

        public string Name { get { return this.ToString(); } }

        protected Dictionary<int, TValue> _map_datas = new Dictionary<int, TValue>();
    }
 }