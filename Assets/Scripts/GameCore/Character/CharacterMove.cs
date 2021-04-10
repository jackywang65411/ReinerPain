using UnityEngine;

namespace GameCore.Character
{
    public class CharacterMove : MonoBehaviour
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
            _transform.position += Vector3.right * horizontalValue * Time.deltaTime * MoveSpeed;

            _spriteRenderer.flipX = CheckFlip(horizontalValue);
        }

        private bool CheckFlip(float horizontalValue)
        {
            var currentFlip = _spriteRenderer.flipX;
            return horizontalValue >= 0;
            if (currentFlip == false && horizontalValue >= 0)
                return true;
            if (currentFlip && horizontalValue < 0)
                return false;
            return false;
        }
    }
}