using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneChanger scene_changer = (SceneChanger)GameObject.Find("SceneManager").GetComponent("SceneChanger");
        Button some_button = (Button)gameObject.GetComponent("Button");
        some_button.onClick.AddListener(scene_changer.ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
