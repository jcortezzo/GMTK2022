using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public GameConfig config;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        config.InifiniteMode = false;
        SceneManager.LoadScene("Nguyen");
    }

    public void RestartInifite()
    {
        config.InifiniteMode = true;
        SceneManager.LoadScene("Nguyen");
    }
}
