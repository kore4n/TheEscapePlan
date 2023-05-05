using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Item;

namespace Player
{
    public class PlayerNetworkBody : NetworkBehaviour
    {
        [SerializeField] private GameObject lookCamera;

        // Disable aim controller for other players
        [SerializeField] private PlayerAiming playerAiming;

        [Tooltip("Layers the player should interact with (ex. Items, doors, etc...)")]
        [SerializeField] private LayerMask interactLayer;

        private NetworkVariable<int> playerHP = new NetworkVariable<int>(5, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [SerializeField] private Inventory inventory;
        [SerializeField] private PlayerPermissions playerPermissions;

        private void Start()
        {
            if (IsOwner) return;

            // turn off all cameras player doesn't own
            lookCamera.SetActive(false);
            playerAiming.enabled = false;
        }

        void Update()
        {
            if (!IsOwner) return;

            if (Input.GetKeyDown(KeyCode.T))
            {
                playerHP.Value -= 1;
                Debug.Log(playerHP.Value);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E was pressed!");
                InteractServerRpc();
            }
        }

        /// <summary>
        /// For interacting with items, doors, buttons, etc...
        /// </summary>
        [ServerRpc]
        void InteractServerRpc()
        {
            //Debug.Log("From server: Trying to interact!");
            //Debug.DrawRay(lookCamera.transform.position, lookCamera.transform.forward, Color.blue, 5f);

            if (Physics.Raycast(lookCamera.transform.position, lookCamera.transform.forward, out RaycastHit hit, Mathf.Infinity, interactLayer))
            {
                Debug.Log($"Hit {hit.transform.name}");

                var thing = hit.transform.GetComponent<NetworkObject>();

                if (thing == null) { return; }

                var interactable = hit.transform.GetComponent<IInteractable>();

                if (interactable == null) { return; }

                // Must be networked and interactable to interact with
                interactable.Interact(playerPermissions);
            }
        }
    }
}