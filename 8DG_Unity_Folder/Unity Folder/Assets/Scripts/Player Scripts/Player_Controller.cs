using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour
{
  #region Variables
  /* Player, bullet game objects */
  public GameObject bullet, geminiShip;
  private GameObject bulletClone;

  #region Player Variables
  public int health = 10;
  public float playerSpeed = 10;        // Default. How fast the player switches between positions
  [Range(0, 2)] public float            // Percentage of playerSpeed to apply when using Shotgun
    playerSpeed_shotgun = 0.8f; 
  private float deltaPlayerSpeed;

  public float turnRate = 0.075f;       // Default. How fast the player turns between angles
  [Range(0, 2)] public float            // Percentage of turnRate to apply when using Shotgun
    turnRate_shotgun = 0.8f; 
  private float deltaTurnRate;
  public float knockBack;               // How hard the bullets knock the player back
  #endregion

  #region Bullet Variables
  /* Bullet Variables */
	public float bulletSpeed = 10.0f;       // Speed the bullet will go
  public float bulletLifetime = 2.0f;     // How long a bullet will live when it doesn't hit anything
  public float bulletROF_default = 1.0f;  // Default. Rate of fire for shooting bullets
  [Range(0,2)] public float 
    bulletROF_shotgun = 0.8f;             // RoF for shotgun shooting
  private float deltaROF;                 // Private change in ROF
  private float bulletROF;
  #endregion

  protected int playerPosIndex;           // Position index. Upper Left starts at 0, goes clockwise up to 7
  protected int shootingAngleIndex;       // Index for shooting angle. Upper Left starts at 0, goes clockwise up to 7
  protected int playerWeapon = 0;         // Determines what weapon the player is currently using


  private const int UPLEFT = 0,    UP = 1,  UPRIGHT = 2,
                    LEFT = 7,               RIGHT = 3,
                    DOWNLEFT = 6, DOWN = 5, DOWNRIGHT = 4;

  #region Various Arrays
  /* Various Arrays */
  private Vector3[] vArray = // Array containing vector shorhands for UpLeft, clockwise to Left (0 - 7)
  { 
    Vector3.up + Vector3.left, Vector3.up, Vector3.up + Vector3.right, Vector3.right, 
    Vector3.down + Vector3.right, Vector3.down, Vector3.down + Vector3.left, Vector3.left
  };

  private int[] angleArray = { 45, 0, 315, 270, 225, 180, 135, 90};    // Array for the angles cooridnate system

  Vector3 playerPos, geminiPos;
  Quaternion playerRot;
  public GameObject[] posArray = new GameObject[8];     // Array for player positionings
  public static int[] 	  weaponArray = new int[8];     // Array for weapons
  #endregion
  private Rigidbody rb;
  #endregion
	
  void Awake() {
    rb = this.GetComponent<Rigidbody>();

		for (int y=0; y<8; y++)
			weaponArray [y] = y;

    deltaPlayerSpeed = playerSpeed;
    deltaTurnRate = turnRate;
    bulletROF = bulletROF_default;

    deltaROF = bulletROF;

    //Top-middle playerPosition is the default starting place.
    playerPos = posArray[playerPosIndex = UP].transform.position;
    playerRot = transform.rotation;
    shootingAngleIndex = 1;
	}
	
	void Start () {

	}

	void FixedUpdate ()
  {
    if (health <= 0)
    {
      Application.LoadLevel("Game_Over");
    }

    playerPos = transform.position;
    playerRot = transform.rotation;

	}

  void OnGUI()
  {
    GUI.color = Color.red;
    GUI.Label(new Rect(25, 10, 400, 25), "Current Position: " + playerPosIndex);
    GUI.Label(new Rect(25, 20, 400, 25), "Current Aiming Angle: " + angleArray[shootingAngleIndex]);
    GUI.Label(new Rect(25, 30, 400, 25), "Shooting Angle Index: " + shootingAngleIndex);
    GUI.Label(new Rect(25, 40, 400, 25), "Health: " + health);
    GUI.Label(new Rect(25, 50, 400, 25), "" + (shootingAngleIndex-1) % 8);

  }

  #region Player Movement
  public void PlayerPosition(float h, float v)
  {
    //Checks if the player has pressed the WASD keys and any two combinations.
    // Up
    if (v == 1)
    {
      // Right
      if (h == 1)
        playerPosIndex = UPRIGHT;
      // Left
      else if (h == -1)
        playerPosIndex = UPLEFT;
      // Pure
      else
        playerPosIndex = UP;
    }

    // Down
    else if (v == -1)
    {
      // Right
      if (h == 1)
        playerPosIndex = DOWNRIGHT;
      // Left
      else if (h == -1)
        playerPosIndex = DOWNLEFT;
      // Pure
      else
        playerPosIndex = DOWN;
    }

    // Pure Left or Right
    else
    {
      // Right
      if (h == 1)
        playerPosIndex = RIGHT;
      // Left
      else if (h == -1)
        playerPosIndex = LEFT;
    }

    // Lerp between position and destination
    //transform.position = Vector3.Lerp(transform.position, posArray[playerPosIndex], Time.deltaTime * playerSpeed);

    rb.AddForce((posArray[playerPosIndex].transform.position - playerPos) * deltaPlayerSpeed);

  }

  public void PlayerRotate(float h, float v)
  {
    // Up
    if (v == 1)
    {
      // Right
      if (h == 1)
        shootingAngleIndex = UPRIGHT;
      // Left
      else if (h == -1)
        shootingAngleIndex = UPLEFT;
      // Pure
      else
        shootingAngleIndex = UP;
    }
    // Down
    else if (v == -1)
    {
      // Right
      if (h == 1)
        shootingAngleIndex = DOWNRIGHT;
      // Left
      else if (h == -1)
        shootingAngleIndex = DOWNLEFT;
      // Pure
      else
        shootingAngleIndex = DOWN;
    }
    // Pure
    else
    {
      // Right
      if (h == 1)
        shootingAngleIndex = RIGHT;
      // Left
      else if (h == -1)
        shootingAngleIndex = LEFT;
    }

    Quaternion rotateTo = Quaternion.AngleAxis(angleArray[shootingAngleIndex], Vector3.forward);
    transform.rotation = Quaternion.Lerp(playerRot, rotateTo, deltaTurnRate);

    //rb.AddTorque();
    //rb.AddForce((posArray[playerPosIndex].transform.position - playerPos) * playerSpeed);
  }
  #endregion

  #region Shooting Functions
  public void PlayerShoot()
  {
    switch (playerWeapon)
    {
      case 1: ShotgunShoot();   break;
      case 2: GeminiShoot();    break;
      default: DefaultShoot();  break;
    }

  }

	public void SwitchWeapon(){
	
		playerWeapon++;
    if (playerWeapon > 2)
      playerWeapon = 0;

    switch (playerWeapon)
    {
      case 1: 
        deltaTurnRate = turnRate * turnRate_shotgun;
        deltaPlayerSpeed = playerSpeed * playerSpeed_shotgun;
        bulletROF = bulletROF_default * bulletROF_shotgun;
        break;

      default: 
        deltaTurnRate = turnRate;
        deltaPlayerSpeed = playerSpeed;
        deltaROF = bulletROF_default;
        break;
    }
	}

  void DefaultShoot()
  {
    deltaROF -= Time.deltaTime;

    if (deltaROF <= 0.0f)
    {

      bulletClone = (GameObject)Instantiate(bullet, playerPos, playerRot);
      Destroy(bulletClone, bulletLifetime);

      rb.AddForce(vArray[GetOppositeDirection(shootingAngleIndex)] * knockBack);

      deltaROF = bulletROF;
    }
  }

	void ShotgunShoot()
  {
		deltaROF -= Time.deltaTime;

    if (deltaROF <= 0.0f)
    {
      DefaultShoot();

      int leftBullet = (shootingAngleIndex + 8) % 8;
      int rightBullet = (shootingAngleIndex + 1) % 8;

      Debug.Log(leftBullet);

      Quaternion quat = Quaternion.AngleAxis(angleArray[rightBullet], Vector3.forward);

      bulletClone = (GameObject)Instantiate(bullet, playerPos, quat);
      Destroy(bulletClone, bulletLifetime);


      quat = Quaternion.AngleAxis(angleArray[leftBullet], Vector3.forward);

      bulletClone = (GameObject)Instantiate(bullet, playerPos, quat);
      Destroy(bulletClone, bulletLifetime);

      deltaROF = bulletROF;
    } 
  }

	void GeminiShoot()
	{
		//defaultShoot ();
	}

  void SuperShot(){
    /*
    for(int x=0; x<8; x++)
    {
      for(int y=0; y<8; y++)
      {
        bulletClone = (GameObject)Instantiate(bullet, playerPos, quat);
        bulletClone.GetComponent<Rigidbody>().AddForce(vArray[(shootingAngleIndex + 1) % 8] * bulletSpeed);
        Destroy(bulletClone, bulletLifetime);
      }
    }
    superShotLimit --;
     */
  }
  #endregion

  private int GetOppositeDirection(int dir)
  {
    return (dir + (vArray.Length / 2)) % 8;
  }

  void OnTriggerEnter(Collider col)
  {
    if (col.gameObject.tag == "Enemy")
    {
      health -= 1;
    }
  }
}

