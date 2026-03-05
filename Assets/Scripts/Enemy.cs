using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Enemy : MonoBehaviour
{
    //Snelheid variabel
    public float speed = 2f;

    //Health Variabelen
    public int maxHealth = 3;
    public int currentHealth;
    public Image healthBar;

    //Audio Variabelen
    public AudioClip hitSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    //Object Ophalen
    Transform player;

    //Start Functie: alles wat hierin gebeurt gebeurt aan het begin van het spel
    void Start()
    {
       currentHealth = maxHealth;
       player = GameObject.FindGameObjectWithTag("Player").transform; 
       audioSource = GetComponent<AudioSource>();
    }

    //Update Functie: Tijdens het spel
    void Update()
    {
       transform.position = Vector2.MoveTowards(
           transform.position,
           player.position,
           speed * Time.deltaTime
       );
    }

    //TakeDamage Functie: als speler enemy raakt krijgt enemy damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        audioSource.PlayOneShot(hitSound);

        if(healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }

        if(currentHealth <= 0)
        {
            audioSource.PlayOneShot(deathSound);
            Destroy(gameObject, 0.15f);
            GameManager.Instance.AddScore(100);

        }
    }
}
