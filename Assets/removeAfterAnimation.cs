using UnityEngine;
using System.Collections;

public class removeAfterAnimation : MonoBehaviour {

	#region private variables
	private Animator anim;
	#endregion


	#region public variables
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
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("destroy")){
			Destroy (this.gameObject, 0);
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
