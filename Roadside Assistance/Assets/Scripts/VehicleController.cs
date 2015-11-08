using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {
    public AudioClip newAccident;
    public AudioClip accidentUpdate;
    public AudioClip accidentUpdateSpeech;
    public AudioClip unsafeToPullOver;

    float Speed;
    float RotateSpeed;

    public AudioSource directionSource;
    public AudioSource notifySource;
    public AudioSource notifySource2;

    public AudioClip[] Clips;

	// Use this for initialization
	void Start () {
        Speed = 10;
        RotateSpeed = 1;
        AudioSource[] sources = this.GetComponents<AudioSource>();
        directionSource = sources[0];
        notifySource = sources[1];
        notifySource2 = sources[2];
	}
	
	// Update is called once per frame
	void Update ()
    {
        float forward = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        float side = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        transform.Translate(side, 0, forward);

        float h = RotateSpeed * Input.GetAxis("Mouse X");
        //float v = 1 * Input.GetAxis("Mouse Y");
        transform.Rotate(0, h, 0);
	}

    public void PlayNewAccident() {
        notifySource.clip = newAccident;
        notifySource.Play();
    }

    public void PlayAccidentUpdate() {
        notifySource.clip = accidentUpdate;
        notifySource.Play();
    }

    public void PlayAccidentUpdateSpeech() {
        notifySource2.clip = accidentUpdateSpeech;
        notifySource2.Play();
    }

    public void PlayUnsafeToPullOver() {
        notifySource.clip = unsafeToPullOver;
        notifySource.Play();
        Debug.Log("unsafe to pull over");
    }

    public void PlayOpenDoor() {
        
    }

}
