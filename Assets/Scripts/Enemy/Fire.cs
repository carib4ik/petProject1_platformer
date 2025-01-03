using Player;
using UnityEngine;

namespace Enemy
{
    public class Fire : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // other.gameObject.GetComponent<PlayerStatsController>().TakeDamage1(_damage);
                PlayerStatsController.PlayerStats.TakeDamage(_damage);
            }
        }
    }
}