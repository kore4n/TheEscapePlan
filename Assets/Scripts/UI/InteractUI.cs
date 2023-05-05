using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private GameObject interactText;

    private void Start()
    {
        interactText.SetActive(false);

        PlayerNetworkBody.OnShowInteractPrompt += HandleShowInteractPrompt;
        PlayerNetworkBody.OnHideInteractPrompt += HandleHideInteractPrompt;
    }

    private void OnDestroy()
    {
        PlayerNetworkBody.OnShowInteractPrompt -= HandleShowInteractPrompt;
        PlayerNetworkBody.OnHideInteractPrompt -= HandleHideInteractPrompt;
    }

    private void HandleShowInteractPrompt()
    {
        interactText.SetActive(true);
    }
    private void HandleHideInteractPrompt()
    {
        interactText.SetActive(false);
    }
}
