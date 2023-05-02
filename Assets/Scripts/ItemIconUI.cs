using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Item;

public class ItemIconUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private TextMeshProUGUI itemName;

    private void Start()
    {
        itemImage.enabled = false;
    }

    public void UpdateItemIcon(ItemStruct itemStruct)
    {
        itemImage.enabled = true;
        itemImage.sprite = itemStruct.GetSprite();
        itemCount.text = itemStruct.count.ToString();
        itemName.text = itemStruct.GetName();
    }
}
