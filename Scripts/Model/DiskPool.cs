using System.Collections.Generic;
using UnityEngine;

public class DiskPool
{
    public GameObject diskPrefab;  
    private Queue<GameObject> diskQueue = new Queue<GameObject>(); // 存储可复用的 Disk 对象

    // 信号：当需要生成新的 Disk 时触发
    public event System.Action OnDiskRequest;

    // 初始化池，增加传入 diskPrefab 参数
    public void InitializePool(int size, GameObject prefab)
    {
        diskPrefab = prefab;

        for (int i = 0; i < size; i++)
        {
            GameObject diskObject = Object.Instantiate(diskPrefab);  // 创建 Prefab 实例
            diskObject.SetActive(false);  
            diskQueue.Enqueue(diskObject);
        }
    }

    // 从对象池获取一个 Disk
    public GameObject GetDiskFromPool()
    {
        Debug.Log("getDiskFromPool");
        if (diskQueue.Count > 0)
        {
            GameObject diskObject = diskQueue.Dequeue();
            diskObject.SetActive(true);
            return diskObject;
        }
        else
        {
            Debug.LogWarning("Disk Pool is empty, expanding pool...");
            GameObject diskObject = Object.Instantiate(diskPrefab);  // 如果池空了，创建一个新的对象
            return diskObject;
        }
    }

    public void ReturnDiskToPool(GameObject diskObject)
    {
        diskObject.SetActive(false);  // 禁用对象
        diskQueue.Enqueue(diskObject);  // 放回池中
    }

    // 触发生成 Disk 的信号
    public void RequestNewDisk()
    {
        OnDiskRequest?.Invoke();  // 如果有订阅，触发事件
    }
}
