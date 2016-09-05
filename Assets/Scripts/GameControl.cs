using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class GameControl : MonoBehaviour
{
    public int speed;
    public bool gameBegan;
    public bool timeToRotate;
    float rotationSpeed;
    Vector2 centerOfBoard;
    public int wrongAnswer;
    public int number;
    public TextMesh numberDisplay;
    public TextMesh gameOver;
    public TextMesh wrongShot;
    Vector3 testPoint;
    Vector3 newTestPoint;
    public bool gameIsOver = false;
    //public AudioSource audioSource;


    // Use this for initialization
    void Start()
    {
        speed = 15;
        gameBegan = false;
        rotationSpeed = 0;
        timeToRotate = true;
        Vector2 centerOfBoard = GameObject.Find("Board").transform.position;
        wrongAnswer = 0;
        gameOver.text = "";
        pickRandomNumber();
        

    }


    void pickRandomNumber()
    {
        number = Random.Range(1, 12);
        numberDisplay.text = number.ToString();
        numberDisplay.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Damn rotation situation @ UPDATE is : " + timeToRotate);
        if (timeToRotate)
        {
            Rotation();
        }
    }


    public void timeIsUp()
    {
        Stop();
        if (wrongAnswer >= 3)
        {
            Debug.Log("rotation situation if wrongAnswer >= 3 is : " + timeToRotate);
            gameOver.color = Color.magenta;
            gameOver.text = "Game Over!\n Press ESC to quit!";
            gameIsOver = true;
            resetAll();
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }
        else
        {
            Invoke("destroySmash", 4.0f);
            Invoke("continueTheGame", 5.0f);
        }

    }
    public void collisionHappenedDoSth()
    {
        Debug.Log("rotation situation @ collisionHappenedDoSth is : " + timeToRotate);
        Vector2 smashedPosition = GameObject.Find("smashed").transform.position;
        Debug.Log("Smashed position is at:" + smashedPosition);
        //when dart hits and creates the smashed Sprite, check the zone number
        Stop();
        GameObject.Find("Sphere").GetComponent<testScript>().stopRotation();
        detectZoneMatch();
        Timer timer = GameObject.Find("Plane").GetComponent<Timer>();
        if (wrongAnswer >= 3)
        {
            Debug.Log("rotation situation if wrongAnswer==3 is : " + timeToRotate);
            gameOver.color = Color.magenta;
            gameOver.text = "Game Over!\n Press ESC to quit!";
            gameIsOver = true;
            resetAll();
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        } else
        {
            Invoke("destroySmash", 4.0f);
            Invoke("continueTheGame", 5.0f);
        }
    }

    void destroySmash()
    {
        GameObject smashed = GameObject.Find("smashed");
        if (smashed)
        {
            Destroy(smashed);
        }
    }
    void continueTheGame()
    {
        resetRotation();
        timeToRotate = true;
        GameObject.Find("Plane").GetComponent<Timer>().timeToReset = true;
        number = Random.Range(1, 12);
        numberDisplay.text = number.ToString();
        numberDisplay.color = Color.red;
        GameObject.Find("Sphere").transform.position = new Vector2 (0,8);
        GameObject.Find("Sphere").GetComponent<testScript>().startRotation();
    }
    public void Rotation()
    {
        rotationSpeed = speed * (-1 * Time.deltaTime);
        transform.Rotate(0, 0, rotationSpeed, Space.Self);
    }

    void detectZoneMatch()
    {
        Debug.Log("rotation situation @ detectZoneMatch is : " + timeToRotate);
        testScript test = GameObject.Find("Sphere").GetComponent<testScript>();
        Vector3 testPosition = test.transform.position;
        float newAngle = angle(testPosition, Vector3.zero);
        float originalAngle = angle(test.originalPosition, Vector2.zero);
        float deltaAngle = newAngle - originalAngle;
        if (deltaAngle <0)
        {
            deltaAngle += 360;
        }

        Vector3 smashedPosition = GameObject.Find("smashed").transform.position;
        float smashedAngle = angle(smashedPosition, Vector3.zero);
        float getOriginalAngle = smashedAngle - deltaAngle;
        if (getOriginalAngle<0)
        {
            getOriginalAngle += 360;
        }
        int newZone = (int)(getOriginalAngle / 360 * 12) + 1;
        Debug.Log(string.Format("newAngle: {0} originalAngle: {1} deltaAngle: {2} smashedAngle: {3}",
            newAngle, originalAngle, deltaAngle, smashedAngle));
        Debug.Log("zone was:" + newZone);
        if ( newZone == number)
        {
            speed = speed +2 ;
            //audioSource.Play();
        }
        else
        {
            wrongAnswer++ ;
            wrongShot.text =("Wrong shots: " + wrongAnswer.ToString());
        }

    }

    float zone(Vector2 target, Vector2 center)
    {
        float angle = Mathf.Atan2(target.x - center.x, target.y - center.y) * 180 / Mathf.PI;
        if (angle < 0)
        {
            angle += 360;
        }
        return (int)(angle / 360 * 12);
    }

    public  void Stop()
    {
        Debug.Log("rotation situation @ STOP is : " + timeToRotate);
        timeToRotate = false;
        transform.Rotate(0, 0, 0, Space.Self);
    }

    float angle(Vector3 target, Vector3 center)
    {
        return Mathf.Atan2(target.x - center.x, target.y - center.y) * 180 / Mathf.PI;
    }

    public void resetRotation()
    {
        GameObject.Find("Board").transform.rotation = Quaternion.identity;
    }

    public void resetAll()
    {
        GameObject.Find("Board").transform.rotation = Quaternion.identity;
        wrongAnswer = 0;
        speed = 12;
    }

}
