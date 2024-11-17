using UnityEngine;

public class DiskRuler
{
    private DiskPool diskPool;

    // ��ɫ�б�
    private Color[] colors = new Color[] {
    new Color(1f, 0f, 0f),    // ��ɫ (Red)
    new Color(1f, 0.647f, 0f), // ��ɫ (Orange)
    new Color(1f, 1f, 0f),    // ��ɫ (Yellow)
    new Color(0f, 1f, 0f),    // ��ɫ (Green)
    new Color(0f, 1f, 1f),    // ��ɫ (Cyan)
    new Color(0f, 0f, 1f),    // ��ɫ (Blue)
    new Color(0.5f, 0f, 1f)   // ��ɫ (Violet)
};

    // ��ɫ��Ȩ�أ����ԽС����ɫ���ֵĸ���Խ��
    private float[] probabilities = new float[] {
        0.20f, 
        0.18f, 
        0.16f, 
        0.14f, 
        0.12f, 
        0.10f, 
        0.08f  
    };

    // �̶���6���ɵ�����λ��
    private Vector3[] spawnPositions = new Vector3[] {
        new Vector3(0, 0, -5.65f),
        new Vector3(-5, 0, -5.65f),
        new Vector3(-5, 0, -2f),
        new Vector3(0, 0, -2f),
        new Vector3(5, 0, -2f),
        new Vector3(5, 0, -5.65f)
    };

    // ���캯��
    public DiskRuler(DiskPool pool)
    {
        diskPool = pool;
    }

    // ���ڼ�Ȩ���ѡ����ɫ
    public Color GetRandomColor()
    {
        float totalWeight = 0f;
        foreach (float weight in probabilities)
        {
            totalWeight += weight;
        }

        // ����һ�� 0 �� totalWeight ֮��������
        float randomValue = Random.Range(0f, totalWeight);

        // �������ֵѡ����ɫ
        float cumulativeWeight = 0f;
        for (int i = 0; i < colors.Length; i++)
        {
            cumulativeWeight += probabilities[i];
            if (randomValue <= cumulativeWeight)
            {
                return colors[i];
            }
        }

        return colors[colors.Length - 1];
    }

    // ���ɷɵ�
    public void GenerateRandomDisk(int diskCount)
    {
        Debug.Log("generateRandomDisk");

        for (int i = 0; i < diskCount; i++)
        {
            // ���ѡ��һ�����ɵ�
            int randomIndex = Random.Range(0, spawnPositions.Length);
            Vector3 spawnPosition = spawnPositions[randomIndex];

            Debug.Log("Generating disk at position: " + spawnPosition);

            GameObject newDisk = diskPool.GetDiskFromPool();  // �ӳ��л�ȡһ���ɵ�

            // ʹ�ü�Ȩ���������ѡ����ɫ
            Color randomColor = GetRandomColor();

            // ������÷ɵ��ٶ�
            float randomSpeedX = Random.Range(-10f, 10f);
            float randomSpeedY = Random.Range(0, 10f);
            float randomSpeedZ = Random.Range(-10f, 6f);
            Vector3 randomSpeed = new Vector3(randomSpeedX, randomSpeedY, randomSpeedZ);

            Disk disk = newDisk.GetComponent<Disk>();
            if (disk == null)
            {
                Debug.LogError("Disk component not found on the disk prefab!");
                continue;
            }

            disk.Initialize(diskPool);  // ��ʼ���ɵ������������ع���
            Debug.Log("Random speed: " + randomSpeed);
            disk.createDisk(randomColor, randomSpeed);

            // ���÷ɵ����ɵ�λ��
            newDisk.transform.position = spawnPosition;
        }
    }
}
