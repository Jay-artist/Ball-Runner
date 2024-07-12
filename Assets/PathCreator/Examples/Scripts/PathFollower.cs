using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public ParticleSystem particleSystem;
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed;
        float distanceTravelled;
        public TextMeshProUGUI timerText;
        public Image Fill;
        public GameObject image;
        public float Max;
        public GameObject panel;
        [SerializeField] private float remainingTime;

        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            
            if (pathCreator != null)
            {
                CoundownTimer();
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }


        public void CoundownTimer()
        {
            if (remainingTime > 1)
            {
                remainingTime -= Time.deltaTime;
                Fill.fillAmount = remainingTime / Max;
            }
            else if (remainingTime < 1)
            {
                remainingTime = 0;
                //speed = 0;
                endOfPathInstruction = EndOfPathInstruction.Stop;
                image.SetActive(false);
                StartCoroutine(Celebration());

                int minutes = Mathf.FloorToInt(remainingTime / 60);
                int seconds = Mathf.FloorToInt(remainingTime % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }

        IEnumerator Celebration()
        {
            particleSystem.Play();
            yield return new WaitForSeconds(4);
            panel.SetActive(true);
        }
    }
}