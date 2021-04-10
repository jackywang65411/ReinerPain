using System;
using System.Collections.Generic;
using AutoBot.Scripts.Utilities;
using GameCore.Character;
using GameCore.ScriptableObjects;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace GameCore.GameMechanics
{
    public class SpawnManager : MonoBehaviour
    {
    #region Private Variables

        [SerializeField]
        [Header("多久產生一次物件")]
        private float eachSpawnTime;

        [SerializeField]
        [Header("將被產生的物件")]
        private GameObject prefab;

        [SerializeField]
        private List<RainaData> _rainaDatas = new List<RainaData>();

    #endregion

    #region Unity events

        private void Start()
        {
            var spawnTime = TimeSpan.FromSeconds(eachSpawnTime);
            Observable.Timer(spawnTime , spawnTime)
                      .Subscribe(_ => SpawnObject())
                      .AddTo(gameObject);
        }

    #endregion

    #region Private Methods

        private void SpawnObject()
        {
            Assert.IsNotNull(_rainaDatas);
            var dataCount = _rainaDatas.Count;
            Assert.IsTrue(dataCount >= 1);
            var randomRainaData = RandomUtilities.GetRandomData(_rainaDatas);

            var instance = Instantiate(prefab
                , Random.insideUnitCircle
                , Quaternion.identity
                , transform);
            var rainaDrop = instance.GetComponent<RainaDrop>();
            Assert.IsNotNull(rainaDrop);
            rainaDrop.SetData(randomRainaData);
        }

    #endregion
    }
}