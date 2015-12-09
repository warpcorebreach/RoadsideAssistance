using UnityEngine;
using System.Collections;

public class WeatherTrigger : MonoBehaviour {

    public bool RainTrigger, WindTrigger, ClearTrigger, SnowTrigger, PlayTrigger;

    public GameObject WeatherScriptHolder;

    private WeatherUpdate weather;

	// Use this for initialization
	void Start () {
        weather = WeatherScriptHolder.GetComponent<WeatherUpdate>();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("weather trigger: rain=" + RainTrigger + ";wind=" + WindTrigger + ";snow=" + SnowTrigger + ";clear=" + ClearTrigger);
        if (PlayTrigger)
        {
            weather.IsPlaying = !weather.IsPlaying;
        }
        if (RainTrigger)
        {
            weather.IsRain = !weather.IsRain;
        }
        if (WindTrigger)
        {
            weather.IsWind = !weather.IsWind;
        }
        if (SnowTrigger)
        {
            weather.IsSnow = !weather.IsSnow;
        }
        if (ClearTrigger)
        {
            weather.IsClear = !weather.IsClear;
        }
    }
}
