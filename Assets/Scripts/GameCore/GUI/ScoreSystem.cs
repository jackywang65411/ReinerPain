using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField]
    Image[] imgHpSet;
    [SerializeField]
    Text txtScoreSet;

    static int score;
    static int hpMax;
    static int hpNow;
    static Image[] imgHp;
    static Text txtScore;
    void Awake() {
        imgHp = imgHpSet;
        hpMax = imgHp.Length;
        hpNow = hpMax;
        txtScore = txtScoreSet;
        txtScore.text = score+"";
        for (int i = 0; i < imgHp.Length; i++) {
            if (i < hpNow) {
                imgHp[i].enabled = true;
            } else {
                imgHp[i].enabled = false;
            }
        }
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
    public static int ScoreNow() {
        return score;
    }
    public static int ScoreIs(int value) {
        score += value;
        txtScore.text = score+"";
        return score;
    }
    public static int ScoreTo(int value) {
        score = value;
        txtScore.text = score + "";
        return score;
    }
    public static void Reset() {
        HpTo(5);
        ScoreTo(0);
    }
}