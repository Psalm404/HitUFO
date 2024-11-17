using UnityEngine;

public class GameController : MonoBehaviour
{
    private int score = 0;
    private int level = 1;
    private MyGUI gui;
    private int diskCount = 0;
    private DiskRuler diskRuler;

    private float levelStartTime = 0f;  // ��¼ÿ�ؿ�ʼ��ʱ��
    private float levelDuration = 5f;   // ÿ�صĳ���ʱ��A
    private bool isLevelRunning = false; // ��ǹؿ��Ƿ����ڽ���

    public GameObject diskPrefab;
    public DiskPool diskPool;

    private void Awake()
    {
        GameObject viewObject = GameObject.Find("View");
        gui = viewObject.GetComponent<MyGUI>();
    }
    private void Start()
    {
        // ���طɵ�Ԥ����
        diskPrefab = Resources.Load<GameObject>("Prefabs/Disk");
        if (diskPrefab == null)
            Debug.Log("prefab is null");

        // ��ʼ�������
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

    // ������ǰ�ؿ�
    private void StartLevel()
    {
        Debug.Log("Starting Level: " + level);

        // ���ݹؿ����÷ɵ������ͳ��ִ���
        SetLevelParameters(level);

        // ��¼�ؿ���ʼʱ��
        levelStartTime = Time.time;
        isLevelRunning = true;

        // ���ɷɵ�
        diskRuler.GenerateRandomDisk(diskCount);
    }

    // ���õ�ǰ�ؿ��Ĳ���
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
            // ����ؿ�ʱ���ѵ���������һ��
            if (Time.time - levelStartTime >= levelDuration)
            {
                // ���ӹؿ�������UI
                level++;
                gui.SetLevel(level);

                // ������ǰ�ؿ�
                isLevelRunning = false;

                // �ȴ� 5 ���ٿ�ʼ��һ��
                Invoke("StartLevel", 3f * Time.deltaTime);
            }
        }
    }
    // ���� UI �еķ���
    private void UpdateScoreUI(int newScore)
    {
        gui.SetScore(newScore); // ������ʾ�ķ���
    }

}
