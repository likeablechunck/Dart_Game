using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Quit : MonoBehaviour {

    public Canvas quitMenu;
    //public Button startText;
    public Button exitText;

    // Use this for initialization
    void Start()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        quitMenu.enabled = false;
        //startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();

    }

    public void ExitPress()
    {
        quitMenu.enabled = true;
       // startText.enabled = false;
        exitText.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        //startText.enabled = true;
        exitText.enabled = true;

    }

    //When you click on "PLAY" I want to load the first Level
    public void StartLevel()
    {
        Application.LoadLevel("Dart_Game");
    }

    //If you click on "YES" in Exit menue, you will quit the game
    public void ExitGame()
    {
        Application.Quit();
    }
   
}
