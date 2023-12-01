using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public bool isRange;
    public Animator Anim;

    public GameObject Text;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRange)
        {
            Debug.Log("is Ranged!");
            if (Input.GetKeyDown(KeyCode.E) )
            {
                Debug.Log("key is pressed");
            }

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
        }
    }
}
