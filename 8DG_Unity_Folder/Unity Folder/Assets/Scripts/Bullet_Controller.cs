using UnityEngine;
using System.Collections;

public class Bullet_Controller : MonoBehaviour {

  public float speed;

  void Start()
  {

  }

  void Update()
  {
    transform.Translate(Vector3.up * speed * Time.deltaTime);
  }

	void OnTriggerEnter(Collider collider)
  {

  }
}
