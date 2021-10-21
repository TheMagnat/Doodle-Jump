using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    GameHandler gameHandler;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = FindObjectOfType<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player")){
            gameHandler.SetEnd();
        }

        
    }
}
