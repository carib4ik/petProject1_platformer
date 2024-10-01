using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] private PlayerInputController playerInputController;
        [SerializeField] private PlayerGroundChecker _playerGroundChecker;

        private Animator _animator;
        
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int IsFlying = Animator.StringToHash("isFlying");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            playerInputController.MoveRight += Walk;
            playerInputController.MoveLeft += Walk;
            playerInputController.StopMove += Stop;
            
            _playerGroundChecker.CheckGround += Jump;
        }

        private void Walk()
        {
            _animator.SetBool(IsRunning, true);
        }
        
        private void Stop()
        {
            _animator.SetBool(IsRunning, false);
        }

        private void Jump(bool isGrounded)
        {
            if (isGrounded)
            {
                _animator.SetBool(IsFlying, false);
            }

            if (!isGrounded)
            {
                _animator.SetBool(IsFlying, true);
            }
            
        }
    }
}
