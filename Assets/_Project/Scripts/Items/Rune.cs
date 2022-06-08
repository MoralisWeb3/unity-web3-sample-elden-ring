using System;
using UnityEngine;

namespace Web3_Elden_Ring
{
    public class Rune : MonoBehaviour
    {
        public static Action<bool, Rune> CollisionDetected;
        
        public string title;
        public int amount;
        public SpriteRenderer spriteRenderer;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            CollisionDetected?.Invoke(true, this);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            CollisionDetected?.Invoke(false, this);
        }
    }   
}
