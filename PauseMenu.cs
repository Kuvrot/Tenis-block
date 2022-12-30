using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;


public class PauseMenu : MonoBehaviour
{

    public GameObject PauseScreen;
    public GameObject SettingsScreen;
    //GameObject postProcess;
    static public bool paused;
    Toggle enablePostProcess;
    bool isMainMenu;
    Volume curVolume;

   
     // Start is called before the first frame update
    void Start()
    {
        Resume();
        enablePostProcess = SettingsScreen.GetComponentInChildren<Toggle>();
       
    }

    // Update is called once per frame
    void Update()
    {


        if (curVolume == null)
        {
            curVolume = GameObject.FindGameObjectWithTag("PostProcess").GetComponent<Volume>();
        }

        curVolume.enabled = enablePostProcess.isOn;


        if (!isMainMenu)
        {
            if (Input.GetButtonDown("Cancel") && !paused)
            {
                pause();

            }
            else if (Input.GetButtonDown("Cancel") && paused)
            {
                Resume();
            }
        }

        if (paused)
        {
            

            Settings();

        }

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            isMainMenu = true;
        }
        else
        {
            isMainMenu = false;
        }

        

        DontDestroyOnLoad(this.gameObject);

    }

    public void pause()
    {
        paused = true;

        if (isMainMenu)
        {
            SettingsScreen.SetActive(true);
        }
        else
        {
            PauseScreen.SetActive(true);
            Time.timeScale = 0;

        }
            
        GetComponent<Canvas>().sortingOrder = 1;
    }

    public void Resume()
    {
       if (!isMainMenu)
        {
            paused = false;
            Time.timeScale = 1;
            PauseScreen.SetActive(false);
            SettingsScreen.SetActive(false);
           // curVolume = null;
        }

        GetComponent<Canvas>().sortingOrder = -1;

    }

    public void SettingsFromMainMenu()
    {
        paused = true;

    }

    public void Settings()
    {

        if (curVolume != null)
        {
            
        }

    }

    public void EXIT()
    {
        SceneManager.LoadScene("MainMenu");
        Resume();
        Destroy(this.gameObject);
    }

}
