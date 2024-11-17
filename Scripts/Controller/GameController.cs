using UnityEngine;

public class GameController : MonoBehaviour
{
    private int score = 0;
    private int level = 1;
    private MyGUI gui;
    private int diskCount = 0;
    private DiskRuler diskRuler;

    private float levelStartTime = 0f;  // 记录每关开始的时间
    private float levelDuration = 5f;   // 每关的持续时间A
    private bool isLevelRunning = false; // 标记关卡是否正在进行

    public GameObject diskPrefab;
    public DiskPool diskPool;

    private void Awake()
    {
        GameObject viewObject = GameObject.Find("View");
        gui = viewObject.GetComponent<MyGUI>();
    }
    private void Start()
    {
        // 加载飞碟预制体
        diskPrefab = Resources.Load<GameObject>("Prefabs/Disk");
        if (diskPrefab == null)
            Debug.Log("prefab is null");

        // 初始化对象池
        diskPool = new DiskPool();
        diskPool.InitializePool(10, diskPrefab);

        if (diskPool == null)
            Debug.Log("diskPool is null");

        diskRuler = new DiskRuler(diskPool);
        PointCal.OnScoreUpdated += UpdateScoreUI;


        gui.SetScore(score);
        gui.SetLevel(level);
        gui.ShowStartButton(true);
        gui.OnStartButtonClick(StartGame);
    }

    public void StartGame()
    {
        Debug.Log("StartGame");
        gui.ShowStartButton(false);  
        StartLevel(); 
    }

    // 启动当前关卡
    private void StartLevel()
    {
        Debug.Log("Starting Level: " + level);

        // 根据关卡设置飞碟数量和出现次数
        SetLevelParameters(level);

        // 记录关卡开始时间
        levelStartTime = Time.time;
        isLevelRunning = true;

        // 生成飞碟
        diskRuler.GenerateRandomDisk(diskCount);
    }

    // 设置当前关卡的参数
    private void SetLevelParameters(int level)
    {
        if (level == 1)
        {
            diskCount = 2;
        }
        else if (level == 2)
        {
            diskCount = 4;
        }
        else if (level == 3)
        {
            diskCount = 5;
        }
        else if (level == 4)
        {
            diskCount = 5;
        }
        else if (level == 5)
        {
            diskCount = 5;
        }
        else if (level == 6)
        {
            diskCount = 5;
        }
        else if (level == 7)
        {
            diskCount = 5;
        }
        else if (level == 5)
        {
            diskCount = 5;
        }
        else if (level == 9)
        {
            diskCount = 5;
        }
        else if (level == 10)
        {
            diskCount = 5;
        }
        else
            diskCount = 6;
    }

    private void Update()
    {
        if (isLevelRunning)
        {
            // 如果关卡时间已到，进行下一关
            if (Time.time - levelStartTime >= levelDuration)
            {
                // 增加关卡并更新UI
                level++;
                gui.SetLevel(level);

                // 结束当前关卡
                isLevelRunning = false;

                // 等待 5 秒再开始下一关
                Invoke("StartLevel", 3f * Time.deltaTime);
            }
        }
    }
    // 更新 UI 中的分数
    private void UpdateScoreUI(int newScore)
    {
        gui.SetScore(newScore); // 更新显示的分数
    }

}
