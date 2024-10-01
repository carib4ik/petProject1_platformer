using System;
using UnityEngine;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        public event Action MoveRight;
        public event Action MoveLeft;
        public event Action StopMove;
        public event Action Jump;
        
        private void Awake()
        {
        }

        private void Update()
        {
            Move();
            Jump1();
        }

        private void Move()
        {
            if (Input.GetKey(KeyCode.A))
            {
                MoveLeft?.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                StopMove?.Invoke();
            }
            if (Input.GetKey(KeyCode.D))
            {
                MoveRight?.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                StopMove?.Invoke();
            }
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                StopMove?.Invoke();
            }
        }
        
        private void Jump1()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump?.Invoke();
            }
        }
    }
}
