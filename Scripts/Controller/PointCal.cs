using System.Collections.Generic;
using UnityEngine;

// ����������
public class PointCal
{

    private static int score = 0;

    // ÿ����ɫ�ķɵ���Ӧ�ķ���
    private static readonly Dictionary<Color, int> colorScores = new Dictionary<Color, int>
{
    { new Color(1f, 0f, 0f), 23 },       
    { new Color(1f, 0.647f, 0f), 5 },  
    { new Color(1f, 1f, 0f), 12 },      
    { new Color(0f, 1f, 0f), 11 },       
    { new Color(0f, 1f, 1f), 9 },       
    { new Color(0f, 0f, 1f), 14 },      
    { new Color(0.5f, 0f, 1f), 19 }     
};


    public static System.Action<int> OnScoreUpdated;

    public static void AddScore(Color diskColor)
    {
        Debug.Log("AddScore");
        if (colorScores.ContainsKey(diskColor))
        {
            score += colorScores[diskColor];

            OnScoreUpdated?.Invoke(score); // ֪ͨ���·���
        }
    }

    // ��ȡ��ǰ����
    public static int GetScore()
    {
        return score;
    }

    public static void ResetScore()
    {
        score = 0;
        OnScoreUpdated?.Invoke(score); // ���÷���ʱ֪ͨ����
    }
}
