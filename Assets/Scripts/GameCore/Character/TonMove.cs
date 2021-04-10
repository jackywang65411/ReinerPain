using UnityEngine;

namespace GameCore.Character
{
    public class TonMove : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Transform _transform;

        [SerializeField]
        private float MoveSpeed = 3f;

        private bool _flipX;

        private void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            var horizontalValue = Input.GetAxisRaw("Horizontal");
            var verticalValue   = Input.GetAxisRaw("Vertical");
            _transform.position += new Vector3(horizontalValue , verticalValue , 0)
                                   * (Time.deltaTime * MoveSpeed);
            HandlerFlip(horizontalValue);
        }

        private void HandlerFlip(float horizontalValue)
        {
            var faceRight = _spriteRenderer.flipX;
            if (horizontalValue > 0 && faceRight == false)
                _spriteRenderer.flipX = !faceRight;
            else if (horizontalValue < 0 && faceRight)
                _spriteRenderer.flipX = !faceRight;
        }
    }
}