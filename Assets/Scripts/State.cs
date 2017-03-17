using UnityEngine;
using System.Collections;

public class State : MonoBehaviour {

    //Estados
    public bool Stunned;
    public bool Silenced;
    public float Slowdown=0.0f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Stun(bool stunned,float durationTime)
    {
        Stunned = stunned;
        StartCoroutine(TimeStun(durationTime));
    }
    public void Silence(bool silenced, float durationTime)
    {
        Silenced = silenced;
        StartCoroutine(TimeSilence(durationTime));
    }
    public void Slow(float slowdown,float durationTime)
    {
        Slowdown = slowdown;
        StartCoroutine(TimeSlow(durationTime));
    }
    public IEnumerator TimeStun(float time)
    {
        yield return new WaitForSeconds(time);
        Stunned = false;
    }
    public IEnumerator TimeSilence(float time)
    {
        yield return new WaitForSeconds(time);
        Silenced = false;
    }
    public IEnumerator TimeSlow(float time)
    {
        yield return new WaitForSeconds(time);
        Slowdown = 0.0f;
    }
}
