using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Web3_Elden_Ring
{
    public class ItemPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemDescription;
        [SerializeField] private Image itemImage;

        public void Fill(string itmName, string itmDesc, Sprite itmSprite)
        {
            itemName.text = itmName;
            itemDescription.text = itmDesc;
            itemImage.sprite = itmSprite;
        }
    }   
}
