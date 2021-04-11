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
        [Header("萊納邊界")]
        private BoxCollider2D _rainaBound;

        [SerializeField]
        [Header("將被產生的物件")]
        private GameObject prefab;

        [SerializeField]
        [Header("目前產生的數量")]
        private int _currentSpawnCount;


        [SerializeField]
        [Header("產生的萊納陣列")]
        private List<RainaData> _rainaDatas = new List<RainaData>();

        [SerializeField]
        [Header("產生器資料")]
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

        private Vector3 GetSpawnPosition()
        {
            var bounds = _rainaBound.bounds;
            var maxX   = bounds.max.x;
            var minX   = bounds.min.x;
            var maxY   = bounds.max.y;
            var minY   = bounds.min.y;
            var x      = Random.Range(minX , maxX);
            var y      = Random.Range(minY , maxY);
            return new Vector3(x , y , 0);
        }

        private void PlaySpawnAudio()
        {
            var playAudio = RandomUtilities.GetRandomResult(20 , 100);
            if (playAudio) AudioManagerScript.Instance.PlayAudioClip("come");
        }

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

            PlaySpawnAudio();
            var instance = Instantiate(prefab , GetSpawnPosition() , Quaternion.identity
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