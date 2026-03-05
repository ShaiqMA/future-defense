using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int maxHealth = 3;
    public int currentHealth;
    public AudioClip shootSound;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    private bool isDead = false;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveX, moveY);
        rb.linearVelocity = movement * speed;

       

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            audioSource.PlayOneShot(shootSound);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;

        FindObjectOfType<GameManager>().UpdateHealthUI(currentHealth);
        StartCoroutine(FlashRed());

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    void Die()
    {
        isDead = true;
        speed = 0f;

        transform.rotation = Quaternion.Euler(0, 0, 90);

        Invoke(nameof(GameOver), 1f);
    }

    void GameOver()
    {
        FindObjectOfType<GameManager>().LoseGame();
    }
}
