using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreSystem : MonoBehaviour , IScore
{
    int _Score;
    int _HighScore;
    int _ReinerHealth;

    public HighScoreSystem(int score,int highscore)
    {
        _Score = score;
        _HighScore = highscore;
    }
    public int Score
    {
        get { return _Score; }
        set { _Score = value; }
    }
    public int HighScore
    {
        get { return _HighScore; }
        set { _HighScore = value; }
    }
    public int Health
    {
        get { return _ReinerHealth; }
        set { _ReinerHealth = value; }
    }
}

interface IScore
{
    int Score { get; set; }
    int HighScore { get; set; }

    int Health { get; set; }
}
