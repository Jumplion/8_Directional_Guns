using UnityEngine;
using System.Collections;

public class Secondary_Player_Controller : MonoBehaviour
{
  /*
	
    public static 	int superShotLimit = 3, score = 0, playerPosition, geminiPosition, angleFiring, playerWeapon = 0;


    // Use this for initialization
    void Awake () {
      player = GameObject.FindWithTag ("Player");
      geminiShip = GameObject.FindWithTag ("GeminiShip");
		
      bullet = GameObject.FindWithTag ("Bullet");
      deltaROF = bulletROF;
  }

    void Start()
    {
    }

    // Update is called once per frame
    void Update () {

      playerPosition = playerPosIndex;

      if (playerWeapon == 2) {
              geminiPosition = (playerPosition + 4) % 8;
              geminiShip.transform.position = posArray [geminiPosition].transform.position;
              GeminiShoot ();
          } else
              geminiShip.transform.position.Set (-1, 0, -45);
		

    }

    void GeminiShoot()
    {
      //angleFiring = Player_Controller.shootingAngleIndex-180;

      deltaROF -= Time.deltaTime;
		
      if (deltaROF <= 0.0f)
      {
        if (Input.GetKey ("down")){
          if(Input.GetKey ("right"))
          {
            bulletClone = (GameObject)Instantiate (bullet, Secondary_Player_Controller.geminiShip.transform.position, Quaternion.AngleAxis (angleUpLeft, Vector3.forward));
            bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (vUpLeft * bulletSpeed);
          }
          else if(Input.GetKey ("left"))
          {
            bulletClone = (GameObject)Instantiate (bullet, Secondary_Player_Controller.geminiShip.transform.position, Quaternion.AngleAxis (angleUpRight, Vector3.forward));
            bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (vUpRight * bulletSpeed);
          }
          else
          {	
            bulletClone = (GameObject)Instantiate (bullet, Secondary_Player_Controller.geminiShip.transform.position, Quaternion.AngleAxis (angleUp, Vector3.forward));
            bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (vUp * bulletSpeed);
          }
        }
        else if (Input.GetKey ("up"))
        {
          if(Input.GetKey ("right"))
          {
            bulletClone = (GameObject)Instantiate (bullet, Secondary_Player_Controller.geminiShip.transform.position, Quaternion.AngleAxis (angleDownLeft, Vector3.forward));
            bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (vDownLeft * bulletSpeed);
          }
          else if(Input.GetKey ("left"))
          {
            bulletClone = (GameObject)Instantiate (bullet, Secondary_Player_Controller.geminiShip.transform.position, Quaternion.AngleAxis (angleDownRight, Vector3.forward));
            bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (vDownRight * bulletSpeed);
          }
          else
          {		
            bulletClone = (GameObject)Instantiate (bullet, Secondary_Player_Controller.geminiShip.transform.position, Quaternion.AngleAxis (angleDown, Vector3.forward));
            bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (vDown * bulletSpeed);
          }
        }
        else if (Input.GetKey ("right"))
        {
          if(Input.GetKey ("down"))
          {
            bulletClone = (GameObject)Instantiate (bullet, Secondary_Player_Controller.geminiShip.transform.position, Quaternion.AngleAxis (angleUpLeft, Vector3.forward));
            bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (vUpLeft * bulletSpeed);
          }
          else if(Input.GetKey ("up"))
          {
            bulletClone = (GameObject)Instantiate (bullet, Secondary_Player_Controller.geminiShip.transform.position, Quaternion.AngleAxis (angleDownRight, Vector3.forward));
            bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (vDownLeft * bulletSpeed);
          }
          else
          {		
            bulletClone = (GameObject)Instantiate (bullet, Secondary_Player_Controller.geminiShip.transform.position, Quaternion.AngleAxis (angleLeft, Vector3.forward));
            bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (vLeft * bulletSpeed);
          }
        }
        else if (Input.GetKey ("left"))
        {
          if(Input.GetKey ("down"))
          {
            bulletClone = (GameObject)Instantiate (bullet, Secondary_Player_Controller.geminiShip.transform.position, Quaternion.AngleAxis (angleUpRight, Vector3.forward));
            bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (vUpRight * bulletSpeed);
          }
          else if(Input.GetKey ("up"))
          {
            bulletClone = (GameObject)Instantiate (bullet, Secondary_Player_Controller.geminiShip.transform.position, Quaternion.AngleAxis (angleDownRight, Vector3.forward));
            bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (vDownRight * bulletSpeed);
          }
          else
          {		
            bulletClone = (GameObject)Instantiate (bullet, Secondary_Player_Controller.geminiShip.transform.position, Quaternion.AngleAxis (angleRight, Vector3.forward));
            bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (vRight * bulletSpeed);
          }
        }

      Destroy (bulletClone, bulletLifetime);
		
      deltaROF = bulletROF;
    }
	
    }
    */

}

