using UnityEngine;

namespace Web3_Elden_Ring
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class CollisionBlocker : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider characterCollider;

        private Rigidbody _rb;
        private CapsuleCollider _myCollider;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _myCollider = GetComponent<CapsuleCollider>();
        }

        private void Start()
        {
            // We don't want to collide with the our own player/creature colliders
            Physics.IgnoreCollision(characterCollider, _myCollider, true);
        }

        public void Disable()
        {
            _myCollider.enabled = false;
        }
    }   
}
