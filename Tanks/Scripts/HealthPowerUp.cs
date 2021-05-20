using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    public float fullHealth=100f;

    void OnTriggerEnter (Collider other)
    {
        Pickup(other);
    }

    void Pickup(Collider player)
    {
        
        TankHealth viata = player.GetComponent<TankHealth>();
        viata.m_CurrentHealth = fullHealth;
        viata.SetHealthUI();

        Destroy(gameObject);
    }
}
