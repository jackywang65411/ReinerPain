using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    private static EndingManager _instance;

    public static EndingManager Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private Slider sld;

    [SerializeField]
    private Text scoreLabel;

    [SerializeField]
    private float sliderValue;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private bool updatingSlider;

    [SerializeField]
    private int scoreTop;

    [SerializeField]
    private List<GameObject> endingGroup;

    [SerializeField]
    private LineCreator lineCreator;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ShowEnding(500);
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
        StartCoroutine(Cor_ShowEnding(score));
    }

    private IEnumerator Cor_ShowEnding(int score)
    {
        if (anim == null) anim = this.GetComponent<Animator>();
        if (sld == null) sld   = this.GetComponentInChildren<Slider>();

        scoreTop        = score;
        scoreLabel.text = score.ToString();
        updatingSlider  = true;
        sld.value       = 0;

        for (int i = endingGroup.Count - 1 ; i >= 0 ; i--)
        {
            bool _onOff = score >= lineCreator.scoreStandard[i];
            endingGroup[i].SetActive(_onOff);

            if (_onOff)
                break;
        }

        anim.SetTrigger("playEnding");
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        Debug.Log("Test");
    }

    private void UpdateSliderValue()
    {
        sld.value = sliderValue * scoreTop;
    }
}