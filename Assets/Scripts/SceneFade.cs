using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    private CanvasGroup canvas_group;
    private bool fade_in = true;
    private bool fade_out = false;
    private Image own_image;

    public float TimeToFade = 2f;
    private float CurrentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (TimeToFade <= 0)
            TimeToFade = 3f;
        own_image = (Image)GetComponent("Image");
        canvas_group = (CanvasGroup)GetComponent("CanvasGroup");
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTime <= TimeToFade)
            CurrentTime += Time.deltaTime;

        if (fade_out == true)
        {
            canvas_group.alpha = CurrentTime / TimeToFade;
        }

        if (fade_in == true)
        {
            canvas_group.alpha = 1 - CurrentTime / TimeToFade;
        }

        if (fade_in || fade_out)
        {
            own_image.raycastTarget = true;
        }
        else
        {
            own_image.raycastTarget = false;
        }

        if (CurrentTime >= TimeToFade)
        {
            if (fade_in)
            {
                fade_in = false;
                canvas_group.alpha = 0;

            }
            else if (fade_out)
            {
                fade_out = false;
                canvas_group.alpha = 1;
            }
        }
    }

    public void SetFadeIn()
    {
        fade_out = false;
        fade_in = true;

        CurrentTime = 0;
    }

    public void SetFadeOut()
    {
        fade_in = false;
        fade_out = true;

        CurrentTime = 0;
    }
}
