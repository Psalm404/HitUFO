using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class MyGUI : MonoBehaviour
{
    public Button startBT;
    public Text levelText;
    public Text scoreText;

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void SetLevel(int level) {
        levelText.text = "Trial: " + level;
    }

    public void ShowStartButton(bool show)
    {
        startBT.gameObject.SetActive(show);
    }

    public void OnStartButtonClick(System.Action onStart)
    {
        startBT.onClick.AddListener(() => onStart.Invoke());
    }

}
