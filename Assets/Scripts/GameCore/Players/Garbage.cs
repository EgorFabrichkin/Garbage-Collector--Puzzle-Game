#nullable enable

using System;
using GameCore.Players.Inputs;
using UnityEngine;
using Utils;
using View;

namespace GameCore.Players
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Garbage : MonoBehaviour
    {
        public event Action OnShoot = null!;

        [SerializeField] private GarbageInputBase input = null!;
        [SerializeField] private TrajectoryRenderer trajectoryRenderer = null!;
        [SerializeField] private float force = 150f;
        [SerializeField] private float maxDistanceLenght = 5f;
        [SerializeField] private LayerMask layerMask;
        
        private Rigidbody2D rigidbody2D = null!;
        private Vector2 startPosition, endPosition, forceImpulse;
        
        [Header("Debug")]
        [SerializeField] private bool isDragging;
        [SerializeField] private bool isTouch;
        [SerializeField] private bool isGrounded;

        public bool isPause = false;

        private void Awake()
        {
            input.EnsureNotNull("Input in Garbage not specified");
            trajectoryRenderer.EnsureNotNull("TrajectoryRenderer in Garbage not specified");
            
            rigidbody2D = GetComponent<Rigidbody2D>()!;
            
            if ((rigidbody2D.constraints & RigidbodyConstraints2D.FreezeRotation) == 0)
            {
                throw new Exception("Rb2D don't FreezeRotation");
            }
        }

        public Rigidbody2D Rigidbody2D => rigidbody2D;
        
        private void Update()
        {
            isGrounded = rigidbody2D.IsTouchingLayers(layerMask);
            isTouch = !isPause && input.IsTouch();
        }

        private void FixedUpdate()
        {
            if (isTouch && isGrounded)
            {
                isDragging = true;
                OnDragStart();
            }
            else if (!isTouch && isDragging)
            {
                isDragging = false;
                OnDragEnd();
            }
            else if (isDragging)
            {
                OnDrag();
            }
        }

        private void OnDragStart()
        {
            startPosition = input.StartTouchPosition();
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.angularVelocity = 0f;
            rigidbody2D.isKinematic = true;
            
            trajectoryRenderer.Draw();
        }

        private void OnDrag()
        {
            endPosition = input.EndTouchPosition();
            
            var distanceLenght = Mathf.Clamp(
                Vector2.Distance(startPosition, endPosition),
                0f,
                maxDistanceLenght);
            
            var direction = (startPosition - endPosition).normalized;

            forceImpulse = direction * (distanceLenght * force * Time.fixedDeltaTime);
            trajectoryRenderer.UpdateDots(transform.position, forceImpulse);
        }

        private void OnDragEnd()
        {
            rigidbody2D.isKinematic = false;
            rigidbody2D.AddForce(forceImpulse, ForceMode2D.Impulse);
            OnShoot.Invoke();
            
            trajectoryRenderer.Hide();
        }
    }
}