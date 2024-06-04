using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visible : MonoBehaviour
{
    // Start is called before the first frame update
    Image image;
    GameObject hintpar;
    void Start()
    {
        image = GetComponent<Image>();
        //hintpar = transform.parent.gameObject;
    }


    IEnumerator toVisible()
    {
    
        for (float f = 1f; f >= -0.05; f -= 0.05f)
        {
            Color color = image.color;
            color.a = f;
            image.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        
        //hintpar.SetActive(false);
    }
    public void StartVisble()
    {
        Debug.Log("pizda");
        StartCoroutine("toVisible");
        
        
    }
}
