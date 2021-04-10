using UnityEngine;

namespace GameCore.Character
{
    public class TonMove : MonoBehaviour
    {
    #region Private Variables

        private bool _flipX;

        private Transform _transform;

        [SerializeField]
        private bool CanControl;

        [SerializeField]
        private bool DefaultFaceRight;

        [SerializeField]
        private float MoveSpeed_Horzontal = 3f;

        [SerializeField]
        private float MoveSpeed_Vertical = 3f;

        [SerializeField]
        private SpriteRenderer SpriteRenderer;

    #endregion

    #region Unity events

        private void Start()
        {
            _transform = transform;
            if (SpriteRenderer) SpriteRenderer.flipX = !DefaultFaceRight;
        }

    #endregion

    #region Public Methods

        public void StopMoving()
        {
            MoveSpeed_Horzontal = 0;
            MoveSpeed_Vertical  = 0;
        }

    #endregion

    #region Private Methods

        private bool GetFaceValue(int horizontalValue)
        {
            if (horizontalValue == 1 && DefaultFaceRight == false)
                return true;
            if (horizontalValue == -1 && DefaultFaceRight == false)
                return false;
            if (horizontalValue == 1 && DefaultFaceRight)
                return true;
            if (horizontalValue == -1 && DefaultFaceRight)
                return false;

            return false;
        }

        private void HandlerFlip(float horizontalValue)
        {
            var faceRight = SpriteRenderer.flipX;
            if (horizontalValue > 0 && faceRight == GetFaceValue(1))
                SpriteRenderer.flipX = !faceRight;
            else if (horizontalValue < 0 && faceRight == GetFaceValue(-1))
                SpriteRenderer.flipX = !faceRight;
        }

        private void Update()
        {
            var horizontalValue = CanControl ? Input.GetAxisRaw("Horizontal") : 1;
            var verticalValue   = CanControl ? Input.GetAxisRaw("Vertical") : 1;
            var x               = horizontalValue * MoveSpeed_Horzontal;
            var y               = verticalValue * MoveSpeed_Vertical;
            _transform.position += new Vector3(x , y , 0) * Time.deltaTime;
            if (CanControl == false) return;
            HandlerFlip(horizontalValue);
        }

    #endregion
    }
}