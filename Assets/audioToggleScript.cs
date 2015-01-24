using UnityEngine;
using System.Collections;

public class audioToggleScript : MonoBehaviour {

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
		AudioSource audio = GetComponent<AudioSource>();
	}

	void Start () {
	
	}
	#endregion


	#region gameloop
	void Update () {
		if (button.enabled){
			if (onOnOn && button.activated && !audio.enabled){
				audio.enabled = true;
				if (stayOn) Destroy(this, 0);
			} else if (onOnOn && !button.activated && audio.enabled) audio.enabled = false;

			if (!onOnOn && button.activated && audio.enabled){
				audio.enabled = false;
				if (stayOff) Destroy (this, 0);
			} else if (!onOnOn && !button.activated && !audio.enabled) audio.enabled = true;
		}
//		if (!onOnOn){
//			if (button.activated && !stayOn){
//				audio.Stop();
//			}
//			else if (!stayOff && !audio.isPlaying){
//				audio.Play();
//			}
//		} else {
//			if (button.activated && !stayOff && !audio.isPlaying){
//				audio.Play();
//			}
//			else if (!stayOn){
//				audio.Stop ();
//			}
//		}
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
