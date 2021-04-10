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

        [SerializeField]
        private bool defaultFaceRight;

        private bool _flipX;

        private void Start()
        {
            _transform            = transform;
            _spriteRenderer.flipX = !defaultFaceRight;
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
            if (horizontalValue > 0 && faceRight == GetFaceValue(1))
                _spriteRenderer.flipX = !faceRight;
            else if (horizontalValue < 0 && faceRight == GetFaceValue(-1))
                _spriteRenderer.flipX = !faceRight;
        }

        private bool GetFaceValue(int horizontalValue)
        {
            if (horizontalValue == 1 && defaultFaceRight == false)
                return true;
            if (horizontalValue == -1 && defaultFaceRight == false)
                return false;
            if (horizontalValue == 1 && defaultFaceRight)
                return true;
            if (horizontalValue == -1 && defaultFaceRight)
                return false;

            return false;
        }
    }
}