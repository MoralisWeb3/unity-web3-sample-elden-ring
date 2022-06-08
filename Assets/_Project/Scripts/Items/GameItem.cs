using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Web3_Elden_Ring
{
    public class GameItem : MonoBehaviour
    {
        public static Action<bool, GameItem> CollisionDetected;
        
        public SpriteRenderer spriteRenderer;

        public string metadataUrl;
        public MetadataObject metadataObject;
        
        
        public void Init(MetadataObject mdObject, string mdUrl)
        {
            metadataObject = mdObject;
            metadataUrl = mdUrl;
            
            // We get the texture from the image URL in the metadata
            StartCoroutine(GetTexture(metadataObject.image));
        }
        
        private IEnumerator GetTexture(string imageUrl)
        {
            using UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imageUrl);
        
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
                uwr.Dispose();
            }
            else
            {
                var tex = DownloadHandlerTexture.GetContent(uwr);
                
                // After getting the texture, we create a new sprite using the texture height (or width) to set the sprite's pixels for unit
                spriteRenderer.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), tex.height);
                
                uwr.Dispose();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            CollisionDetected?.Invoke(true, this);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            CollisionDetected?.Invoke(false, this);
        }
    }   
}
