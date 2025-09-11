using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyEnemy : Enemy
{
    [SerializeField] private GameObject energyObject;
    Coroutine dameOverTimeCoroutine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || player == null) return;

        player.TakeDamage(enterDamage);
        playerInside = true;

        if (dameOverTimeCoroutine == null) dameOverTimeCoroutine = StartCoroutine(DamageOverTime());

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        playerInside = false;
        if (dameOverTimeCoroutine != null) { StopCoroutine(dameOverTimeCoroutine); dameOverTimeCoroutine = null; }

    }

    IEnumerator DamageOverTime()
    {
        while (playerInside && player != null)
        {
            player.TakeDamage(stayDamage * tick);
            yield return new WaitForSeconds(tick);
        }
        
    }

    protected override void Die()
    {
        if (energyObject != null) 
        {
            GameObject energy = Instantiate(energyObject, transform.position, Quaternion.identity);
            Destroy(energy, 5f);
        }
        base.Die();
    }
}
