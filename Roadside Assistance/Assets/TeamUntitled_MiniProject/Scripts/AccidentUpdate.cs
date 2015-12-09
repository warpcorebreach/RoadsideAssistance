using UnityEngine;
using System.Collections;

public class AccidentUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c) {
        if (VehicleController.CURRENTSTATE == State.ENROUTE)
        {
            if (c.tag.Equals("car"))
            {
                c.GetComponent<VehicleController>().PlayAccidentUpdate();
            } else if (c.tag.Equals("car2")) {
                c.GetComponent<Scen2Vehicle>().PlayAccidentUpdate();
            }
        }
        
    }
}
