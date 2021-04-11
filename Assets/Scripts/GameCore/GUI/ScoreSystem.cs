using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreSystem : MonoBehaviour , IGUI
{
    int _Score;
    int _ReinerHealth;
    
    public ScoreSystem(int value)
    {
        _Score = value;
    }
    public int GetScore
    {
        get { return _Score; }
    }
    public int ResetScore
    {
        set { _Score = 0; }
    }
    public int Health
    {
        get { return _ReinerHealth; }
        set { _ReinerHealth = value; }
    }

    public void AddScore(int AddNewScore)
    {
        _Score += AddNewScore;
    }

}

interface IGUI
{
    int GetScore { get; }
    int ResetScore { set; }
    int Health { get; set; }

}
