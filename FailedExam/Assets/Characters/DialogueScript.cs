using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] string[] lines;
    [SerializeField] float speedtext;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] int index;
    Image listener;
    Image speaker;
    [SerializeField] Sprite listenerSprite;
    [SerializeField] Sprite speakerSprite;
    GameObject[] dialogWindow;
    // Start is called before the first frame update
    void Start()
    {

        dialogWindow = GameObject.FindGameObjectsWithTag("Dialogue");
        Debug.Log(dialogWindow[0]);
        listener = gameObject.transform.GetChild(0).GetComponent<Image>();
        speaker = gameObject.transform.GetChild(1).GetComponent<Image>();
        listener.sprite = listenerSprite;
        speaker.sprite = speakerSprite;
        dialogueText.text = string.Empty;
        StartDialogue();
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
            dialogueText.text += c;
            if (dialogueText.text == lines[index].ToString())
            {
                //NextLines();
            }
            yield return new WaitForSeconds(speedtext);
        }
        

    }
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ScipText();
        }
    }
    public void ScipText()
    {
        if (dialogueText.text == lines[index])
        {
            NextLines();

        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = lines[index].ToString();
        }
    }
    private void NextLines()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {

            dialogWindow[0].SetActive(false);
        }
    }
}