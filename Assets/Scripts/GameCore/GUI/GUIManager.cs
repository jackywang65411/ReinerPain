using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public int Score;
    ScoreSystem Scoresys;

    Text NowScore;
    void Start()
    {
        Scoresys = new ScoreSystem(Score);

    }

    void Update()
    {
        NowScore.text = "Score: " + Score.ToString();
    }
}
