using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class PlayerAnimationController : NetworkBehaviour
{
    [SerializeField] private Animator playerAnimator;

    private void Update()
    {
        if (!IsOwner) return;   // Must be owner to control animations

        int x = 0;
        int y = 0;

        //if (Input.GetKey(KeyCode.W))
        //{
        //    y += 1;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    y -= 1;
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    x -= 1;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    x += 1;
        //}

        playerAnimator.SetFloat("VelocityY", Input.GetAxisRaw("Vertical"));
        playerAnimator.SetFloat("VelocityX", Input.GetAxisRaw("Horizontal"));

    }
}
