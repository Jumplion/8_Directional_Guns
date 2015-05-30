using UnityEngine;
using System.Collections;

public class GlobalVars : MonoBehaviour
{

  public const int UPLEFT = 0,   UP = 1,   UPRIGHT = 2,
                      LEFT = 7,               RIGHT = 3,
                      DOWNLEFT = 6, DOWN = 5, DOWNRIGHT = 4;

  // Array for the built in vectors
  public Vector3[] vArray = { Vector3.up + Vector3.left,
                              Vector3.up,
                              Vector3.up + Vector3.right,
                              Vector3.right,
                              Vector3.down + Vector3.right,
                              Vector3.down,
                              Vector3.down + Vector3.left,
                              Vector3.left
                            };   
  public int[] angleArray = {45,0,315,270,225,180,135,90};    // Array for the angles cooridnate system
  
      //Angle array
    /*
    angleArray[UPLEFT]   = 45;      angleArray[UP] = 0;        angleArray[UPRIGHT] = 315;
    angleArray[LEFT]     = 90;                                   angleArray[RIGHT] = 270;
    angleArray[DOWNLEFT] = 135;   angleArray[DOWN] = 180;    angleArray[DOWNRIGHT] = 225;
     */
    //Vector3 Array holding combinations of the standard Vector3 directions
    
}
