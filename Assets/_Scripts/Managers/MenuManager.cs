using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Configuration")]
    [SerializeField] private float buttonActionDelay = 1f;

    [Header("Canvas References")]
    [SerializeField] private GameObject creditsPage;
    [SerializeField] private GameObject menuPage;

    [Header("Scene References")]
    [SerializeField] private Camera secondaryCamera;
    private Camera mainCamera;
    [SerializeField] private Rigidbody ballRB;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public IEnumerator ButtonTestPressed()
    {
        yield return new WaitForSeconds(buttonActionDelay);
    }

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    public void CreditsButtonPressed()
    {        
        mainCamera.gameObject.SetActive(false);
        secondaryCamera.gameObject.SetActive(true);

        menuPage.SetActive(false);
        creditsPage.SetActive(true);
    }

    public void BackButtonPressed()
    {
        secondaryCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        creditsPage.SetActive(false);
        menuPage.SetActive(true);        
    }
}
