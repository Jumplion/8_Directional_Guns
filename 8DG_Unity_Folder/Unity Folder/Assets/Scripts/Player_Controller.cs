using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour 
{
	
	/* Player, bullet game objects */
	public static GameObject player, geminiShip, bullet, bulletClone;
	Vector3 playerPos, geminiPos;
	/* Bullet Variables */
	public float bulletSpeed = 10.0f, bulletROF = 1.0f,	bulletLifetime = 2.0f;
	float deltaROF;

	public static 	int superShotLimit = 3, score = 0, playerPosition, shootingAngleIndex, playerWeapon = 0;


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

	void Awake() {
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
	
	void Start () {
		//Top-middle playerPosition is the default starting place.
		playerPos = posArray [up].transform.position;
		playerPosition = up;

		shootingAngleIndex = 180;
	}

	void Update () {
		PlayerPosition ();

		if (Input.GetKeyUp("space"))
			switchWeapon ();

		switch (playerWeapon) {
			case 0: defaultShoot ();  break;
			case 1: shotgunShoot ();  break;
			case 2:	geminiShoot  ();  break;
			default: defaultShoot (); break;
		}

		if (Input.GetKeyDown ("q"))
			if(superShotLimit > 0)
				SuperShot ();

	}

	void PlayerPosition(){
		//Checks if the player has pressed the w, s, a, or d keys and any two combinations.
		if (Input.GetKey ("w")) {
			if (Input.GetKey ("a"))
				player.transform.position = posArray [playerPosition = upLeft].transform.position;
			else if (Input.GetKey ("d"))
				player.transform.position = posArray [playerPosition = upRight].transform.position;
			else
				player.transform.position = posArray [playerPosition = up].transform.position;
		}
		if (Input.GetKey ("s")) {
			if (Input.GetKey ("a"))
				player.transform.position = posArray [playerPosition = downLeft].transform.position;
			else if (Input.GetKey("d"))
				player.transform.position = posArray [playerPosition = downRight].transform.position;
			else
				player.transform.position = posArray [playerPosition = down].transform.position;		
		}
		else if (Input.GetKey ("a")) {
			if (Input.GetKey ("w"))
				player.transform.position = posArray [playerPosition = upLeft].transform.position;
			else if (Input.GetKey ("s"))
				player.transform.position = posArray [playerPosition = downLeft].transform.position;
			else
				player.transform.position = posArray [playerPosition = left].transform.position;
		}
		else if (Input.GetKey ("d")) {
			if (Input.GetKey ("w"))
				player.transform.position = posArray [playerPosition = upRight].transform.position;
			else if (Input.GetKey ("s"))
				player.transform.position = posArray [playerPosition = downRight].transform.position;
			else
				player.transform.position = posArray [playerPosition = right].transform.position;
		}
	}

	void switchWeapon(){
	
		playerWeapon++;

		if (playerWeapon > 2)
			playerWeapon = 0;
	}


	void defaultShoot()
	{
		deltaROF -= Time.deltaTime;

		if (deltaROF <= 0.0f)
		{
			if (Input.GetKey ("up")){
				if(Input.GetKey ("left"))
				{
					bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleUpLeft, Vector3.forward));
					bulletClone.rigidbody.velocity = transform.TransformDirection (vUpLeft * bulletSpeed);
					shootingAngleIndex = angleUpLeft;
				}
				else if(Input.GetKey ("right"))
				{
					bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleUpRight, Vector3.forward));
					bulletClone.rigidbody.velocity = transform.TransformDirection (vUpRight * bulletSpeed);
					shootingAngleIndex = angleUpRight;
				}
				else
				{	
					bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleUp, Vector3.forward));
					bulletClone.rigidbody.velocity = transform.TransformDirection (vUp * bulletSpeed);
					shootingAngleIndex = angleUp;
				}
			}
			else if (Input.GetKey ("down"))
			{
				if(Input.GetKey ("left"))
				{
					bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleDownLeft, Vector3.forward));
					bulletClone.rigidbody.velocity = transform.TransformDirection (vDownLeft * bulletSpeed);
					shootingAngleIndex = angleDownLeft;
				}
				else if(Input.GetKey ("right"))
				{
					bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleDownRight, Vector3.forward));
					bulletClone.rigidbody.velocity = transform.TransformDirection (vDownRight * bulletSpeed);
					shootingAngleIndex = angleDownRight;
				}
				else
				{		
					bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleDown, Vector3.forward));
					bulletClone.rigidbody.velocity = transform.TransformDirection (vDown * bulletSpeed);
					shootingAngleIndex = angleDown;
				}
			}
			else if (Input.GetKey ("left"))
			{
				if(Input.GetKey ("up"))
				{
					bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleUpLeft, Vector3.forward));
					bulletClone.rigidbody.velocity = transform.TransformDirection (vUpLeft * bulletSpeed);
					shootingAngleIndex = angleUpLeft;
				}
				else if(Input.GetKey ("down"))
				{
					bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleDownRight, Vector3.forward));
					bulletClone.rigidbody.velocity = transform.TransformDirection (vDownLeft * bulletSpeed);
					shootingAngleIndex = angleDownLeft;
				}
				else
				{		
					bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleLeft, Vector3.forward));
					bulletClone.rigidbody.velocity = transform.TransformDirection (vLeft * bulletSpeed);
					shootingAngleIndex = angleLeft;
				}
			}
			else if (Input.GetKey ("right"))
			{
				if(Input.GetKey ("up"))
				{
					bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleUpRight, Vector3.forward));
					bulletClone.rigidbody.velocity = transform.TransformDirection (vUpRight * bulletSpeed);
					shootingAngleIndex = angleUpRight;
				}
				else if(Input.GetKey ("down"))
				{
					bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleDownRight, Vector3.forward));
					bulletClone.rigidbody.velocity = transform.TransformDirection (vDownRight * bulletSpeed);
					shootingAngleIndex = angleDownRight;
				}
				else
				{		
					bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleRight, Vector3.forward));
					bulletClone.rigidbody.velocity = transform.TransformDirection (vRight * bulletSpeed);
					shootingAngleIndex = angleRight;
				}
			}
			Destroy (bulletClone, bulletLifetime);
			deltaROF = bulletROF;
		}
	}

	void shotgunShoot(){
		deltaROF -= Time.deltaTime;

		if (deltaROF <= 0.0f) {
			if (Input.GetKey ("up")){
				if(Input.GetKey ("left")){
					for(int x=3; x<=5; x++){
						bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleArray[x], Vector3.forward));
						bulletClone.rigidbody.velocity = transform.TransformDirection (vArray[x] * bulletSpeed);
					}
				}
				else if(Input.GetKey ("right")){
					for(int x=5; x<=7; x++){
						bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleArray[x], Vector3.forward));
						bulletClone.rigidbody.velocity = transform.TransformDirection (vArray[x] * bulletSpeed);
					}
				}
				else{
					for(int x=4; x<=6; x++){
						bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleArray[x], Vector3.forward));
						bulletClone.rigidbody.velocity = transform.TransformDirection (vArray[x] * bulletSpeed);
					}
				}
			}
			else if (Input.GetKey ("down")){
				if(Input.GetKey ("left")){
					for(int x=1; x<=3; x++){
						bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleArray[x], Vector3.forward));
						bulletClone.rigidbody.velocity = transform.TransformDirection (vArray[x] * bulletSpeed);
					}
				}
				else if(Input.GetKey ("right")){
					for(int x=7; x<=9; x++){
						bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleArray[x%8], Vector3.forward));
						bulletClone.rigidbody.velocity = transform.TransformDirection (vArray[x%8] * bulletSpeed);
					}
				}
				else{	
					for(int x=0; x<=2; x++){
						bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleArray[x], Vector3.forward));
						bulletClone.rigidbody.velocity = transform.TransformDirection (vArray[x] * bulletSpeed);
					}
				}
			}
			else if (Input.GetKey ("left")){
				if(Input.GetKey ("up")){
					for(int x=3; x<=5; x++){
						bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleArray[x], Vector3.forward));
						bulletClone.rigidbody.velocity = transform.TransformDirection (vArray[x] * bulletSpeed);
					}
				}
				else if(Input.GetKey ("down")){
					for(int x=1; x<=3; x++){
						bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleArray[x], Vector3.forward));
						bulletClone.rigidbody.velocity = transform.TransformDirection (vArray[x] * bulletSpeed);
					}
				}
				else{		
					for(int x=2; x<=4; x++){
						bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleArray[x], Vector3.forward));
						bulletClone.rigidbody.velocity = transform.TransformDirection (vArray[x] * bulletSpeed);
					}
				}
			}
			else if (Input.GetKey ("right")){
				if(Input.GetKey ("up")){
					for(int x=5; x<=7; x++){
						bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleArray[x], Vector3.forward));
						bulletClone.rigidbody.velocity = transform.TransformDirection (vArray[x] * bulletSpeed);
					}
				}
				else if(Input.GetKey ("down")){
					for(int x=7; x<=9; x++){
						bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleArray[x%8], Vector3.forward));
						bulletClone.rigidbody.velocity = transform.TransformDirection (vArray[x%8] * bulletSpeed);
					}
				}
				else{	
					for(int x=6; x<=8; x++){
						bulletClone = (GameObject)Instantiate (bullet, Player_Controller.player.transform.position, Quaternion.AngleAxis (angleArray[x%8], Vector3.forward));
						bulletClone.rigidbody.velocity = transform.TransformDirection (vArray[x%8] * bulletSpeed);
					}
				}
			}
			Destroy (bulletClone, bulletLifetime);
			deltaROF = bulletROF;
		}
	}

	void geminiShoot()
	{
		defaultShoot ();
	}

/* Super Shot Code */
void SuperShot(){
		for(int x=0; x<8; x++)
		{
			for(int y=0; y<8; y++)
			{
				bulletClone = (GameObject) Instantiate (bullet, posArray[x].transform.position, Quaternion.AngleAxis (angleArray[y], Vector3.forward));
				bulletClone.rigidbody.velocity = transform.TransformDirection (vArray[y] * bulletSpeed);
				Destroy (bulletClone, bulletLifetime);
			}
		}
		superShotLimit --;
	}
	

}

