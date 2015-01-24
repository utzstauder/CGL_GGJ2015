using UnityEngine;
using System.Collections;

public class toggleScriptActivation : MonoBehaviour {

	#region private variables
	[SerializeField]
	private bool onOnOn;
	[SerializeField]
	private bool stayOn;
	[SerializeField]
	private bool stayOff;

	private interactableScript targetScript;
	#endregion
	
	
	#region public variables
	public interactableScript button;
	#endregion


	#region initialization
	void Awake() {
		targetScript = GetComponent<interactableScript>();
		if (!onOnOn) targetScript.enabled = true;
		else targetScript.enabled = false;

		if (GetComponent<Light>()) GetComponent<Light>().enabled = false;
	}

	void Start () {
	
	}
	#endregion


	#region gameloop
	void Update () {
		if (!onOnOn){
			if (button.activated && !stayOn) targetScript.enabled = false;
			else  if (!stayOff) targetScript.enabled = true;
		} else {
			if (button.activated && !stayOff) targetScript.enabled = true;
			else if (!stayOn) targetScript.enabled = false;
		}

		if (GetComponent<Light>() && targetScript.enabled) GetComponent<Light>().enabled = true;
	}

	void FixedUpdate() {

	}
	#endregion


	#region methods
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
