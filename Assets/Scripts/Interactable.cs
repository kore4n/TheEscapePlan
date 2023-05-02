using Item;
using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

public interface IInteractable
{
    // For doors/gadgets/buttons
    public void Interact(PlayerPermissions playerPermissions);
}