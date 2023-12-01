using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractionDialogueBox1 : MonoBehaviour
{
    public GameObject playerCanvas;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;
    public float wordSpeed;

    public bool isRange;
    public Animator Anim;
    public GameObject Text;

    public GameObject contButton;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Keyinput();
    }

    public void Keyinput(){
        if (isRange)
        {
            Debug.Log("is Ranged!");
            if (Input.GetKeyDown(KeyCode.E) && isRange)
            {
                Debug.Log("key is pressed");
                if (dialoguePanel.activeInHierarchy)
                {

                    zeroText();
                }
                else
                {
                    playerCanvas.SetActive(false);
                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                }
            }

            if (dialogueText.text == dialogue[index])
            {
                contButton.SetActive(true);
            }
        }
    }


    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        playerCanvas.SetActive(true);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Text.activeInHierarchy == false)
        {
            isRange = true;
            Debug.Log("is in range");
            Text.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isRange = false;
            Debug.Log("is out range");
            Text.SetActive(false);
            zeroText();
        }
    }
}
