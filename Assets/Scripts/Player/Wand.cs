using UnityEngine;

namespace Player
{
    public class Wand : MonoBehaviour
    {
        [SerializeField] private GameObject _fireBall;
        [SerializeField] private PlayerInputController _playerInputController;
        [SerializeField] private Transform _wandPosition;

        private void Start()
        {
            // _playerInputController.Attack += ThrowFireBall;
        }

        private void ThrowFireBall()
        {
            Instantiate(_fireBall, _wandPosition);
        }
        
        
    }
}
