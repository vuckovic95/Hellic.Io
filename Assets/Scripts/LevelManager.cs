using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using NaughtyAttributes;

public class LevelManager : MonoBehaviour
{
    [BoxGroup("Level List")]public List<Level> levels = new List<Level>();
    [BoxGroup("Active Level")] public Level activeLevel;
    [BoxGroup("Holders")] public Transform platformsHolder;
    [BoxGroup("Holders")] public Transform boundariesHolder;
    [BoxGroup("Holders")] public Transform foodHolder;
    [BoxGroup("Holders")] public Transform obstacleHolder;
    [BoxGroup("Prefabs")] public GameObject platformPrefab;
    [BoxGroup("Prefabs")] public GameObject boundaryPrefab;
    [BoxGroup("Prefabs")] public GameObject foodPrefab;
    [BoxGroup("Prefabs")] public GameObject obstaclePrefab;

    [BoxGroup("Parameters")] public int heightAndWidth;
    [BoxGroup("Parameters")] public int foodPopulation;
    [BoxGroup("Parameters")] public int obstaclesPopulation;
    [BoxGroup("Parameters")] public Vector3 startPos;
    [BoxGroup("Parameters")] public int blockNum = 1;
    [BoxGroup("Parameters")] public float offset;

    [HideInInspector] public List<GameObject> platforms = new List<GameObject>();
    [HideInInspector] public List<GameObject> boundaries = new List<GameObject>();
    [HideInInspector] public List<GameObject> food = new List<GameObject>();
    [HideInInspector] public List<GameObject> obstacles = new List<GameObject>();
    [HideInInspector] public List<GameObject> foodCurrent = new List<GameObject>();

    private List<GameObject> currentPlatformList = new List<GameObject>();

    private void Awake()
    {
        GlobalManager.LevelManager = this;
        SpawnArena();
        PopulateFood();
        PopulateObstacles();
    }

    public void SpawnLevel()
    {
        //if (GlobalManager.SaveData.level > levels.Count)
        //{
        //    GlobalManager.SaveData.ResetLevels();
        //}
        //activeLevel = levels[GlobalManager.SaveData.level - 1];

        foreach(GameObject o in platforms)
        {
            currentPlatformList.Add(o);
        }
        SpawnObstacles();
        SpawnFood();
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
        foreach(GameObject o in obstacles)
        {
            int random = Random.Range(0, currentPlatformList.Count);

            o.SetActive(true);
            o.transform.position = new Vector3(currentPlatformList[random].transform.position.x,
                                              currentPlatformList[random].transform.position.y + 2,
                                              currentPlatformList[random].transform.position.z);
            currentPlatformList.RemoveAt(random);
        }
    }

    void SpawnFood()
    {
        foreach(Transform tr in foodHolder)
        {
            tr.gameObject.SetActive(true);
            foodCurrent.Add(tr.gameObject);

            int random = Random.Range(0, platforms.Count);
            float randomOffsetX = Random.Range(-1.5f, 1.5f);
            float randomOffsetZ = Random.Range(-1.5f, 1.5f);

            tr.position = new Vector3(platforms[random].transform.position.x + randomOffsetX,
                                      platforms[random].transform.position.y + 1.5f,
                                      platforms[random].transform.position.z + randomOffsetZ);
        }
    }

    void PopulateFood()
    {
        for(int i = 0; i < foodPopulation; i++)
        {
            GameObject _food = Instantiate(foodPrefab);
            food.Add(_food);
            _food.SetActive(false);
            _food.transform.parent = foodHolder.transform;
        }
    }

    void PopulateObstacles()
    {
        for (int i = 0; i < obstaclesPopulation; i++)
        {
            GameObject obst = Instantiate(obstaclePrefab);
            obstacles.Add(obst);
            obst.SetActive(false);
            obst.transform.parent = obstacleHolder.transform;
        }
    }

    public void ResetLevel()
    {       
        foreach(GameObject o in foodCurrent)
        {
            o.SetActive(false);
        }

        foreach(GameObject o in obstacles)
        {
            o.SetActive(false);
        }
        foodCurrent.Clear();
        currentPlatformList.Clear();
    }
}

[System.Serializable]
public class Level
{

}
