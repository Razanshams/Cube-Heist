using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    private int current_scene_index = 0;
    public float SceneDelay = 2.1f;
    private float NextSceneDelay = -1f;
    private float current_time = 0f;
    private int intended_scene_index = -1;
    public List<string> AllScenes;
    private bool ViewedTutorial = false;
    private PauseMenuController pause_menu;

    void Start()
    {
        pause_menu = pause_menu = (PauseMenuController)GameObject.Find("SceneManager").GetComponent("PauseMenuController");
    }

    // Update is called once per frame
    void Update()
    {
        if (current_scene_index == intended_scene_index)
        {
            intended_scene_index = -1;
            current_time = 0;
        }
        
        if (intended_scene_index >= 0)
        {
            current_time += Time.deltaTime;
            if (NextSceneDelay >= 0)
            {
                if (current_time >= NextSceneDelay)
                {
                    ChangeScene(intended_scene_index);
                    NextSceneDelay = -1f;
                }
            }
            else if (current_time >= SceneDelay)
            {
                ChangeScene(intended_scene_index);
            }   
        }

        if (current_scene_index == 1 && !ViewedTutorial)
        {
            ViewedTutorial = true;
            // pause_menu.ShowTutorialPanel();
        }
    }

    public void ChangeSceneDelay(int scene_index)
    {
        if (scene_index < AllScenes.Count && scene_index >= 0)
        {
            current_time = 0f;
            intended_scene_index = scene_index;
        }
    }

    public void SetNextDelay(float time)
    {
        if (time >= 0)
            NextSceneDelay = time;
    }

    public void ChangeScene(int scene_index)
    {
        if (scene_index < AllScenes.Count && scene_index >= 0)
        {
            // If scene index is valid, unload current scene, change current scene's index,
            // and then load the new scene and set active.
            current_scene_index = scene_index;
            SceneManager.LoadScene(AllScenes[current_scene_index]);
        }
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
