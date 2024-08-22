using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;


public class UIManager : NetworkBehaviour
{
    [SerializeField]
    private TextMeshProUGUI clientsCounter;

    [SerializeField]
    private TextMeshProUGUI shotHitsCounter;

    private NetworkVariable<int> playersNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        clientsCounter.text = playersNum.Value.ToString();
        shotHitsCounter.text = "";

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            shotHitsCounter.text += player.name + ": " + player.GetComponent<PlayerController>().shotsHit.Value + "\n";
        }

        if (!IsServer) return;
        playersNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
