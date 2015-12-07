using UnityEngine;
using System.Collections;

public class DirNav2 : MonoBehaviour {
    public Transform serviceVehicle;
    public float navBeepDelay;

    private AudioSource aud;
    private bool isInRange;

    // Use this for initialization
    void Start() {
        aud = GetComponent<AudioSource>();
        isInRange = true;
    }

    // Update is called once per frame
    void Update() {

    }

    public void InitializeNav() {
        float distance = (serviceVehicle.position - this.transform.position).magnitude;
        isInRange = false;
        StartCoroutine(DirectionalNavigate(distance));
    }

    IEnumerator DirectionalNavigate(float maxDistance) {
        float delay;

        while (!isInRange && Scen2Vehicle.CURRENTSTATE == State.ENROUTE) {
            if (DashboardUI.LEVEL >= SoundLevel.MEDIUM) {
                aud.Play();
            }
            delay = (serviceVehicle.position - transform.position).magnitude / maxDistance;
            yield return new WaitForSeconds(delay * navBeepDelay);
        }
    }

    void OnTriggerEnter(Collider c) {
        if (c.tag.Equals("car") || c.tag.Equals("car2")) {
            isInRange = true;
        }
    }
}

