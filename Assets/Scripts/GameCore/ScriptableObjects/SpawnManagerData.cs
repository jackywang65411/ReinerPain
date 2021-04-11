using UnityEngine;

namespace GameCore.ScriptableObjects
{
    [CreateAssetMenu]
    public class SpawnManagerData : ScriptableObject
    {
    #region Public Variables

        [Header("產生時間的曲線")]
        public AnimationCurve Curve;


        [Header("產生時間的曲線最大值(sec)")]
        public float CurveMaxTime = 3f;

        [Header("第一隻產生的時間(sec)")]
        public float FirstRainaSpawnTime = 0.5f;

        [Header("萊納最快的隻數,到達後以時間最小值產生")]
        public int MaxRainaCount = 10;

    #endregion
    }
}