using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Random;

public class GameHandler : MonoBehaviour
{

    public GameObject green;

    private float maxHeightDifficulty = 100f;
    private float maxDist = 3f;

    private float lastSpawn = -4f;


    private float maxSide = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        generatePlatforms(1000);
    }

    void generatePlatforms(float endPos)
    {

        Instantiate(green, new Vector3(0, lastSpawn, 0), green.transform.rotation);

        while (lastSpawn < endPos)
        {
            float randomX = Random.Range(-maxSide, maxSide);
            float randomY = Random.Range(0.3f, 0.3f + (maxDist - 0.3f) * System.Math.Min(1f, lastSpawn/maxHeightDifficulty));            

            lastSpawn += randomY;

            Instantiate(green, new Vector3(randomX, lastSpawn, 0), green.transform.rotation);
        }

        float newPos = lastSpawn;

        Vector3 spawnPos = new Vector3(0, newPos, 0);
        Instantiate(green, spawnPos, green.transform.rotation);

        newPos = lastSpawn + maxDist;


        spawnPos = new Vector3(0, newPos, 0);
        Instantiate(green, spawnPos, green.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
