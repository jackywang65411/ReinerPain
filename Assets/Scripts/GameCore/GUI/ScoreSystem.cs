using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreSystem : MonoBehaviour , IGUI
{
    int _Score;
    int _HighScore;
    int _ReinerHealth;
    
    public ScoreSystem(int score,int highscore)
    {
        _Score = score;
        _HighScore = highscore;
    }
    public int GetScore
    {
        get { return _Score; }
    }
    public int AddScore
    {
        set { _Score = value; }
    }
    public int ResetScore
    {
        set { _Score = 0; }
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

interface IGUI
{
    int GetScore { get; }
    int AddScore { set; }
    int HighScore { get; set; }

    int Health { get; set; }
}
