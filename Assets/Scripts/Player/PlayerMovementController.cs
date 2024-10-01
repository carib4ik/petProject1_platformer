using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private PlayerInputController playerInputController;
        [SerializeField] private PlayerGroundChecker _playerGroundChecker;
        [SerializeField] private float _moveSpeed = 5f; // Скорость движения игрока
        [SerializeField] private float _flightSpeed = 3f;
        [SerializeField] private float _jumpForce = 6f; // Сила прыжка
        
        private Rigidbody2D _rigidbody;
        private Vector2 _movement;
        private bool _facingRight = true; // Флаг, показывающий в какую сторону повернут персонаж
        private bool _isGrounded;
        private float _currentSpeed;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            // Зафиксировать вращение
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            
            playerInputController.MoveRight += MoveRight;
            playerInputController.MoveLeft += MoveLeft;
            playerInputController.StopMove += StopMove;
            playerInputController.Jump += Jump;

            _playerGroundChecker.CheckGround += CheckGround;
        }
        
        private void Update()
        {
        }

        private void MoveRight()
        {
            TurnRight();
            
            if (_isGrounded)
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
            _rigidbody.velocity = new Vector2(_movement.x, _rigidbody.velocity.y);
        }
        
        private void MoveLeft()
        {
            TurnLeft();
            
            if (_isGrounded)
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
            _rigidbody.velocity = new Vector2(_movement.x, _rigidbody.velocity.y);
        }

        private void StopMove()
        {
            // Обновляем направление движения вправо
            _movement.x = 0;

            // Задаем движение по оси X, при этом оставляем ось Y без изменений
            _rigidbody.velocity = new Vector2(_movement.x, _rigidbody.velocity.y);
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
            if (_isGrounded)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            }
        }

        private void CheckGround(bool isGrounded)
        {
            _isGrounded = isGrounded;
        }
        
    }
}
