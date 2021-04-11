using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.GUI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance
        {
            get
            {
                if (_instace != null)
                {
                    var uiManager    = new GameObject("UIManager");
                    _instace = uiManager.AddComponent<UIManager>();
                }
                return _instace;
            }
        }

        [SerializeField]
        private Text _textScore;

        [SerializeField]
        private GameObject _defaultHpGameObject;

        [SerializeField]
        private Transform _hpObjectParent;



        private static UIManager _instace;
        private void Start()
        {
            var i = fjhgkjdh();
        }

        private static int fjhgkjdh()
        {
            return 1;
        }

        public void SetMaxHp(int hp)
        {

        }
        public void AddScore(int score)
        {

        }

        public void SubtractHp(int value)
        {

        }
    }
}