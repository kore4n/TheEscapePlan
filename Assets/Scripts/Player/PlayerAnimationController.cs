using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class PlayerAnimationController : NetworkBehaviour
{
    [Tooltip("Skin of the player. To turn off client skin at start.")]
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private Animator playerAnimator;

    private void Start()
    {
        if (!IsOwner) return;   // Hide user's mesh

        meshRenderer.enabled = false;
    }

    private void Update()
    {
        if (!IsOwner) return;   // Must be owner to control animations

        playerAnimator.SetFloat("VelocityY", Input.GetAxisRaw("Vertical"));
        playerAnimator.SetFloat("VelocityX", Input.GetAxisRaw("Horizontal"));

    }
}
