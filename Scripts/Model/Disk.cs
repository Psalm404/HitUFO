using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    private Color diskColor;
    private Vector3 diskSpeed;

    private DiskPool diskPool;  // 引入对象池

    // 通过构造函数传递对象池实例
    public void Initialize(DiskPool pool)
    {
        diskPool = pool;
    }
    public void createDisk(Color diskColor, Vector3 diskSpeed)
    {
        this.diskColor = diskColor;
        this.diskSpeed = diskSpeed;
        // 获取 MeshRenderer 组件并将颜色绑定到 Material
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            Material material = meshRenderer.material;
            material.SetColor("_Color", diskColor);
        }
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = diskSpeed;
    }
    public void OnMouseDown()
    {
        Debug.Log("click " + diskColor);
        PointCal.AddScore(diskColor);

        diskPool.ReturnDiskToPool(gameObject);
    }
    private void Update()
    {
        // 将飞碟的世界坐标转换为屏幕坐标
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // 检查飞碟是否在屏幕外
        if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
        {
            Debug.Log("飞碟飞出视线，放回对象池！");

            diskPool.ReturnDiskToPool(gameObject);
        }
    }
}
