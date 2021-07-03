using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText = null;
    [SerializeField] private Renderer displayColorRenderer = null;

    [SyncVar(hook =nameof(HandleDisplayNameTextUpdate))]
    [SerializeField]
    private string displayName = "Missing name";

    // hook is going to look for a method and run it.
    // Whenever playerColor get updated on a client, the fuction we scpecified gets called (thanks to hook)
    [SyncVar(hook=nameof(HandleDisplayColorUpdate))]
    [SerializeField]
    private Color playerColor = Color.black;

    // Server attribute stops clients from runnig this method
    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }

    [Server]
    public void SetColor(Color newDisplayColor)
    {
        playerColor = newDisplayColor;
    }

    private void HandleDisplayColorUpdate(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.SetColor("_BaseColor", newColor);
    }

    private void HandleDisplayNameTextUpdate(string oldName, string newName)
    {
        displayNameText.text = newName;
    }
}
