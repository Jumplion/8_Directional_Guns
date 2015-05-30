using UnityEngine;
using System.Collections;

public class Main_Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    if(Input.GetKey("space"))
      Application.LoadLevel("Basic_Layout");
	}

  void OnGUI()
  {
    GUI.Box(new Rect(Screen.width/4, Screen.height/4, Screen.width/4, Screen.height/4), "WASD to move \nArrow keys to shoot \nSpace to switch weapons \nAvoid the enemies as best you can \nPress 'space' to start!");

  }
}
