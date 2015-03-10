using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public static GameObject enemy_01, enemy_02, enemyClone;

	public static int enemy_01_health = 1, enemy_02_health = 2, enemySpawn = 0;

	int angleUpRight = 315,		angleUp = 0,		angleUpLeft = 45,
		angleLeft = 90,								angleRight = 270,
		angleDownLeft = 135, 	angleDown = 180,	angleDownRight = 225;

	public float enemySpeed = 10.0f, enemyLifetime = 10.0f, enemySpawnRate = 1.0f, spawn = 0.0f, random, random2;
	
	Vector3 vUpLeft = Vector3.up+Vector3.left,		vUp = Vector3.up, 		vUpRight = Vector3.up+Vector3.right,	
			vLeft = Vector3.left, 											vRight = Vector3.right,			
			vDownLeft = Vector3.down+Vector3.left,	vDown = Vector3.down,	vDownRight = Vector3.down+Vector3.right;

	public static GameObject[] enemPosArray = new GameObject[8];
	public static int[] enemAngleArray = new int[8];
	public static Vector3[] enemVArray = new Vector3[8];

	void Awake(){
		enemy_01 = GameObject.FindWithTag ("Enemy_01");
			enemy_01_health = 1;
		enemy_02 = GameObject.FindWithTag ("Enemy_02");
			enemy_02_health = 2;
		
		for (int x=0; x<8; x++)	//Initializes player position Array
			enemPosArray [x] = GameObject.FindGameObjectWithTag ("E" + (x+1));
		
		//Enemy Angles
		enemAngleArray [4] = angleUpLeft;		enemAngleArray [5] = angleUp;		enemAngleArray [6] = angleUpRight;
		enemAngleArray [3] = angleLeft;												enemAngleArray [7] = angleRight;
		enemAngleArray [2] = angleDownLeft;		enemAngleArray [1] = angleDown;		enemAngleArray [0] = angleDownRight;
		
		//Enemy Directions
		enemVArray [4] = vUpLeft;		enemVArray [5] = vUp;		enemVArray [6] = vUpRight;
		enemVArray [3] = vLeft;										enemVArray [7] = vRight;
		enemVArray [2] = vDownLeft;		enemVArray [1] = vDown;		enemVArray [0] = vDownRight;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		random2 = Random.Range (1, 50);
		spawn += Time.deltaTime*random2;
		enemySpawn = (int)Random.Range (0, 7);

		if(spawn >= enemySpawnRate)
			SpawnEnemy ();
	}

	void SpawnEnemy(){
		
		random = Random.Range (0, 100);

		if (random < 75) {
			enemyClone = (GameObject)Instantiate (enemy_01, enemPosArray [enemySpawn].transform.position, Quaternion.identity);
			enemyClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (enemVArray [enemySpawn] * enemySpeed);
			Destroy (enemyClone, enemyLifetime);
		}
		else if (random >= 75) {
			enemyClone = (GameObject)Instantiate (enemy_02, enemPosArray [enemySpawn].transform.position, Quaternion.identity);
			enemyClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection (enemVArray [enemySpawn] * enemySpeed);
			Destroy (enemyClone, enemyLifetime);
		}

		spawn = 0;
	}

}
