using TMPro;
using UnityEngine;

namespace Web3_Elden_Ring
{
    public class PlayerWalletAddress : MonoBehaviour
    {
        [SerializeField] private TextMeshPro textMeshPro;

        private void LateUpdate()
        {
            if (Camera.main == null) return;
            
            textMeshPro.transform.rotation =
                Quaternion.LookRotation(textMeshPro.transform.position - Camera.main.transform.position);
        }

        public void Enable()
        {
            GetWalletAddress();
            textMeshPro.gameObject.SetActive(true);
        }

        public void Disable()
        {
            textMeshPro.text = string.Empty;
            textMeshPro.gameObject.SetActive(false);
        }

        private async void GetWalletAddress()
        {
            textMeshPro.text = await MoralisTools.Web3Tools.GetWalletAddress();
        }
    }
}
