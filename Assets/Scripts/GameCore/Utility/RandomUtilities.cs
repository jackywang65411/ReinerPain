using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace AutoBot.Scripts.Utilities
{
    public static class RandomUtilities
    {
    #region Public Methods

        public static T GetRandomData<T>(List<T> datas)
        {
            var datasCount = datas.Count;
            Assert.AreNotEqual(0 , datasCount , "count can not be zero");
            if (datasCount == 0) return default;

            var randomIndex = Random.Range(0 , datasCount);
            return datas[randomIndex];
        }

        /// <summary>
        ///     Return a random integer number between min [inclusive] and max [inclusive] (Read Only)
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool GetRandomResult(int rate , int max)
        {
            var randomValue = GetRandomValue(max);
            return GetRPNGResult(randomValue , rate);
        }

        public static int GetRandomValue(int max)
        {
            var randomValue = Random.Range(1 , max + 1);
            return randomValue;
        }

        public static bool GetRPNGResult(int randomValue , int rate)
        {
            return randomValue <= rate;
        }

    #endregion
    }
}