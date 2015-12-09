using UnityEngine;
using System.Collections;

public class DemoController : MonoBehaviour {

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.Alpha2)) {
            Application.LoadLevel("Scenario2");
        }
        if (Input.GetKey(KeyCode.Alpha1)) {
            Application.LoadLevel("Demo");
        }
	}
}
