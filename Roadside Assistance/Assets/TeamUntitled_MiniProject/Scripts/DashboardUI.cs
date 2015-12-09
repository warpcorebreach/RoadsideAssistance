using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public enum SoundLevel
{
    OFF, LOW, MEDIUM, HIGH
}

public class DashboardUI : MonoBehaviour {

    public Text SoundButtonText;

    public GameObject NewJobLight;
    public GameObject UpdateJobLight;
    public GameObject WeatherLight;

    private int m_soundLevel;

    public static SoundLevel LEVEL;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (LEVEL == SoundLevel.OFF)
        {
            SoundButtonText.text = "Sound Off";
        }
        else if (LEVEL == SoundLevel.LOW)
        {
            SoundButtonText.text = "Sound Low";
        }
        else if (LEVEL == SoundLevel.MEDIUM)
        {
            SoundButtonText.text = "Sound Medium";
        }
        else if (LEVEL == SoundLevel.HIGH)
        {
            SoundButtonText.text = "Sound High";
        }
	}

    public void NewJobLightOn()
    {
        //Color originalColor = NewJobLight.GetComponent<Image>().color;
        //Color lColor = originalColor == Color.black ? Color.green : Color.black;
        NewJobLight.GetComponent<Image>().color = Color.green;
    }

    public void UpdateLightOn()
    {
        //Color originalColor = UpdateJobLight.GetComponent<Image>().color;
        //Color lColor = originalColor == Color.black ? Color.green : Color.black;
        UpdateJobLight.GetComponent<Image>().color = Color.green;
    }

    public void ToggleWeatherLight()
    {
        Color originalColor = WeatherLight.GetComponent<Image>().color;
        Color lColor = originalColor == Color.black ? Color.green : Color.black;
        Debug.Log(lColor);
        WeatherLight.GetComponent<Image>().color = lColor;
    }

    public void ClearWeather()
    {
        WeatherLight.GetComponent<Image>().color = Color.black;
    }

    public void ClearNewJob()
    {
        NewJobLight.GetComponent<Image>().color = Color.black;
    }

    public void ClearUpdate()
    {
        UpdateJobLight.GetComponent<Image>().color = Color.black;
    }

    
    public void ChangeSoundLevel()
    {
        m_soundLevel++;
        if (m_soundLevel > 3)
        {
            m_soundLevel = 0;
        }
        LEVEL = (SoundLevel)m_soundLevel;
        Debug.Log(LEVEL);
    }
}
