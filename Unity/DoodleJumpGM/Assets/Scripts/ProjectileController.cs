using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    private float speed = 10f;
    private float startY;
    private float endY;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
        endY = startY + 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
        if (transform.position.y > endY)
        {
            Object.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("monstre"))
        {
            collision.gameObject.GetComponent<monstre>().Kill();
            Object.Destroy(this.gameObject);
        }
    }
}
