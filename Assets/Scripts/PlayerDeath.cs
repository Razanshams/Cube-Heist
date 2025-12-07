using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public float reloadDelay = 0.5f;
    private bool isDead = false;

    public void Die()
    {
        if (isDead) return;
        
        isDead = true;
        Debug.Log("Player died! Reloading in " + reloadDelay + " seconds...");
        
    
        PlayerMovement pm = GetComponent<PlayerMovement>();
        if (pm != null)
            pm.enabled = false;
        
       
        Invoke("ReloadScene", reloadDelay);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public bool IsDead()
    {
        return isDead;
    }
}