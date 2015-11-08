using UnityEngine;
using System.Collections;

public class ServiceVehicleControl : MonoBehaviour {
    public AudioClip newAccident, accidentUpdate, accidentUpdateSpeech;

    private AudioSource aud;

	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayNewAccident() {
        aud.clip = newAccident;
        aud.Play();
    }

    public void PlayAccidentUpdate() {
        aud.clip = accidentUpdate;
        aud.Play();
    }
}
