using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float maxHp = 200f;
    [SerializeField] private Image hpBar;
    protected float currentHp;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();    
    }
    void Start()
    {
        currentHp = maxHp;
        UpdateHpBar();
    }

   
    void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            gameManager.PauseGameMenu();
        }


    }

    void MovePlayer()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = playerInput.normalized * moveSpeed;
        if (playerInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (playerInput != Vector2.zero)
        {
            animator.SetBool("IsRun", true);
        }
        else 
        {
            animator.SetBool("IsRun", false);   
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Usb"))
        {
            gameManager.GameWinMenu();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Energy"))
        {
            gameManager.AddEnergy();
            Destroy(collision.gameObject);
            audioManager.PlayEnergySound();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            Die();
        }

    }

    public void Heal(float healValue) 
    {
        if (currentHp < maxHp)
        {
            currentHp += healValue;
            currentHp = Mathf.Min(currentHp, maxHp);
            UpdateHpBar() ; 
        }   
    }


    private void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp/maxHp;
        }
    }

    
    private void Die()
    {
        gameManager.GameOverMenu();
    }
}
