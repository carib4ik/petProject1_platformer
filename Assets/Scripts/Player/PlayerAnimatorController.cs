using UnityEngine;

namespace Player
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] private PlayerGroundChecker _playerGroundChecker;
        
        private PlayerInputController _playerInputController;
        private Animator _animator;
        private float _previousYPosition; // Предыдущее значение Y координаты
        private float _currentYPosition;  // Текущее значение Y координаты
        
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int IsFlyingUp = Animator.StringToHash("isFlyingUp");
        private static readonly int IsFlyingDown = Animator.StringToHash("isFlyingDown");
        private static readonly int Attack1 = Animator.StringToHash("attack");

        private void Awake()
        {
            _playerInputController = GetComponent<PlayerInputController>();
            _animator = GetComponent<Animator>();
            
            // Инициализируем предыдущую позицию текущей позицией персонажа
            _previousYPosition = transform.position.y;
        }

        private void Start()
        {
            _playerInputController.MoveRight += Run;
            _playerInputController.MoveLeft += Run;
            _playerInputController.StopMove += Stop;
            _playerInputController.Attack += Attack;
        }

        private void Attack()
        {
            _animator.SetTrigger(Attack1);
        }

        private void FixedUpdate()
        {
            HandleJumpAnimation();
        }
        
        private void HandleJumpAnimation()
        {
            var currentYPosition = transform.position.y;

            if (_playerGroundChecker.IsGrounded)
            {
                SetFlying(false, false);
            }
            else
            {
                var isFlyingUp = currentYPosition > _previousYPosition;
                var isFlyingDown = currentYPosition < _previousYPosition;

                SetFlying(isFlyingUp, isFlyingDown);
            }

            _previousYPosition = currentYPosition;
        }
        
        private void SetFlying(bool isFlyingUp, bool isFlyingDown)
        {
            if (_animator.GetBool(IsFlyingUp) != isFlyingUp)
            {
                _animator.SetBool(IsFlyingUp, isFlyingUp);
            }

            if (_animator.GetBool(IsFlyingDown) != isFlyingDown)
            {
                _animator.SetBool(IsFlyingDown, isFlyingDown);
            }
        }

        private void Run()
        {
            _animator.SetBool(IsRunning, true);
        }
        
        private void Stop()
        {
            _animator.SetBool(IsRunning, false);
        }

    }
}
