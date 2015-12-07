using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum State { PATROL, ENROUTE, ATGOAL};


public class VehicleController : MonoBehaviour {
    // notification audio
    public AudioClip newAccident;
    public AudioClip accidentUpdate;
    public AudioClip accidentUpdateSpeech;
    public AudioClip unsafeToPullOver;
    public AudioClip unsafeToOpenDoor;
    public AudioClip safeToOpenDoor;
    public AudioClip turnSignal;
    public AudioClip safeToMerge;
    public AudioClip newJobSpeech;
    public AudioClip weatherSpeech;

    public GameObject[] mudSpots;

    //Dashboard UI
    public GameObject DashboardPanel;
    private DashboardUI dUI;
    public Button JobDone;
    public static State CURRENTSTATE;

    public DirectionalNav accident;
    public Transform firstPerson, thirdPerson;
    public float RotateSpeed;

    private float Speed;
    private bool newJob, isFirstPerson, safeToPullOver;

    private AudioSource notifySource;
    private AudioSource notifySource2;
    private AudioSource notifySource3;
    private AudioSource alarmSource;

	// Use this for initialization
	void Start () {
        CURRENTSTATE = State.PATROL;
        newJob = false;
        isFirstPerson = false;
        safeToPullOver = false;
        Speed = 10;
        //RotateSpeed = 1;
        AudioSource[] sources = this.GetComponents<AudioSource>();
        notifySource = sources[0];
        notifySource2 = sources[1];
        alarmSource = sources[2];
        notifySource3 = sources[3];
        dUI = DashboardPanel.GetComponent<DashboardUI>();
        DashboardUI.LEVEL = SoundLevel.MEDIUM;

        GetComponentInChildren<Camera>().transform.position = firstPerson.position;
        GetComponentInChildren<Camera>().transform.rotation = firstPerson.rotation;

        foreach (GameObject mud in mudSpots) {
            Debug.Log("mud");
            mud.GetComponent<Mud>().SetSeverity(Random.Range(-1.5f, 1.5f));
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        //float forward = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            float forward = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
            transform.Translate(0, 0, forward);
            //rb.AddForce(transform.forward * 10);
        }
        if (Input.GetKey(KeyCode.S))
        {
            float forward = ((Input.GetAxis("Vertical") < 0) ? Input.GetAxis("Vertical") : 0) * Speed * Time.deltaTime;
            transform.Translate(0, 0, forward);
            //rb.AddForce(transform.forward * -10);
        }
        if (rb.velocity.x != 0 || rb.velocity.y != 0 || rb.velocity.z != 0)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, RotateSpeed, 0);
                //this.GetComponent<Rigidbody>().AddForce(transform.right * 10);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, -RotateSpeed, 0);
                //this.GetComponent<Rigidbody>().AddForce(-transform.right * 10);
            }
        }
        //float side = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        //transform.Translate(0, 0, forward);

        /*
        float h = RotateSpeed * Input.GetAxis("Mouse X");
        //float v = 1 * Input.GetAxis("Mouse Y");
        transform.Rotate(0, h, 0);
        */

        // use A and D to turn instead of mouse
        

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

        if (CURRENTSTATE == State.PATROL)
        {
            JobDone.interactable = false;
            JobDone.GetComponentInChildren<Text>().text = "Patrolling...";
        }
        else if (CURRENTSTATE == State.ENROUTE)
        {
            JobDone.interactable = true;
            JobDone.GetComponentInChildren<Text>().text = "Set Arrived";
        }
        else
        {
            JobDone.interactable = true;
            JobDone.GetComponentInChildren<Text>().text = "Set Complete";
        }
        //Debug.Log(CURRENTSTATE);

	}

    public void PlayNewAccident(AudioClip AccidentType, AudioClip SpeechType) {
        newAccident = AccidentType;
        newJobSpeech = SpeechType;
        if (CURRENTSTATE == State.PATROL)
        {
            if (DashboardUI.LEVEL > SoundLevel.OFF)
            {
                notifySource.clip = newAccident;
                notifySource.Play();
            }
            //StartCoroutine("NewLightOn", newAccident.length);
            newJob = true;
            dUI.NewJobLightOn();
            CURRENTSTATE = State.ENROUTE;
        }
    }

    public void ToggleNewJob()
    {
        if (CURRENTSTATE == State.ENROUTE)
        {
            CURRENTSTATE = State.ATGOAL;
        }
        else if (CURRENTSTATE == State.ATGOAL)
        {
            CURRENTSTATE = State.PATROL;
        }
    }
    
    

    public void PlayAccidentUpdate() {
        if (CURRENTSTATE == State.ENROUTE)
        {
            if (DashboardUI.LEVEL > SoundLevel.OFF)
            {
                notifySource.clip = accidentUpdate;
                notifySource.Play();
                //StartCoroutine("NewUpdateOn", accidentUpdate.length);
            }
            dUI.UpdateLightOn();
        }
    }
    
    private IEnumerator NewUpdateOff(float time)
    {
        yield return new WaitForSeconds(time);
        dUI.ClearUpdate();
    }

    private IEnumerator NewJobOff(float time)
    {
        yield return new WaitForSeconds(time);
        dUI.ClearNewJob();
    }

    public void PlayAccidentUpdateSpeech() {
        notifySource2.clip = accidentUpdateSpeech;
        notifySource2.Play();
        StartCoroutine("NewUpdateOff", accidentUpdateSpeech.length);
    }

    public void PlayNewJobSpeech()
    {
        notifySource2.clip = newJobSpeech;
        notifySource2.Play();
        StartCoroutine("NewJobOff", newJobSpeech.length);
    }

    public void PlayWeatherSpeech()
    {
        notifySource2.clip = weatherSpeech;
        notifySource2.Play();
    }
    
    public void PlayUnsafeToPullOver() {
        if (CURRENTSTATE == State.ENROUTE)
        {
            safeToPullOver = false;
            if (DashboardUI.LEVEL >= SoundLevel.MEDIUM)
            {
                alarmSource.clip = unsafeToPullOver;
                StartCoroutine(PlaySafeToPullOver());
            }
        }
    }

    public void PlayTurnSignal() {
        if (DashboardUI.LEVEL >= SoundLevel.MEDIUM && !isBlinking)
        {
            isBlinking = true;
            notifySource3.clip = turnSignal;
            notifySource3.loop = true;
            notifySource3.Play();
            StartCoroutine(MergeIntoTraffic());
        }
        else if (isBlinking)
        {
            notifySource3.Stop();
            StopCoroutine(MergeIntoTraffic());
            isBlinking = false;
        }
    }

    public void PlayUnsafeToOpenDoor() {
        if (DashboardUI.LEVEL >= SoundLevel.MEDIUM && !isDoor)
        {
            StartCoroutine(PlayDoorOpen());
        }
        else
        {
            StopCoroutine(PlayDoorOpen());
            isDoor = false;
        }
        
    }
    bool isDoor;
    private IEnumerator PlayDoorOpen() {
        alarmSource.clip = unsafeToOpenDoor;
        //for (int i = 0; i < 8; i++) {
           // alarmSource.Play();
            //yield return new WaitForSeconds(0.5f);
        //}
        alarmSource.loop = true;
        alarmSource.Play();
        while (CarPassingBy.GetLowestDistance(this.transform) > 0.5 || !doorSafe)
        {
            yield return null;
        }
        alarmSource.Stop();
        alarmSource.loop = false;
        alarmSource.clip = safeToOpenDoor;
        alarmSource.Play();
    }

    private IEnumerator PlaySafeToPullOver() {
        while (!safeToPullOver) {
            alarmSource.Play();
            yield return new WaitForSeconds(1.5f);
        }
    }
    bool isBlinking;
    private IEnumerator MergeIntoTraffic() {
        //yield return new WaitForSeconds(4f);

        isBlinking = true;
        Debug.Log(CarPassingBy.GetLowestDistance(this.transform));
        while (CarPassingBy.GetLowestDistance(this.transform) > 0.5)
        {
            Debug.Log(CarPassingBy.GetLowestDistance(this.transform));
            yield return null;
        }
        notifySource3.Stop();   // disable blinker
        notifySource3.loop = false;

        notifySource2.clip = safeToMerge;
        notifySource2.Play();
        isBlinking = false;
    }

    public void SafeToPullOver() {
        safeToPullOver = true;
    }
    bool doorSafe = true;
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "wall")
        {
            Debug.Log("Collision");
            doorSafe = false;
        }
    }

    public void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "wall")
        {
            Debug.Log("Collision");
            doorSafe = true;
        }
    }

}
