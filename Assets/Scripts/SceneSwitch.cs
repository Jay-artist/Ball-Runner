using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public string scenenames;
    public static SceneSwitch instance;

    public void Start()
    {
        instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void loadscene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(scenenames);
        }
    }
}
