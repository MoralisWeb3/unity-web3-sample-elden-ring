using MoralisUnity.Platform.Objects;
using Pixelplacement;
using UnityEngine;

namespace Web3_Elden_Ring
{
    #region DATA_CLASSES

    public class DatabaseItem : MoralisObject
    {
        public string metadata { get; set; }
        public DatabaseItem() : base("DatabaseItem") {}
    }
    
    public class MetadataObject
    {
        public string name;
        public string description;
        public string image;
    }

    #endregion

    public class GameManager : StateMachine
    {
        [Header("GameItem Contract Data")]
        public static string GameItemContractAddress = "";
        public static string GameItemContractAbi = "";
        
        [Header("Rune Contract Data")]
        public static string RuneContractAddress = "";
        public static string RuneContractAbi = "";
        
        [HideInInspector] public GameItem currentGameItem;
        [HideInInspector] public Rune currentRune;

        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip tranquilAmbience;
        public AudioClip fightingAmbience;
        [Range(0f, 1f)]
        public float tranquilAmbienceVolume;
        [Range(0f, 1f)]
        public float fightingAmbienceVolume;

        private void LateUpdate()
        {
            // REDO this using new input system
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }

        public void StartGame()
        {
            ChangeState("Exploring");
        }

        public void RestartGame()
        {
            ChangeState(defaultState); // Authenticating
        }

        public void OnStateEnteredHandler(GameObject stateObject)
        {
            if (stateObject.name == "Combating")
            {
                Tween.Volume(audioSource, 0, 0.25f, 0, null, Tween.LoopType.None, null, () =>
                {
                    // Now volume is 0
                    audioSource.Stop();
                    audioSource.clip = fightingAmbience;
                    audioSource.Play();

                    Tween.Volume(audioSource, fightingAmbienceVolume, 0.25f, 0);
                });
            }
        }
        
        public void OnStateExitedHandler(GameObject stateObject)
        {
            lastActiveState = currentState; // We save the last state that has been active
            
            if (stateObject.name == "Combating")
            {
                Tween.Volume(audioSource, 0, 1f, 0, null, Tween.LoopType.None, null, () =>
                {
                    // Now volume is 0
                    audioSource.Stop();
                    audioSource.clip = tranquilAmbience;
                    audioSource.Play();

                    Tween.Volume(audioSource, tranquilAmbienceVolume, 1f, 0);
                });
            }
        }
    }   
}
