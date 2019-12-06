using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHints : MonoBehaviour
{
    // text hint varibles
    public static string message;
    static Text textHint; 

    // <summary>
    // Text Timer
     public static bool textOn = false;
     private static float timer = 0.0f;
     public float textOnTime =5.0f;
    


    // Start is called before the first frame update
    void Start()
    {
        textHint = GetComponent<Text>();
        timer = 0.0f;
        textOn = false;
        textHint.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (textOn)
        {
            textHint.enabled = true;
            textHint.text = message;
            timer += time.deltaTime;

        }
        if(timer >= textOnTime)
        {
            textOn = false;
            textHint.enabled = false;
            timer = 0.0f;

        }
    }
}
