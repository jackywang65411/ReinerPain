using UnityEngine;

namespace GameCore.Character
{
    public class RainaDrop : MonoBehaviour
    {
    #region Private Variables

        private          Rigidbody2D _rigidbody2D;
        private readonly string      _raina  = "萊納";
        private readonly string      _tonsen = "統神";
        private          Vector2     zero;

    #endregion

    #region Unity events

        private void Start()
        {
            name         = _raina;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

    #endregion

    #region Events

        private void OnTriggerEnter2D(Collider2D triggeredObject)
        {
            if (triggeredObject.name == _tonsen ||
                triggeredObject.name == _raina)
                StopMoving(triggeredObject);
        }

    #endregion

    #region Private Methods

        private void StopMoving(Collider2D triggeredObject)
        {
            transform.parent = triggeredObject.transform;
            GetComponent<TonMove>()?.StopMoving();
        }

    #endregion
    }
}