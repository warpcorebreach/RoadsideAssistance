using UnityEngine;
using System.Collections;

public class WeatherUpdate : MonoBehaviour {

    public AudioSource AlarmSource;
    public AudioSource RainSource, WindSource, ClearSource, SnowSource;

    public AudioClip AlarmClip;
    public AudioClip RainClip, WindClip, ClearClip, SnowClip;

    public bool IsRain, IsWind, IsClear, IsSnow;

    [Range(0, 3600)]
    public float SecondsBetweenPlays;

    private bool m_isPlaying;
    public bool IsPlaying
    {
        get { return m_isPlaying; }
        set
        {
            if (value)
            {
                StartCoroutine("PlayWeatherSounds");
            }
            else
            {
                StopCoroutine("PlayWeatherSounds");
            }
            m_isPlaying = value;
        }
    }

	// Use this for initialization
	void Start () {
        RainSource.clip = RainClip;
        WindSource.clip = WindClip;
        SnowSource.clip = SnowClip;
        ClearSource.clip = ClearClip;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator PlayWeatherSounds()
    {
        while (IsPlaying)
        {
            AlarmSource.Play();
            yield return new WaitForSeconds(AlarmClip.length);
            float waitTime = 0;
            if (IsRain)
            {
                RainSource.Play();
                waitTime = RainClip.length;
            }
            if (IsSnow)
            {
                SnowSource.Play();
                waitTime = SnowClip.length;
            }
            if (IsWind)
            {
                WindSource.Play();
                waitTime = WindClip.length;
            }
            if (IsClear)
            {
                ClearSource.Play();
                waitTime = ClearClip.length;
            }
            yield return new WaitForSeconds(waitTime + SecondsBetweenPlays);
        }
    }
}
