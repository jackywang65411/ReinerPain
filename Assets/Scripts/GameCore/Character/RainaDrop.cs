using UnityEngine;

namespace GameCore.Character
{
    public class RainaDrop : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            this.name    = "萊納";
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D triggeredObject)
        {
            if (triggeredObject.name == "統神" || triggeredObject.name == "萊納")
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