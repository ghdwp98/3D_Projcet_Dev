using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField]Image image;
    
    void Start()
    {
        text=GetComponent<TextMeshProUGUI>();
        StartCoroutine(BlinkTexts());
        
    }

    


    public IEnumerator BlinkTexts()
    {
        while(true)
        {
            text.text = "";
            image.enabled = false;
            yield return new WaitForSeconds(0.7f);
            text.text = "Press Any Key";
            image.enabled = true;
            yield return new WaitForSeconds(0.7f);
        }
    }
}
