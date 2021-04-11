using UnityEngine;

namespace GameCore.Manager
{
    public class RainaFlyManager : MonoBehaviour
    {
    #region Private Variables

        private readonly float _maxSpawnTime = 1f;

        private readonly float _minSpawnTime = 0.5f;

        [SerializeField]
        private float _maxX;

        [SerializeField]
        private float _minX;

        [SerializeField]
        private GameObject _defaultRaina;

    #endregion

    #region Unity events

        private void Start()
        {
            Invoke("Spawn" , 0.2f);
        }

    #endregion

    #region Private Methods

        private void Spawn()
        {
            var rainaPos = _defaultRaina.transform.position;
            var x        = Random.Range(_minX , _maxX) + rainaPos.x;
            var y        = rainaPos.y;
            var pos      = new Vector3(x , y , 0);
            var instance = Instantiate(_defaultRaina , pos , Quaternion.identity , transform);
            Destroy(instance , 5f);
            var time = Random.Range(_minSpawnTime , _maxSpawnTime);
            Invoke("Spawn" , time);
        }

    #endregion
    }
}