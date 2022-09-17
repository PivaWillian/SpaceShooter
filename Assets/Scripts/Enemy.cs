using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 3f;
    private Player player;
    Animator _animator;
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private GameObject enemyLaser;
    Collider2D collider2D;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
        StartCoroutine(Firing());
    }

    void Update()
    {
        ManageMovement();
        
    }

    private void ManageMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -5.5f)
        {
            float randomX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
    }

    private IEnumerator  Firing()
    {
        while (true)
        {
            GameObject enemyFire = Instantiate(enemyLaser, transform.position, Quaternion.identity);
            Laser[] lasers = enemyFire.GetComponentsInChildren<Laser>();
            foreach (Laser laser in lasers)
            {
                laser.CheckEnemy();
            }
            yield return new WaitForSeconds(Random.Range(4, 7));
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.name == "Player")
        {
            if(player!=null)
                player.Damage();
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(collider2D);
            Destroy(this.gameObject, 2f);
        }
        if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if(player!=null)
                player.UpdateScore();
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(collider2D);
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            Destroy(this.gameObject, 2f);
        }
    }
}
