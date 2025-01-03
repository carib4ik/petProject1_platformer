using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Wand : MonoBehaviour
    {
        [SerializeField] private GameObject _fireBall;
        [SerializeField] private Transform _wandPosition;

        private PlayerMovementController _playerMovementController;

        private readonly Queue<GameObject> _fireBalls = new();

        private void Awake()
        {
            _playerMovementController = GetComponent<PlayerMovementController>();
        }

        private void ThrowFireBall()
        {
            // Определяем направление фаербола в зависимости от направления персонажа
            var direction = _playerMovementController.IsFacingRight ? 1f : -1f;

            GameObject fireBall;
            
            if (_fireBalls.Count > 0)
            {
                fireBall = _fireBalls.Dequeue();
                fireBall.transform.SetPositionAndRotation(_wandPosition.position, Quaternion.identity);
                fireBall.SetActive(true);
            }
            else
            {
                fireBall = Instantiate(_fireBall, _wandPosition.position, Quaternion.identity);
            }
            
            // Разворачиваем фаербол, если он летит влево
            if (direction < 0)
            {
                var scaler = fireBall.transform.localScale;
                scaler.x *= -1;
                fireBall.transform.localScale = scaler;
            }
            
            fireBall.GetComponent<FireBall>().Initialize(new Vector2(direction, 0), this);
        }

        public void DisableFireBall(GameObject fireBall)
        {
            fireBall.SetActive(false);
            _fireBalls.Enqueue(fireBall);
        }
        
        
    }
}
