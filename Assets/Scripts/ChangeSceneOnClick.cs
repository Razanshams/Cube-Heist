using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneOnClick : MonoBehaviour
{
    // Used when OnClick effects reference objects that may change after loading a scene (such as
    //      an object that is set to not be destroyed).

    public int SceneIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        SceneChanger scene_changer = (SceneChanger)GameObject.Find("SceneManager").GetComponent("SceneChanger");
        Button some_button = (Button)gameObject.GetComponent("Button");
        some_button.onClick.AddListener(delegate { scene_changer.ChangeSceneDelay(SceneIndex);  });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
