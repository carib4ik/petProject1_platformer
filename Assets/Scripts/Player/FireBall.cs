using UnityEngine;

namespace Player
{
    public class FireBall : MonoBehaviour
    {
        [SerializeField] private float fireBallSpeed = 10f;

        private void Update()
        {
            transform.Translate(Vector2.right * fireBallSpeed * Time.deltaTime);
        }
    }
}