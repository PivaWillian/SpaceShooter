using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;
    private bool isEnemy = false;
    
    void Update()
    {
        if (!isEnemy)
        {
            PlayerLaserMovement();
        }
        else
        {
            EnemyLaserMovement();
        }
    }

    private void PlayerLaserMovement()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.y > 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void EnemyLaserMovement()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isEnemy)
        {
            collision.transform.GetComponent<Player>().Damage();
        }
        
    }
    public void CheckEnemy()
    {
        isEnemy = true;
    }
}
