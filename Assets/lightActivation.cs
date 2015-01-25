using UnityEngine;
using System.Collections;

[RequireComponent (typeof(NetworkView))]

public class lightActivation : MonoBehaviour {

	#region private variables
	[SerializeField]
	private bool onOnOn;
	[SerializeField]
	private bool stayOn;
	[SerializeField]
	private bool stayOff;
	#endregion
	
	
	#region public variables
	public interactableScript button;
	#endregion
	
	
	#region initialization
	void Awake() {
		if (!onOnOn) GetComponent<Light>().enabled = true;
		else GetComponent<Light>().enabled = false;
	}
	
	void Start () {
		
	}
	#endregion
	
	
	#region gameloop
	void Update () {
		if (!onOnOn){
			if (button.activated && !stayOn) GetComponent<Light>().enabled = false;
			else  if (!stayOff) GetComponent<Light>().enabled = true;
		} else {
			if (button.activated && !stayOff) GetComponent<Light>().enabled = true;
			else if (!stayOn) GetComponent<Light>().enabled = false;
		}
	}
	
	void FixedUpdate() {
		
	}
	#endregion


	#region methods
/*	[RPC]
  	void Activate(){
		light.enabled = true;
	}

	[RPC]
	void Deactivate(){
		light.enabled = false;
	}*/

//	[RPC]
//	public void Switch(){
//		if (light.enabled) light.enabled = false;
//		else light.enabled = true;
//
//		if (networkView.isMine) networkView.RPC("Switch", RPCMode.OthersBuffered, null); 
//	}
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
