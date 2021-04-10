using UnityEngine;

namespace GameCore.ScriptableObjects
{
    [CreateAssetMenu]
    public class RainaData : ScriptableObject
    {
    #region Public Variables

        [Header("減速效果，0為沒效果，30為30%以此類推")]
        public int MoveSpeedDecrease;

        [Header("分數")]
        public int Score;

        [Header("圖片")]
        public Sprite Sprite;

        [Header("名字，程式判斷用")]
        public string Name;

    #endregion
    }
}