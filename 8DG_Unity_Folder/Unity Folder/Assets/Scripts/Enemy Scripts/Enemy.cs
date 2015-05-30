using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

  int health;
  float speed;

  public GameObject bullet;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnCollisionEnter(Collision col)
  {
    if (col.gameObject.tag == "Bullet")
    {
      Destroy(col.gameObject);
      Destroy(gameObject);
    }

    /*
    if(col.gameObject.Equals(bullet))
    {
      Destroy(col.gameObject);
      Destroy(this.gameObject);
    }
    */ 
        /*
    if (collider.gameObject.tag == "Enemy_01") {
			Destroy (collider.gameObject);
			Player_Controller.score++;	
		}

		else if (collider.gameObject.tag == "Enemy_02" && Enemy02_Controller.health <= 0) {
			Destroy (collider.gameObject);
			Player_Controller.score++;
		}
         */
  }
}
