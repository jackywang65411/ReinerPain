using UnityEngine;

namespace GameCore.ScriptableObjects
{
    [CreateAssetMenu]
    public class RainaData : ScriptableObject
    {
    #region Public Variables

        public Color ScoreTextColor;

        [Header("分數")]
        public int Score;

        [Header("萊納圖片")]
        public Sprite Sprite;

        [Header("萊納地板圖片")]
        public Sprite Sprite_Ground;

    #endregion
    }
}