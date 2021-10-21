using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator animator;

    private Camera cam;

    private Rigidbody2D rigidBody;

    private BoxCollider2D feetCollider;
    private BoxCollider2D bodyCollider;

    private GameHandler gameHandler;

    public float MAXSPEED = 5f;

    public float SPEED = 45f;

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

        gameHandler = FindObjectOfType<GameHandler>();

        animator = gameObject.GetComponent<Animator>();

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

        if (!gameHandler.gameEnd)
        {

            if (Input.GetKeyDown("space"))
            {
                elapsedFire = 0.0f;
                animator.SetBool("fire", true);

                Vector3 screenPos = cam.WorldToScreenPoint(rigidBody.position);
                Debug.Log("target is " + screenPos.x + " pixels from the left : " + cam.pixelWidth);
            }


            bool decreaseVelocity = true;
            if (Input.GetKey("left"))
            {

                animator.SetBool("right", false);

                decreaseVelocity = false;

                xVelocity -= SPEED * Time.deltaTime;

                if (xVelocity < -MAXSPEED)
                {
                    xVelocity = -MAXSPEED;

                }
            }

            if (Input.GetKey("right"))
            {

                animator.SetBool("right", true);

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
                    xVelocity += SPEED * Time.deltaTime;
                    if (xVelocity > 0f)
                    {
                        xVelocity = 0f;
                    }
                }
                else if (xVelocity > 0f)
                {
                    xVelocity -= SPEED * Time.deltaTime;
                    if (xVelocity < 0f)
                    {
                        xVelocity = 0f;
                    }
                }
            }


            //Timer
            if (animator.GetBool("fire"))
            {
                elapsedFire += Time.deltaTime;
                if (elapsedFire > 0.3)
                {
                    animator.SetBool("fire", false);
                }
            }

            if (animator.GetBool("jumping"))
            {
                elapsedPlie += Time.deltaTime;
                if (elapsedPlie > 0.25)
                {
                    animator.SetBool("jumping", false);
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

        if (gameHandler.gameEnd)
        {
            feetCollider.enabled = false;
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

        if (collision.gameObject.CompareTag("spring"))
        {

            if (rigidBody.velocity.y <= 0)
            {
                //plie = true;
                animator.SetBool("jumping", true);
                elapsedPlie = 0.0f;

                rigidBody.AddForce(new Vector2(0, 40f), ForceMode2D.Impulse);
                feetCollider.enabled = false;

                collision.gameObject.GetComponent<SpringController>().Trigger();

            }
        }
        else if (collision.gameObject.CompareTag("solid"))
        {

            if (rigidBody.velocity.y <= 0)
            {
                //plie = true;
                animator.SetBool("jumping", true);
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
