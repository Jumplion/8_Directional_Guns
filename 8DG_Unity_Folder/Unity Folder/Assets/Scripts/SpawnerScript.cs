using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

  public float speed = 10.0f;
  public float lifetime = 10.0f;
  public float spawnTime = 0.25f;
  private float deltaSpawnTime;

  private const int UPLEFT = 0,   UP = 1,   UPRIGHT = 2,
                    LEFT = 7,               RIGHT = 3,
                    DOWNLEFT = 6, DOWN = 5, DOWNRIGHT = 4;

  public GameObject player;
  public GameObject[] enemyArray;
  public GameObject[] posArray = new GameObject[8];
  private Vector3[] vArray = // Array containing vector shorhands for UpLeft, clockwise to Left (0 - 7)
  { 
    Vector3.up + Vector3.left, Vector3.up, Vector3.up + Vector3.right, Vector3.right, 
    Vector3.down + Vector3.right, Vector3.down, Vector3.down + Vector3.left, Vector3.left
  };
  private int[] angleArray = { 45, 0, 315, 270, 225, 180, 135, 90 };    // Array for the angles cooridnate system

	// Use this for initialization
  void Awake()
  {
    deltaSpawnTime = spawnTime;
  }
  
  void Start ()
  {
	
	}
	
	// Update is called once per frame
	void Update ()
  {
    deltaSpawnTime -= Time.deltaTime;

    if (deltaSpawnTime < 0)
    {
      Vector3 pos = posArray[Random.Range(0,posArray.Length-1)].transform.position;
      GameObject enemy = (GameObject)Instantiate(enemyArray[0], pos, new Quaternion());

      enemy.GetComponent<Rigidbody>().AddForce((player.transform.position - enemy.transform.position) * speed);
      
      //enemy.GetComponent<Rigidbody>().AddForce(vArray[shootingAngleIndex] * speed);

      Destroy(enemy, lifetime);
      deltaSpawnTime = spawnTime;
    }
	}
}
