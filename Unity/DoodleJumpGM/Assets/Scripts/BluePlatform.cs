using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlatform : MonoBehaviour
{


    private Camera cam;

    bool goRight = true;
    float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main;


        float goLeft = Random.Range(0f, 1f);

        if (goLeft < 0.5)
        {
            goRight = false;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float direction = -1f;

        if (goRight)
        {
            direction = 1f;
        }

        transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime) * direction, transform.position.y, transform.position.z);


        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);

        float fromLeft = screenPos.x;
        float fromRight = cam.pixelWidth - screenPos.x;

        float limit = 20f;

        if (fromLeft < limit)
        {
            goRight = true;

            Vector3 worldPos = cam.ScreenToWorldPoint(new Vector2(limit + (limit - fromLeft), 0));

            transform.position = new Vector3(worldPos.x, transform.position.y, transform.position.z);

        }
        else if (fromRight < limit)
        {
            goRight = false;

            Vector3 worldPos = cam.ScreenToWorldPoint(new Vector2((cam.pixelWidth - limit) - (limit - fromRight), 0));

            transform.position = new Vector3(worldPos.x, transform.position.y, transform.position.z);

        }

    }
}
