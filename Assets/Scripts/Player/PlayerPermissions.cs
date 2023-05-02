using Item;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Most permissions are based around items present in the inventory
    /// </summary>
    public class PlayerPermissions : NetworkBehaviour
    {
        // TODO: Set to false. Change to true when picking up correct items
        [SerializeField] private NetworkVariable<bool> canOpenDoor = new NetworkVariable<bool>(true);
        [SerializeField] private NetworkVariable<bool> canPickupItem = new NetworkVariable<bool>(true);

        [SerializeField] private Inventory inventory;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            canOpenDoor.Value = true;
            canPickupItem.Value = true;
        }
        public Inventory GetInventory() { return inventory; }
        public bool GetCanOpenDoor() { return canOpenDoor.Value; }
        public bool GetCanPickupItem() { return canPickupItem.Value; }

        public void UpdatePlayerPermissions()
        {
            // TODO: Update permissions depending on items here
        }
    }
}