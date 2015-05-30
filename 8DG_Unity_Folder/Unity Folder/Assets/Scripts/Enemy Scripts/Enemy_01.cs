using UnityEngine;
using System.Collections;

public class Enemy_01 : MonoBehaviour {


	// Use this for initialization
	void Start () 
  {
	}
	
	// Update is called once per frame
	void Update () 
  {
	
	}

  void OnTriggerEnter(Collider col)
  {
    if (col.gameObject.tag == "Bullet")
    {
      Destroy(col.gameObject);
      Destroy(gameObject);
    }
    else if (col.gameObject.tag == "Player")
    {
      Destroy(gameObject);
    }
  }
}
