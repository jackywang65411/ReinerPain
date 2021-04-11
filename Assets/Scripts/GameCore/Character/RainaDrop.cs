using DG.Tweening;
using GameCore.ScriptableObjects;
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
        [Header("自動刪除自己的時間")]
        private float _autoDestroyTime = 5f;

        [SerializeField]
        [Header("目前的資料，不用設定此處，預設為空，用來Debug")]
        private RainaData _rainaData;

        [SerializeField]
        [Header("判定面向的SpriteRenderer")]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Sprite _sprite;

        private float     h_WithShadow;
        private Transform _shadow;
        private bool      isDie;
        private bool      isCatch;

    #endregion

    #region Unity events

        private void Start()
        {
            name = _raina;
            RainaInit();
            Destroy(gameObject , _autoDestroyTime);
        }

        private void RainaInit()
        {
            _shadow        = transform.Find("Shadow");
            _shadow.parent = null;
        }

        private void Update()
        {
            if (isCatch) return;
            if (isDie) return;
            if (_spriteRenderer.transform.position.y <= _shadow.position.y)
            {
                isDie = true;
                SpawnRainaGround();
            }
        }

        private void SpawnRainaGround()
        {
            var rainaGround = new GameObject("RainaGround");
            GetComponent<TonMove>()?.StopMoving();
            _spriteRenderer.transform.DOScaleY(0 , 0.5f)
                           .SetEase(Ease.OutQuad)
                           .OnStepComplete(() => Destroy(gameObject));
            _spriteRenderer.DOFade(0 , 0.5f);
            rainaGround.transform.position = _shadow.position;
            var spriteRenderer = rainaGround.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite            = _rainaData.Sprite_Ground;
            rainaGround.transform.localScale = Vector3.zero;
            rainaGround.transform.DOScale(Vector3.one , 1)
                       .SetEase(Ease.OutQuad);
            Destroy(_shadow.gameObject);
        }

    #endregion

    #region Events

        private void OnTriggerEnter2D(Collider2D triggeredObject)
        {
            if (triggeredObject.name == _tonsen)
                StopMoving(triggeredObject);
        }

    #endregion

    #region Public Methods

        public void SetData(RainaData rainaData)
        {
            _rainaData             = rainaData;
            _spriteRenderer.sprite = rainaData.Sprite;
        }

    #endregion

    #region Private Methods

        private void SetFlip(bool flip)
        {
            _spriteRenderer.flipX = flip;
        }

        private void StopMoving(Collider2D triggeredObject)
        {
            isCatch = true;
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