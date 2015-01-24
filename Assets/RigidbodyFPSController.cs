using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (BoxCollider))]
[RequireComponent (typeof(NetworkView))]

public class RigidbodyFPSController : MonoBehaviour {

	#region private variables
	private bool grounded = false;
	[SerializeField]

	private float lastSynchronizationTime = 0.0f;
	private float syncDelay = 0.0f;
	private float syncTime = 0.0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	#endregion


	#region public variables
	public Transform camera;
	[SerializeField]

	public float speed = 10.0f;
	public float mouseSpeed = 5.0f;
	public float maxInteractionDistance = 1.0f;
//	public float maxAngle = 70.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	#endregion


	#region initialization
	void Awake() {
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;

		if (networkView.isMine) camera.tag = "MainCamera";
			else Destroy(camera.gameObject, 0);

		Screen.showCursor = false;
	}

	void Start () {
	
	}
	#endregion


	#region gameloop
	void Update () {
		if (networkView.isMine){
			// Rotate player on y axis according to mouse movement
			transform.Rotate (0, mouseSpeed * (Screen.width / Screen.height) * Input.GetAxis("Mouse X"), 0);
			
			// Rotate camera vertically accorting to mouse movement
			//		camera.transform.Rotate(Mathf.Clamp(-mouseSpeed  * Input.GetAxis("Mouse Y"), -maxAngle, maxAngle), 0, 0);
			Vector3 verticalRotation = camera.transform.localEulerAngles;
			verticalRotation.x -= mouseSpeed * Input.GetAxis("Mouse Y");
			
			//		if (!(verticalRotation.x <= maxAngle && verticalRotation.x >= 0) && !(verticalRotation.x >= 360f-maxAngle && verticalRotation.x < 360f)){
			////			Debug.Log ("out of bounds");
			////			verticalRotation.x = Input.GetAxis("Mouse Y") > 0 ? 360f-maxAngle : maxAngle;
			//		}
			
			camera.transform.localEulerAngles = verticalRotation;

			// Interact with items in sight
			RaycastHit hit;
			if (Physics.Raycast(camera.position, camera.transform.forward, out hit, maxInteractionDistance)){
				Debug.Log (hit);
				if (Input.GetButtonDown("Use") && hit.transform.gameObject.tag == "Interactable"){
					interactableScript interactionScript = hit.transform.gameObject.GetComponent<interactableScript>();
					NetworkViewID hitNetworkViewID = hit.transform.GetComponent<NetworkView>().viewID;
					if (!interactionScript.activated){
						Activate(hitNetworkViewID);
						if (interactionScript.moveable){
							interactionScript.rigidbody.isKinematic = true;
							interactionScript.transform.parent = this.camera.transform;
						}
					} else {
						Deactivate(hitNetworkViewID);
						if (interactionScript.moveable){
							interactionScript.rigidbody.isKinematic = false;
							interactionScript.transform.parent = null;
						}
					}
				}
			}

		}else{
			SyncedMovement();
		}
	}

	void FixedUpdate() {
		if (networkView.isMine){
			if (grounded){
				// Calculate movement vector
				Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				targetVelocity = transform.TransformDirection(targetVelocity);
				targetVelocity *= speed;

				// Apply a force that attempts to reach the target speed
				Vector3 velocity = rigidbody.velocity;
				Vector3 velocityChange = (targetVelocity - velocity);

				velocityChange.x = Mathf.Clamp (velocityChange.x, -maxVelocityChange, maxVelocityChange);
				velocityChange.z = Mathf.Clamp (velocityChange.z, -maxVelocityChange, maxVelocityChange);
				velocityChange.y = 0;

				rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

				// Jump
				if (canJump && Input.GetButtonDown("Jump")){
					rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				}
			}

			// Apply gravity manually for more "tuning control" (whatever, man)
			rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

			grounded = false;
		}
	}
	#endregion


	#region methods
	[RPC]
	void Activate(NetworkViewID networkViewID){
		interactableScript interactionScript = NetworkView.Find (networkViewID).GetComponent<interactableScript>();

		interactionScript.OnActivation();

		if (networkView.isMine) networkView.RPC ("Activate", RPCMode.OthersBuffered, networkViewID);
	}

	[RPC]
	void Deactivate(NetworkViewID networkViewID){
		interactableScript interactionScript = NetworkView.Find (networkViewID).GetComponent<interactableScript>();
		
		interactionScript.OnDeactivation();

		if (networkView.isMine) networkView.RPC ("Deactivate", RPCMode.OthersBuffered, networkViewID);
	}
	#endregion


	#region functions
	float CalculateJumpVerticalSpeed(){
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
	#endregion

	#region colliders
	void OnCollisionStay(Collision other){
		if (other.gameObject.tag == "Ground"){
			grounded = true;
		}
	}
	#endregion

	#region triggers
//	void OnTriggerStay(Collider other){
//		if (other.gameObject.tag == "Interactable" && Input.GetButtonDown("Use")){
//			interactableButton button = other.GetComponent<interactableButton>();
//			if (!button.activated){
//				other.GetComponent<interactableButton>().OnActivation(this.gameObject);
//			} else {
//				other.GetComponent<interactableButton>().OnDeactivation(this.gameObject);
//			}
//		}
//	}
	#endregion

	#region coroutines
	#endregion

	#region network
	void SyncedMovement(){
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		
		if (stream.isWriting){
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
		}else{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0.0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;
		}
	}
	#endregion


	#region gizmos
	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawRay(camera.position, camera.transform.forward * maxInteractionDistance);
	}
	#endregion
}
