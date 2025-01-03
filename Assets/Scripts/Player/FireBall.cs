using UnityEngine;

namespace Player
{
    public class FireBall : MonoBehaviour
    {
        [SerializeField] private float _fireBallSpeed = 10f;
        [SerializeField] private float _lifeTime = 4f; // Время жизни фаербола
        
        private Wand _wand;
        private Vector2 _direction;
        private Animator _animator;
        private bool _isHit;
        private float _timeAlive; // Таймер времени жизни
        
        private static readonly int Hit = Animator.StringToHash("hit");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!_isHit)
            {
                transform.Translate(_direction * _fireBallSpeed * Time.deltaTime);
                
                _timeAlive += Time.deltaTime;
                if (_timeAlive >= _lifeTime)
                {
                    Disable();
                }
            }
        }
        
        public void Initialize(Vector2 direction, Wand wand)
        {
            _direction = direction.normalized; // Нормализуем направление
            _wand = wand;
            _isHit = false;
            _timeAlive = 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                _isHit = true;
                _animator.SetTrigger(Hit);
            }
        }

        private void Disable()
        {
            // возвращаем направление по умолчанию, чтобы не было переповоротов спрайта при повторном использовании
            var scaler = transform.localScale;
            scaler.x = 1;
            transform.localScale = scaler;
            
            _wand.DisableFireBall(transform.gameObject);
        }
    }
}