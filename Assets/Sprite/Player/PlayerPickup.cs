using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private bool Healed;
    public Health healthScript;
    void Start()
    {
        healthScript = GetComponent<Health>();
        if (healthScript == null)
        {
            Debug.LogError("Health script not found on the same GameObject as PlayerPickup.");
        }
    }

    void Update()
    {
        Canheal();
    }

    private void Canheal()
    {
        if (healthScript != null && healthScript.currentHealth < 5f)
        {
            Healed = true;
        }
        else
        {
            Healed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Healed)
        {
            if (other.CompareTag("Heal"))
            {
                Destroy(other.gameObject);
                Debug.Log("player Healed");
            }
        }
        else
        {
            Debug.Log("can't be healed");
        }
    }
}
