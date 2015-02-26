using UnityEngine;
using System.Collections;

public class Bullet_Controller : MonoBehaviour {

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Enemy_01") {
			Destroy (collider.gameObject);
			Player_Controller.score++;	
		}

		else if (collider.gameObject.tag == "Enemy_02" && Enemy02_Controller.health <= 0) {
			Destroy (collider.gameObject);
			Player_Controller.score++;
		}
	}
}
