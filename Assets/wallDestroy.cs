using UnityEngine;
using System.Collections;

public class wallDestroy : MonoBehaviour {

	#region private variables
	private Animator anim;
	#endregion


	#region public variables
	public GameObject target;
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
			Destroy(target.gameObject, 0);
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
	void OnTriggerStay(Collider other){
		Debug.Log("trigger");
		if (other.gameObject.name == "dino"){
			Destroy (other.gameObject, 0);
			Destroy (this.gameObject, 0);
		}
	}
	#endregion

	#region coroutines
	#endregion
}
