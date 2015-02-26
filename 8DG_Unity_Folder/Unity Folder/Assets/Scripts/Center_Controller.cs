using UnityEngine;
using System.Collections;

public class Center_Controller : MonoBehaviour {

	public static int centerHealth = 25;

	void Start(){
		centerHealth = 25;
	}

	void Update(){
		if (centerHealth <= 0)
						Application.LoadLevel ("Game_Over");
	

	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Enemy_01" || collider.gameObject.tag == "Enemy_02") {
			Destroy (collider.gameObject);
			centerHealth--;
		}
	}
}
