using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
[RequireComponent(typeof(Rigidbody))]

public class interactableScript : MonoBehaviour {
	
	#region private variables
	#endregion


	#region public variables
	public bool activated = false;
	public bool button = false;
	public bool moveable = false;

	public lightActivation light;
	#endregion


	#region initialization
	void Awake() {
		if(button) rigidbody.isKinematic = true;
		this.gameObject.layer = 6; //"InteractableObject"
		this.gameObject.tag = "Interactable";
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
	[RPC]
	public void OnActivation(){
		activated = true;
		if (button){
			light.Switch();
		}

		if (networkView.isMine){
			networkView.RPC("OnActivation", RPCMode.OthersBuffered, null);
		}
	}

	[RPC]
	public void OnDeactivation(){
		activated = false;
		if (button){
			light.Switch();
		}

		if (networkView.isMine){
			networkView.RPC("OnActivation", RPCMode.OthersBuffered, null);
		}
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
