using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

  float h;
  float v;
  float fireH;
  float fireV;
  bool switchWep;
  public bool canShoot;

  Player_Controller player;

	// Use this for initialization
	void Start ()
  {
    player = this.GetComponent<Player_Controller>();
	}
	
	// Update is called once per frame
	void Update ()
  {

    h = Input.GetAxisRaw("Horizontal");
    v = Input.GetAxisRaw("Vertical");

    fireH = Input.GetAxisRaw("FireHorz");
    fireV = Input.GetAxisRaw("FireVert");

    switchWep = Input.GetButtonUp("Jump");

    player.PlayerPosition(h, v);
    player.PlayerRotate(fireH, fireV);

    if(canShoot)
      if (fireH != 0 || fireV != 0)
        player.PlayerShoot();
    
    if(switchWep)
      player.SwitchWeapon();

	}
}
