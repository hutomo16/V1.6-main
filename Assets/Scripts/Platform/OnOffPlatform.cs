using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OnOffPlatform : MonoBehaviour
{
    public KeyCode disableColliderKey = KeyCode.S;
    public Tilemap oneWayPlatformTilemap;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is associated with the Tilemap
        if (oneWayPlatformTilemap != null && other.CompareTag("OneWayPlatform"))
        {
            // Check if the player is pressing the designated key
            if (Input.GetKey(disableColliderKey))
            {
                // Disable the collider for 1 second
                StartCoroutine(DisableColliderForTime(other, 1f));
            }
        }
    }

    IEnumerator DisableColliderForTime(Collider2D collider, float seconds)
    {
        // Disable the collider
        collider.enabled = false;

        // Wait for the specified time
        yield return new WaitForSeconds(seconds);

        // Enable the collider again
        collider.enabled = true;
    }
}
