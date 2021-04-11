using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    private static EndingManager _instance;
    public static EndingManager Instance { get { return _instance; } }

    [SerializeField] private Slider sld;
    [SerializeField] private float sliderValue;
    [SerializeField] private Animator anim;
    [SerializeField] private bool updatingSlider;
    [SerializeField] private int scoreTop;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {

        }

        if (updatingSlider)
        {
            UpdateSliderValue();
        }
    }

    /// <summary>
    /// 顯示結束畫面
    /// </summary>
    /// <param name="score">分數</param>
    public void ShowEnding(int score)
    {
        if (anim == null) anim = this.GetComponent<Animator>();

        EndingProccess(score, () =>
        {
            anim.SetTrigger("playEnding");
        });

    }

    private void EndingProccess(int score, System.Action body)
    {
        if (sld == null) sld = this.GetComponentInChildren<Slider>();

        scoreTop = score;
        updatingSlider = true;

        body.Invoke();

        //scoreTop = 0;
        //updatingSlider = false;
    }

    private void UpdateSliderValue()
    {
        sld.value = sliderValue * ( scoreTop / sld.maxValue );
    }
}
