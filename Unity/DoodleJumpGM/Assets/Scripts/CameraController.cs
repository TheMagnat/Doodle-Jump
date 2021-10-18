using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    GameObject player;

    float currentY = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {


        if(currentY < player.transform.position.y)
        {
            currentY = player.transform.position.y;

            //transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, currentY, transform.position.z), 0.5f);

        }

    }
}
