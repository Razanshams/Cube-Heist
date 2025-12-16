using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public float reloadDelay = 2f;
    
   
    public Canvas deathCanvas;
    public Image redFlashImage;
    public Text caughtText;
    public Text countdownText;
    
    private bool isDead = false;
    private float deathTime;

    void Start()
    {
       
        if (deathCanvas == null)
        {
            CreateDeathUI();
        }
        
        
        if (deathCanvas != null)
        {
            deathCanvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isDead)
        {
            // Flash red effect
            float flashSpeed = 3f;
            float alpha = Mathf.PingPong(Time.time * flashSpeed, 0.3f);
            if (redFlashImage != null)
                redFlashImage.color = new Color(1f, 0f, 0f, alpha);
            
            // Update countdown
            float timeLeft = reloadDelay - (Time.time - deathTime);
            if (countdownText != null && timeLeft > 0)
                countdownText.text = "Respawning in " + Mathf.Ceil(timeLeft) + "s";
        }
    }

    public void Die()
    {
        if (isDead) return;
        
        isDead = true;
        deathTime = Time.time;
        
        Debug.Log("Player caught! Reloading scene...");
        
        
        Time.timeScale = 1f;
        
        
        
        
        PlayerMovement pm = GetComponent<PlayerMovement>();
        if (pm != null)
            pm.enabled = false;
        
        
        PauseMenuController pmc = FindObjectOfType<PauseMenuController>();
        if (pmc != null)
        {
            pmc.canPause = false;
            Debug.Log("Pausing disabled!");
        }
        else
        {
            Debug.LogWarning("PauseMenuController not found!");
        }
        
        
        if (deathCanvas != null)
            deathCanvas.gameObject.SetActive(true);
        
        // Reload scene after delay
        Invoke("ReloadScene", reloadDelay);
    }

    void ReloadScene()
    {
        
        PauseMenuController pmc = FindObjectOfType<PauseMenuController>();
        if (pmc != null)
            pmc.canPause = true;
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public bool IsDead()
    {
        return isDead;
    }
    
    void CreateDeathUI()
    {
        // Create Canvas
        GameObject canvasObj = new GameObject("DeathCanvas");
        deathCanvas = canvasObj.AddComponent<Canvas>();
        deathCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        deathCanvas.sortingOrder = 100;
        
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        canvasObj.AddComponent<GraphicRaycaster>();
        
        // Red flash overlay (fullscreen)
        GameObject flashObj = new GameObject("RedFlash");
        flashObj.transform.SetParent(canvasObj.transform, false);
        redFlashImage = flashObj.AddComponent<Image>();
        redFlashImage.color = new Color(1f, 0f, 0f, 0f);
        RectTransform flashRect = flashObj.GetComponent<RectTransform>();
        flashRect.anchorMin = Vector2.zero;
        flashRect.anchorMax = Vector2.one;
        flashRect.sizeDelta = Vector2.zero;
        
        
        GameObject caughtObj = new GameObject("CaughtText");
        caughtObj.transform.SetParent(canvasObj.transform, false);
        caughtText = caughtObj.AddComponent<Text>();
        caughtText.text = "YOU'VE BEEN CAUGHT!";
        caughtText.fontSize = 60;
        caughtText.color = Color.blue;
        caughtText.alignment = TextAnchor.MiddleCenter;
        caughtText.fontStyle = FontStyle.Bold;
        caughtText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        
        Shadow shadow1 = caughtObj.AddComponent<Shadow>();
        shadow1.effectColor = Color.black;
        shadow1.effectDistance = new Vector2(3, -3);
        
        RectTransform caughtRect = caughtObj.GetComponent<RectTransform>();
        caughtRect.anchorMin = new Vector2(0.5f, 0.5f);
        caughtRect.anchorMax = new Vector2(0.5f, 0.5f);
        caughtRect.sizeDelta = new Vector2(800, 100);
        caughtRect.anchoredPosition = new Vector2(0, 50);
        
        
        GameObject countdownObj = new GameObject("CountdownText");
        countdownObj.transform.SetParent(canvasObj.transform, false);
        countdownText = countdownObj.AddComponent<Text>();
        countdownText.text = "Respawning in 2s";
        countdownText.fontSize = 30;
        countdownText.color = Color.red;
        countdownText.alignment = TextAnchor.LowerRight;
        countdownText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        
        Shadow shadow2 = countdownObj.AddComponent<Shadow>();
        shadow2.effectColor = Color.black;
        shadow2.effectDistance = new Vector2(2, -2);
        
        RectTransform countdownRect = countdownObj.GetComponent<RectTransform>();
        countdownRect.anchorMin = new Vector2(1, 0);
        countdownRect.anchorMax = new Vector2(1, 0);
        countdownRect.pivot = new Vector2(1, 0);
        countdownRect.sizeDelta = new Vector2(400, 100);
        countdownRect.anchoredPosition = new Vector2(-20, 20);
    }
}