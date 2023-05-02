using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Netcode;
using UnityEngine;

namespace Item
{
    public class ItemSpawner : NetworkBehaviour
    {
        [SerializeField] private GameObject itemPrefab;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) && IsServer)
            {
                //SpawnItem(ItemManager.Instance.GetItemListSO().GetFirstItem());
                SpawnItem(ItemManager.Instance.GetItemListSO().GetRandomItem());
            }
        }

        private void SpawnItem(ItemScriptableObject itemSO)
        {
            GameObject newItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);

            Item item = newItem.GetComponent<Item>();

            item.Initialize(itemSO);

            newItem.GetComponent<NetworkObject>().Spawn(true);
        }
    }
}