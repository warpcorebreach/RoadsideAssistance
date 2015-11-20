using UnityEngine;
using System.Collections;

public class CarPassingBy : MonoBehaviour {

    public GameObject Target;
    public float Scale;
    public Transform PointA;
    public Transform PointB;

    private AudioSource source;

    private float m_secondsBetween, m_speed, m_curPoint;

	// Use this for initialization
	void Start () {
        source = this.GetComponent<AudioSource>();
        StartCoroutine("LoopOnFrequency");
        m_speed = 0.1f;
        Scale = 1 / 30f;
        m_curPoint = 0.0f;
        this.transform.position = PointA.position;

	}

    IEnumerator LoopOnFrequency()
    {
        while(true)
        {
            source.Play();
            yield return new WaitForSeconds(source.clip.length + m_secondsBetween);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (PointB.position == this.transform.position)
        {
            this.transform.position = PointA.position;
            m_curPoint = 0;
        }
        m_secondsBetween = Vector3.Distance(this.transform.position, Target.transform.position)*Scale;
        m_curPoint += Time.deltaTime * m_speed;
        this.transform.position = Vector3.Lerp(PointA.position, PointB.position, m_curPoint);
	}
}
