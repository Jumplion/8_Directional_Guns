using UnityEngine;
using System.Collections;

public class Bullet_Controller : MonoBehaviour {

  //public static Rigidbody rb;
  //private Player_Controller player;

  void Start()
  {
   // rb = this.GetComponent<Rigidbody>();
   // player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();

  }

  void ApplyForce(Vector3 v, float speed)
  {
   // rb.AddForce(v * speed);
  }

	void OnTriggerEnter(Collider collider)
  {

  }
}
