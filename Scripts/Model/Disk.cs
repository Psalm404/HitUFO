using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    private Color diskColor;
    private Vector3 diskSpeed;

    private DiskPool diskPool;  // ��������

    // ͨ�����캯�����ݶ����ʵ��
    public void Initialize(DiskPool pool)
    {
        diskPool = pool;
    }
    public void createDisk(Color diskColor, Vector3 diskSpeed)
    {
        this.diskColor = diskColor;
        this.diskSpeed = diskSpeed;
        // ��ȡ MeshRenderer ���������ɫ�󶨵� Material
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
        // ���ɵ�����������ת��Ϊ��Ļ����
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // ���ɵ��Ƿ�����Ļ��
        if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
        {
            Debug.Log("�ɵ��ɳ����ߣ��Żض���أ�");

            diskPool.ReturnDiskToPool(gameObject);
        }
    }
}
