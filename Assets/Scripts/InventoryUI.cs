using Item;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private const int itemLimit = 10;
    [SerializeField] private ItemIconUI[] itemIcons = new ItemIconUI[itemLimit];
    

    void Start()
    {
        // When our inventory changes, update our UI
        Inventory.OnClientInventoryChanged += HandleClientInventoryChanged;
    }

    private void OnDestroy()
    {
        Inventory.OnClientInventoryChanged -= HandleClientInventoryChanged;
    }

    private void HandleClientInventoryChanged(object sender, EventArgs e)
    {
        //Debug.Log("This client's inventory has been updated!");

        Inventory clientInventory = (Inventory)sender;
        List<ItemStruct> itemsList = clientInventory.GetInventoryList();

        int curItems = Math.Min(itemLimit, itemsList.Count);
        for (int i = 0; i < curItems; i++)
        {
            ItemStruct curItemStruct = itemsList[i];
            //Debug.Log($"Updating the client's {curItemStruct.GetName()} icon.");

            itemIcons[i].UpdateItemIcon(itemsList[i]);
        }
    }
} 
