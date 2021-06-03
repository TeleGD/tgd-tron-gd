using UnityEngine;
using System.Collections;

public class checkpoint_behave : MonoBehaviour {

	public map_generator map;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals ("Player")) {
			map.checkpoint_reached ();
			Destroy (gameObject);
		}
	}
}
