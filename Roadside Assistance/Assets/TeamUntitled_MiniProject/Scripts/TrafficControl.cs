using UnityEngine;
using System.Collections;

public class TrafficControl : MonoBehaviour {
    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.right * speed);

        if (transform.position.x >= 100f) {
            Destroy(gameObject);
        }
	}
}
