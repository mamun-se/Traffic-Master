using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public bool verticalSwipe = false;
    public bool horizontalSwipe = false;
    private Rigidbody rigidBody;
    [SerializeField] private float movementSpeed = 0;
    private Vector3 movementDirection;

    public bool shallMove = false;
    public bool isSelectedCar = false;
    [SerializeField] private List<ParticleSystem> smokeparticles;
    [SerializeField] private Animator carAnimator;
    public bool shallPlay = false;
    private AudioSource carEngineSfxSource;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        carEngineSfxSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (shallMove)
        {
            MoveCar();
        }
    }

    private void MoveCar()
    {
        if (movementDirection != null)
        {
            if(rigidBody.velocity.magnitude < 5f )
            {
                rigidBody.velocity = movementDirection * movementSpeed;
            }
        }
    }

    public void SetMovementandDirection(Vector3 direction , bool moveCar)
    {
        movementDirection = direction;
        shallMove = moveCar;
    }

    public void OnCollisionFeedback()
    {
        if (isSelectedCar)
        {
            rigidBody.isKinematic = true;
            shallMove = false;
            isSelectedCar = false;
            GameManager.gameManagerInstance.selectedCar = null;
            Vector3 hitPosition = transform.position;
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            Vector3 newPos = hitPosition - (movementDirection * 0.5f);
            transform.DOMove(newPos,0.5f).OnComplete(() =>
                {
                    //To Add more collision effeft;
                }
            );
        }
        else
        {
            transform.DOShakePosition(0.25f,0.5f,2);
            var emoji = EmojiManager.emojiManagerInstance.InstantiateEmoji();
            emoji.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            Destroy(emoji,2f);
        }
    }

    public void StopPhysicsBasedMovement()
    {
        rigidBody.isKinematic = true;
        shallMove = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        isSelectedCar = false;
    }

    public void PlayParticlesAndSmokeAnimation()
    {
        if (carEngineSfxSource.isPlaying)
        {
            carEngineSfxSource.mute = !shallPlay;
        }
        else
        {
            carEngineSfxSource.Play();
            carEngineSfxSource.mute = !shallPlay;
        }
        if (carAnimator != null)
        {
            carAnimator.enabled = shallPlay;
        }

        if (smokeparticles.Count > 0)
        {
            for (int i = 0; i < smokeparticles.Count; i++)
            {
                if (shallPlay)
                {
                     smokeparticles[i].Play();
                }
                else
                {
                    smokeparticles[i].Stop();
                }
            }
        }

    }
    
}
