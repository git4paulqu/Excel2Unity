//=====================================================
// - FileName:      Main.cs
// - Created:       #AuthorName#
// - UserName:      #CreateTime#
// - Email:         #AuthorEmail#
// - Description:   
// - Copyright © 2018 Qu Tong. All rights reserved.
//======================================================
using UnityEngine;
using DataTable;

public class Main : MonoBehaviour {

	void Start () {
        DataTableManager manager = new DataTableManager();
        manager.OnInit();

        Data data = manager.TryGetValue<Data>(1);
        Debug.LogError(data.ut_string);
        data.extend = 1111.001f;

        DataDataReader reader = manager.TryGetReader<Data>() as DataDataReader;

        foreach (var item in reader.Data)
        {
            Debug.LogErrorFormat("data {0}, extend {1}", item.Value.id, item.Value.extend);

            foreach (var kv in item.Value.ut_map)
            {
                Debug.LogFormat("map {0}, {1}", kv.Key, kv.Value);
            }
        }
    }
}
