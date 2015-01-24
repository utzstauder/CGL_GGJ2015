using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {
	[SerializeField]
	AudioSource[] sounds = new AudioSource[5];
	int currentIndex = 0;
	int lastIndex = 0;

	AudioClip myaudio1;
	AudioClip myaudio2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	

	}

	public void PlayTheSound(){

		if( !sounds[lastIndex].isPlaying ){
			sounds[currentIndex].Play();
			currentIndex++;
			lastIndex++;
			if(currentIndex >= sounds.Length){
				currentIndex = 0;
				lastIndex = 0;
			}
			} 

	}
}
