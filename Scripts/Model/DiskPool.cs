using System.Collections.Generic;
using UnityEngine;

public class DiskPool
{
    public GameObject diskPrefab;  
    private Queue<GameObject> diskQueue = new Queue<GameObject>(); // �洢�ɸ��õ� Disk ����

    // �źţ�����Ҫ�����µ� Disk ʱ����
    public event System.Action OnDiskRequest;

    // ��ʼ���أ����Ӵ��� diskPrefab ����
    public void InitializePool(int size, GameObject prefab)
    {
        diskPrefab = prefab;

        for (int i = 0; i < size; i++)
        {
            GameObject diskObject = Object.Instantiate(diskPrefab);  // ���� Prefab ʵ��
            diskObject.SetActive(false);  
            diskQueue.Enqueue(diskObject);
        }
    }

    // �Ӷ���ػ�ȡһ�� Disk
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
            GameObject diskObject = Object.Instantiate(diskPrefab);  // ����ؿ��ˣ�����һ���µĶ���
            return diskObject;
        }
    }

    public void ReturnDiskToPool(GameObject diskObject)
    {
        diskObject.SetActive(false);  // ���ö���
        diskQueue.Enqueue(diskObject);  // �Żس���
    }

    // �������� Disk ���ź�
    public void RequestNewDisk()
    {
        OnDiskRequest?.Invoke();  // ����ж��ģ������¼�
    }
}
