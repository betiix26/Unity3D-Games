using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPowerUp : MonoBehaviour
{
   public float multiplier = 1.4f;
    private float waiting = 5f;
    void OnTriggerEnter (Collider other)
    {
        StartCoroutine(Pickup(other));
    }

    IEnumerator Pickup(Collider player)
    {
        
        player.transform.localScale *=multiplier;
        ShellExplosion dmg = player.GetComponent<ShellExplosion>();

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        
        yield return new WaitForSeconds (waiting);

        player.transform.localScale /=multiplier;

        Destroy(gameObject);
    }
}
