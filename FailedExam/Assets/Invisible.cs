using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invisible : MonoBehaviour
{
    // Start is called before the first frame update
    Image image;
    void Start()
    {
       
        image = GetComponent<Image>();
        Color color = image.color;
        color.a = 0f;
        image.color = color;
    }


    IEnumerator toInvisible()
    {
        for(float f = 0.05f;  f <= 1.05; f+=0.05f)
        {
            Color color = image.color;
            color.a = f;
            image.color = color;
            yield return new WaitForSeconds(0.05f);
        }

    }
    public void StartInvivisble()
    {
        Debug.Log("вышло");
        StartCoroutine("toInvisible");
        
       
    }
}
