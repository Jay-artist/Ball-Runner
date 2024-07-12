using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelFinish : MonoBehaviour
{
    public string scene_name;
    [SerializeField] GameObject panel;
    public TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;
    public Image Fill;
    public GameObject image;
    public float Max;

    public GameObject ball;
    private Vector3 initialPosition;
    public static LevelFinish instance;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = ball.transform.position;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.transform.position != initialPosition)
        {
            CoundownTimer();
        }
    }

    public void loadscene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    public void CoundownTimer()
    {
        if (remainingTime > 1)
        {
            remainingTime -= Time.deltaTime;
            Fill.fillAmount = remainingTime / Max;
        }
        else if (remainingTime < 1 && PiecesShatter.instance.gameOver == false)
        {
            remainingTime = 0;
            panel.gameObject.SetActive(true);
            timerText.gameObject.SetActive(false);
            image.SetActive(false);
            BallMovement.instance.rb.isKinematic = true;
            Obj.instance.StopAllCoroutines();
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
