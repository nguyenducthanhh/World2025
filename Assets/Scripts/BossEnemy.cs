using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossEnemy : Enemy
{
    [SerializeField] private GameObject BulletPrefabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedDanThuong = 35f;
    [SerializeField] private float speedDanVongTron = 20f;
    [SerializeField] private float speedDanSay = 25f;
    [SerializeField] private float hpValue = 100f;
    [SerializeField] private GameObject miniEnemy; 
    [SerializeField] private float skillCoolDown = 3f;
    [SerializeField] private GameObject usbPrefabs;
    private float nextSkillTime = 0f;

    Coroutine dameOverTimeCoroutine;
   
    protected override void Update()
    {
        base.Update();
        if (Time.time >= nextSkillTime)
        {
            SuDungSkill();
        }
    }

    protected override void Die()
    {
        Instantiate(usbPrefabs, transform.position, Quaternion.identity);
        base.Die();

    }
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

    private void BanDanThuong()
    {
        if(player != null)
        {
            Vector3 directionToPlayer = player.transform.position - firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(BulletPrefabs, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetmovementDirection(directionToPlayer * speedDanThuong);
        }
    }

    private void BanDanVongTron() 
    {
        
       StartCoroutine(BanDanVongTronCoroutine());

    }

    IEnumerator BanDanVongTronCoroutine()
    {
        const int bulletCount = 24;
        float angleStep = 360f / bulletCount;
        for (int i = 0; i<3; i++)
        {
            for (int j = 0; j < bulletCount; j++)
            {
                float angle = j * angleStep;
                Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
                GameObject bullet = Instantiate(BulletPrefabs, transform.position, Quaternion.identity);
                EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
                enemyBullet.SetmovementDirection(bulletDirection*speedDanVongTron);
            }
            yield return new WaitForSeconds(1f);
        }
      
    }
    private void HoiMau(float hpAmount) 
    {
        currentHp = Mathf.Min(currentHp+hpAmount, maxHp);
        UpdateHpBar();
    }

    private void SinhMiniEnemy()
    {
      StartCoroutine(SpawnMiniEnemy());    
    }

    IEnumerator SpawnMiniEnemy()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(miniEnemy, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    private void DichChuyen() 
    { 
        if(player != null)
        {
            transform.position = player.transform.position;
        }
    }

    private void SayDan()
    {
        if (player != null)
        {

            StartCoroutine(SayDanCoroutine());

        }
    }

    IEnumerator SayDanCoroutine()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 directionToPlayer = player.transform.position - firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(BulletPrefabs, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetmovementDirection(directionToPlayer * speedDanSay);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void ChonSkillNgauNhien()
    {
        int randomSkill = Random.Range(0, 6);
        switch (randomSkill) 
        {
            case 0:
                BanDanThuong();
                break;
            case 1:
                BanDanVongTron();
                break;
            case 2:
                HoiMau(hpValue);
                break;  
            case 3:
                SinhMiniEnemy();
                break;
            case 4:
                DichChuyen();
                break;  
            case 5:
                SayDan();
                break;

        }

    }

    private void SuDungSkill()
    {
        nextSkillTime = Time.time + skillCoolDown;
        ChonSkillNgauNhien();
    }

}
