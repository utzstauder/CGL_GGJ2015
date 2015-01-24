using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	public float xRangeMin;
	public float xRangeMax;
	public float yRangeMin;
	public float yRangeMax;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2(Random.Range(xRangeMin,xRangeMax), Random.Range(yRangeMin,yRangeMax));
	}
}
