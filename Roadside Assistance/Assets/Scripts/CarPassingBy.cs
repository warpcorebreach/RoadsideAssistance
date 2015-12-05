using UnityEngine;
using System.Collections;

public class CarPassingBy : MonoBehaviour {

    public GameObject Target;
    public Transform PointA;
    public Transform PointB;

    private AudioSource source;

    private float m_secondsBetween, m_speed, m_curPoint;
    private float m_scale;
    private float m_normalizedDistance;

	// Use this for initialization
	void Start () {
        source = this.GetComponent<AudioSource>();
        StartCoroutine("LoopOnFrequency");
        m_speed = 0.1f;
        m_scale = 2;
        m_curPoint = 0.0f;
        this.transform.position = PointA.position;

	}

    IEnumerator LoopOnFrequency()
    {
        while(true)
        {

            source.pitch = m_normalizedDistance;
            //Debug.Log(source.pitch);
            if (DashboardUI.LEVEL >= SoundLevel.HIGH)
            {
                source.Play();
            }
            yield return new WaitForSeconds(source.clip.length + m_secondsBetween);
        }
    }

    float NormalizeToRange(float val, float min, float max)
    {
        float range = max - min;
        return Mathf.Abs(val - min) / range;
    }

    float NormalizeDistance()
    {
        //Because vectors can cross the axis, we have to convert them to local space.
        //Here we set the origin to be vector3, and convert all other vectors to this origin
        Vector3 aPos = Vector3.zero;
        Vector3 bPos = PointB.position - PointA.position;
        Vector3 objPos = this.transform.position - PointA.position;
        Vector3 targetPos = Target.transform.position - PointA.position;
        float min = 0;
        float max = Vector3.Distance(aPos, bPos);
        float range = max - min;
        //Debug.Log("range: " + range);
        float curDist = Vector3.Distance(objPos, targetPos);
        //Debug.Log("curDist: " + curDist + " Min: " + min);
        return NormalizeToRange(Mathf.Abs(curDist - 0) / range, 0.5f, 1);
    }
	
	// Update is called once per frame
	void Update () {
        if (PointB.position == this.transform.position)
        {
            this.transform.position = PointA.position;
            m_curPoint = 0;
        }
        //m_secondsBetween = Vector3.Distance(this.transform.position, Target.transform.position)*Scale;
        m_normalizedDistance = NormalizeDistance();
        m_secondsBetween = 1/Mathf.Exp(m_normalizedDistance * m_scale);
        //Debug.Log("normalized: " + NormalizeDistance());
        //Debug.Log("seconds b/w: " + m_secondsBetween);
        m_curPoint += Time.deltaTime * m_speed;
        this.transform.position = Vector3.Lerp(PointA.position, PointB.position, m_curPoint);
	}
}
