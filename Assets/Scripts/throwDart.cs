using UnityEngine;
using System.Collections;

public class throwDart : MonoBehaviour
{

    private Ray ray;
    public GameObject dartPrefab;
    public float dartForce;

    // Use this for initialization
    void Start()
    {
        ray = new Ray();
    }

    // Update is called once per frame
    void Update()
    {

        // if game is over we should not throw the dart
        if (GameObject.Find("Board") != null)
        {
            if (GameObject.Find("Board").GetComponent<GameControl>().gameIsOver)
            {
                return;
            }
            else if (!GameObject.Find("Board").GetComponent<GameControl>().timeToRotate)
            {
                return;
            }
        }
        
        // if the board is not rotating ( for whatever reason) 
        // we should not rotate
        GameObject dart = GameObject.Find("DART");

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        bool hit = Physics.Raycast(ray, out hitData);

        //if the left mouse button is pressed do ....
        if (Input.GetMouseButtonDown(0))
        {
            print("user clicked the left mouse button again");
    
            if (hit == true)
            {
                //Debug.Log("The original point that was created by ray was:" + ray.origin);
                if (dart)
                {
                    print("ball is already instatiated");
                } else
                {
                    print(string.Format("Did I click? :{0}", hit));
                    // if there is a smashed object destroy that before creating a new dart
                    //Debug.DrawLine( ray.origin , hitData.point);
                    if (GameObject.Find("Board") != null)
                    {
                        if (!GameObject.Find("Board").GetComponent<GameControl>().gameIsOver && GameObject.Find("Board").GetComponent<GameControl>().timeToRotate)
                        {
                            GameObject newDart = Instantiate(dartPrefab, Camera.main.transform.position, Quaternion.identity) as GameObject;
                            newDart.name = ("DART");
                            print(string.Format("the object that was created is :{0}", newDart.name));
                            newDart.GetComponent<Rigidbody>().AddForce(ray.direction * dartForce);
                        } else
                        {
                            print("either Game is not over or time to rotate is false");
                        }
                    } else
                    {
                        print("Board IS NULL");
                    }
                }
                //Destroy(newDart, 3);
                //GameObject.Find("BALL").SetActive(false);
            }         
        }

    }
}
