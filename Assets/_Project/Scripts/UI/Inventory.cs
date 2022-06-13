using System;
using System.Collections.Generic;
using System.Linq;
using MoralisUnity;
using MoralisUnity.Web3Api.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Web3_Elden_Ring
{
    public class Inventory : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private InventoryItem itemPrefab;
        [SerializeField] private GridLayoutGroup itemsGrid;

        private int _currentItemsCount;
        
        public async void LoadItems(string playerAddress, string contractAddress, ChainList contractChain)
        {
            try
            {
                NftOwnerCollection noc =
                    await Moralis.GetClient().Web3Api.Account.GetNFTsForContract(playerAddress.ToLower(),
                        contractAddress, contractChain);
            
                List<NftOwner> nftOwners = noc.Result;

                // We only proceed if we find some
                if (!nftOwners.Any())
                {
                    Debug.Log("You don't own items");
                    return;
                }

                if (nftOwners.Count == _currentItemsCount)
                {
                    Debug.Log("There are no new items to load");
                    return;
                }
                
                ClearAllItems(); // We clear the grid before adding new items
                
                foreach (var nftOwner in nftOwners)
                {
                    if (nftOwner.Metadata == null)
                    {
                        // Sometimes GetNFTsForContract fails to get NFT Metadata. We need to re-sync
                        Moralis.GetClient().Web3Api.Token.ReSyncMetadata(nftOwner.TokenAddress, nftOwner.TokenId, contractChain);
                        Debug.Log("We couldn't get NFT Metadata. Re-syncing...");
                        continue;
                    }
                    
                    var metadata = nftOwner.Metadata;
                    MetadataObject metadataObject = JsonUtility.FromJson<MetadataObject>(metadata);

                    PopulatePlayerItem(nftOwner.TokenId, metadataObject);
                }
            }
            catch (Exception exp)
            {
                Debug.LogError(exp.Message);
            }
        }
    
        private void PopulatePlayerItem(string tokenId, MetadataObject metadataObject)
        {
            InventoryItem newItem = Instantiate(itemPrefab, itemsGrid.transform);
            
            newItem.Init(tokenId, metadataObject);

            _currentItemsCount++;
        }

        private void ClearAllItems()
        {
            foreach (Transform item in itemsGrid.transform)
            {
                Destroy(item.gameObject);
            }

            _currentItemsCount = 0;
        }
    }   
}
