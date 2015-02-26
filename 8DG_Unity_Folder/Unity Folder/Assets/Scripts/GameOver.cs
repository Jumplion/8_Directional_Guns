using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey ("r"))
						Application.LoadLevel ("Basic_Layout");

	}

	void OnGUI(){
		GUI.Box (new Rect (720, 455, 150, 60), "Game Over! \nPress R to Restart!");
	}

}
