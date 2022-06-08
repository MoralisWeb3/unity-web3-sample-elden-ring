using UnityEngine;

namespace Web3_Elden_Ring
{
    public class RangeChecker : MonoBehaviour
    {
        private enum Type
        {
            Sight,
            Melee
        }
        [SerializeField] private Type myType;
        
        [SerializeField] private Creature creature;
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }
    
            switch (myType)
            {
                case Type.Sight:
                    creature.OnPlayerEnteredSightRange(true);
                    break;
                case Type.Melee:
                    creature.OnPlayerEnteredMeleeRange(true);
                    break;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }
            
            switch (myType)
            {
                case Type.Sight:
                    creature.OnPlayerEnteredSightRange(false);
                    break;
                case Type.Melee:
                    creature.OnPlayerEnteredMeleeRange(false);
                    break;
            }
        }
    }
}
