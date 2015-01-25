using UnityEngine;
using System.Collections;

public class onSpriteEnablePlayAnim : MonoBehaviour {

	private Animator anim;
	private SpriteRenderer spriter;

	private bool isPlaying = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		spriter = GetComponent<SpriteRenderer>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(spriter.enabled && !isPlaying){
			anim.SetTrigger("startAnim");
			isPlaying = true;
		}
	}
}
