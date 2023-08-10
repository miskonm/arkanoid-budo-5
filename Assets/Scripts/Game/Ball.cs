using Arkanoid.Game.Services;
using UnityEngine;

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

        #region Unity lifecycle

        private void Start()
        {
            _offset = transform.position - _platform.transform.position;

            PerformStartActions();
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

            if (GameService.Instance.NeedAutoPlay)
            {
                StartBall();
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