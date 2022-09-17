using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 36f;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;
    [SerializeField]
    AudioClip audioClip;

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Laser(Clone)")
        {
            Destroy(collision.gameObject);
            _spawnManager.StartSpawning();
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            Destroy(this.gameObject);
            Instantiate(_explosion, transform.position, Quaternion.identity);
        }
    }
}
