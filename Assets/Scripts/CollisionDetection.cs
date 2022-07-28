using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollisionDetection : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        Vector3 totalPoint = Vector3.zero;
        foreach (ContactPoint contact in collision.contacts)
        {
            totalPoint += contact.point;
        }

        Vector3 hitPoint = totalPoint / collision.contactCount;
        if (collision.gameObject.CompareTag("Car"))
        {
            GameManager.gameManagerInstance.PlayHitParticle(hitPoint,false);
            AudioManager.audioManagerinstance.PlayCarHitSfx();
            Player playerInstance = GetComponent<Player>();
            if (playerInstance != null)
            {
                playerInstance.OnCollisionFeedback();
            }
            HapticManager.DoVibrate(100); // value in miliseconds
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            AudioManager.audioManagerinstance.PlayObstacleHitSfx();
            GameManager.gameManagerInstance.PlayHitParticle(hitPoint,true);
            collision.gameObject.transform.DOShakePosition(0.15f,0.25f,1,90f);
            Player playerInstance = GetComponent<Player>();
            if (playerInstance != null)
            {
                playerInstance.OnCollisionFeedback();
            }
            HapticManager.DoVibrate(100); // value in miliseconds
        }

        else
        {
            GameManager.gameManagerInstance.uiInstance.GameEnd();
            HapticManager.DoVibrate(500); //value in miliseconds
        }
    }
}
