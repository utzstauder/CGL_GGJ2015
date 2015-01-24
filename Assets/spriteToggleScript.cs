using UnityEngine;
using System.Collections;

public class spriteToggleScript : MonoBehaviour {

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
		if (!onOnOn) GetComponent<SpriteRenderer>().enabled = true;
		else GetComponent<SpriteRenderer>().enabled = false;
	}

	void Start () {
	
	}
	#endregion


	#region gameloop
	void Update () {
		if (!onOnOn){
			if (button.activated && !stayOn) GetComponent<SpriteRenderer>().enabled = false;
			else  if (!stayOff) GetComponent<SpriteRenderer>().enabled = true;
		} else {
			if (button.activated && !stayOff) GetComponent<SpriteRenderer>().enabled = true;
			else if (!stayOn) GetComponent<SpriteRenderer>().enabled = false;
		}
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
