using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Web3_Elden_Ring
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Creature : MonoBehaviour
    {
        public Action<float> Damaged;
        public Action PlayerEnteredSightRange;
        public Action PlayerLeftSightRange;

        public enum StateEnum 
        {
            Idle,
            Roaring,
            Walking,
            Punching,
            Swiping,
            Jumping,
            Dead
        }

        public StateEnum currentState;

        [Header("Components")]
        [SerializeField] private CreatureVoice voice;
        
        [Header("Range Checker")]
        [SerializeField] private GameObject rangeChecker;
        
        [Header("Collision Blocker")]
        [SerializeField] private CollisionBlocker blocker;
        
        [Header("Hands Colliders")]
        [SerializeField] private Collider leftSwipeCol;
        [SerializeField] private Collider rightPunchCol;

        [Header("Particles")] [SerializeField] private ParticleSystem impactWave;
        
        // Components
        private Animator _animator;
        private Transform _playerT;
        private CapsuleCollider _mainCollider;
        private Rigidbody _rigidbody;

        // Control vars
        [HideInInspector] public bool isAttacking;
        private bool _playerInSightRange;
        private const float DamageValue = 0.1f;

        // Animator parameters
        private int _paramWalkID;
        private int _paramAttackID;
        private int _paramDeathID;
        private int _paramRoarID;
        private int _paramJumpAttackID;


        #region UNITY_LIFECYCLE

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mainCollider = GetComponent<CapsuleCollider>();
            _playerT = GameObject.FindGameObjectWithTag("Player").transform;
            _rigidbody = GetComponent<Rigidbody>();
            
            AssignAnimatorParametersIds();

            leftSwipeCol.enabled = false;
            rightPunchCol.enabled = false;
        }

        private void FixedUpdate()
        {
            if (!_playerInSightRange) return;
            
            transform.LookAt(_playerT.transform);
        }

        #endregion


        #region PUBLIC_METHODS

        public void OnPlayerEnteredSightRange(bool inRange)
        {
            _playerInSightRange = inRange;

            if (_playerInSightRange)
            {
                PlayerEnteredSightRange?.Invoke();

                if (currentState == StateEnum.Roaring) return; // We don't want to roar again if we're already roaring
                
                _animator.SetTrigger(_paramRoarID);
            }
            else
            {
                PlayerLeftSightRange?.Invoke();
            }
            
            _animator.SetBool(_paramWalkID, _playerInSightRange);
        }
        
        public void OnPlayerEnteredMeleeRange(bool inRange)
        {
            switch (inRange)
            { 
                case true:
                    var value = Random.Range(1, 3);
                    _animator.SetInteger(_paramAttackID, value); 
                    break;
                
                case false:
                    _animator.SetInteger(_paramAttackID, 0);
                    
                    leftSwipeCol.enabled = false;
                    rightPunchCol.enabled = false;
                    break;
            }
        }
        
        // This is called from the Combating State
        public void Death()
        {
            rangeChecker.SetActive(false);

            blocker.Disable();
            
            leftSwipeCol.enabled = false;
            rightPunchCol.enabled = false;

            _animator.SetBool(_paramWalkID, false);
            _animator.SetInteger(_paramAttackID, 0);
            
            _animator.SetTrigger(_paramDeathID);
            
            enabled = false;
        }

        #endregion

        
        #region COLLISION_EVENTS

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Sword"))
            {
                // First we let the game know we have received some damage
                Damaged?.Invoke(DamageValue);
                
                // If creature is dead or jump-attacking, we do nothing :)
                if (currentState is StateEnum.Dead or StateEnum.Jumping) return;
                
                // There's a 25% chance that the creature gets angry and jump-attacks!
                bool isAngry = (Random.value > 0.75f);

                if (isAngry)
                {
                    _animator.SetTrigger(_paramJumpAttackID);
                }
            }
        }

        #endregion
        
        
        #region ANIMATION_EVENTS

        public void OnAnimStart(int animId)
        {
            switch (animId)
            {
                case 0: // Idle
                    currentState = StateEnum.Idle;
                    break;
                
                case 1: // Roaring
                    currentState = StateEnum.Roaring;
                    break;
                
                case 2: // Walking
                    currentState = StateEnum.Walking;
                    break;
                
                case 3: // Punching
                    currentState = StateEnum.Punching;
                    isAttacking = true;
                    break;
                
                case 4: // Swiping
                    currentState = StateEnum.Swiping;
                    isAttacking = true;
                    break;
                
                case 5: // Jumping
                    currentState = StateEnum.Jumping;
                    
                    // We don't want the PhysicsEngine work on us while jumping
                    EnablePhysics(false);
                    voice.JumpSwish();
                    isAttacking = true;
                    break;
                
                case 6: // Dead
                    currentState = StateEnum.Dead;
                    
                    EnablePhysics(false);
                    voice.Death();
                    break;
            }
        }
        
        public void OnAnimEnd(int animId)
        {
            switch (animId)
            {
                case 0: // Idle
                    
                    break;
                
                case 1: // Roaring
                    
                    break;
                
                case 2: // Walking

                    break;
                
                case 3: // Punching
                    isAttacking = false;
                    break;
                
                case 4: // Swiping
                    isAttacking = false;
                    break;
                
                case 5: // Jumping
                    isAttacking = false;
                    break;
                
                case 6: // Dead
                    break;
            }
        }
        
        public void OnAttackStart()
        {
            switch (currentState)
            {
                case StateEnum.Punching:

                    voice.Punch();
                    rightPunchCol.enabled = true;
                    break;
                
                case StateEnum.Swiping:
                    
                    leftSwipeCol.enabled = true;
                    break;
                
                case StateEnum.Jumping:
                    
                    // Both hands active when jump attacking
                    rightPunchCol.enabled = true;
                    leftSwipeCol.enabled = true;
                    break;
            }
        }

        public void OnAttackEnd()
        {
            switch (currentState)
            {
                case StateEnum.Punching:

                    rightPunchCol.enabled = false;
                    break;
                
                case StateEnum.Swiping:

                    leftSwipeCol.enabled = false;
                    break;
                
                case StateEnum.Jumping: // We hit the ground here
                    
                    // We enable physics components back again just after hitting floor :)
                    EnablePhysics(true);

                    voice.JumpImpact();
                    impactWave.Play();
                    
                    rightPunchCol.enabled = false;
                    leftSwipeCol.enabled = false;
                    break;
            }
        }

        public void OnRoarStart()
        {
            if (currentState != StateEnum.Roaring) return;
            
            voice.Roar();
        }

        public void OnSwipeSoundStart()
        {
            if (currentState != StateEnum.Swiping) return;
            
            voice.Swipe();
        }
        
        #endregion
        
        
        #region PRIVATE_METHODS

        private void EnablePhysics(bool status)
        {
            _rigidbody.useGravity = status; // Important to go first
            _mainCollider.enabled = status;
        }

        private void AssignAnimatorParametersIds()
        {
            _paramWalkID = Animator.StringToHash("Walk");
            _paramAttackID = Animator.StringToHash("Attack");
            _paramDeathID = Animator.StringToHash("Death");
            _paramRoarID = Animator.StringToHash("Roar");
            _paramJumpAttackID = Animator.StringToHash("JumpAttack");
        }

        #endregion
    }   
}
