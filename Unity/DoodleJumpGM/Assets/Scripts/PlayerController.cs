using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Camera cam;

    private Rigidbody2D rigidBody;

    private BoxCollider2D feetCollider;
    private BoxCollider2D bodyCollider;

    public float MAXSPEED = 4f;

    public float SPEED = 30f;

    private float xVelocity = 0f;



    private SpriteRenderer spriteRenderer;

    //State
    private bool onFire = false;
    private bool right = true;
    private bool plie = false;

    //Timer
    private float elapsedFire = 0.0f;
    private float elapsedPlie = 0.0f;

    //All sprites
    public Sprite normalLeft;
    public Sprite plieLeft;

    public Sprite normalRight;
    public Sprite plieRight;

    public Sprite Fire;
    public Sprite plieFire;
    


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        cam = Camera.main;

        BoxCollider2D[] allColliders = GetComponents<BoxCollider2D>();

        foreach(BoxCollider2D collider in allColliders)
        {
            if (collider.isTrigger)
            {
                bodyCollider = collider;
            }
            else
            {
                feetCollider = collider;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            elapsedFire = 0.0f;
            onFire = true;

            Vector3 screenPos = cam.WorldToScreenPoint(rigidBody.position);
            Debug.Log("target is " + screenPos.x + " pixels from the left : " + cam.pixelWidth);
        }


        bool decreaseVelocity = true;
        if (Input.GetKey("left"))
        {

            right = false;

            decreaseVelocity = false;

            xVelocity -= SPEED * Time.deltaTime;

            if (xVelocity < -MAXSPEED)
            {
                xVelocity = -MAXSPEED;

            }
        }

        if (Input.GetKey("right"))
        {

            right = true;

            decreaseVelocity = false;

            xVelocity += SPEED * Time.deltaTime;

            if (xVelocity > MAXSPEED)
            {

                xVelocity = MAXSPEED;

            }
        }

        if (decreaseVelocity)
        {

            if (xVelocity < 0f)
            {
                xVelocity += SPEED * Time.deltaTime ;
                if (xVelocity > 0f)
                {
                    xVelocity = 0f;
                }
            }
            else if(xVelocity > 0f)
            {
                xVelocity -= SPEED * Time.deltaTime;
                if (xVelocity < 0f)
                {
                    xVelocity = 0f;
                }
            }
        }


        //Timer
        if (onFire)
        {
            elapsedFire += Time.deltaTime;
            if (elapsedFire > 0.3)
            {
                onFire = false;
            }
        }

        if (plie)
        {
            elapsedPlie += Time.deltaTime;
            if (elapsedPlie > 0.25)
            {
                plie = false;
            }
        }


        //Render
        if (onFire)
        {
            if (plie)
            {
                spriteRenderer.sprite = plieFire;
            }
            else
            {
                spriteRenderer.sprite = Fire;
            }
        }
        else
        {
            if (plie)
            {
                if (right)
                {
                    spriteRenderer.sprite = plieRight;
                }
                else
                {
                    spriteRenderer.sprite = plieLeft;
                }
            }
            else
            {
                if (right)
                {
                    spriteRenderer.sprite = normalRight;
                }
                else
                {
                    spriteRenderer.sprite = normalLeft;
                }
            }
        }



    }

    private void FixedUpdate()
    {

        if (rigidBody.velocity.y <= 0)
        {
            feetCollider.enabled = true;
        }


        //Updare velocity from inputs
        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);

        //Handle sides
        Vector3 screenPos = cam.WorldToScreenPoint(rigidBody.position);

        float fromLeft = screenPos.x;
        float fromRight = cam.pixelWidth - screenPos.x;


        float tolerence = 20;

        if (fromLeft < -tolerence)
        {

            Vector3 worldPos = cam.ScreenToWorldPoint(new Vector2(screenPos.x + (cam.pixelWidth + tolerence*2), 0));

            rigidBody.MovePosition(new Vector2(worldPos.x, rigidBody.position.y));
        }
        else if (fromRight < -tolerence)
        {
            Vector3 worldPos = cam.ScreenToWorldPoint(new Vector2(screenPos.x - (cam.pixelWidth + tolerence*2), 0));

            rigidBody.MovePosition(new Vector2(worldPos.x, rigidBody.position.y));
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("solid"))
        {

            if (rigidBody.velocity.y <= 0)
            {
                plie = true;
                elapsedPlie = 0.0f;

                rigidBody.AddForce(new Vector2(0, 22), ForceMode2D.Impulse);
                feetCollider.enabled = false;

            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Verify if enemie and die
    }

}
