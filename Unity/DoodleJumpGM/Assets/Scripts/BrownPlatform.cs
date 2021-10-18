using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownPlatform : MonoBehaviour
{

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.isTrigger)
        {
            GetComponent<Collider2D>().enabled = false;

            animator.SetBool("broken", true);
            //Object.Destroy(this.gameObject);
        }
    }

}
