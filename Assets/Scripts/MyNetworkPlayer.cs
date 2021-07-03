using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    [SerializeField]
    private string displayName = "Missing name";

    [SyncVar]
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
}
