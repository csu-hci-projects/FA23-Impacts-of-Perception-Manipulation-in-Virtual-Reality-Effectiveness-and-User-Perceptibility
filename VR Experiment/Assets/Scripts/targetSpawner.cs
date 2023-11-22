using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    float respawnTime = 2f;
    float currRespawnTime;
    bool spawning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawning)
        {
            currRespawnTime -= Time.deltaTime;
            if(currRespawnTime <= 0f)
            {
                spawnTarget();
                spawning = false;
            }
        }
    }

    void spawnTarget()
    {
        GameObject newTarget = Instantiate(targetPrefab);
        newTarget.transform.position = transform.position;
        newTarget.transform.parent = transform;
    }

    public void startSpawnTimer()
    {
        currRespawnTime = respawnTime;
        spawning = true;
    }

}
