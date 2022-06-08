using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoralisUnity;
using Pixelplacement;
using UnityEngine;
using MoralisUnity.Platform.Queries;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

namespace Web3_Elden_Ring
{
    public class Victory: State
    {
        [SerializeField] private Player player;
        [SerializeField] private Creature creature;
        
        [Header("Loot")]
        [SerializeField] private GameItem gameItemPrefab;
        [SerializeField] private Rune runePrefab;

        [Header("UI")]
        [SerializeField] private ItemPanel itemPanel;
        [SerializeField] private ItemPanel runePanel;

        private Vector3 _populatePosition;
        private MoralisQuery<DatabaseItem> _gameItemsQuery;

        private GameManager _gameManager;
        private GameInput _gameInput;
        private AudioSource _audioSource;

        #region UNITY_LIFECYCLE

        private async void Start()
        {
            _gameManager = GetComponentInParent<GameManager>(); //Assuming this is under GameManager
            _audioSource = GetComponent<AudioSource>();
            
            _populatePosition = creature.transform.position;
            
            _gameItemsQuery = await Moralis.GetClient().Query<DatabaseItem>();
            
            PopulateItemsFromDB();
            PopulateRunes();
        }

        private void OnEnable()
        {
            player.input.EnableInput(true);
            
            _gameInput = new GameInput();
            _gameInput.Victory.Enable();
            _gameInput.Victory.OpenMenu.performed += GoToMenuState;
            _gameInput.Victory.PickUp.performed += OnPickUp;
            _gameInput.Victory.Thriller.performed += PlayerDanceThriller;
            
            GameItem.CollisionDetected += ShowItemPanel;
            Rune.CollisionDetected += ShowRunePanel;
        }

        private void OnDisable()
        {
            _gameInput.Victory.OpenMenu.performed -= GoToMenuState;
            _gameInput.Victory.PickUp.performed -= OnPickUp;
            _gameInput.Victory.Thriller.performed -= PlayerDanceThriller;
            _gameInput.Victory.Disable();
            
            GameItem.CollisionDetected -= ShowItemPanel;
            Rune.CollisionDetected -= ShowRunePanel;
            
            // We deactivate the panels every time we go back to VictoryState
            itemPanel.gameObject.SetActive(false);
            runePanel.gameObject.SetActive(false);
        }

        #endregion


        #region INPUT_SYSTEM_HANDLERS

        private void GoToMenuState(InputAction.CallbackContext obj)
        {
            if (itemPanel.isActiveAndEnabled) return;
            
            ChangeState("Menu");
        }
        
        private void OnPickUp(InputAction.CallbackContext obj)
        {
            if (itemPanel.isActiveAndEnabled)
            {
                ChangeState("PickingUpItem");
                return;
            }

            if (runePanel.isActiveAndEnabled)
            {
                ChangeState("PickingUpRune");
            }
        }
        
        private void PlayerDanceThriller(InputAction.CallbackContext obj)
        {
            player.DanceThriller();

            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
            else
            {
                _audioSource.Play();
            }
        }

        #endregion
        

        #region EVENT_HANDLERS

        private void ShowItemPanel(bool collided, GameItem collidedItem)
        {
            if (collided)
            {
                itemPanel.Fill(collidedItem.metadataObject.name, collidedItem.metadataObject.description, collidedItem.spriteRenderer.sprite);
                itemPanel.gameObject.SetActive(true);

                _gameManager.currentGameItem = collidedItem;
            }
            else
            {
                itemPanel.gameObject.SetActive(false);
            }
        }
        
        private void ShowRunePanel(bool collided, Rune collidedRune)
        {
            if (collided)
            {
                runePanel.Fill(collidedRune.title, "x" + collidedRune.amount, collidedRune.spriteRenderer.sprite);
                runePanel.gameObject.SetActive(true);

                _gameManager.currentRune = collidedRune;
            }
            else
            {
                runePanel.gameObject.SetActive(false);
            }
        }

        #endregion


        #region PRIVATE_METHODS

        private async void PopulateItemsFromDB()
        {
            IEnumerable<DatabaseItem> databaseItems = await _gameItemsQuery.FindAsync();

            var databaseItemsList = databaseItems.ToList();
            if (!databaseItemsList.Any()) return;

            foreach (var databaseItem in databaseItemsList)
            {
                // databaseItem.metadata points to a JSON URL. We need to get the result of that URL first
                StartCoroutine(GetMetadataObject(databaseItem.metadata));
            }
        }

        private IEnumerator GetMetadataObject(string metadataUrl)
        {
            // We create a GET UWR passing that JSON URL 
            using UnityWebRequest uwr = UnityWebRequest.Get(metadataUrl);
        
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
                uwr.Dispose();
            }
            else
            {
                // If successful, we get the JSON content as a string
                var uwrContent = DownloadHandlerBuffer.GetContent(uwr);

                // Finally we need to convert that string to a MetadataObject
                MetadataObject metadataObject = JsonUtility.FromJson<MetadataObject>(uwrContent);
                
                // And voilà! We populate a new GameItem passing the metadataObject
                PopulateGameItem(metadataObject, metadataUrl);
                
                uwr.Dispose();
            }
        }
        
        private void PopulateGameItem(MetadataObject metadataObject, string metadataUrl)
        {
            GameItem newItem = Instantiate(gameItemPrefab, _populatePosition, Quaternion.identity);
        
            newItem.Init(metadataObject, metadataUrl);
        }

        private void PopulateRunes()
        {
            Instantiate(runePrefab, _populatePosition, Quaternion.identity);
        }

        #endregion
    }   
}
