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

        [SerializeField]
        private GameSetting _gameSetting;

    #endregion

    #region Unity events

        private void Start()
        {
            EndingManager.Instance.ShowEnding(1000);
            var restart       = EndingManager.Instance.transform.Find("Restart Button").gameObject;
            var restartButton = restart.GetComponent<Button>();
            restartButton.OnClickAsObservable()
                         .Subscribe(_ => Restart());
        }

    #endregion

    #region Private Methods

        private void Awake()
        {
            SceneManager.LoadScene("GUI" , LoadSceneMode.Additive);
            // SceneManager.LoadScene("EndingScene" , LoadSceneMode.Additive);
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