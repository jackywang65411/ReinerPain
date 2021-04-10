using UniRx;
using UnityEngine;

namespace GameCore.Character
{
    public class RainaDrop : MonoBehaviour
    {
    #region Private Variables

        private readonly string  _raina  = "萊納";
        private readonly string  _tonsen = "統神";
        private          Vector2 zero;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

    #endregion

    #region Unity events

        private void Start()
        {
            name = _raina;
        }

    #endregion

    #region Events

        private void OnTriggerEnter2D(Collider2D triggeredObject)
        {
            if (triggeredObject.name == _tonsen)
                StopMoving(triggeredObject);
        }

    #endregion

    #region Private Methods

        private void SetFlip(bool flip)
        {
            _spriteRenderer.flipX = flip;
        }

        private void StopMoving(Collider2D triggeredObject)
        {
            var tonMove = triggeredObject.GetComponent<TonMove>();
            SetFlip(tonMove.currentFlipX);
            tonMove.ObserveEveryValueChanged(move => move.currentFlipX)
                   .Subscribe(flip => SetFlip(flip))
                   .AddTo(gameObject).AddTo(triggeredObject.gameObject);
            transform.parent = triggeredObject.transform;
            GetComponent<TonMove>()?.StopMoving();
        }

    #endregion
    }
}