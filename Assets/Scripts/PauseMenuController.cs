using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject optionsPanel;
    public GameObject exitPanel;
    public GameObject creditsPanel;
    public GameObject gameEndPanel;
    
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;

    public InputActionReference PauseReference;
    private InputAction PauseAction;

    private bool isPaused = false;
    private Resolution[] resolutions;

    void Start()
    {
        // Hide all menu panels by default
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (gameEndPanel != null) gameEndPanel.SetActive(false);

        // --- Resolution Dropdown Setup ---
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        var options = new System.Collections.Generic.List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // --- Volume Slider Setup ---
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1f);
        volumeSlider.value = savedVolume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
        SetVolume(savedVolume);

        PauseAction = PauseReference.ToInputAction();
    }

    void Update()
    {
        if (PauseAction.WasPressedThisFrame())
        {
            if (gameEndPanel != null && gameEndPanel.activeSelf)
                return; // Prevent player from exiting Game End screen with escape
            // Cancel Credits first if showing
            else if (creditsPanel != null && creditsPanel.activeSelf)
            {
                HideCredits();
            }
            // Then Exit Panel
            else if (exitPanel != null && exitPanel.activeSelf)
            {
                HideExitPanel();
            }
            // Then Options Panel
            else if (optionsPanel != null && optionsPanel.activeSelf)
            {
                CloseOptions();
            }
            // Open/Close Pause Menu
            else if (!isPaused)
            {
                ShowPauseMenu();
            }
            else if (pauseMenuPanel != null && pauseMenuPanel.activeSelf)
            {
                HidePauseMenu();
            }
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void ShowGameEndMenu()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (gameEndPanel != null) gameEndPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        //((AudioSource)GameObject.Find("GameMusic").GetComponent("AudioSource")).Pause();
        //((AudioSource)gameObject.GetComponent("AudioSource")).Play();
    }

    public void HideGameEndMenu()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (gameEndPanel != null) gameEndPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

        //((AudioSource)gameObject.GetComponent("AudioSource")).Stop();
    }

    // Pause Menu methods
    public void ShowPauseMenu()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;

        //((AudioSource)GameObject.Find("GameMusic").GetComponent("AudioSource")).Pause();
        //((AudioSource)gameObject.GetComponent("AudioSource")).Play();
    }

    public void HidePauseMenu()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

        //((AudioSource)GameObject.Find("GameMusic").GetComponent("AudioSource")).UnPause();
        //((AudioSource)gameObject.GetComponent("AudioSource")).Stop();
    }

    // Options Panel
    public void OpenOptions()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(true);
        if (exitPanel != null) exitPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    public void CloseOptions()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    // Exit Panel
    public void ShowExitPanel()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(true);
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    public void HideExitPanel()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    public void ExitWithoutSaving()
    {
        Time.timeScale = 1f;
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void SaveAndExit()
    {
        // Replace with save logic!
        Debug.Log("Saved game before exit.");
        Time.timeScale = 1f;
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Credits Panel
    public void ShowCredits()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    // Resolution & Volume
    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("GameVolume", volume);
        PlayerPrefs.Save();
    }
}
