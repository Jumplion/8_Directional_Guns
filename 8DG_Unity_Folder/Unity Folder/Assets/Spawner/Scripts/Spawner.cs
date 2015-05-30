///////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Filename: Spawner.cs
//  
// Author: Garth de Wet <garthofhearts@gmail.com>
// Website: http://corruptedsmilestudio.blogspot.com/
// Date Modified: 15 November 2013
//
// Copyright (c) 2013 Garth de Wet
///////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;
using CorruptedSmileStudio.Spawn;


/// Spawns prefabs, either in waves, at once or continually till all enemies are spawned.

/// <description>
/// Controls the spawning of selected perfabs, useful for making enemy spawn points.<br />
/// It supports a variety of spawn modes, which allows you to bend the system to fit your needs.<br />
/// This class is required for the system to work, you will need to place this class on a GameObject with
/// a tag of Spawner (However, this is changeable within the SpawnAI class).
/// </description>
public class Spawner : MonoBehaviour
{
    #region Variables
    
	public UnitLevels unitLevel = UnitLevels.Easy;		/// The unit level to spawn.  
	public GameObject[] unitList = new GameObject[4];   /// The list of enemies, do not go over 4.
	public SpawnModes spawnType = SpawnModes.Normal;	/// The type of spawning.
	public Transform spawnLocation;	/// The location of where to spawn units.

	public int totalUnits = 10;	/// The total number of enemies to spawn on each spawn session.
	public int spawnID = 0;		/// The ID of the spawner.
	public int totalWaves = 5;	/// The total number of waves to spawn.
	private int numberOfUnits = 0;	/// The current number of spawned enemies. 
	private int totalSpawnedUnits = 0;	/// The total number of spawned enemies. Not just alive.
	private int numWaves = 0;	/// The number of waves that has spawned.

	public bool spawn = true;	/// Determines whether the spawn should spawn.
	private bool waveSpawn = true;	/// Used to determine if there is actual spawning to occur.
	private bool checkEnemyLevel = false;	/// Used within the TimeSplitWave.

	public float waveTimer = 30.0f;	/// The time between each wave when spawn type is set to wave.
	public float timeBetweenSpawns = 0.5f;	/// The time between each spawn of a unit
	private float lastWaveSpawnTime = 0.0f;	/// The time the last wave was spawned
    
    #endregion


    void Start()
    {
        if (spawnLocation == null)
        {
            spawnLocation = transform;
        }
        InstanceManager.ReadyPreSpawn(unitList[(int)unitLevel].transform, totalUnits);
        StartCoroutine("DoSpawn");
    }
    
    /// Spawns a unit based on the level set.
    
    private void SpawnUnit()
    {
        if (unitList[(int)unitLevel] != null)
        {
            Transform unit = InstanceManager.Spawn(unitList[(int)unitLevel].transform, spawnLocation.position, Quaternion.identity);
			unit.GetComponent<SpawnAI>().SetOwner(this);
            // Increase the total number of enemies spawned and the number of spawned enemies
            numberOfUnits++;
            totalSpawnedUnits++;
        }
        else
        {
            Debug.LogError("Error trying to spawn unit of level " + unitLevel.ToString() + " on spawner " + spawnID + " - No unit set");
            spawn = false;
        }
    }
    
    /// This removes an "unit" in order to allow waves and such that depend on the number of enemies decreasing
    /// to properly start a new spawn.
    
    /// <param name="sID">The spawner ID that created the unit.</param>
    public void RemoveUnit(int sID)
    {
        // if the unit's spawnID is equal to this spawnersID then remove an unit count
        if (spawnID == sID)
        {
            numberOfUnits--;
        }
    }
	
	/// This removes an "unit" in order to allow waves and such that depend on the number of enemies decreasing
	/// to properly start a new spawn.
	
	public void RemoveUnit()
	{
		numberOfUnits--;
	}
    
    /// Enable the spawner by ID
    
    /// <param name="sID">The spawner's ID</param>
    public void EnableSpawner(int sID)
    {
        if (spawnID == sID)
        {
            spawn = true;
            InstanceManager.ReadyPreSpawn(unitList[(int)unitLevel].transform, totalUnits);
            StartCoroutine("DoSpawn");
        }
    }
    
    /// Disable the spawner by ID
    
    /// <param name="sID">The spawner's ID</param>
    public void DisableSpawner(int sID)
    {
        if (spawnID == sID)
        {
            spawn = false;
            StopCoroutine("DoSpawn");
        }
    }
    
    /// The time till the next wave
    
    public float TimeTillWave
    {
        get
        {
            if (numWaves >= totalWaves)
            {
                return 0;
            }
            if (spawnType == SpawnModes.TimeSplitWave && waveSpawn || numberOfUnits > 0)
            {
                return 0;
            }

            float time = (lastWaveSpawnTime + waveTimer) - Time.time;
            if (time >= 0)
                return time;
            else
                return 0;
        }
    }
    
    /// Enables the spawner. Useful for triggers.
    
    public void EnableViaTrigger()
    {
        spawn = true;
        StartCoroutine("DoSpawn");
    }
    
    /// Draws an icon to show where the spawn point is. Useful if you don't have a object that show the spawn point
    
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "SpawnIcon.psd");
    }
    
    /// Resets all the private variables to their original state.
    
    public void Reset()
    {
        waveSpawn = false;
        checkEnemyLevel = true;
        numWaves = 0;
        totalSpawnedUnits = 0;
        lastWaveSpawnTime = Time.time;
    }
    
    /// Returns the number of waves left
    
    public int WavesLeft
    {
        get
        {
            return totalWaves - numWaves;
        }
    }
    
    /// Controls the spawning of units and so forth.
    
    /// <returns></returns>
    private System.Collections.IEnumerator DoSpawn()
    {
        while (spawn)
        {
            switch (spawnType)
            {
                case SpawnModes.Normal:
                    if (numberOfUnits < totalUnits)
                    {
                        yield return new WaitForSeconds(timeBetweenSpawns);
                        SpawnUnit();
                    }
                    break;
                case SpawnModes.Once:
                    if (totalSpawnedUnits >= totalUnits)
                    {
                        spawn = false;
                    }
                    else
                    {
                        yield return new WaitForSeconds(timeBetweenSpawns);
                        SpawnUnit();
                    }
                    break;
                case SpawnModes.Wave:
                    if (numWaves <= totalWaves)
                    {
                        if (waveSpawn)
                        {
                            yield return new WaitForSeconds(timeBetweenSpawns);
                            SpawnUnit();
                            checkEnemyLevel = true;

                            if ((totalSpawnedUnits / (numWaves + 1)) == totalUnits)
                            {
                                waveSpawn = false;
                            }
                        }
                        if (checkEnemyLevel)
                        {
                            if (numberOfUnits <= 0)
                            {
                                checkEnemyLevel = false;
                                waveSpawn = true;
                                numberOfUnits = 0;
                                numWaves++;
                            }
                        }
                    }
                    else
                    {
                        spawn = false;
                    }
                    break;
                case SpawnModes.TimedWave:
                    if (numWaves <= totalWaves)
                    {
                        if (waveSpawn)
                        {
                            yield return new WaitForSeconds(timeBetweenSpawns);
                            SpawnUnit();

                            if ((totalSpawnedUnits / (numWaves + 1)) == totalUnits)
                            {
                                waveSpawn = false;
                            }
                        }
                        else
                        {
                            waveSpawn = true;
                            numWaves++;
                            // A hack to spawn even if there are unit left alive.
                            numberOfUnits = 0;
                            lastWaveSpawnTime = Time.time;
                            yield return new WaitForSeconds(waveTimer);
                        }
                    }
                    else
                    {
                        spawn = false;
                    }
                    break;
                case SpawnModes.TimeSplitWave:
                    if (numWaves <= totalWaves)
                    {
                        if (waveSpawn)
                        {
                            yield return new WaitForSeconds(timeBetweenSpawns);
                            SpawnUnit();
                            if ((totalSpawnedUnits / (numWaves + 1)) == totalUnits)
                            {
                                waveSpawn = false;
                                checkEnemyLevel = true;
                            }
                        }
                        else
                        {
                            if (checkEnemyLevel)
                            {
                                if (numberOfUnits <= 0)
                                {
                                    numWaves++;
                                    checkEnemyLevel = false;
                                    numberOfUnits = 0;
                                    lastWaveSpawnTime = Time.time;
                                    yield return new WaitForSeconds(waveTimer);
                                    waveSpawn = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        spawn = false;
                    }
                    break;
                default:
                    spawn = false;
                    break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    
    /// Starts spawning, if you want to interact with a spawner from code.
    
    public void StartSpawn()
    {
        EnableViaTrigger();
    }
}