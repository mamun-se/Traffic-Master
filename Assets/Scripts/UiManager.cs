using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button restartButtonInGame;
    [SerializeField] private Button homeButtonInGame;
    [SerializeField] private Button restartButtonMenu;
    [SerializeField] private Button homeButtonMenu;
    [SerializeField] private GameObject levelCompletePopUp;
    [SerializeField] private GameObject levelCompleteParent;
    [SerializeField] private bool isMainMenu = false;
    [SerializeField] private ParticleSystem leftWinConfetti;
    [SerializeField] private ParticleSystem rightWinConfetti;
    private IEnumerator gameEndPopUpCoroutine;
    void Start()
    {
        if (!isMainMenu)
        {
            restartButtonInGame.onClick.AddListener(ReloadLevel);
            homeButtonInGame.onClick.AddListener(GoBackToHome);
            restartButtonMenu.onClick.AddListener(ReloadLevel);
            homeButtonMenu.onClick.AddListener(GoBackToHome);
        }
    }

    void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void GoBackToHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnLevelComplete()
    {
        if (GameManager.gameManagerInstance.isLevelCompleted)
        {
            leftWinConfetti.Play();
            rightWinConfetti.Play();
            AudioManager.audioManagerinstance.PlayLevelCompleteSfx();
        }
        gameEndPopUpCoroutine = OpenGameEndPopUp(2.0f);
        StartCoroutine(gameEndPopUpCoroutine);
    }

    public void GameEnd()
    {
        restartButtonInGame.gameObject.SetActive(false);
        homeButtonInGame.gameObject.SetActive(false);
        levelCompleteParent.SetActive(true);
        levelCompletePopUp.transform.localScale = Vector3.zero;
        levelCompletePopUp.SetActive(true);
        levelCompletePopUp.transform.DOScale(Vector3.one,1f);
    }

    private IEnumerator OpenGameEndPopUp(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        restartButtonInGame.gameObject.SetActive(false);
        homeButtonInGame.gameObject.SetActive(false);
        levelCompleteParent.SetActive(true);
        levelCompletePopUp.transform.localScale = Vector3.zero;
        levelCompletePopUp.SetActive(true);
        levelCompletePopUp.transform.DOScale(Vector3.one,1f);
    }

    public void OnQuit() {
        #if UNITY_ANDROID
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
