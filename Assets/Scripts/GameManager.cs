using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    [HideInInspector] public GameObject selectedCar = null;
    private Camera m_Camera;
    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private Vector3 touchPosWorld;
    private bool stopTouch = false;
    [SerializeField] private float swipeRange;
    [SerializeField] private float tapRange;
    private Player selectedCarInstance;
    private Transform targetPoint;
    [SerializeField] private int pendingCarAmount = 0;
    [HideInInspector] public UiManager uiInstance;
    [SerializeField] private GameObject obstacleParticle;
    [SerializeField] private GameObject carParticle;
    [HideInInspector] public bool isLevelCompleted = false;
    void Awake()
    {
        m_Camera = Camera.main;
        gameManagerInstance = this;
    }
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
           Vector3 TouchPosition = Input.GetTouch(0).position;
            Ray ray = m_Camera.ScreenPointToRay(TouchPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Car"))
                {
                    selectedCar = hit.transform.gameObject;
                    selectedCarInstance = selectedCar.GetComponent<Player>();
                    selectedCarInstance.isSelectedCar = true;
                    selectedCar.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }

        Swipe();

        if (selectedCar != null)
        {
            targetPoint = selectedCar.GetComponent<CarWayPointMover>().GetClosestWayPoint();
            CheckDistanceToMoveBasedOnWayPoints();
        }

        if (!isLevelCompleted && pendingCarAmount <= 0)
        {
            isLevelCompleted = true;
            uiInstance.OnLevelComplete();
            Debug.LogError("calling Game End");
        }
    }

    void CheckDistanceToMoveBasedOnWayPoints()
    {
        if (targetPoint != null)
        {
            if (Vector3.Distance(selectedCar.transform.position, targetPoint.position) < 2f)
            {
                // Move Based on waypoint
                EmojiManager.emojiManagerInstance.PlayCoinAnimation(selectedCar.transform.position);
                selectedCar.GetComponent<CarWayPointMover>().SetStartingWayPoint(targetPoint);
                selectedCarInstance.StopPhysicsBasedMovement();
                selectedCar.GetComponent<CarWayPointMover>().shallMovedWithWayPoint = true;
                selectedCar = null;

            }
        }
    }

    void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;
            if (!stopTouch)
            {
                if (Distance.x < -swipeRange)
                {
                    if (selectedCarInstance.horizontalSwipe)
                    {
                        selectedCarInstance.SetMovementandDirection(-transform.right,true);
                    }
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange)
                {
                    if (selectedCarInstance.horizontalSwipe)
                    {
                        selectedCarInstance.SetMovementandDirection(transform.right,true);
                    }
                    stopTouch = true;
                }
                else if (Distance.y > swipeRange)
                {
                    if (selectedCarInstance.verticalSwipe)
                    {
                        selectedCarInstance.SetMovementandDirection(transform.forward,true);
                    }
                    stopTouch = true;
                }
                else if (Distance.y < -swipeRange)
                {
                    if (selectedCarInstance.verticalSwipe)
                    {
                        selectedCarInstance.SetMovementandDirection(-transform.forward,true);
                    }
                    stopTouch = true;
                }

            }

        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPosition;
            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
            {
                Player playerinstance = selectedCar.GetComponent<Player>();
               bool shouldPlay = playerinstance.shallPlay;
               playerinstance.shallPlay = !shouldPlay;
               playerinstance.PlayParticlesAndSmokeAnimation();

            }
        }
    }

    public int SetPendingCarAmount()
    {
        return pendingCarAmount --;
    }

    public void PlayHitParticle(Vector3 hitPoint, bool isObstacle)
    {
        if (isObstacle)
        {
            var particle = Instantiate(obstacleParticle,hitPoint,Quaternion.identity);
            particle.GetComponent<ParticleSystem>().Play();
        }

        else
        {
            var particle = Instantiate(carParticle,hitPoint,Quaternion.identity);
            particle.GetComponent<ParticleSystem>().Play();
        }
    }
}
