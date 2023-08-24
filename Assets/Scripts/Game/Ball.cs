using System;
using System.Collections.Generic;
using Arkanoid.Game.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arkanoid.Game
{
    public class Ball : MonoBehaviour
    {
        #region Variables

        private const string LogTag = nameof(Ball);

        [SerializeField] private Platform _platform;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Vector2 _startVelocity;

        private readonly Dictionary<Type, BallEffect> _ballEffectsByType = new();

        private bool _isStarted;
        private Vector3 _offset;

        #endregion

        #region Events

        public static event Action<Ball> OnCreated;
        public static event Action<Ball> OnDestroyed;

        #endregion

        #region Properties

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Rigidbody2D Rb => _rb;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _offset = transform.position - _platform.transform.position;
            FillEffectsDict();
            PerformStartActions();

            OnCreated?.Invoke(this);
        }

        private void Start()
        {
            if (GameService.Instance.NeedAutoPlay)
            {
                StartBall();
            }
        }

        private void Update()
        {
            if (_isStarted)
            {
                return;
            }

            MoveWithPad();

            if (Input.GetMouseButtonDown(0))
            {
                StartBall();
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            CollisionEffects(other);
        }

        private void CollisionEffects(Collision2D other)
        {
            foreach (BallEffect ballEffect in _ballEffectsByType.Values)
            {
                ballEffect.PerformCollision(other);
            }
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            if (!_isStarted)
            {
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)_startVelocity);
            }
            else
            {
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)_rb.velocity);
            }
        }

        #endregion

        #region Public methods

        public Ball Clone()
        {
            Ball clone = Instantiate(this, transform.position, Quaternion.identity);
            clone._isStarted = _isStarted;
            clone._offset = _offset;
            clone._rb.velocity = _rb.velocity;
            CloneAllEffects(clone);

            return clone;
        }

        public TEffect GetEffect<TEffect>() where TEffect : BallEffect
        {
            Type type = typeof(TEffect);
            _ballEffectsByType.TryGetValue(type, out BallEffect effect);
            return effect as TEffect;
        }

        public void RandomizeDirection()
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float currentSpeed = _rb.velocity.magnitude;
            _rb.velocity = randomDirection * currentSpeed;
        }

        public void ResetBall()
        {
            MoveWithPad();
            PerformStartActions();
            ResetEffects();
        }

        #endregion

        #region Private methods

        private void CloneAllEffects(Ball clone)
        {
            foreach (KeyValuePair<Type, BallEffect> ballEffectKvp in clone._ballEffectsByType)
            {
                ballEffectKvp.Value.Copy(_ballEffectsByType[ballEffectKvp.Key]);
            }
        }

        private void FillEffectsDict()
        {
            BallEffect[] ballEffects = GetComponentsInChildren<BallEffect>();
            foreach (BallEffect ballEffect in ballEffects)
            {
                Type ballType = ballEffect.GetType();
                if (!_ballEffectsByType.ContainsKey(ballType))
                {
                    ballEffect.SetOwner(this);
                    _ballEffectsByType.Add(ballType, ballEffect);
                }
                else
                {
                    Debug.LogError($"[{LogTag}:{nameof(FillEffectsDict)}] There are more than 1 effect with " +
                                   $"type '{ballType}' on ball!");
                }
            }
        }

        private void MoveWithPad()
        {
            Vector3 platformPosition = _platform.transform.position;
            platformPosition += _offset;
            transform.position = platformPosition;
        }

        private void PerformStartActions()
        {
            _isStarted = false;
            _rb.velocity = Vector2.zero;
        }

        private void ResetEffects()
        {
            foreach (BallEffect ballEffect in _ballEffectsByType.Values)
            {
                ballEffect.Reset();
            }
        }

        private void StartBall()
        {
            _isStarted = true;
            _rb.velocity = _startVelocity;
        }

        #endregion
    }
}