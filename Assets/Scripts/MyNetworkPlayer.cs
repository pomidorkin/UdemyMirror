using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    /*
     * [Command]: Client tells server that he wants some action to be executed on the server side
     * [ClientRpc]: Server calls a method and all clients execute it
     * [TargetRpc]: Server calls a method and only the targer client executes it
     */

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

    #region Server

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

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        if (newDisplayName.Length < 2) { return; }
        RpcLogNewName(newDisplayName);
        SetDisplayName(newDisplayName);
    }

    #endregion

    #region Client

    private void HandleDisplayColorUpdate(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.SetColor("_BaseColor", newColor);
    }

    private void HandleDisplayNameTextUpdate(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("My new name");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    #endregion
}
