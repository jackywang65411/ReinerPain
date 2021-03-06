using DG.Tweening;
using GameCore.Manager;
using GameCore.ScriptableObjects;
using UniRx;
using UnityEngine;

namespace GameCore.Character
{
    public class RainaDrop : MonoBehaviour
    {
    #region Private Variables

        private bool isCatch;
        private bool isDie;

        private float h_WithShadow;

        private readonly string    _raina  = "萊納";
        private readonly string    _tonsen = "統神";
        private          Transform _shadow;
        private          Vector2   zero;

        [SerializeField]
        [Header("自動刪除自己的時間")]
        private float _autoDestroyTime = 5f;

        [SerializeField]
        [Header("目前的資料，不用設定此處，預設為空，用來Debug")]
        private RainaData _rainaData;

        [SerializeField]
        private Sprite _sprite;

        [SerializeField]
        [Header("判定面向的SpriteRenderer")]
        private SpriteRenderer _spriteRenderer;

    #endregion

    #region Unity events

        private void Start()
        {
            name = _raina;
            RainaInit();
            Destroy(gameObject , _autoDestroyTime);
        }

    #endregion

    #region Events

        private void OnTriggerStay2D(Collider2D triggeredObject)
        {
            if (isCatch || isDie)
                return;

            if (DetectCatch(triggeredObject))
                Catch(triggeredObject);
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

        private void Catch(Collider2D triggeredObject)
        {
            isCatch = true;
            AudioManagerScript.Instance.PlayAudioClip("catch");
            var score     = _rainaData.Score;
            var textColor = _rainaData.ScoreTextColor;
            FindObjectOfType<GameSceneManager>().AddScore(score);
            var scoreString = $"+{score}";
            FindObjectOfType<ScoreTextManager>().SpawnText(_shadow.position + Vector3.up * 2
                , textColor , scoreString);
            if (_shadow) Destroy(_shadow.gameObject);
            var tonMove = triggeredObject.GetComponent<TonMove>();
            SetFlip(tonMove.currentFlipX);
            tonMove.ObserveEveryValueChanged(move => move.currentFlipX)
                   .Subscribe(flip => SetFlip(flip))
                   .AddTo(gameObject).AddTo(triggeredObject.gameObject);
            transform.parent = triggeredObject.transform;
            GetComponent<TonMove>()?.StopMoving();
            _spriteRenderer.DOFade(0 , 0.5f);
            GetComponent<Collider2D>().enabled = false;
        }

        private bool DetectCatch(Collider2D triggeredObject)
        {
            var nameIsTonsen            = triggeredObject.name == _tonsen;
            var distance                = Mathf.Abs(_spriteRenderer.transform.position.y - _shadow.position.y);
            var matchDistance           = distance <= 1.5f;
            var triggeredObjectPosition = triggeredObject.transform.position;

            var distanceWithTriggerObject = Mathf.Abs(triggeredObjectPosition.y - _shadow.position.y);
            var matchTriggerObject        = distanceWithTriggerObject <= 3.5f;

            var isCatch = nameIsTonsen && matchDistance && matchTriggerObject;

            return isCatch;
        }

        private void RainaInit()
        {
            _shadow            =  transform.Find("Shadow");
            transform.position += (transform.position.y - _shadow.position.y) * Vector3.up;
            _shadow.parent     =  null;
        }

        private void SetFlip(bool flip)
        {
            _spriteRenderer.flipX = flip;
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
            spriteRenderer.sortingOrder      = -1;
            spriteRenderer.sprite            = _rainaData.Sprite_Ground;
            rainaGround.transform.localScale = Vector3.zero;
            rainaGround.transform.DOScale(Vector3.one , 1)
                       .SetEase(Ease.OutQuad);
            Destroy(_shadow.gameObject);
        }

        private void Update()
        {
            if (isCatch) return;
            if (isDie) return;
            if (_spriteRenderer.transform.position.y <= _shadow.position.y)
            {
                isDie = true;
                AudioManagerScript.Instance.PlayAudioClip("oh_shit");
                FindObjectOfType<GameSceneManager>().SubtractHp(1);
                SpawnRainaGround();
            }
        }

    #endregion
    }
}