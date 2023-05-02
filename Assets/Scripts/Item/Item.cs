using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    [System.Serializable, System.Flags]
    public enum Category
    {
        Material = 1 << 0,
        Utility = 1 << 1,
        Weapon = 1 << 2,
    }

    public class Item : NetworkBehaviour, IInteractable
    {
        public int count;
        public Category category;

        public ItemStruct ToStruct()
        {
            return new ItemStruct()
            {
                // trim clone name here
                name = name,
                count = count,
                category = category
            };
        }

        public void Initialize(ItemScriptableObject itemSO)
        {
            name = itemSO.name;
            category = itemSO.category;
            count = 1;

            //Debug.Log($"Should set item name to {itemSO.name}");
            //Debug.Log($"Actually setting item name to {name}");
        }

        public void Interact(PlayerPermissions playerPermissions)
        {
            // Always update permissions before picking up items
            playerPermissions.UpdatePlayerPermissions();

            if (!playerPermissions.GetCanPickupItem()) { return; }

            playerPermissions.GetInventory().AddItemToInventory(ToStruct());

            GetComponent<NetworkObject>().Despawn();
        }
    }

    /// <summary>
    /// Needed to keep track of items over the network.
    /// Networked items must be struct.
    /// </summary>
    public struct ItemStruct : INetworkSerializable, System.IEquatable<ItemStruct>
    {
        public FixedString128Bytes name;
        public int count;
        public Category category;

        public bool Equals(ItemStruct other)
        {
            return name == other.name;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref name);
            serializer.SerializeValue(ref count);
            serializer.SerializeValue(ref category);
        }

        public void PrintItem()
        {
            Debug.Log($"Name: {name}");
            Debug.Log($"Count: {count}");
            Debug.Log($"Category: {category}");
        }

        public string GetName()
        {
            return name.ToString();
        }

        public Sprite GetSprite()
        {
            return ItemManager.Instance.GetItemListSO().GetSprite(name.ToString());
        }
    }
}