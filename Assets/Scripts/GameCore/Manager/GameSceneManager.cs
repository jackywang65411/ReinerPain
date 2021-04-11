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
            SceneManager.sceneLoaded += (scene , mode) => OnSceneLoaded(scene);
        }

    #endregion

    #region Events

        private void OnSceneLoaded(Scene scene)
        {
            if (scene.name == "EndingScene")
            {
                EndingManager.Instance.ShowEnding(_currentScore);
                var restart       = EndingManager.Instance.transform.Find("Restart Button").gameObject;
                var restartButton = restart.GetComponent<Button>();
                restartButton.OnClickAsObservable()
                             .Subscribe(_ => Restart());
            }
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
        }

    #endregion

    #region Private Methods

        private void Awake()
        {
            SceneManager.LoadScene("GUI" , LoadSceneMode.Additive);
        }

        private void Restart()
        {
            SceneManager.UnloadSceneAsync("EndingScene");
            SceneManager.UnloadSceneAsync("GUI");
            SceneManager.LoadScene("GameScene");
        }

    #endregion
    }
}