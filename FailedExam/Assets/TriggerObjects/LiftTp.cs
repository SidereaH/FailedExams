using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LiftTp : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    Animator planim;
    public Scene scene;
    [SerializeField] bool _enabled;
    [SerializeField] string _name;
    GameObject player;
    DamageableCharacter characterStats;
    private float hpAfterLevel;
    [SerializeField] ScoreManager scoreManager;
    bool inTrigger;
    [SerializeField] Visible toVisiblehintImg;
    [SerializeField] Invisible toInvisiblehintImg;
    [SerializeField] TextVisible toVisiblehintText;
    [SerializeField] TextInvisible toInvisiblehintText;
    [SerializeField] GameObject hint;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterStats = player.GetComponent<DamageableCharacter>();
        animator = GetComponent<Animator>();
        scene = SceneManager.GetActiveScene();
        planim = player.GetComponent<Animator>();


    }
    private void Update()
    {
        if (inTrigger)
        {
            //

            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                int kills = scoreManager.kills;
                PlayerPrefs.SetInt("kills", kills);
                int gold = scoreManager.gold;
                PlayerPrefs.SetInt("gold", gold);
                hpAfterLevel = characterStats.Health;
                PlayerPrefs.SetFloat("hp", hpAfterLevel);
                PlayerPrefs.SetString("lastScene", _name);
                SceneManager.LoadScene(_name);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {

            if (collision.gameObject.tag == "Player")
            {

                if (planim.GetBool("isSafety") == true)
                {
                    if (_enabled)
                    {
                        if (collision.tag == "Player")
                        {
                            animator.SetBool("isOpen", true);
                            inTrigger = true;
                            //hint.SetActive(true);
                            if (toInvisiblehintImg != null)
                            {
                                toInvisiblehintImg.StartInvivisble();
                                toInvisiblehintText.StartInvisible();
                            }
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
        inTrigger = false;
        //hint.SetActive(false);
        if(planim.GetBool("isSafety") == true)
        {
            if(_enabled)
            {
                if (toVisiblehintImg != null)
                {
                    toVisiblehintImg.StartVisble();
                    toVisiblehintText.StartVisible();
                }
            }
        }
        

    }
    void TpPlayer()
    {
        SceneManager.LoadScene(_name);
    }
}
