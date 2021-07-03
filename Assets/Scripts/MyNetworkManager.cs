using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyNetworkManager : NetworkManager
{

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();

        player.SetDisplayName($"Player {numPlayers}");
        player.SetColor(new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f)));

        Debug.Log("A new client has been added. Avtive players: " + numPlayers);
    }
}
