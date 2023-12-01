using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsInterval : MonoBehaviour
{
    public Health healthScript;
    public float DamageInterval = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(IntervalOff());
        }
    }

    IEnumerator IntervalOff()
    {
        healthScript.ChangeHealth(-1);

        // Wait for the specified delay before allowing damage again
        yield return new WaitForSeconds(DamageInterval);

        // Add any additional logic here if needed
    }
}
