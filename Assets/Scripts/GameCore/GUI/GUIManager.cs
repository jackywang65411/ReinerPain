using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
#region Public Variables

    public int Score;

#endregion

#region Private Variables

    private ScoreSystem Scoresys;

    private Text NowScore;

#endregion

#region Unity events

    private void Start()
    {
        // Scoresys = new ScoreSystem(Score);
    }

#endregion

#region Private Methods

    private void Update()
    {
        NowScore.text = "Score: " + Score;
    }

#endregion
}