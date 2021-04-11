using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    Image[] imgHpSet;

    static int hpMax;
    static int hpNow;
    static Image[] imgHp;
    void Awake() {
        imgHp = imgHpSet;
        hpMax = imgHp.Length;
        hpNow = hpMax;
    }
    /// <summary>
    /// 回傳當前HP
    /// </summary>
    /// <returns></returns>
    public static int HpNow() {
        return hpNow;
    }
    /// <summary>
    /// HP增減多少，並回傳處理後結果
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int HpIs(int value) {
        hpNow += value;
        if (hpNow<=0) {
            hpNow = 0;
        }
        for (int i = 0; i < imgHp.Length; i++) {
            if (i< hpNow) {
                imgHp[i].enabled = true;
            } else {
                imgHp[i].enabled = false;
            }
        }
        return hpNow;
    }
    /// <summary>
    /// HP變成多少，並回傳處理後結果
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int HpTo(int value) {
        hpNow = value;
        if (hpNow <= 0) {
            hpNow = 0;
        }
        for (int i = 0; i < imgHp.Length; i++) {
            if (i < hpNow) {
                imgHp[i].enabled = true;
            } else {
                imgHp[i].enabled = false;
            }
        }
        return hpNow;
    }
}

interface IGUI
{
    int GetScore { get; }
    int ResetScore { set; }
    int Health { get; set; }

}
