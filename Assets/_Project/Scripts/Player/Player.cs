using UnityEngine;

namespace Web3_Elden_Ring
{
    public class Player : MonoBehaviour
    {
        [Header("Main Custom Components")]
        public PlayerInputController input;
        public PlayerMovement movement;
        public PlayerCombatSystem combat;
        public PlayerWalletAddress walletAddress;
        
        [Header("Unity Native Components")]
        public Animator animator;
        public CharacterController characterController;

        public void Death()
        {
            movement.Deactivate();
            combat.Deactivate();
            input.EnableInput(false);
            
            animator.SetTrigger("Death");
        }

        public void DanceThriller()
        {
            movement.ShowMyMoves();
        }
    }
}

