using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.Manager
{
    public class ScoreTextManager : MonoBehaviour
    {
    #region Private Variables

        [SerializeField]
        private Text _scoreText;

        [SerializeField]
        private Transform _textParent;

    #endregion

    #region Unity events

        private void Start()
        {
            _scoreText.gameObject.SetActive(false);
        }

    #endregion

    #region Public Methods

        public void SpawnText(Vector3 position , Color color , string score)
        {
            var instance = Instantiate(_scoreText , position , Quaternion.identity , _textParent);
            var text     = instance.GetComponent<Text>();
            text.text  = score;
            text.color = color;
            var scale = instance.transform.localScale;
            instance.transform.DOScale(scale * 0.5f , 1f).SetEase(Ease.OutQuart)
                    .OnComplete(() => Destroy(instance));
            instance.transform.DOLocalMoveY(0.25f , 0.5f).SetEase(Ease.OutQuart);
            instance.gameObject.SetActive(true);
        }

    #endregion
    }
}