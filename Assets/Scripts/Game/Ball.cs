using System;
using Arkanoid.Game.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arkanoid.Game
{
    public class Ball : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Platform _platform;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Vector2 _startVelocity;

        private bool _isStarted;
        private Vector3 _offset;

        #endregion

        #region Events

        public static event Action<Ball> OnCreated;
        public static event Action<Ball> OnDestroyed;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _offset = transform.position - _platform.transform.position;

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
            
            

            return clone;
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
        }

        #endregion

        #region Private methods

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

        private void StartBall()
        {
            _isStarted = true;
            _rb.velocity = _startVelocity;
        }

        #endregion
    }
}