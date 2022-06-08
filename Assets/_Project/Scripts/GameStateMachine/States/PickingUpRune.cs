using System.Numerics;
using Cysharp.Threading.Tasks;
using MoralisUnity;
using Nethereum.Hex.HexTypes;
using Pixelplacement;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Web3_Elden_Ring
{
    // Class which takes care to transfer some of the already minted supply of an ERC-20 token to the player address
    public class PickingUpRune : State
    {
        [Header("Components")]
        [SerializeField] private Player player;
        private GameManager _gameManager;
        private GameInput _gameInput;
        
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI statusText;


        #region UNITY_LIFECYCLE

        private void OnEnable()
        {
            _gameManager = GetComponentInParent<GameManager>(); // We assume we are under GameManager

            _gameInput = new GameInput();
            _gameInput.PickingUp.Enable();
            _gameInput.PickingUp.Cancel.performed += CancelTransaction;
            
            player.input.EnableInput(false);
            
            PickUpRune(_gameManager.currentRune.amount);
        }

        private void OnDisable()
        {
            _gameInput.PickingUp.Disable();
            _gameInput.PickingUp.Cancel.performed -= CancelTransaction;
        }

        #endregion
        

        #region PRIVATE_METHODS

        private async void PickUpRune(int amount)
        {
            statusText.text = "Please confirm transaction in your wallet";
        
            var result = await GetExperience(amount);

            if (result is null)
            {
                statusText.text = "Transaction failed";
                LastActiveState();
                return;
            }
        
            statusText.text = "Transaction completed!";
            Destroy(_gameManager.currentRune.gameObject); // We don't need this item anymore
            LastActiveState();
        }
        
        private async UniTask<string> GetExperience(int amount)
        {
            BigInteger amountValue = new BigInteger(amount);
            
            object[] parameters = {
                amountValue.ToString("x")
            };

            // Set gas estimate
            HexBigInteger value = new HexBigInteger(0);
            HexBigInteger gas = new HexBigInteger(0);
            HexBigInteger gasPrice = new HexBigInteger(0);

            string resp = await Moralis.ExecuteContractFunction(GameManager.RuneContractAddress, GameManager.RuneContractAbi, "getExperience", parameters, value, gas, gasPrice);

            return resp;
        }

        #endregion
        
        
        #region INPUT_SYSTEM_HANDLERS

        private async void CancelTransaction(InputAction.CallbackContext obj)
        {
            // Check out what we're doing to "cancel" the transaction:
            // https://ethereum.stackexchange.com/questions/31298/is-it-possible-to-cancel-a-transaction
            
            BigInteger amountValue = new BigInteger(0);
            
            object[] parameters = {
                amountValue.ToString("x")
            };
            
            HexBigInteger value = new HexBigInteger(0);
            HexBigInteger gas = new HexBigInteger(0);
            HexBigInteger gasPrice = new HexBigInteger(1); //Higher than "GetItem" transaction
            
            string resp = await Moralis.ExecuteContractFunction(GameManager.RuneContractAddress, GameManager.RuneContractAbi, "getExperience", parameters, value, gas, gasPrice);
            
            LastActiveState();
        }

        #endregion
    }   
}