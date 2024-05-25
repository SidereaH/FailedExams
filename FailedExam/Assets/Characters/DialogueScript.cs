using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] TypeOfSetText type;
    [SerializeField] string[] lines;
    [SerializeField] float speedtext;
    [SerializeField] float startEditTime;
    private float nextLineCountTime = 0;
    [SerializeField] float nextLineTime;
    private float timeBtwSymbDel = 0;
    private float timeToLoadScene = 0;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] int index;
    Image listener;
    Image speaker;
    [SerializeField] Sprite listenerSprite;
    [SerializeField] Sprite speakerSprite;
    GameObject[] dialogWindow;
    [SerializeField] GameObject audioWrite;
    [SerializeField] enum TypeOfSetText {Dialogue, Text };
    private bool successLine;
    private bool successText;

    // Start is called before the first frame update
    void Start()
    {
        if(type == TypeOfSetText.Dialogue)
        {
            dialogWindow = GameObject.FindGameObjectsWithTag("Dialogue");
            listener = gameObject.transform.GetChild(0).GetComponent<Image>();
            speaker = gameObject.transform.GetChild(1).GetComponent<Image>();
            listener.sprite = listenerSprite;
            speaker.sprite = speakerSprite;
            dialogueText.text = string.Empty;
            StartDialogue();
        }
        else
        {
            Debug.Log(type.ToString());
            dialogueText.text = string.Empty;
            StartDialogue();
        }
       
    }
    private void FixedUpdate()
    {
        
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());

    }
    // Update is called once per frame
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            if(c != ' ')
            {
                GameObject _temp = Instantiate(audioWrite, transform.position, Quaternion.identity);

                Destroy(_temp, 1);
            }
            dialogueText.text += c;
            if (dialogueText.text == lines[index].ToString())
            {


                successLine = true;
                //NextLines();
            }
            yield return new WaitForSeconds(speedtext);
        }
    }
    string AddSymb(string text)
    {
        dialogueText.text = text + " |";
        return dialogueText.text;
    }
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            nextLineCountTime = 0;
            ScipText();
        }
        if (successLine == true)
        {
            if (timeBtwSymbDel <= 0)
            {

                if (dialogueText.text == lines[index].ToString() || dialogueText.text == lines[index].ToString() + "  ")
                {
                    dialogueText.text = AddSymb(lines[index].ToString());
                    timeBtwSymbDel = startEditTime;
                }
                else
                {
                    dialogueText.text = lines[index].ToString() + "  ";
                    timeBtwSymbDel = startEditTime;
                }


            }
            else
            {

                timeBtwSymbDel -= Time.deltaTime;

            }

            if (nextLineCountTime >= nextLineTime)
            {
                nextLineCountTime = 0;
                NextLines();
            }
            else
            {
                nextLineCountTime += Time.deltaTime;
            }

        }
        if (successText == true)
        {
            if (timeToLoadScene >= nextLineTime)
            {
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                timeToLoadScene += Time.deltaTime;
            }
        }
    }
    public void ScipText()
    {
        if (dialogueText.text == lines[index] || dialogueText.text == lines[index]+" |" || dialogueText.text == lines[index] + " " || dialogueText.text == lines[index] + "|" || dialogueText.text == lines[index] + "" )
        {
            successLine = true;
            NextLines();

        }
        else
        {
            dialogueText.text = lines[index].ToString();
            successLine = true;
            StopAllCoroutines();
            
        }
        if(successText == true)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    private void NextLines()
    {
        
        if (index < lines.Length - 1)
        {
            successLine = false;
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            if(type == TypeOfSetText.Dialogue)
            {
                dialogWindow[0].SetActive(false);
            }
            else
            {
                successText = true;

            }
        }
    }
}