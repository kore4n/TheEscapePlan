using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace Player
{
    public class PlayerNetworkMain : NetworkBehaviour
    {
        [SerializeField] private Transform playerBodyPrefab;

        [Tooltip("For testing. Normally set to true.")]
        [SerializeField] private bool spawnBodyOnConnection = true;

        void Start()
        {
            if (IsServer)
            {
                if (!spawnBodyOnConnection) return;

                var playerBodySpawned = Instantiate(playerBodyPrefab);
                var networkPlayerBody = playerBodySpawned.GetComponent<NetworkObject>();
                networkPlayerBody.SpawnWithOwnership(OwnerClientId);
            }
        }
    }
}