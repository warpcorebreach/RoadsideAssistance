using UnityEngine;
using System.Collections;

public class DirectionalNav : MonoBehaviour {
    public Transform serviceVehicle;
    public float navBeepDelay;

    private AudioSource aud;

	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitializeNav() {
        float distance = (serviceVehicle.position - transform.position).magnitude;
        StartCoroutine(DirectionalNavigate(distance));
    }

    IEnumerator DirectionalNavigate(float maxDistance) {
        float delay;

        while (true) {
            aud.Play();
            delay = (serviceVehicle.position - transform.position).magnitude / maxDistance;
            yield return new WaitForSeconds(delay * navBeepDelay);
        }
    }

    void OnTriggerEnter(Collider c) {
        if (c.tag.Equals("car")) {
            aud.Stop();
        }
    }
}
