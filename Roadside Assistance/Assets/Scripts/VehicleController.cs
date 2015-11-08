using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {
    public AudioClip newAccident;
    public AudioClip accidentUpdate;
    public AudioClip accidentUpdateSpeech;
    public AudioClip unsafeToPullOver;
    public AudioClip unsafeToOpenDoor;
    public AudioClip safeToOpenDoor;
    public DirectionalNav accident;
    public Transform firstPerson, thirdPerson;

    private float Speed;
    private float RotateSpeed;
    private bool newJob, isFirstPerson, safeToPullOver;

    private AudioSource notifySource;
    private AudioSource notifySource2;
    private AudioSource alarmSource;

	// Use this for initialization
	void Start () {
        newJob = false;
        isFirstPerson = false;
        safeToPullOver = false;
        Speed = 10;
        RotateSpeed = 1;
        AudioSource[] sources = this.GetComponents<AudioSource>();
        notifySource = sources[0];
        notifySource2 = sources[1];
        alarmSource = sources[2];
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


        // space bar switches between first and third person POV
        if (Input.GetKeyDown("space")) {
            if (isFirstPerson) {
                GetComponentInChildren<Camera>().transform.position = thirdPerson.position;
                GetComponentInChildren<Camera>().transform.rotation = thirdPerson.rotation;
                isFirstPerson = false;
            } else {
                GetComponentInChildren<Camera>().transform.position = firstPerson.position;
                GetComponentInChildren<Camera>().transform.rotation = firstPerson.rotation;
                isFirstPerson = true;
            }
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
        safeToPullOver = false;
        alarmSource.clip = unsafeToPullOver;
        StartCoroutine(PlaySafeToPullOver());
    }

    public void PlayUnsafeToOpenDoor() {
        StartCoroutine(PlayDoorOpen());
    }

    private IEnumerator PlayDoorOpen() {
        alarmSource.clip = unsafeToOpenDoor;
        for (int i = 0; i < 8; i++) {
            alarmSource.Play();
            yield return new WaitForSeconds(0.5f);
        }
        alarmSource.clip = safeToOpenDoor;
        alarmSource.Play();
    }

    private IEnumerator PlaySafeToPullOver() {
        while (!safeToPullOver) {
            alarmSource.Play();
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void SafeToPullOver() {
        safeToPullOver = true;
    }

}
