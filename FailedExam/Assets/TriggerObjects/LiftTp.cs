using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftTp : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    Animator planim;
    public Scene scene;
    private bool _enabled;
    [SerializeField] string _name;
    GameObject player;
    DamageableCharacter characterStats;
    private float hpAfterLevel;
    [SerializeField] ScoreManager scoreManager;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterStats = player.GetComponent<DamageableCharacter>();
        animator = GetComponent<Animator>();
        scene = SceneManager.GetActiveScene();
        if(scene.name == "SampleScene")
        {
           _enabled = true;
        }
        else
        {
            _enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            
                if (collision.gameObject.tag == "Player")
                {
                    planim = collision.gameObject.GetComponent<Animator>();

                    if (planim.GetBool("isSafety") == true)
                    {
                        if (_enabled)
                        {
                            if (collision.tag == "Player")
                            {
                                int kills = scoreManager.kills;
                                PlayerPrefs.SetInt("kills", kills);
                                hpAfterLevel = characterStats.Health;
                                PlayerPrefs.SetFloat("hp", hpAfterLevel);
                                animator.SetBool("isOpen", true);
                                PlayerPrefs.SetString("lastScene", _name);
                            }
                        }


                    }
                    else
                    {
                        // Debug.Log("not safety");
                    }
                }
            }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("isOpen", false);
        }
    }
    void TpPlayer()
    {
        
        SceneManager.LoadScene(_name);
    }
}
