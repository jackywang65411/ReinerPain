using UnityEngine;

namespace GameCore.Character
{
    public class TonMove : MonoBehaviour
    {
    #region Private Variables

        private bool _flipX;

        private Transform _transform;

        [SerializeField]
        private bool defaultFaceRight;

        [SerializeField]
        private float MoveSpeed_Horzontal = 3f;

        [SerializeField]
        private float MoveSpeed_Vertical = 3f;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

    #endregion

    #region Unity events

        private void Start()
        {
            _transform            = transform;
            _spriteRenderer.flipX = !defaultFaceRight;
        }

    #endregion

    #region Private Methods

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

        private void HandlerFlip(float horizontalValue)
        {
            var faceRight = _spriteRenderer.flipX;
            if (horizontalValue > 0 && faceRight == GetFaceValue(1))
                _spriteRenderer.flipX = !faceRight;
            else if (horizontalValue < 0 && faceRight == GetFaceValue(-1))
                _spriteRenderer.flipX = !faceRight;
        }

        private void Update()
        {
            var horizontalValue = Input.GetAxisRaw("Horizontal");
            var verticalValue   = Input.GetAxisRaw("Vertical");
            var x               = horizontalValue * MoveSpeed_Horzontal;
            var y               = verticalValue * MoveSpeed_Vertical;
            _transform.position += new Vector3(x , y , 0) * Time.deltaTime;
            HandlerFlip(horizontalValue);
        }

    #endregion
    }
}