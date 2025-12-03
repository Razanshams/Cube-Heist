using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyStart : MonoBehaviour
{
    // If a GameObject has this component, the GameObject will not be destroyed between scene loads;
    // the object will be added to the ObjectsToSave list on start.
    // If an object with this component starts, and there is another object with the same name already
    // in the ObjectsToSave list, then the new one that just started will be destroyed, preserving the
    // one that's already in the ObjectsToSave list.

    // Start is called before the first frame update
    public static List<GameObject> ObjectsToSave = new List<GameObject>();

    void Start()
    {
        bool DestroyObject = false;

        foreach(GameObject g in ObjectsToSave)
        {
            if (g.name == gameObject.name)
            {
                DestroyObject = true;
                break;
            }
        }

        if (DestroyObject)
            Destroy(gameObject);
        else
        {
            ObjectsToSave.Add(gameObject);
            DontDestroyOnLoad(gameObject);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
