using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineCreator : MonoBehaviour
{
    public List<int> scoreStandard;
    public GameObject linePrefab;

    [ContextMenu("CreateLines")]
    public void CreateLines()
    {
        if (scoreStandard == null || scoreStandard.Count <= 0 || linePrefab == null)
            return;

        RectTransform _rt = this.GetComponent<RectTransform>();

        if (_rt == null)
            return;

        Slider _sld = GetComponentInParent<Slider>();
        int _maxValue = _sld != null ? (int)_sld.maxValue : 0;
        float _unit = _rt.rect.height / _maxValue;

        for (int i = 0; i < scoreStandard.Count; i++)
        {
            GameObject _go = Instantiate(linePrefab, this.transform);
            Text _scoreTxt = _go.GetComponentInChildren<Text>();
            _scoreTxt.text = scoreStandard[i].ToString();
            _go.transform.localPosition = new Vector3(transform.localPosition.x, ( -_rt.rect.height / 2 ) + ( scoreStandard[i] * _unit ), transform.localPosition.z);
        }


    }
}
