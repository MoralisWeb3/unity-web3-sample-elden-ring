using UnityEngine;
using Random = UnityEngine.Random;

namespace Web3_Elden_Ring
{
    public class DropBehaviour : MonoBehaviour
    {
        private Vector3 _velocity = Vector3.up;
        private Collider _collider;
        private Rigidbody _rb;
        private Vector3 _startPos;

        private void Start()
        {
            _startPos = transform.position;
            _velocity *= Random.Range(4f, 6f); // Random upwards velocity
            _velocity += new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1, 1f)); // Random outward to scatter loot

            _collider = GetComponent<SphereCollider>();
            _collider.enabled = false;
            
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            _rb.isKinematic = true;
        }

        private void Update()
        {
            _rb.position += _velocity * Time.deltaTime; // Update position of loot game item

            // Limit downwards velocity
            if (_velocity.y < -4f)
            {
                _velocity.y = -4f;
            }
            else
            {
                _velocity -= Vector3.up * 5 * Time.deltaTime; // 5 is half the "usual" 10 for gravity
            }
            
            // If the object is close to where it started, let the built engine do its physics stuff
            if (Mathf.Abs(_rb.position.y - _startPos.y) < 0.6f && _velocity.y < 0f)
            {
                //_rb.useGravity = true;
                //_rb.isKinematic = false;
                //_rb.velocity = _velocity;
                _collider.enabled = true;
                enabled = false;
            }
        }
    }   
}
