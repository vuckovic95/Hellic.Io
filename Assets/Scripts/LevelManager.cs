using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using NaughtyAttributes;

public class LevelManager : MonoBehaviour
{
    [BoxGroup("Level List")]public List<Level> levels = new List<Level>();
    [BoxGroup("Active Level")] public Level activeLevel;

    [BoxGroup("Platforms Holder")] public Transform platformsHolder;

    [BoxGroup("Boundaries Holder")] public Transform boundariesHolder;

    [BoxGroup("Platform Prefab")] public GameObject platformPrefab;

    [BoxGroup("Boundary Prefab")] public GameObject boundaryPrefab;

    [BoxGroup("Parameters")] public int heightAndWidth;
    [BoxGroup("Parameters")] public Vector3 startPos;
    [BoxGroup("Parameters")] public int blockNum = 1;
    [BoxGroup("Parameters")] public float offset;

    [HideInInspector] public List<GameObject> platforms = new List<GameObject>();
    [HideInInspector] public List<GameObject> boundaries = new List<GameObject>();

    private void Awake()
    {
        GlobalManager.LevelManager = this;
        SpawnArena();
    }

    public void SpawnLevel()
    {
        if (GlobalManager.SaveData.level > levels.Count)
        {
            GlobalManager.SaveData.ResetLevels();
        }
        activeLevel = levels[GlobalManager.SaveData.level - 1];
    }

    void SpawnArena()
    {
        //Set Platforms
        int helper = -1;
        for(int i = 0; i < Mathf.Pow(heightAndWidth, 2) - 1; i++)
        {
            GameObject platform = Instantiate(platformPrefab);
            platform.transform.parent = platformsHolder.transform;
            platforms.Add(platform);
            helper++;

            if (i == 0)
            {
                platforms[i].transform.position = startPos;                
            }
            else
            {
                if(helper <= heightAndWidth)
                {
                    platforms[i].transform.position = new Vector3(platforms[i - 1].transform.position.x + offset, 
                                                                  platforms[i - 1].transform.position.y, 
                                                                  platforms[i - 1].transform.position.z);
                }
                else
                {
                    platforms[i].transform.position = new Vector3(startPos.x, 
                                                                  platforms[i - 1].transform.position.y, 
                                                                  platforms[i - 1].transform.position.z + offset);
                    helper = 0;
                }
            }
        }

        //Set Boundaries
        int counter = 0;
        for (int i = 0; i < heightAndWidth * 4 + 4; i++)
        {
            GameObject boundary = Instantiate(boundaryPrefab);
            boundary.transform.parent = boundariesHolder.transform;
            boundaries.Add(boundary);
            counter++;

            if (i == 0)
            {
                boundaries[i].transform.position = new Vector3(platforms[0].transform.position.x - offset,
                                                               platforms[0].transform.position.y, platforms[0].transform.position.z - offset);
            }
            else if (counter <= heightAndWidth + 3)
            {
                boundaries[i].transform.position = new Vector3(boundaries[i - 1].transform.position.x + offset,
                                                               boundaries[i - 1].transform.position.y,
                                                               boundaries[i - 1].transform.position.z);
            }
            else if (counter <= heightAndWidth * 2 + 3) 
            {
                boundaries[i].transform.position = new Vector3(boundaries[i - 1].transform.position.x,
                                                               boundaries[i - 1].transform.position.y,
                                                               boundaries[i - 1].transform.position.z + offset);
            }
            else if (counter <= heightAndWidth * 3 + 5)
            {
                boundaries[i].transform.position = new Vector3(boundaries[i - 1].transform.position.x - offset,
                                                               boundaries[i - 1].transform.position.y,
                                                               boundaries[i - 1].transform.position.z);
            }
            else
            {
                boundaries[i].transform.position = new Vector3(boundaries[i - 1].transform.position.x,
                                                               boundaries[i - 1].transform.position.y,
                                                               boundaries[i - 1].transform.position.z - offset);
            }
        }
    }

    void SpawnObstacles()
    {
        int randomObstacles = Random.Range(0, 6);
        
        for(int i = 0; i < randomObstacles; i++)
        {
            //todo
        }
    }
}

[System.Serializable]
public class Level
{

}
