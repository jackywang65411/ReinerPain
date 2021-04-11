using System.Runtime.Serialization;
using GameCore.ScriptableObjects;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameCore.Manager
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField]
        private GameSetting _gameSetting;

        private void Awake()
        {
            SceneManager.LoadScene("GUI" , LoadSceneMode.Additive);
            SceneManager.LoadScene("EndingScene" , LoadSceneMode.Additive);
        }

        private void Start()
        {
            EndingManager.Instance.ShowEnding(1000);
            var restart = EndingManager.Instance.transform.Find("Restart Button").gameObject;
            var restartButton     = restart.GetComponent<Button>();
            restartButton.OnClickAsObservable()
                         .Subscribe(_ => Restart());
        }

        private void Restart()
        {
            SceneManager.UnloadSceneAsync("EndingScene");
            Debug.Log($"Restart");
        }
    }
}