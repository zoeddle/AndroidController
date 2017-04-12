using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MyNetworkMessages {
	public const short Movement = 100;	
	public const short Jump = 200;	
	public const short Fire = 201;	
}

public class DefaultMessage : MessageBase
{}

public class MovementUpdate : MessageBase
{
	public float forward;
	public float sideways;
}
