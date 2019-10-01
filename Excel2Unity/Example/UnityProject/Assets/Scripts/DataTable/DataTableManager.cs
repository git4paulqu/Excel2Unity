//=====================================================
// - FileName:      DataTableManager.cs
// - Created:       #AuthorName#
// - UserName:      #CreateTime#
// - Email:         #AuthorEmail#
// - Description:   
// - Copyright © 2018 Qu Tong. All rights reserved.
//======================================================

using System;
using System.Collections.Generic;

namespace DataTable
{
    public partial class DataTableManager
    {
        public void Reload()
        {
            _map_readers.Clear();
            Register();
        }

        public TValue TryGetValue<TValue>(int key)
        {
            IDataReader reader = TryGetReader<TValue>();
            if (null == reader)
                return default(TValue);

            BaseReader<TValue> data = reader as BaseReader<TValue>;
            return data.TryGetValue(key);
        }

        public IDataReader TryGetReader<T>()
        {
            IDataReader manager = null;
            _map_readers.TryGetValue(typeof(T), out manager);
            return manager;
        }

        public void OnInit()
        {
            Register();
        }

        partial void Register();

        private void RegisterReader<T>(IDataReader manager)
        {
            _map_readers.Add(typeof(T), manager);
        }

        private Dictionary<Type, IDataReader> _map_readers = new Dictionary<Type, IDataReader>();
    }
}
