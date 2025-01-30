using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBig : EnemyBase
{
    // INHERITANCE
    // ABSTRACTION
    public override void FollowPlayer(Transform player)
    {
        // Ottieni la posizione corrente del nemico
        Vector3 currentPosition = transform.position;

        // Ottieni la posizione del giocatore
        Vector3 targetPosition = player.position;

        // Calcola la nuova posizione spostandoti verso il giocatore
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, GetSpeed() * Time.deltaTime);
        // Ruota il nemico verso il giocatore (opzionale)
        Vector3 direction = (targetPosition - currentPosition).normalized;
        // Calcola la distanza tra il nemico e il giocatore
        float distanceToPlayer = Vector3.Distance(currentPosition, targetPosition);

        // Soglia per eseguire azioni specifiche (puoi personalizzare questo valore)
        float distanceThreshold = 5.0f;

        // Controlla se il nemico è entro il raggio specificato
        if (distanceToPlayer <= distanceThreshold)
        {
            SetSpeed(2.0f);
            SetPoints(15);
        }
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
