using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Player;

public class Door : NetworkBehaviour, IInteractable
{
    // SerialzieField only for testing
    [Tooltip("All doors start closed for now.")]
    [SerializeField] private NetworkVariable<bool> isDoorOpen = new NetworkVariable<bool>(false);

    [SerializeField] private Animator doorAnimator;

    [Tooltip("Collider of the door. Turn off when door opens.")]
    [SerializeField] private Collider doorCollider;

    /// <summary>
    /// Always called on server.
    /// </summary>
    /// <param name="playerPermissions"></param>
    public void Interact(PlayerPermissions playerPermissions)
    {
        if (!playerPermissions.GetCanOpenDoor()) { return; }


        bool curIsDoorOpen = isDoorOpen.Value;
        bool newIsDoorOpen = !isDoorOpen.Value;

        Debug.Log($"Door is currently open status: {curIsDoorOpen}");
        if (newIsDoorOpen) { Debug.Log("Opening door!"); } else { Debug.Log("Closing door!"); }

        doorCollider.enabled = !newIsDoorOpen;

        doorAnimator.SetBool("OpenDoor", newIsDoorOpen);

        isDoorOpen.Value = newIsDoorOpen;
    }
}
