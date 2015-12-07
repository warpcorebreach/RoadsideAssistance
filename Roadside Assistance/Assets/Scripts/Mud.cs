using UnityEngine;
using System.Collections;

public class Mud : MonoBehaviour {
    private AudioSource aud;
    //private int severity;

	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetSeverity(float sev) {
        aud.pitch = sev;
    }

    void OnTriggerEnter(Collider c) {
        if (c.tag.Equals("car") || c.tag.Equals("car2")) {
            //c.GetComponent<VehicleController>().PlayUnsafeToPullOver();
            aud.Play();
        }
    }

    void OnTriggerExit(Collider c) {
        if (c.tag.Equals("car") || c.tag.Equals("car2")) {
            //c.GetComponent<VehicleController>().SafeToPullOver();
            aud.Stop();
        }
    }
}
