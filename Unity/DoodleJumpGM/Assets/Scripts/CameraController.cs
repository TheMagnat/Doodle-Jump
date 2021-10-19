using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    GameObject player;
    GameHandler gameHandler;

    float currentY = 0;

    float endInterpol = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameHandler = FindObjectOfType<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameHandler.gameEnd)
        {

            if (currentY < player.transform.position.y)
            {
                currentY = player.transform.position.y;

                //transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, currentY, transform.position.z), 0.5f);

            }

        }
        else
        {

            if (gameHandler.endPosition - 10 < player.transform.position.y)
            {

                endInterpol += (1f / 0.3f) * Time.deltaTime;

                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y, transform.position.z), Mathf.Min(1f, endInterpol));

                //transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            }

        }

    }
}
