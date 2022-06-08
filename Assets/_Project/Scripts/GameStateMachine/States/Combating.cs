using System;
using Pixelplacement;
using UnityEngine;
using UnityEngine.UI;

namespace Web3_Elden_Ring
{
    public class Combating : State
    {
        [Header("Player")]
        [SerializeField] private Player player;
        
        [Header("Creature")]
        [SerializeField] private Creature creature;

        [Header("UI")]
        [SerializeField] private CanvasGroup hud;
        [SerializeField] private Image playerCurrentHealth;
        [SerializeField] private Image creatureCurrentHeath;
        

        private void OnEnable()
        {
            player.combat.Damaged += OnPlayerDamaged;
            
            creature.PlayerLeftSightRange += GoToExploringState;
            creature.Damaged += OnCreatureDamaged;
            
            player.walletAddress.Disable();
        }

        private void OnDisable()
        {
            player.combat.Damaged -= OnPlayerDamaged;
            
            creature.PlayerLeftSightRange -= GoToExploringState;
            creature.Damaged -= OnCreatureDamaged;

            player.walletAddress.Enable();
        }
        
        private void OnPlayerDamaged(float damage)
        {
            playerCurrentHealth.fillAmount -= damage;

            if (playerCurrentHealth.fillAmount <= 0)
            {
                player.Death();
                hud.gameObject.SetActive(false);
                
                ChangeState("Defeat");
            }
        }
        
        private void OnCreatureDamaged(float damage)
        {
            creatureCurrentHeath.fillAmount -= damage;

            if (creatureCurrentHeath.fillAmount <= 0)
            {
                creature.Death();
                hud.gameObject.SetActive(false);
                
                ChangeState("Victory");
            }
        }
        
        private void GoToExploringState()
        {
            ChangeState("Exploring");
        }
    }   
}
