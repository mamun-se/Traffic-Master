using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PoleMover : MonoBehaviour
{
    [SerializeField] private float startPosy;
    [SerializeField] private float EndPosY;
    [SerializeField] private GameObject trafficLight;
    private IEnumerator coroutine;
    void Start()
    {
        MoveDown();
    }

    public void MoveDown()
    {
        trafficLight.transform.DOScale(Vector3.zero,1f);
        transform.DOMoveY(EndPosY,5f).OnComplete(()=>
            {
                coroutine = MoveUp(5.0f);
                StartCoroutine(coroutine);
            });
    }

    private IEnumerator MoveUp(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        trafficLight.transform.DOScale(Vector3.one,0.15f);
        transform.DOMoveY(startPosy,6f).OnComplete(() =>
            {
                MoveDown();
            }
        );
    }
}
