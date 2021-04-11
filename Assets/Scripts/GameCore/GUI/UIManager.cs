using UnityEngine;
using UnityEngine.UI;

namespace GameCore.GUI
{
    public class UIManager : MonoBehaviour
    {
    #region Public Variables

        public static UIManager Instance
        {
            get
            {
                if (_instace != null)
                {
                    var uiManager = new GameObject("UIManager");
                    _instace = uiManager.AddComponent<UIManager>();
                }

                return _instace;
            }
        }

    #endregion

    #region Private Variables

        private static UIManager _instace;
        private        int       currentHP;

        private int currentScore;

        private int maxHp;

        [SerializeField]
        private GameObject _defaultHpGameObject;

        [SerializeField]
        private Sprite _image_Head_Dead;

        [SerializeField]
        private Sprite image_head;

        [SerializeField]
        private Text _textScore;

        [SerializeField]
        private Transform _hpObjectParent;

    #endregion

    #region Unity events

        private void Start()
        {
            _defaultHpGameObject.SetActive(false);
            ResetAll();
        }

    #endregion

    #region Public Methods

        public void AddScore(int score)
        {
            currentScore    += score;
            _textScore.text =  currentScore.ToString();
        }

        [ContextMenu("ResetAll")]
        public void ResetAll()
        {
            _textScore.text = "0";
            SetMaxHp(maxHp);
        }

        public void SetMaxHp(int hp)
        {
            var childCount       = _hpObjectParent.childCount;
            var needToSpawnCount = hp - childCount;
            for (var i = 0 ; i < needToSpawnCount ; i++)
            {
                var hpInstance = Instantiate(_defaultHpGameObject , _hpObjectParent);
                hpInstance.SetActive(true);
            }

            for (var i = 0 ; i < hp ; i++)
            {
                var childHp = _hpObjectParent.GetChild(i);
                var image   = childHp.GetComponent<Image>();
                image.sprite = image_head;
            }

            maxHp     = hp;
            currentHP = hp;
        }

        public void SubtractHp(int value)
        {
            for (var i = 0 ; i < value ; i++)
                if (currentHP > 0)
                {
                    var hpObjectIndex = maxHp - currentHP;
                    var hpChild       = _hpObjectParent.GetChild(hpObjectIndex);
                    var image         = hpChild.GetComponent<Image>();
                    image.sprite =  _image_Head_Dead;
                    currentHP    -= 1;
                }
        }

        [ContextMenu("SubtractHp")]
        public void SubtractHpMenu()
        {
            SubtractHp(3);
        }

    #endregion

    #region Private Methods

        [ContextMenu("AddScoreMenu")]
        private void AddScoreMenu()
        {
            var randomScore = Random.Range(1 , 100);
            AddScore(randomScore);
        }

        [ContextMenu("SetMaxHPMenu")]
        private void SetMaxHPMenu()
        {
            SetMaxHp(5);
        }

    #endregion
    }
}