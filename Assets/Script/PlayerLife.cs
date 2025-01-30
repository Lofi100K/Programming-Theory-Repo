using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proiettile
    public Transform shootPoint;        // Punto di sparo (posizione iniziale del proiettile)
    public float projectileSpeed = 10f; // Velocità del proiettile
    public float projectileLifetime = 3f; // Durata del proiettile prima di essere distrutto
    public float cooldownTime = 0.5f;   // Tempo di cooldown tra due spari

    private float cooldownTimer = 0f;   // Timer per il cooldown


    public float moveSpeed = 5f; // Velocità di movimento del giocatore
    public float xLimit = 14f;   // Limite su X
    public float zLimit = 14f;   // Limite su Z

    // Update è chiamato una volta per frame
    void Update()
    {
        ShootingControl();
        MovingControl();
    }

    void ShootingControl()
    {
        // Aggiorna il timer del cooldown
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Controlla i tasti per sparare solo se il cooldown è terminato
        if (cooldownTimer <= 0f)
        {
            if (Input.GetKey(KeyCode.J)) // Sinistra
            {
                ShootProjectile(Vector3.left);
            }
            if (Input.GetKey(KeyCode.L)) // Destra
            {
                ShootProjectile(Vector3.right);
            }
            if (Input.GetKey(KeyCode.I)) // Su
            {
                ShootProjectile(Vector3.forward);
            }
            if (Input.GetKey(KeyCode.K)) // Giù
            {
                ShootProjectile(Vector3.back);
            }
        }

    }
    void ShootProjectile(Vector3 direction)
    {
        // Crea un nuovo proiettile
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Ottieni il Rigidbody del proiettile
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Aggiungi velocità al proiettile nella direzione specificata
        projectileRb.velocity = direction * projectileSpeed;

        // Distruggi il proiettile dopo pochi secondi
        Destroy(projectile, projectileLifetime);

        // Imposta il cooldown
        cooldownTimer = cooldownTime;
    }
    void MovingControl()
    {
        // Ottieni l'input per gli assi orizzontale (X) e verticale (Z)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Crea un vettore di direzione in base all'input
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Muovi il giocatore
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Limita la posizione del giocatore su X
        float clampedX = Mathf.Clamp(transform.position.x, -xLimit, xLimit);

        // Limita la posizione del giocatore su Z
        float clampedZ = Mathf.Clamp(transform.position.z, -zLimit, zLimit);

        // Imposta la posizione limitata
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0;
        }
    }
}

