using UnityEngine;

namespace GameCore.ScriptableObjects
{
    [CreateAssetMenu]
    public class GameSetting : ScriptableObject
    {
        [Header("統神生命值")]
        public int TonsenLife;
    }
}