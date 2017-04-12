using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class Client : MonoBehaviour {

	public int port = 4444;

	private NetworkClient caveConnection = null;
	Button buttonConnect;
	InputField inputIpAdress;
	Text explenaition;
	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
		buttonConnect = GameObject.Find ("b_connect").GetComponent<UnityEngine.UI.Button> ();
		buttonConnect.onClick.AddListener(TaskOnClick);

		inputIpAdress = GameObject.Find ("ip_ipAdress").GetComponent<UnityEngine.UI.InputField> ();
		explenaition = GameObject.Find ("tx_explenaition").GetComponent<UnityEngine.UI.Text> ();
		explenaition.gameObject.SetActive (false);

		caveConnection = new NetworkClient ();
		caveConnection.RegisterHandler (MsgType.Connect, OnConnected);
		caveConnection.RegisterHandler (MsgType.Disconnect, OnDisconnect);
	}

	void TaskOnClick()
	{
		Debug.Log("You have clicked the button!");
		caveConnection.Connect (inputIpAdress.text, port);
	}

	// Update is called once per frame
	void Update () {
		if (caveConnection.isConnected) {
			MovementUpdate Message = new MovementUpdate ();
			Message.forward = Mathf.Clamp(Input.acceleration.y, -1f, 1f);
			Message.sideways = Mathf.Clamp (Input.acceleration.x, -1f, 1f);
			caveConnection.SendUnreliable (MyNetworkMessages.Movement, Message);

			if (-Input.acceleration.z <=0.1) {
				caveConnection.Send (MyNetworkMessages.Jump, new DefaultMessage());
			}

			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				caveConnection.Send (MyNetworkMessages.Fire, new DefaultMessage());
			}
		}
	}

	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
		buttonConnect.gameObject.SetActive (false);
		inputIpAdress.gameObject.SetActive (false);
		explenaition.gameObject.SetActive (true);
	}	

	public void OnDisconnect(NetworkMessage netMsg)
	{
		Debug.Log("Disconnected from server");
		buttonConnect.gameObject.SetActive (true);
		inputIpAdress.gameObject.SetActive (true);
		explenaition.gameObject.SetActive (false);
	}
}
