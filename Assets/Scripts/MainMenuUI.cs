using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuUI : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public GameObject pausePanel;

    void Start()
    {
        AudioListener.volume =
            volumeSlider.value;

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit Game");
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    public void PauseGame()
    {
        pausePanel.SetActive(true);

        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);

        Time.timeScale = 1;
    }
}