using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class Server : MonoBehaviour {

	UnityEngine.UI.Text tx_ipAdress;
	public int port = 4444;
	public CharacterInput player;


	// Use this for initialization
	void Start () {
		tx_ipAdress = GameObject.Find("tx_ipAdress").GetComponent<UnityEngine.UI.Text>();

		NetworkServer.RegisterHandler (MsgType.Connect, OnConnected);
		NetworkServer.RegisterHandler (MyNetworkMessages.Movement, OnMovement);
		NetworkServer.RegisterHandler (MyNetworkMessages.Jump, OnJump);
		NetworkServer.RegisterHandler (MyNetworkMessages.Fire, OnFire);
		NetworkServer.Listen (port);
		string ipAdress = Network.player.ipAddress;
		Debug.Log ("Ip-Adresse "+ipAdress);

		tx_ipAdress.text = "Ip-Adresse: " + ipAdress;
	}
	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
	}		

	public void OnMovement(NetworkMessage netMsg)
	{
		MovementUpdate Message = netMsg.ReadMessage<MovementUpdate>();
		player.UpdateMovement (Message.forward, Message.sideways);
	}	

	public void OnJump(NetworkMessage netMsg)
	{
		player.Jump ();
	}

	public void OnFire(NetworkMessage netMsg)
	{
		player.Fire ();
	}
}
