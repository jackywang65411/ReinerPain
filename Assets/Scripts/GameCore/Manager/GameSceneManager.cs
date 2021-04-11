using System;
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

        private int _currentScore;

        [SerializeField]
        private GameSetting _gameSetting;

    #endregion

    #region Unity events

        private void Start()
        {
            ScoreSystem.Reset();
            AudioManagerScript.Instance.PlayAudioClip("song");
        }

    #endregion

    #region Public Methods

        public void AddScore(int score)
        {
            _currentScore += score;
            ScoreSystem.ScoreIs(score);
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }

        public void LoadEndingScene()
        {
            SceneManager.LoadScene("EndingScene" , LoadSceneMode.Additive);
            Observable.Timer(TimeSpan.FromSeconds(0.1f))
                      .Subscribe(_ => BindingRestartButton());
        }

    #endregion

    #region Private Methods

        private void Awake()
        {
            SceneManager.LoadScene("GUI" , LoadSceneMode.Additive);
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
            ScoreSystem.Reset();
            SceneManager.UnloadSceneAsync("EndingScene");
            SceneManager.UnloadSceneAsync("GUI");
            SceneManager.LoadScene("GameScene");
        }

    #endregion
    }
}