using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("w")) {
            transform.Translate(transform.forward * speed);
        }
	}


}
