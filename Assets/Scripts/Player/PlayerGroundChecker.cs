using UnityEngine;

namespace Player
{
    public class PlayerGroundChecker : MonoBehaviour
    {
        public bool IsGrounded = true;
        
        [SerializeField] private LayerMask _groundLayer; // Слой земли
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                IsGrounded = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                IsGrounded = false;
            }
        }
    }
}
