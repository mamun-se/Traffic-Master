using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EmojiManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static EmojiManager emojiManagerInstance;
    [SerializeField] private Transform canvasInstance;
    [SerializeField] private GameObject emojiPrefab;
    [SerializeField] private GameObject coinPrefab;
    public Sprite[] emojiList;

    void Awake()
    {
        emojiManagerInstance = this;
    }

    public GameObject InstantiateEmoji()
    {
        var go = Instantiate(emojiPrefab);
        int randomNumber = Random.Range(0,emojiList.Length);
        go.GetComponent<Image>().sprite = emojiList[randomNumber];
        go.transform.parent = canvasInstance;
        go.transform.DOShakePosition(2f,10f,10);
        return go;
    }

    public void PlayCoinAnimation(Vector3 carPos)
    {
        var coin = Instantiate(coinPrefab);
        coin.transform.parent = canvasInstance;
        coin.transform.position = Camera.main.WorldToScreenPoint(carPos);
        AudioManager.audioManagerinstance.PlayCoinPopSfx();
        coin.transform.DOShakeRotation(1f,120,30);
        coin.transform.DOMoveY((coin.transform.position.y + 50f),1f).OnComplete(() =>{
            coin.transform.DOScale(Vector3.zero,0.5f);
        });
        Destroy(coin,1.1f);
    }
}
