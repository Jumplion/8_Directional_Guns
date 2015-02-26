﻿using UnityEngine;
using System.Collections;

public class Enemy02_Controller : MonoBehaviour {

	public static int health;

	// Use this for initialization
	void Start () {
		health = GameState.enemy_02_health;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Bullet") {
				Destroy (collider.gameObject);
			if(health !=0){
				health--;
			}

		}
	}
}

