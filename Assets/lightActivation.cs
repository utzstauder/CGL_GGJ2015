using UnityEngine;
using System.Collections;

[RequireComponent (typeof(NetworkView))]

public class lightActivation : MonoBehaviour {

	#region private variables
	[SerializeField]
	private interactableScript buttonScript;
	[SerializeField]
	private Light light;
	#endregion


	#region public variables
	#endregion


	#region initialization
	void Awake() {
		light.enabled = false;
	}

	void Start () {
	
	}
	#endregion


	#region gameloop
	void Update () {

	}

	void FixedUpdate() {

	}
	#endregion


	#region methods
//	[RPC]
//	void Activate(){
//		light.enabled = true;
//	}
//
//	[RPC]
//	void Deactivate(){
//		light.enabled = false;
//	}

	[RPC]
	public void Switch(){
		if (light.enabled) light.enabled = false;
		else light.enabled = true;

		if (networkView.isMine) networkView.RPC("Switch", RPCMode.OthersBuffered, null); 
	}
	#endregion


	#region functions
	#endregion

	#region colliders
	#endregion

	#region triggers
	#endregion

	#region coroutines
	#endregion
}
