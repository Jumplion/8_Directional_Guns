using UnityEngine;
using System.Collections;

public class Secondary_Player_Controller : MonoBehaviour 
{

	
	/* Player, bullet game objects */
	public static GameObject player, geminiShip, bullet, bulletClone;
	Vector3 playerPos, geminiPos;
	/* Bullet Variables */
	public float bulletSpeed = 10.0f, bulletROF = 1.0f,	bulletLifetime = 2.0f;
	float deltaROF;
	
	public static 	int superShotLimit = 3, score = 0, playerPosition, geminiPosition, angleFiring, playerWeapon = 0;

	/***************************************************** Positionings *****************************************************/
	int upLeft 	 = 0,	 up  = 1, 	upRight 	= 2,
		left 	 = 7, 				right 		= 3,
		downLeft = 6,	down = 5,	downRight 	= 4;
	/* Angles of entrance */
	int angleUpRight 	= 315, 	  angleUp = 0,		angleUpLeft 	= 45,
		angleLeft 		= 90,						angleRight 		= 270,
		angleDownLeft 	= 135, 	angleDown = 180,	angleDownRight 	= 225;
	/* Vector Positionings */
	Vector3 vUpLeft 	= Vector3.up+Vector3.left,		  vUp = Vector3.up, 	vUpRight 	= Vector3.up+Vector3.right,	
			vLeft 		= Vector3.left, 										vRight 		= Vector3.right,			
			vDownLeft 	= Vector3.down+Vector3.left,	vDown = Vector3.down,	vDownRight 	= Vector3.down+Vector3.right;
	public static GameObject[] posArray = new GameObject[8];
	public static Vector3[] 	vArray  = new Vector3[8];
	public static int[] 	weaponArray = new int[8];
	public static int[] 	angleArray 	= new int[8];
	/***************************************************** Positionings *****************************************************/

	// Use this for initialization
	void Awake () {
		player = GameObject.FindWithTag ("Player");
		geminiShip = GameObject.FindWithTag ("GeminiShip");
		
		//playerPos = player.transform.position;
		//geminiPos = geminiShip.transform.position;
		
		for (int x=0; x<8; x++)	//Initializes player position Array
			posArray [x] = GameObject.FindGameObjectWithTag ("P" + (x+1));
		
		for (int y=0; y<8; y++)
			weaponArray [y] = y;
		
		//Enemy Angles
		angleArray [4] = angleUpLeft;		angleArray [5] = angleUp;		angleArray [6] = angleUpRight;
		angleArray [3] = angleLeft;											angleArray [7] = angleRight;
		angleArray [2] = angleDownLeft;		angleArray [1] = angleDown;		angleArray [0] = angleDownRight;
		
		//Enemy Directions
		vArray [4] = vUpLeft;		vArray [5] = vUp;		vArray [6] = vUpRight;
		vArray [3] = vLeft;									vArray [7] = vRight;
		vArray [2] = vDownLeft;		vArray [1] = vDown;		vArray [0] = vDownRight;
		
		
		bullet = GameObject.FindWithTag ("Bullet");
		deltaROF = bulletROF;
}

	void Start()
	{
	}

	// Update is called once per frame
	void Update () {

		playerPosition = Player_Controller.playerPosition;

		if (Player_Controller.playerWeapon == 2) {
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

}
