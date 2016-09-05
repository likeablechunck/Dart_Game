using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour
{
    int speed;
    bool checkRotation;
    public Vector2 originalPosition;


	// Use this for initialization
	void Start ()
    {
        originalPosition = new Vector2(0, 8);
        GameControl gc = GameObject.Find("Board").GetComponent<GameControl>();
        speed = gc.speed;
        checkRotation = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameControl gc = GameObject.Find("Board").GetComponent<GameControl>();
        speed = gc.speed;
        if (checkRotation)
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, speed * (-1 * Time.deltaTime));

        }   
	}
    public void stopRotation()
    {
        checkRotation = false;
        
    }

    public void startRotation()
    {
        checkRotation = true;
    }
}
