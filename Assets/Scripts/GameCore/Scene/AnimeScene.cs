using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class AnimeScene : MonoBehaviour
    {
        private void Start()
        {
            Invoke("ChangeGameScene" , 3f);
        }

        private void ChangeGameScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}