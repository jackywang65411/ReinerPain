using System;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace GameCore.GameMechanics
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        [Header("將被產生的物件")]
        private GameObject prefab;

        [SerializeField]
        private float eachSpawnTime;

        private void Start()
        {
            var spawnTime = TimeSpan.FromSeconds(eachSpawnTime);
            Observable.Timer(spawnTime , spawnTime)
                      .Subscribe(_ => SpawnObject())
                      .AddTo(gameObject);
        }

        private void SpawnObject()
        {
            Instantiate(prefab
                , Random.insideUnitCircle
                , Quaternion.identity
                , transform);
        }
    }
}