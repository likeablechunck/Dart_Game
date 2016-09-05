using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float timeRemaining;
    public float minTime;
    public TextMesh timer;
    bool timeIsUp = false;
    public bool timeToReset = false;
    public bool timeToFreeze = false;

	// Use this for initialization
	void Start ()
    {
        timeRemaining = 6.00f;
        minTime = 0.0f;
        timer.text = "";
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timeToReset)
        {
            timeRemaining = 6;
            timeIsUp = false;
            timeToFreeze = false;
            timeToReset = false;
            return;
        }
        if (timeToFreeze)
        {
            return;
        }
        //Tell the TestScript to stop rotating if timer goes to zero
        testScript ts = GameObject.Find("Sphere").GetComponent<testScript>();
        //Tell the board in GameControl to stop rotating if timer goes to zero
        GameControl gc = GameObject.Find("Board").GetComponent<GameControl>();
        
        //print the time remaining with 2 digits only
        timer.text = timeRemaining.ToString( "f2");

        if( timeRemaining >= minTime)
        {
            startTimer();
        }
        else if ( timeRemaining <= 0 && !timeIsUp)
        {
            print("time is up");
            timeIsUp = true;
            gc.wrongAnswer += 1;
            gc.wrongShot.text = gc.wrongAnswer.ToString();
            gc.timeIsUp();
            ts.stopRotation();
        }
	}

    public void startTimer()
    {
        timeRemaining -= Time.deltaTime;
    }

    public void stopTimer()
    {
        timeToFreeze = true;
        Debug.Log("time that ball hit the target" + timeRemaining);
    }
}
