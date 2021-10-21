using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Random;

public class GameHandler : MonoBehaviour
{

    public bool gameEnd = false;
    public float endPosition;

    public GameObject green;
    public GameObject brown;
    public GameObject blue;

    public GameObject spring;

    //Enemie
    public GameObject blackHole;


    private GameObject player;

    private Camera cam;

    List<(float, float)> greenPos;

    //Pos param
    private float maxHeightDifficulty = 200f;
    private float maxDist = 3.05f;
    private float minDist = 0.6f;

    //Prob param
    private float brownProb = 0.35f;
    private float maxBlueProb = 0.35f;
    private float springProb = 0.25f;

    //Other
    float currentBrownProb;

    public int score = 0;



    private float lastSpawn = -4f;

    float lastGreenY;


    private float maxSide = 2.5f;

    float genPos = 0f;


    // Start is called before the first frame update
    void Start()
    {
        currentBrownProb = brownProb;

        player = GameObject.Find("Player");
        cam = Camera.main;



        greenPos = new List<(float, float)>();

        Instantiate(green, new Vector3(0, lastSpawn, 0), green.transform.rotation);
        greenPos.Add((0, lastSpawn));
        lastGreenY = lastSpawn;

        generatePlatforms(10);
    }

    void generatePlatforms(float endPos)
    {


        List<(float, float)> newGreenPos = new List<(float, float)>();



        while (lastSpawn < endPos)
        {

            float doBrown = Random.Range(0f, 1f);


            string color = "green";

            float randomX;
            float randomY;

            bool spawnSpring = false;

            if (doBrown < currentBrownProb)
            {
                color = "brown";
                currentBrownProb = 0f;

                randomX = Random.Range(-maxSide, maxSide);
                randomY = Random.Range(minDist, minDist + (maxDist - minDist*2) * System.Math.Min(1f, lastSpawn / maxHeightDifficulty));

                lastSpawn += randomY;
            }
            else
            {

                currentBrownProb = brownProb;



                float doBlue = Random.Range(0f, 1f);

                if (doBlue < maxBlueProb * System.Math.Min(1f, lastSpawn / maxHeightDifficulty))
                {
                    color = "blue";
                }

                float doSpring = Random.Range(0f, 1f);
                if (doSpring < springProb)
                {
                    spawnSpring = true;
                }

                float greenSpace = lastSpawn - lastGreenY;

                randomX = Random.Range(-maxSide, maxSide);
                randomY = Random.Range(minDist, minDist + (maxDist - minDist - greenSpace) * System.Math.Min(1f, lastSpawn / maxHeightDifficulty));

                lastSpawn += randomY;

                lastGreenY = lastSpawn;
            }


            GameObject instance = null;

            if (color.Equals("green"))
            {
                instance = Instantiate(green, new Vector3(randomX, lastSpawn, 0), green.transform.rotation);
            }
            else if (color.Equals("brown"))
            {
                instance = Instantiate(brown, new Vector3(randomX, lastSpawn, 0), brown.transform.rotation);
            }
            else if (color.Equals("blue"))
            {
                instance = Instantiate(blue, new Vector3(randomX, lastSpawn, 0), brown.transform.rotation);
            }

            if (spawnSpring)
            {
                float randomXspring = Random.Range(-0.4f, 0.4f);
                GameObject instanceSpring = Instantiate(spring, new Vector3(randomX + randomXspring, lastSpawn + 0.36f, 0), spring.transform.rotation);
                instanceSpring.transform.SetParent(instance.transform, true);
            }

            newGreenPos.Add((randomX, lastSpawn));

        }



        greenPos.AddRange(newGreenPos);


        genPos = lastSpawn - 10;

    }

    // Update is called once per frame
    void Update()
    {

        if (!gameEnd)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(player.transform.position);

            float tolerence = 0;

            if (screenPos.y < -tolerence)
            {

                SetEnd();

            }


            if(cam.transform.position.y > genPos)
            {

                generatePlatforms(lastSpawn + 10);

            }


        }

    }

    public void SetEnd()
    {
        endPosition = player.transform.position.y;
        gameEnd = true;


        //Set menu

        GameObject playAgain = GameObject.Find("EndScreen");

        playAgain.transform.position = new Vector3(playAgain.transform.position.x, endPosition - 9, playAgain.transform.position.z);
    }

}
