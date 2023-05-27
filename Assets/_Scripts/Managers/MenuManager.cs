using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Configuration")]
    [SerializeField] private float buttonActionDelay = 1f;

    [Header("Canvas References")]
    [SerializeField] private GameObject menuPage;
    [SerializeField] private GameObject creditsPage;

    [Header("Scene References")]
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private Rigidbody ballRB;
    private Camera mainCamera;
    [SerializeField] private Camera secondaryCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void PlayButtonPressed() => StartCoroutine(nameof(PlayButton));
    private IEnumerator PlayButton()
    {
        musicManager.PlayMusicClip(2);

        yield return new WaitForSeconds(buttonActionDelay);
        SceneManager.LoadScene("Gameplay");
    }

    public void ExitButtonPressed() => StartCoroutine(nameof(ExitButton));
    private IEnumerator ExitButton()
    {
        yield return new WaitForSeconds(buttonActionDelay); 
        Application.Quit();
    }

    public void CreditsButtonPressed() => StartCoroutine(nameof(CreditsButton));
    private IEnumerator CreditsButton()
    {
        mainCamera.gameObject.SetActive(false);
        secondaryCamera.gameObject.SetActive(true);

        menuPage.SetActive(false);
        creditsPage.SetActive(true);

        musicManager.PlayMusicClip(1);
        yield return new WaitForSeconds(buttonActionDelay);
    }

    public void BackButtonPressed() => StartCoroutine(nameof(BackButton));
    private IEnumerator BackButton()
    {
        secondaryCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        creditsPage.SetActive(false);
        menuPage.SetActive(true);

        musicManager.PlayMusicClip(0);
        yield return new WaitForSeconds(buttonActionDelay);
    }

}
