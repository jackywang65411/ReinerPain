using UnityEngine;

namespace GameCore.Character
{
    public class RainaDrop : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Vector2     zero;
        private string      _tonsen = "統神";
        private string      _raina  = "萊納";

        private void Start()
        {
            this.name    = _raina;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D triggeredObject)
        {
            if (triggeredObject.name == _tonsen ||
                triggeredObject.name == _raina)
            {
                StopMoving(triggeredObject);
            }
        }

        private void StopMoving(Collider2D triggeredObject)
        {
            _rigidbody2D.isKinematic = true;
            _rigidbody2D.velocity    = Vector2.zero;
            transform.parent         = triggeredObject.transform;
        }
    }
}