using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main;



    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);


        float tolerence = 10;

        if (screenPos.y < -tolerence)
        {
            Object.Destroy(this.gameObject);
        }
    }
}
