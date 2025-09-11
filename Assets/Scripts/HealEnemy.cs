using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEnemy : Enemy
{
    [SerializeField] private float healValue = 20f;
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
        HealPlayer();
        base.Die();
    }

    private void HealPlayer()
    {
        if (player != null)
        {
            player.Heal(healValue);
        }

    }
}
