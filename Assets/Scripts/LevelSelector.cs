using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    Text text;
    Event e;
    Button btn;

    public string type;

      void Start()
    {
        text = GetComponentInChildren<Text>();
        btn = GetComponent<Button>();
        type = text.text[text.text.Length - 1].ToString();
        btn.onClick.AddListener(LoadSelectedLevel);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadSelectedLevel()
    {
         SceneManager.LoadScene("Level_" + type);
    }
}
