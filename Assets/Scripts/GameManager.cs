using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float spawnRate = 1;
    private int score;


    public List<GameObject> target;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public bool isGameActive;
    public bool isPaused = false;
    public int playerLives;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPause();
    }

    private void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGameActive && !isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseScreen.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isGameActive && isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pauseScreen.gameObject.SetActive(false);
        }
    }
    // Spawns a random object every second
    private IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, target.Count);
            Instantiate(target[index]);
        }
    }

    // Adds to the score and prints it on the canvas
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Decreases the player lives and shows it on the screen
    public void UpdateLives(int livesToDecrease)
    {
        playerLives -= livesToDecrease;
        livesText.text = "Lives: " + playerLives;
    }

    // Sets the game as over and shows up "Game Over" text and Restart Button.
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // Restarts the game to the current scene (start).
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Removes the starting screen, sets the score and lives, and start spawning according to choosen difficulty
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(-3);
        titleScreen.gameObject.SetActive(false);
    }
}
