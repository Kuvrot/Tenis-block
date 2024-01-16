using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null , Vector2.zero , CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play ()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
