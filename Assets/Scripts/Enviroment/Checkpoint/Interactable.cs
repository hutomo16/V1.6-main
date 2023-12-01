using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public bool isRange;

    private GameMaster gm;

    public Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRange)
        {
            Debug.Log("is Ranged!");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("key is pressed");
                GameMaster.instance.setLastCheckPoint(transform);

                Anim.SetBool("LightsOn", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            isRange = true;
            Debug.Log("is in range");

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isRange = false;
            Debug.Log("is out range");
        }
    }
}
