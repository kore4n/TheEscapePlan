using Item;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace Item
{
    public class Inventory : NetworkBehaviour
    {
        // TODO: Finish key items
        private static NetworkList<ItemStruct> keyItems;
        private NetworkList<ItemStruct> inventoryItems;

        public static event EventHandler OnClientInventoryChanged;

        private void Awake()
        {
            keyItems = new NetworkList<ItemStruct>();
            inventoryItems = new NetworkList<ItemStruct>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            inventoryItems.OnListChanged += OnListChanged;
        }

        private void OnListChanged(NetworkListEvent<ItemStruct> changeEvent)
        {
            if (IsOwner) OnClientInventoryChanged?.Invoke(this, EventArgs.Empty);

            // For debugging
            Debug.Log($"The list changed and now has {inventoryItems.Count} elements.");
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                Debug.Log($"Item {i + 1} in the list out of {inventoryItems.Count}.");
                inventoryItems[i].PrintItem();
            }
        }

        public List<ItemStruct> GetInventoryList()
        {
            //Debug.Log("Getting inventory from client handler!");

            List<ItemStruct> regularList = new List<ItemStruct>();
            foreach (ItemStruct item in inventoryItems)
            {
                // Add the element to the regular list
                regularList.Add(item);
            }
            return regularList;
        }

        public void AddItemToInventory(ItemStruct item)
        {
            //Debug.Log("Adding item to inventory!");

            // Inventory already contains same type of item
            if (InventoryContains(item, out int index))
            {
                int newCount = inventoryItems[index].count + item.count;

                // Can't modify network list directly, have to make a new struct on the stack
                ItemStruct newItem = inventoryItems[index];
                newItem.count = newCount;
                inventoryItems[index] = newItem;

                Debug.Log($"New count {inventoryItems[index].count}");
            }
            // Add new item to inventory
            else
            {
                inventoryItems.Add(item);
            }

            //Debug.Log($"Sprite is {item.GetSprite()}");
        }

        public bool InventoryContains(ItemStruct item, out int index)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].name == item.name)
                {
                    index = i;
                    return true;
                }
            }

            index = -1;
            return false;
        }

        public bool InventoryContains(ItemStruct item)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].name == item.name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
