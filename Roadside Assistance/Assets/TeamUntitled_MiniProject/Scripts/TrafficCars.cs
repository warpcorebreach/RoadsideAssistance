using UnityEngine;
using System.Collections;

public class TrafficCars : MonoBehaviour {
    public GameObject carPrefab;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnTraffic());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator SpawnTraffic() {
        while (true) {
            for (int i = 0; i < 3; i++) {
                GameObject newCar = Instantiate(carPrefab, transform.position, Quaternion.identity) as GameObject;
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
