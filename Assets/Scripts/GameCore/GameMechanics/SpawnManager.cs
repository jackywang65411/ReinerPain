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
        [Header("Log產生時間與細節")]
        private bool UseLog;

        [SerializeField]
        [Header("將被產生的物件")]
        private GameObject prefab;

        [SerializeField]
        private int _currentSpawnCount;

        [SerializeField]
        private List<RainaData> _rainaDatas = new List<RainaData>();

        [SerializeField]
        private SpawnManagerData _spawnManagerData;

    #endregion

    #region Unity events

        private void Start()
        {
            var firstRainaSpawnTime = _spawnManagerData.FirstRainaSpawnTime;
            SpawnTimer(firstRainaSpawnTime);
        }

    #endregion

    #region Private Methods

        private void SpawnComplete()
        {
            var countRatio = (float)_currentSpawnCount / _spawnManagerData.MaxRainaCount;
            var ratio      = Mathf.Min(countRatio , 1);
            var y          = _spawnManagerData.Curve.Evaluate(ratio);
            var nextTime   = y * _spawnManagerData.CurveMaxTime;
            if (UseLog)
                Debug.Log($"SpawnComplete , currentCount: {_currentSpawnCount}" +
                          $" countRatio : {countRatio} " +
                          $" ratio : {ratio} nextTime: {nextTime}");

            SpawnTimer(nextTime);
            _currentSpawnCount++;
        }

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

        private void SpawnTimer(float firstRainaSpawnTime)
        {
            var spawnTime = TimeSpan.FromSeconds(firstRainaSpawnTime);
            Observable.Timer(spawnTime)
                      .Subscribe(_ => SpawnObject() , SpawnComplete)
                      .AddTo(gameObject);
        }

    #endregion
    }
}