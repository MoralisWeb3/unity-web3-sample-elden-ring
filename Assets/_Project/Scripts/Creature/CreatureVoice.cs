using UnityEngine;

namespace Web3_Elden_Ring
{
    [RequireComponent(typeof(AudioSource))]
    public class CreatureVoice : MonoBehaviour
    {
        [Header("Sounds")]
        public AudioClip roar;
        public AudioClip punch;
        public AudioClip swipe;
        public AudioClip death;
        public AudioClip jumpSwish;
        public AudioClip jumpImpact;

        private AudioSource _as;

        private void Awake()
        {
            _as = GetComponent<AudioSource>();
        }

        public void Roar()
        {
            _as.clip = roar;
            _as.Play();
        }

        public void Punch()
        {
            _as.clip = punch;
            _as.Play();
        }
        
        public void Swipe()
        {
            _as.clip = swipe;
            _as.Play();
        }
        
        public void Death()
        {
            _as.clip = death;
            _as.Play();
        }

        public void JumpSwish() // Not really a "voice" sound but we put it here for easiness sake
        {
            _as.clip = jumpSwish;
            _as.Play();
        }
        
        public void JumpImpact() // Not really a "voice" sound but we put it here for easiness sake
        {
            _as.clip = jumpImpact;
            _as.Play();
        }
    }   
}
