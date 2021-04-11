using System;
using GameCore.GameMechanics;
using GameCore.GUI;
using GameCore.ScriptableObjects;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameCore.Manager
{
    public class GameSceneManager : MonoBehaviour
    {
    #region Private Variables

        private int _currentMaxHp;

        private int _currentScore;

        private UIManager _uiManager;

        [SerializeField]
        private GameSetting _gameSetting;

    #endregion

    #region Unity events

        private void Start()
        {
            AudioManagerScript.Instance.PlayAudioClip("song");
        }

    #endregion

    #region Public Methods

        public void AddScore(int score)
        {
            _currentScore += score;
            _uiManager.AddScore(score);
        }

        public void LoadEndingScene()
        {
            SceneManager.LoadScene("EndingScene" , LoadSceneMode.Additive);
            Observable.Timer(TimeSpan.FromSeconds(0.1f))
                      .Subscribe(_ => BindingRestartButton());
        }

        public void SubtractHp(int hp)
        {
            _currentMaxHp -= hp;
            _uiManager.SubtractHp(hp);
            if (_currentMaxHp <= 0)
            {
                AudioManagerScript.Instance.PlayAudioClip("yelling");
                FindObjectOfType<SpawnManager>().Stop();
                LoadEndingScene();
            }
        }

    #endregion

    #region Private Methods

        private void Awake()
        {
            SceneManager.LoadScene("BattleUIScene" , LoadSceneMode.Additive);
            Observable.ReturnUnit().DelayFrame(2)
                      .Subscribe(_ => SetMaxHp());
        }

        private void BindingRestartButton()
        {
            EndingManager.Instance.ShowEnding(_currentScore);
            var restart       = EndingManager.Instance.transform.Find("Restart Button").gameObject;
            var restartButton = restart.GetComponent<Button>();

            restartButton.OnClickAsObservable()
                         .Take(1)
                         .Subscribe(_ => Restart())
                         .AddTo(restartButton);
        }

        private void Restart()
        {
            _uiManager.ResetAll();
            SceneManager.UnloadSceneAsync("EndingScene");
            SceneManager.UnloadSceneAsync("BattleUIScene");
            SceneManager.LoadScene("GameScene");
        }

        private void SetMaxHp()
        {
            _uiManager = FindObjectOfType<UIManager>();
            _uiManager.SetMaxHp(_gameSetting.TonsenLife);
            _currentMaxHp = _gameSetting.TonsenLife;
        }

    #endregion
    }
}