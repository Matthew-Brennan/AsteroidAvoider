using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private AsteroidSpawner asteroidSpawner;

    public void EndGame()
    {
        asteroidSpawner.enabled = false;
        int finalScore = scoreSystem.EndScore();
        gameOverText.text = $"SCORE: {finalScore}";
        gameOverDisplay.gameObject.SetActive(true);
    }

    public void ContinueButton()
    {
        AdManager.Instance.ShowAd(this);
        continueButton.interactable = false;

    }
    public void ContinueGame()
    {
        //Start the score counter again
        scoreSystem.StartTimer();
        //remove the game over display
        gameOverDisplay.gameObject.SetActive(false);
        //re-center the player
        player.transform.position = Vector3.zero;
        //make the player visible again
        player.SetActive(true);
        //start spawning the asteroids again
        asteroidSpawner.enabled = true;        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
