using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    private float speed; // Velocità del nemico
    private int points; // Punti assegnati al nemico  

    // Evento statico per notificare la distruzione del nemico
    public static event System.Action<int> OnEnemyDestroyed;

    // ENCAPSULATION
    public void SetSpeed(float value)
    {
        if (value <= 0)
        {
            speed = 0;
        }
        else
        {
            speed = value;
        }

    }

    public float GetSpeed()
    {
        return speed;
    }

    public int GetPoints()
    {
        return points;
    }

    public void SetPoints(int value)
    {
        if (value <= 0)
        {
            points = 0;
        }
        else
        {
            points = value;
        }
    }
    // ABSTRACTION
    public abstract void FollowPlayer(Transform player);
    // Metodo per configurare il nemico
    public  void Initialize(float speed, int points)
    {
        SetSpeed(speed);
        SetPoints(points);  

    }
    

    private void OnDestroy()
    {
        // Emetti l'evento notificando i punti di questo nemico
        OnEnemyDestroyed?.Invoke(points);
    }
}
