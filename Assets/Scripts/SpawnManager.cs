using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    public GameObject _EnemyDad;
    [SerializeField]
    private GameObject _tripleShot, _shield, _speed;
    private bool _spawnObjects = true;
    [SerializeField]
    private int _buffToSpawn;
    

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(BuffSpawning());
    }

    
    IEnumerator SpawnRoutine()
    {
        while (_spawnObjects)
        {
            Instantiate(_enemyPrefab, new Vector3(Random.Range(-9.5f, 9.5f), 7, 0), Quaternion.identity, _EnemyDad.transform);
            yield return new WaitForSeconds(Random.Range(3, 7));
        }
    }

    IEnumerator BuffSpawning()
    {
        while (_spawnObjects)
        {
            _buffToSpawn = Random.Range(0, 3);
            yield return new WaitForSeconds(Random.Range(3, 8));
            if (_buffToSpawn == 0)
                Instantiate(_tripleShot, new Vector3(Random.Range(-9.5f, 9.5f), 7, 0), Quaternion.identity);
            else if (_buffToSpawn == 1)
                Instantiate(_shield, new Vector3(Random.Range(-9.5f, 9.5f), 7, 0), Quaternion.identity);
            else
                Instantiate(_speed, new Vector3(Random.Range(-9.5f, 9.5f), 7, 0), Quaternion.identity);
        }
    }

    public void StopSpawning()
    {
        _spawnObjects = false;
    }
}
