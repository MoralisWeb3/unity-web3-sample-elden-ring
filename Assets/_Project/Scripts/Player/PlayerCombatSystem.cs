using System;
using UnityEngine;

namespace Web3_Elden_Ring
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(Animator))]
    
    // TODO We could implement a similar state machine as we do for the Creature, but we're reaching the same result watching at the animator states
    public class PlayerCombatSystem : MonoBehaviour
    {
        public Action<float> Damaged;

        [Header("Components")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SwordBehaviour swordBehaviour;
        
        // Components
        private Animator _animator;
        private PlayerMovement _playerMovement;
        
        // Animator parameters
        private int _paramLightAttack1;
        private int _paramLightAttack2;
        private int _paramLightAttack3;
        private int _paramImpact;
        
        // Control vars
        [HideInInspector] public bool isAttacking;
        [HideInInspector] public bool isImpacted;
        
        private const float DefaultDamageTaken = 0.1f;
        private const float StrongDamageTaken = 0.3f;

        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
            
            AssignAnimatorParametersIDs();
        }


        #region PUBLIC_METHODS

        public void Deactivate()
        {
            ResetAttackParameters();
            enabled = false;
        }

        #endregion


        #region INPUT_SYSTEM_EVENTS

        public void OnLightAttack()
        {
            if (!_playerMovement.grounded) return;
            if (_animator.IsInTransition(0)) return;
            
            
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
            {
                ResetAttackParameters();

                _animator.SetBool(_paramLightAttack1, true);
                
                _animator.applyRootMotion = true; // We activate it when we attack.
                isAttacking = true;
                return;
            }
            
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f) return; // We only accept the input if the animation has completed at least by half
                
                _animator.SetBool(_paramLightAttack2, true);
                
                _animator.applyRootMotion = true; // We activate it when we attack.
                isAttacking = true;
                return;
            }
            
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f) return; // We only accept the input if the animation has completed at least by half
                
                _animator.SetBool(_paramLightAttack3, true);
                
                _animator.applyRootMotion = true; // We activate it when we attack.
                isAttacking = true;
            }
        }

        #endregion
        

        #region ANIMATION_EVENTS

        public void OnSwordStart(int attackId)
        {
            swordBehaviour.Activate(true, attackId);
        }
        
        public void OnSwordEnd(int attackId)
        {
            swordBehaviour.Activate(false, 0); // We don't need to differentiate between attacks for this
        }

        public void OnAttackFinished(int attackId)
        {
            switch (attackId)
            {
                case 1:
                    if (_animator.GetBool(_paramLightAttack2)) return; //If we have clicked another time and next bool is active, we leave everything as it is. Same for other cases.
                    
                    _animator.SetBool(_paramLightAttack1, false);
                    
                    _animator.applyRootMotion = false;
                    isAttacking = false;
                    break;
                
                case 2:
                    if (_animator.GetBool(_paramLightAttack3)) return;
                    
                    _animator.SetBool(_paramLightAttack1, false);
                    _animator.SetBool(_paramLightAttack2, false);
                    
                    _animator.applyRootMotion = false;
                    isAttacking = false;
                    break;
                case 3:
                    ResetAttackParameters();
                    
                    _animator.applyRootMotion = false;
                    isAttacking = false;
                    break;
            }
        }

        public void OnImpactStart()
        {
            isImpacted = true;
            audioSource.Play(); //Impact sound
        }

        public void OnImpactEnd()
        {
            isImpacted = false;
        }

        #endregion
        

        #region COLLISION EVENTS

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("RightHand"))
            {
                _animator.SetTrigger(_paramImpact);

                ResetAttackParameters();
                
                _animator.applyRootMotion = false;
                isAttacking = false;
                
                Damaged?.Invoke(DefaultDamageTaken);
            }
            
            if (other.CompareTag("LeftHand"))
            {
                _animator.SetTrigger(_paramImpact);

                ResetAttackParameters();
                
                _animator.applyRootMotion = false;
                isAttacking = false;
                
                Damaged?.Invoke(StrongDamageTaken); // Left hand is more powerful than right hand
            }
        }

        #endregion
        
        
        #region PRIVATE_METHODS

        private void AssignAnimatorParametersIDs()
        {
            _paramLightAttack1 = Animator.StringToHash("Attack1");
            _paramLightAttack2 = Animator.StringToHash("Attack2");
            _paramLightAttack3 = Animator.StringToHash("Attack3");
            
            _paramImpact = Animator.StringToHash("Impact");
        }

        private void ResetAttackParameters()
        {
            _animator.SetBool(_paramLightAttack1, false);
            _animator.SetBool(_paramLightAttack2, false);
            _animator.SetBool(_paramLightAttack3, false);
        }

        #endregion
    }
}
