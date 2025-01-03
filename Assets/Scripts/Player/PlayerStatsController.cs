using System.Collections;
using Root;
using UnityEngine;

namespace Player
{
    public class PlayerStatsController : MonoBehaviour, IHealthAndDamage
    {
        // public event Action TakeDamage;
        
        public static PlayerStatsController PlayerStats { get; set; }

        [SerializeField] private int _health = 10;
        [SerializeField] private float _pushForce = 5;

        private Animator _animator;
        private Rigidbody2D _rb;
        private PlayerMovementController _playerMovementController;
        private PlayerInputController _playerInputController;
        private Vector2 _pushDirection = new Vector2(1, 1);
        
        private static readonly int GetHit = Animator.StringToHash("getHit");
        private static readonly int Die1 = Animator.StringToHash("die");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _playerMovementController = GetComponent<PlayerMovementController>();
            _playerInputController = GetComponent<PlayerInputController>();

            PlayerStats = this;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            
            if (_health <= 0)
            {
                Die();
            }
            else
            {
                _animator.SetTrigger(GetHit);

                CheckDirection();
                
                _rb.AddForce(_pushDirection.normalized * _pushForce, ForceMode2D.Impulse);
                
                StartCoroutine(WaitCoroutine());
            }
        }

        private void CheckDirection()
        {
            if (_playerMovementController.IsFacingRight)
            {
                _pushDirection.x = -1;
            }
            else
            {
                _pushDirection.x = 1;
            }
        }

        private void Die()
        {
            _playerInputController.enabled = false;
            _animator.SetTrigger(Die1);
        }

        public int GetCurrentHealth()
        {
            return _health;
        }
        
        private IEnumerator WaitCoroutine()
        {
            _playerInputController.enabled = false;
            yield return new WaitForSeconds(1f);
            _playerInputController.enabled = true;

        }
    }
}