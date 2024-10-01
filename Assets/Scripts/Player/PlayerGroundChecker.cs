using System;
using UnityEngine;

namespace Player
{
    public class PlayerGroundChecker : MonoBehaviour
    {
        public event Action<bool> CheckGround;
        
        [SerializeField] private LayerMask _groundLayer; // Слой земли
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                CheckGround?.Invoke(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                CheckGround?.Invoke(false);
            }
        }
    }
}
