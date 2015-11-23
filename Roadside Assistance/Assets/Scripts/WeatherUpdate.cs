using UnityEngine;
using System.Collections;

public class WeatherUpdate : MonoBehaviour {

    public AudioSource AlarmSource;
    public AudioSource RainSource, WindSource, ClearSource, SnowSource;


    public bool IsRain, IsWind, IsClear, IsSnow;

    [Range(0, 3600)]
    public float SecondsBetweenPlays;

    private bool m_isPlaying;
    public bool IsPlaying
    {
        get { return m_isPlaying; }
        set
        {
            m_isPlaying = value;
            if (value)
            {
                StartCoroutine("PlayWeatherSounds");
            }
            else
            {
                StopCoroutine("PlayWeatherSounds");
            }
        }
    }

	// Use this for initialization
	void Start () {
        IsClear = true;
        IsPlaying = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator PlayWeatherSounds()
    {
        //Debug.Log("is playing: " + m_isPlaying);
        while (IsPlaying)
        {
            AlarmSource.Play();
            yield return new WaitForSeconds(AlarmSource.clip.length);
            float waitTime = 0;
            if (IsRain)
            {
                RainSource.Play();
                waitTime = RainSource.clip.length;
            }
            if (IsSnow)
            {
                SnowSource.Play();
                waitTime = SnowSource.clip.length;
            }
            if (IsWind)
            {
                WindSource.Play();
                waitTime = WindSource.clip.length;
            }
            if (IsClear)
            {
                ClearSource.Play();
                waitTime = ClearSource.clip.length;
            }
            yield return new WaitForSeconds(waitTime + SecondsBetweenPlays);
        }
    }

    
}
