#nullable enable

using GameCore.Players;
using UnityEngine;

namespace GameCore.Obstacles
{
    public class Blower : MonoBehaviour
    {
        [SerializeField] private Collider2D collider2D = null!;
        [SerializeField] private float impulseForce = 5f;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Garbage garbage))
            {
                garbage.Rigidbody2D.AddForce(Vector2.right * impulseForce, ForceMode2D.Impulse);
            }
        }
    }
}