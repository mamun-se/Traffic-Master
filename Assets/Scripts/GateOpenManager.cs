using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GateOpenManager : MonoBehaviour
{
    [SerializeField] private Transform Gate;
    [SerializeField] private float startX = -1.9f;
    [SerializeField] private float destinationX = -5.9f;
    private IEnumerator gateOpenCoroutine;
    private IEnumerator carPassConfettiCoroutine;
    [SerializeField] private ParticleSystem carPassConfetti;
    void OnTriggerEnter(Collider other)
    {
        Gate.DOMoveX(destinationX,0.5f);
        gateOpenCoroutine = CloseGate(1.0f);
        StartCoroutine(gateOpenCoroutine);
        carPassConfettiCoroutine = PlayCarPassConfetti(0.5f);
        StartCoroutine(carPassConfettiCoroutine);
    }

    private IEnumerator CloseGate(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
         Gate.DOMoveX(startX,0.5f);
    }

    private IEnumerator PlayCarPassConfetti(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        AudioManager.audioManagerinstance.PlayCarPassSfx();
        if (carPassConfetti.isPlaying)
        {
            carPassConfetti.Stop();
            carPassConfetti.Play();
        }
        else
        {
            carPassConfetti.Play();
        }
    }
}
