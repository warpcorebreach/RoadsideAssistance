using UnityEngine;
using System.Collections;

public class NewAccident : MonoBehaviour {

    public AudioClip AccidentType;
    public AudioClip SpeechType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c) {
        if (c.tag.Equals("car")) {
            c.GetComponent<VehicleController>().PlayNewAccident(AccidentType, SpeechType);
        } else if (c.tag.Equals("car2")) {
            c.GetComponent<Scen2Vehicle>().PlayNewAccident(AccidentType, SpeechType);
        }
    }
}
