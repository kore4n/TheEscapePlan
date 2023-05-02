using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemsListScriptableObject", menuName = "ScriptableObjects/ItemsListScriptableObject")]
public class ItemListScriptableObject : ScriptableObject
{
    [SerializeField] private List<ItemScriptableObject> itemsListSO;

    public List<ItemScriptableObject> GetItemsListSO()
    {
        return itemsListSO;
    }

    // For testing only
    public ItemScriptableObject GetFirstItem()
    {
        return itemsListSO[0];
    }

    public ItemScriptableObject GetRandomItem()
    {
        int randomIndex = Random.Range(0, itemsListSO.Count);

        return itemsListSO[randomIndex];
    }

    public Sprite GetSprite(string itemName)
    {
        for (int i = 0; i < itemsListSO.Count; i++)
        {
            Debug.Log($"Checking if the item name {itemName} is equal to {itemsListSO[i].name}");
            if (itemsListSO[i].name == itemName)
            {
                return itemsListSO[i].itemIcon;
            }
        }

        return null;
    }
}
