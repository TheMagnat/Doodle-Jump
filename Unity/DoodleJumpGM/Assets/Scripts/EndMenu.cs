using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
