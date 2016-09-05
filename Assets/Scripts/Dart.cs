using UnityEngine;
using System.Collections;

public class Dart : MonoBehaviour
{
    public GameObject smashedBullet;
    public bool ballHit;
    
    void start()
    {
        ballHit = false;
    }

    void OnCollisionEnter(Collision other)
    {
        //Tell the TestScript to stop rotating
        testScript ts = GameObject.Find("Sphere").GetComponent<testScript>();
        ts.stopRotation();

        //Tell the Timer script to stop counting
        Timer timer = GameObject.Find("Plane").GetComponent<Timer>();
        timer.stopTimer();
        ballHit = true;
        print("Ball hit the board");
        print(string.Format("Ball Hit situation is {0}:", ballHit));
        GameObject smashed = Instantiate(smashedBullet, transform.position, Quaternion.identity) as GameObject;
        GameControl gameControl = GameObject.Find("Board").GetComponent<GameControl>();
        Destroy(this.gameObject);
        smashed.name = "smashed";
        gameControl.collisionHappenedDoSth();
    }
}
