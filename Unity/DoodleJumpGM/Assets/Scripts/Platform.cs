using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
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

        //Debug.Log(screenPos.x);
        if (screenPos.y < 0)
        {
            Object.Destroy(this.gameObject);
        }
    }
}
