using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    Text text;

    void Start()
    {
        text=GetComponent<Text>();
        StartCoroutine(BlinkTexts());
    }

    


    public IEnumerator BlinkTexts()
    {
        while(true)
        {
            text.text = "";
            yield return new WaitForSeconds(0.7f);
            text.text = "Press Any Key";
            yield return new WaitForSeconds(0.7f);
        }
    }
}
