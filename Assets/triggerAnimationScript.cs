using UnityEngine;
using System.Collections;

public class triggerAnimationScript : MonoBehaviour {

	#region private variables
	[SerializeField]
	private bool onOnOn;
	[SerializeField]
	private bool stayOn;
	[SerializeField]
	private bool stayOff;

	private Animator anim;
	#endregion
	
	
	#region public variables
	public interactableScript button;
	#endregion


	#region initialization
	void Awake() {
		anim = GetComponent<Animator>();
	}

	void Start () {
	
	}
	#endregion


	#region gameloop
	void Update () {
		if (button.enabled && !anim.GetBool("activate")){
			if (onOnOn && button.activated){
				anim.SetBool("activate", true);
				if (stayOn) Destroy(this, 0);
			} else if (onOnOn && !button.activated && anim.GetBool("activate")) anim.SetBool("activate", false);
			
			if (!onOnOn && button.activated && anim.GetBool("activate")){
				anim.SetBool("activate", false);
				if (stayOff) Destroy (this, 0);
			} else if (!onOnOn && !button.activated && !anim.GetBool("activate")) anim.SetBool("activate", true);
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
