using Pixelplacement;
using UnityEngine;

namespace Web3_Elden_Ring
{
    public class Authenticating : State
    {
        [SerializeField] private PlayerInputController playerInputController;

        private void OnEnable()
        {
            playerInputController.EnableInput(false);
        }
    }   
}
