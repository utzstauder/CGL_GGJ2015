using UnityEngine;
using System.Collections;

public class toolboxScript : MonoBehaviour {

	#region private variables
	private bool hammerInTrigger;
	private bool zangeInTrigger;
	#endregion


	#region public variables
	public bool activated = false;
	#endregion


	#region initialization
	void Awake() {

	}

	void Start () {
	
	}
	#endregion


	#region gameloop
	void Update () {
		if (activated) GetComponent<Light>().enabled = true;
	}

	void FixedUpdate() {
		if (hammerInTrigger && zangeInTrigger) activated = true;

		hammerInTrigger = false;
		zangeInTrigger = false;
	}
	#endregion


	#region methods
	#endregion


	#region functions
	#endregion

	#region colliders
	#endregion

	#region triggers
	void OnTriggerStay(Collider other){
		if (other.gameObject.name == "Hammer_Mesh") hammerInTrigger = true;
		if (other.gameObject.name == "Zange_Mesh") zangeInTrigger = true;
	}
	#endregion

	#region coroutines
	#endregion
}
