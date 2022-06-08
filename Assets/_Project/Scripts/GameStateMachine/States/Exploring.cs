using Pixelplacement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Web3_Elden_Ring
{
    public class Exploring : State
    {
        [SerializeField] private PlayerInputController playerInputController;
        [SerializeField] private Creature creature;

        private GameInput _gameInput;

        private void OnEnable()
        {
            playerInputController.EnableInput(true);
            
            _gameInput = new GameInput();
            _gameInput.Exploring.Enable();
            _gameInput.Exploring.OpenMenu.performed += OnOpenMenu;

            creature.PlayerEnteredSightRange += GoToCombatingState;
        }

        private void OnDisable()
        {
            _gameInput.Exploring.OpenMenu.performed -= OnOpenMenu;
            _gameInput.Exploring.Disable();

            creature.PlayerEnteredSightRange -= GoToCombatingState;
        }
        
        private void OnOpenMenu(InputAction.CallbackContext obj)
        {
            ChangeState("Menu");
        }

        private void GoToCombatingState()
        {
            ChangeState("Combating");
        }
    }   
}
