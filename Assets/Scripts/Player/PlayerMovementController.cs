using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private PlayerInputController _playerInputController;
        [SerializeField] private PlayerGroundChecker _playerGroundChecker;
        [SerializeField] private float _moveSpeed = 5f; // Скорость движения игрока
        [SerializeField] private float _flightSpeed = 3f;
        [SerializeField] private float _jumpForce = 6f; // Сила прыжка
        
        private Rigidbody2D _rigidbody;
        private Vector2 _movement;
        private bool _facingRight = true; // Флаг, показывающий в какую сторону повернут персонаж
        private float _currentSpeed;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            _playerInputController.MoveRight += MoveRight;
            _playerInputController.MoveLeft += MoveLeft;
            _playerInputController.StopMove += StopMove;
            _playerInputController.Jump += Jump;
            _playerInputController.Attack += Attack;
        }

        private void MoveRight()
        {
            TurnRight();
            
            if (_playerGroundChecker.IsGrounded)
            {
                // Обновляем направление движения вправо
                _movement.x = _moveSpeed;
                
                // возвращаяем скорость к нормальной, чтобы можно было прыгнуть с разбега
                _currentSpeed = _moveSpeed;
            }
            else
            {
                // уменьшаем скорость полета, если игрок нажимает кнопки перемещения в воздухе
                if (Input.GetKeyDown(KeyCode.D))
                {
                    _currentSpeed = _flightSpeed;
                }
                
                // Обновляем направление движения вправо в воздухе
                _movement.x = _currentSpeed;
            }
            
            // Задаем движение по оси X, при этом оставляем ось Y без изменений
            _rigidbody.linearVelocity = new Vector2(_movement.x, _rigidbody.linearVelocity.y);
        }
        
        private void MoveLeft()
        {
            TurnLeft();
            
            if (_playerGroundChecker.IsGrounded)
            {
                // Обновляем направление движения влево
                _movement.x = -_moveSpeed;
                
                // возвращаяем скорость к нормальной, чтобы можно было прыгнуть с разбега
                _currentSpeed = _moveSpeed;
            }
            else
            {
                // уменьшаем скорость полета, если игрок нажимает кнопки перемещения в воздухе
                if (Input.GetKeyDown(KeyCode.A))
                {
                    _currentSpeed = _flightSpeed;
                }
                
                // Обновляем направление движения влево в воздухе
                _movement.x = -_currentSpeed;
            }
            
            // Задаем движение по оси X, при этом оставляем ось Y без изменений
            _rigidbody.linearVelocity = new Vector2(_movement.x, _rigidbody.linearVelocity.y);
        }

        private void StopMove()
        {
            _movement.x = 0;

            // Задаем движение по оси X, при этом оставляем ось Y без изменений
            _rigidbody.linearVelocity = new Vector2(_movement.x, _rigidbody.linearVelocity.y);
        }
        
        private void TurnRight()
        {
            if (_facingRight) return;
            
            // Переворачиваем объект по оси X, что автоматически перевернет спрайт и коллайдер
            var scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
            
            _facingRight = true;
        }
        
        private void TurnLeft()
        {
            if (!_facingRight) return;
            
            // Переворачиваем объект по оси X, что автоматически перевернет спрайт и коллайдер
            var scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
            
            _facingRight = false;
        }
        
        private void Jump()
        {
            if (_playerGroundChecker.IsGrounded)
            {
                _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
            }
        }

        private void Attack()
        {
        }
    }
}
