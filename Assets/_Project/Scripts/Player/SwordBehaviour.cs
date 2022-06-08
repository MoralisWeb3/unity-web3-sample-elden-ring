using UnityEngine;

namespace Web3_Elden_Ring
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(BoxCollider))]
    public class SwordBehaviour : MonoBehaviour
    {
        [SerializeField] private Collider playerCollider;
        
        [SerializeField] private AudioClip firstAttackClip;
        [SerializeField] private AudioClip secondAttackClip;
        [SerializeField] private AudioClip thirdAttackClip;
        
        private AudioSource _audioSource;
        private BoxCollider _collider;

        private int _currentAttackId; // 0
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _collider = GetComponent<BoxCollider>();
            
            Physics.IgnoreCollision(_collider, playerCollider, true);

            Activate(false, _currentAttackId);
        }

        public void Activate(bool activate, int attackId)
        {
            _currentAttackId = attackId;
            _collider.enabled = activate;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Creature"))
            {
                switch (_currentAttackId)
                {
                    case 1:
                        _audioSource.clip = firstAttackClip;
                        break;
                    
                    case 2:
                        _audioSource.clip = secondAttackClip;
                        break;
                    
                    case 3:
                        _audioSource.clip = thirdAttackClip;
                        break;
                }
                
                _audioSource.Play();
            }
        }
    }   
}
