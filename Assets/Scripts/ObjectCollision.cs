using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectCollision : MonoBehaviour
{
    public GameObject cube;
    private GameObject cylinder;
    public bool replaced = false;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI timerText1;
    [SerializeField] private float remainingTime;
    public bool timerOn = false;

    public Image Fill;
    public GameObject image;
    public float Max;

    // Update is called once per frame
    void Update()
    {
        if(timerOn == true)
        {
            CoundownTimer();
        }  
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Cube
        if(collision.transform.tag == "cube")
        {
            if (collision.transform.GetComponent<MeshRenderer>().material.color == Color.red)
            {
                collision.transform.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
            else if (collision.transform.GetComponent<MeshRenderer>().material.color == Color.blue)
            {
                collision.transform.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            else
            {
                collision.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }


        //Cylinder
        if (!replaced && collision.transform.tag == "cylinder")
        {
            cylinder = collision.gameObject;
            Switch(cylinder, cube);
        }


        //Cone
        if (collision.transform.tag == "cone")
        {
            StopAllCoroutines();
            Destroy(collision.gameObject);
            BallMovement.instance.rb.isKinematic = true;
            //BallMovement.instance.speed = 0;
            timerOn = true;
            image.SetActive(true);
            StartCoroutine(FreezeMovement());
        }
    }


    // Waiting for Ball Movement
    IEnumerator FreezeMovement()
    {
        yield return new WaitForSeconds(5);
        BallMovement.instance.rb.isKinematic = false;
        remainingTime = 5;
        //BallMovement.instance.speed = 5;
    }


    //Switching Cylinder with a cube
    public void Switch(GameObject obj1, GameObject obj2)
    {
        Instantiate(obj2, obj1.transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(obj1);
    }

    public void CoundownTimer()
    {
        if(remainingTime > 1)
        { 
            remainingTime -= Time.deltaTime;
            Fill.fillAmount = remainingTime / Max;
        }
        else if( remainingTime < 1)
        {
            remainingTime = 0;
            timerOn = false; 
            image.SetActive(false);
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
