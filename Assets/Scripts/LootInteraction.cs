using UnityEngine;

public class LootInteraction : MonoBehaviour
{
    // public GameObject WinText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            SceneChanger changer = GameObject.Find("SceneManager").GetComponent<SceneChanger>();

            int i = changer.GetCurrentSceneIndex() + 1;
            if (i < changer.GetTotalScenes())
                changer.ChangeScene(i);
            else
                changer.ChangeScene(0);
        }
    }
}
