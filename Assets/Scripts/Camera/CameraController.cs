using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _smoothSpeed = 0.09f;
        [SerializeField] private float _offsetDirection;
        [SerializeField] private Transform _cameraTransform;
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // Желаемая позиция камеры с учетом смещения (движение только по оси X)
                var desiredPosition = new Vector3(_cameraTransform.position.x + _offsetDirection, 
                    _cameraTransform.position.y, _cameraTransform.position.z);

                // Плавный переход между текущей позицией камеры и желаемой
                var smoothedPosition = Vector3.Lerp(_cameraTransform.position, desiredPosition, _smoothSpeed);

                // Устанавливаем новую позицию камеры
                _cameraTransform.position = smoothedPosition;
            }
        }
    }
}
