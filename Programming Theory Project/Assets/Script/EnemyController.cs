using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed; // Velocità del nemico
    public int points; // Punti assegnati al nemico
    private Transform player; // Riferimento al giocatore

    private EnemyBase enemy;

    void Start()
    {
        // Ottieni il componente della sottoclasse EnemyType1
        enemy = GetComponent<EnemyBase>();
        enemy.Initialize(speed, points);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (enemy != null && player != null)
        {
            // Chiama il metodo FollowPlayer per far seguire il giocatore
            enemy.FollowPlayer(player);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Se il nemico collide con il giocatore
        if (collision.gameObject.CompareTag("projectile"))
        {
            // Distruggi il nemico
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}

