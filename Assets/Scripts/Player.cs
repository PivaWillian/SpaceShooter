using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab, _triplePrefab;
    [SerializeField]
    private GameObject LeftDmg, RightDmg;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject Shield;
    [SerializeField]
    private UI_Manager _UIManager;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserSound, _explosionSound;
    
    private int _lives = 3;
    public int score = 0;
    private float _fireRate = 0.5f;
    private float _canFire = -0.1f;
    public float speed;
    private float lastTimeDmgd = 0f;
    private bool isTripleShotEnabled = false;
    private bool isSpeedBuffActive = false;
    private bool isShieldActive = false;

    
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
        
    void Update()
    {
        MovementControl();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Fire();
        }
    }

    private void MovementControl()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);

        if (transform.position.y >= 0.5f)
        {
            transform.position = new Vector3(transform.position.x, 0.5f, 0);
        }
        else if (transform.position.y <= -3.5f)
        {
            transform.position = new Vector3(transform.position.x, -3.5f, 0);
        }

        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }

    private void Fire()
    {
        _canFire = Time.time + _fireRate;
        if (!isTripleShotEnabled)
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        else
            Instantiate(_triplePrefab, transform.position, Quaternion.identity);
            
        _audioSource.clip = _laserSound;
        _audioSource.Play();
    }

    public void Damage()
    {
        if(lastTimeDmgd < Time.time)
        {
            if (isShieldActive == true)
            {
                Shield.SetActive(false);
                isShieldActive = false;
                return;
            }

            if (_lives > 0)
                _lives--;
            else
            {
                _spawnManager.StopSpawning();
                _audioSource.clip = _explosionSound;
                _audioSource.Play();
                Destroy(this.gameObject, .4f);
                _UIManager.GameOver();
            }

            if (_lives < 3)
                RightDmg.SetActive(true);
            if (_lives < 2)
                LeftDmg.SetActive(true);
            _UIManager.UpdateLives(_lives);
            lastTimeDmgd = Time.time + 1.5f;
        }
    }

    public void ActivateTripleShot() 
    {
        isTripleShotEnabled = true;
        StartCoroutine(DisableTripleShot());
    }

    IEnumerator DisableTripleShot()
    {
        yield return new WaitForSeconds(5);
        isTripleShotEnabled = false;
    }

    public void ActivateSpeedBuff()
    {
        if (!isSpeedBuffActive)
        {
            isSpeedBuffActive = true;
            speed *= 2;
            StartCoroutine(DisableSpeedBuff());
        }
    }

    public void ActivateShield()
    {
        Shield.SetActive(true);
        isShieldActive = true;
    }

    IEnumerator DisableSpeedBuff()
    {
      
        yield return new WaitForSeconds(8);
        speed /= 2;
        isSpeedBuffActive = false;
    }

    public void UpdateScore()
    {
        score += 10;
    }
}
