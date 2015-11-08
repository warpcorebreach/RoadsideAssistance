using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {
    public AudioClip newAccident;
    public AudioClip accidentUpdate;
    public AudioClip accidentUpdateSpeech;
    public AudioClip unsafeToPullOver;
    public DirectionalNav accident;

    private float Speed;
    private float RotateSpeed;
    private bool newJob;

    private AudioSource notifySource;
    private AudioSource notifySource2;

	// Use this for initialization
	void Start () {
        newJob = false;
        Speed = 10;
        RotateSpeed = 1;
        AudioSource[] sources = this.GetComponents<AudioSource>();
        notifySource = sources[0];
        notifySource2 = sources[1];
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

        // wait until new accident notification is finished playing to start nav
        if (newJob && !notifySource.isPlaying) {
            accident.InitializeNav();
            newJob = false;
        }
	}

    public void PlayNewAccident() {
        notifySource.clip = newAccident;
        notifySource.Play();
        newJob = true;
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
    }

    public void PlayOpenDoor() {
        
    }

}
