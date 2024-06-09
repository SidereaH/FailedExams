using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RickRoll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource rickMusic;
    [SerializeField] GameObject rickPrefab;
    GameObject _temp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            rickPrefab.SetActive(true);
            rickMusic.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        rickPrefab.SetActive(false);
        rickMusic.Stop();
    }
}
