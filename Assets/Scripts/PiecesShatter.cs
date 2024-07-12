using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PiecesShatter : MonoBehaviour
{
    public GameObject brokenSphere;
    public GameObject wholeSphere;
    public bool gameOver = false;
    public string scene_name;
    public static PiecesShatter instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Objectss")
        {
            StartCoroutine(Shatter());
        }
    }

    IEnumerator Shatter()
    {
        gameOver = true;
        LevelFinish.instance.image.gameObject.SetActive(false);
        brokenSphere.transform.position = wholeSphere.transform.position;
        wholeSphere.GetComponent<MeshRenderer>().enabled = false;
        brokenSphere.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(scene_name);
    }
}
