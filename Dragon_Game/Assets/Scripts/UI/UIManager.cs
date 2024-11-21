using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    #region Game Over
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    //Функции

    public void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit(); //Выход из готовой игры

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Выход из редактора
        #endif
    }
    #endregion

    #region Pause

    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);  

        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion

    #region Main Menu
    //Функции

    public void LevelSelect()
    {
        SceneManager.LoadScene(1);
    }
    public void NewGame()
    {
        SceneManager.LoadScene(2);
    }
    #endregion

}
