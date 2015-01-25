using UnityEngine;
using System.Collections;

public class toggleScaleScript : MonoBehaviour {

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
	public float scale = .1f;
	#endregion


	#region initialization
	void Awake() {
		if (networkView.isMine) button = GameObject.Find ("mushroom").GetComponent<interactableScript>();
	}

	void Start () {
	
	}
	#endregion


	#region gameloop
	void Update () {
		if (button.enabled){
			if (onOnOn && button.activated && transform.localScale != new Vector3(scale, scale, scale)){
				transform.localScale = new Vector3(scale, scale, scale);
				if (stayOn) Destroy(this, 0);
			} else if (onOnOn && !button.activated && transform.localScale == new Vector3(scale, scale, scale)) transform.localScale = Vector3.one;
			
			if (!onOnOn && button.activated && transform.localScale == new Vector3(scale, scale, scale)){
				transform.localScale = Vector3.one;
				if (stayOff) Destroy (this, 0);
			} else if (!onOnOn && !button.activated && transform.localScale != new Vector3(scale, scale, scale)){
				transform.localScale = new Vector3(scale, scale, scale);
			}
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
