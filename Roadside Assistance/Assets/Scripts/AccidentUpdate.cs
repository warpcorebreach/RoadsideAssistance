using UnityEngine;
using System.Collections;

public class AccidentUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c) {
        if (c.tag.Equals("car")) {
            c.GetComponent<VehicleController>().PlayAccidentUpdate();
            Debug.Log("accident update");
        }
    }
}
