using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{

    private AudioSource source;

    Camera cam;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        source = gameObject.GetComponent<AudioSource>();

        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);

        animator = gameObject.GetComponent<Animator>();

        float tolerence = 10;

        if (screenPos.y < -tolerence)
        {
            Object.Destroy(this.gameObject);
        }

        
    }

    public void Trigger()
    {
        animator.SetBool("trigger", true);
        source.Play();
    }
}
