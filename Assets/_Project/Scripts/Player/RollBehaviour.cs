using UnityEngine;

namespace Web3_Elden_Ring
{
    [RequireComponent(typeof(Animator))]
    public class RollBehaviour : MonoBehaviour
    {
        [HideInInspector] public bool isRolling;
        
        private Animator _animator;
        private static readonly int RollParameterID = Animator.StringToHash("Roll");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        // New Input System Event
        public void OnRoll()
        {
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Movement")) return;

            _animator.applyRootMotion = true;
            _animator.SetTrigger(RollParameterID);
        }

        public void OnRollFinished()
        {
            _animator.applyRootMotion = false;
        }
    }   
}
