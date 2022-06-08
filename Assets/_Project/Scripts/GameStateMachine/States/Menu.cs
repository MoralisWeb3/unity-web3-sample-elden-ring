using System.Collections.Generic;
using System.Linq;
using MoralisUnity;
using MoralisUnity.Kits.AuthenticationKit;
using MoralisUnity.Web3Api.Models;
using Pixelplacement;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using MoralisTools;

namespace Web3_Elden_Ring
{
    public class Menu : State
    {
        [Header("Components")]
        [SerializeField] private PlayerInputController playerInputController;
        [SerializeField] private AuthenticationKit authenticationKit;

        [Header("UI")] 
        [SerializeField] private Inventory inventory;
        [SerializeField] private TextMeshProUGUI runeAmountText;

        private string _walletAddress;
        private GameInput _gameInput;

        private async void Awake()
        {
            _walletAddress = await Web3Tools.GetWalletAddress();
        }

        private async void OnEnable()
        {
            playerInputController.EnableInput(false);
            
            _gameInput = new GameInput();
            _gameInput.Menu.Enable();
            _gameInput.Menu.CloseMenu.performed += OnCloseMenu;

            if (_walletAddress is null)
            {
                Debug.Log("We need the wallet address to continue");
                return;
            }
            
            List<Erc20TokenBalance> listOfTokens = await Moralis.Web3Api.Account.GetTokenBalances(_walletAddress, Moralis.CurrentChain.EnumValue);
            if (!listOfTokens.Any()) return;
            
            foreach (var token in listOfTokens)
            {
                // We make the sure that is the token that we deployed
                if (token.TokenAddress == GameManager.RuneContractAddress.ToLower())
                {
                    runeAmountText.text = token.Balance;
                    Debug.Log($"We have {token.Balance} runes (XP)");
                }
            }
            
            // We load all the items that we loot
            inventory.LoadItems(_walletAddress, GameManager.GameItemContractAddress, Moralis.CurrentChain.EnumValue);
        }

        private void OnDisable()
        {
            _gameInput.Menu.CloseMenu.performed -= OnCloseMenu;
            _gameInput.Menu.Disable();
        }

        private void OnCloseMenu(InputAction.CallbackContext obj)
        {
            LastActiveState();
        }

        public void OnDisconnectPressed()
        {
            authenticationKit.Disconnect();
        }

        public void ViewRuneContractOnPolygonScan()
        {
            Web3Tools.CheckContractOnPolygonScan(GameManager.RuneContractAddress);
        }
    }   
}
