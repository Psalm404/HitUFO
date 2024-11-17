using UnityEngine;

public class DiskRuler
{
    private DiskPool diskPool;

    // 颜色列表
    private Color[] colors = new Color[] {
    new Color(1f, 0f, 0f),    // 红色 (Red)
    new Color(1f, 0.647f, 0f), // 橙色 (Orange)
    new Color(1f, 1f, 0f),    // 黄色 (Yellow)
    new Color(0f, 1f, 0f),    // 绿色 (Green)
    new Color(0f, 1f, 1f),    // 青色 (Cyan)
    new Color(0f, 0f, 1f),    // 蓝色 (Blue)
    new Color(0.5f, 0f, 1f)   // 紫色 (Violet)
};

    // 颜色的权重，编号越小的颜色出现的概率越高
    private float[] probabilities = new float[] {
        0.20f, 
        0.18f, 
        0.16f, 
        0.14f, 
        0.12f, 
        0.10f, 
        0.08f  
    };

    // 固定的6个飞碟生成位置
    private Vector3[] spawnPositions = new Vector3[] {
        new Vector3(0, 0, -5.65f),
        new Vector3(-5, 0, -5.65f),
        new Vector3(-5, 0, -2f),
        new Vector3(0, 0, -2f),
        new Vector3(5, 0, -2f),
        new Vector3(5, 0, -5.65f)
    };

    // 构造函数
    public DiskRuler(DiskPool pool)
    {
        diskPool = pool;
    }

    // 用于加权随机选择颜色
    public Color GetRandomColor()
    {
        float totalWeight = 0f;
        foreach (float weight in probabilities)
        {
            totalWeight += weight;
        }

        // 生成一个 0 到 totalWeight 之间的随机数
        float randomValue = Random.Range(0f, totalWeight);

        // 根据随机值选择颜色
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

    // 生成飞碟
    public void GenerateRandomDisk(int diskCount)
    {
        Debug.Log("generateRandomDisk");

        for (int i = 0; i < diskCount; i++)
        {
            // 随机选择一个生成点
            int randomIndex = Random.Range(0, spawnPositions.Length);
            Vector3 spawnPosition = spawnPositions[randomIndex];

            Debug.Log("Generating disk at position: " + spawnPosition);

            GameObject newDisk = diskPool.GetDiskFromPool();  // 从池中获取一个飞碟

            // 使用加权随机方法来选择颜色
            Color randomColor = GetRandomColor();

            // 随机设置飞碟速度
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

            disk.Initialize(diskPool);  // 初始化飞碟并将其与对象池关联
            Debug.Log("Random speed: " + randomSpeed);
            disk.createDisk(randomColor, randomSpeed);

            // 设置飞碟生成的位置
            newDisk.transform.position = spawnPosition;
        }
    }
}
