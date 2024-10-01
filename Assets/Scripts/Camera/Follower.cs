using UnityEngine;

namespace Camera
{
    public class Follower : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private float _followSpeed;
        [SerializeField] private Vector3 _offset;

        private void FixedUpdate()
        {
            var nextPosition = Vector3.Lerp(transform.position, _targetTransform.position + _offset, _followSpeed * Time.fixedDeltaTime);
            
            transform.position = nextPosition;
        }
    }
}
