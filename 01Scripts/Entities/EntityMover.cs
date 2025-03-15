using System;
using System.Collections;
using DG.Tweening;
using LJS.Core.StatSystem;
using UnityEngine;

namespace LJS.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        public enum AddForceType
        {
            MoveTransfom, UseRigid
        }
        
        public event Action<Vector2> OnMovement;

        [Header("Collision detect")] 
        [SerializeField] private Transform _groundCheckTrm;
        [SerializeField] private Vector2 _checkerSize;
        [SerializeField] private float _checkDistance;
        [SerializeField] private LayerMask _whatIsGround;

        [Header("Stats")] 
        [SerializeField] private StatSO _moveSpeedStat;
        
        private Rigidbody2D _rbCompo;
        private EntityRenderer _renderer;
        private EntityStat _statCompo;

        private float _moveSpeed = 6f; //todo : 나중에 스텟시스템으로 변경한다.
        private Vector2 _movement;
        private float _moveSpeedMultiplier, _originalGravity;

        [field: SerializeField] public bool CanManualMove { get; set; } = true;
        [field: SerializeField] public float SpeedReduction { get; private set; }
        public bool JumpNow { get; private set; } = false;
        
        private Entity _entity;
        private Tween _moveYTween;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _rbCompo = entity.GetComponent<Rigidbody2D>();
            _renderer = entity.GetCompo<EntityRenderer>();
            _statCompo = entity.GetCompo<EntityStat>();
            
            _originalGravity = _rbCompo.gravityScale;
            _moveSpeedMultiplier = 1f;
        }
        
        public void AfterInit()
        {
            _moveSpeedStat = _statCompo.GetStat(_moveSpeedStat);
            _moveSpeedStat.OnValueChange += HandleMoveSpeedChange;
            _moveSpeed = _moveSpeedStat.Value; //초기화 한번은 필요해
        }

        private void OnDestroy()
        {
            _moveSpeedStat.OnValueChange -= HandleMoveSpeedChange;
        }

        private void HandleMoveSpeedChange(StatSO stat, float current, float prev)
        {
            _moveSpeed = current;
        }

        private void FixedUpdate()
        {
            if(CanManualMove)
                _rbCompo.linearVelocity = !JumpNow ? 
                    _movement * _moveSpeed * _moveSpeedMultiplier : 
                    _movement / SpeedReduction * _moveSpeed * _moveSpeedMultiplier;
            
            OnMovement?.Invoke(_rbCompo.linearVelocity);
        }

        public void SetMovement(Vector2 Movement)
        {
            _movement = Movement;
            _renderer.FlipController(_movement.x);
        }

        public void StopImmediately(bool isYAxisToo = false)
        {
            if (isYAxisToo)
                _rbCompo.linearVelocity = Vector2.zero;
            else
                _rbCompo.linearVelocityX = 0;

            _movement = Vector2.zero;
        }
        
        public void SetMovementMultiplier(float value) => _moveSpeedMultiplier = value;
        public void SetGravityMultiplier(float value) => _rbCompo.gravityScale = value;

        public void AddForceToEntity(Vector2 force, AddForceType forceType, 
            ForceMode2D mode = ForceMode2D.Impulse, Action endEvent = null)
        {
            if(forceType == AddForceType.MoveTransfom)
                AddForceCoro(force, mode, _entity.transform, endEvent);
            else 
                _rbCompo.AddForce(force, mode);
        }
        
        public void AddForceToVisual(Vector2 force, AddForceType forceType,
            ForceMode2D mode = ForceMode2D.Impulse, Action endEvent = null
            )
        {
            if (forceType == AddForceType.MoveTransfom)
                StartCoroutine(AddForceCoro(force, mode, 
                    _entity.VisualTrm, endEvent));
            else
                _rbCompo.AddForce(force, mode);
        }

        private IEnumerator AddForceCoro(Vector2 force, ForceMode2D mode, 
            Transform target = null, Action endEvent = null)
        {
            Vector2 origiPos = transform.position;
            float currentTime = 0;
            while (currentTime < 1)
            {
                currentTime += Time.deltaTime;
                transform.position = Vector3.Lerp(origiPos, origiPos + force, currentTime);
                yield return null;
            }
        }

        public void Jump(Vector2 force,  Ease jumpEase = Ease.Linear, Ease fallEase = Ease.Linear, 
            Action jumpEndEvent = null, Action endEvent = null)
        {
            JumpNow = true;
            Vector2 originPos = _entity.transform.position;
            Transform target = _entity.VisualTrm;
            _moveYTween.Kill();
            
            _moveYTween = target.DOMoveY((originPos + force).y,
                    1).SetEase(jumpEase)
                .OnComplete(() =>
                {
                    jumpEndEvent?.Invoke();
                    ReturnPos(-force, fallEase, endEvent);
                });
        }

        private void ReturnPos(Vector2 force, Ease ease = Ease.Linear, Action endEvent = null)
        {
            Transform target = _entity.VisualTrm;
            Vector2 originPos = target.transform.position;
            _moveYTween.Kill();
            
            _moveYTween = target.DOMoveY((originPos + force).y,
                    1).SetEase(ease)
                .OnComplete(() =>
                {
                    endEvent?.Invoke();
                    JumpNow = false;
                });
        }
        
        #region KnockBack

        public void KnockBack(Vector2 force, float time, AddForceType forceType)
        {
            CanManualMove = false;
            StopImmediately(true);
            AddForceToEntity(force, forceType);
            DOVirtual.DelayedCall(time, () => CanManualMove = true);
        }
        
        #endregion
        
        #region  CheckCollision
        
        public virtual bool IsGroundDetected()
            => Physics2D.BoxCast(_groundCheckTrm.position, 
                _checkerSize, 0, Vector2.down, _checkDistance, _whatIsGround);
        
        #endregion
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            if (_groundCheckTrm != null)
            {
                Vector3 offset = new Vector3(0, _checkDistance * 0.5f);
                Gizmos.DrawWireCube(_groundCheckTrm.position - offset, 
                    new Vector3(_checkerSize.x, _checkDistance, 1f));
            }
        }
#endif
        
    }
}
