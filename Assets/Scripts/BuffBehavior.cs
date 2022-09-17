using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBehavior : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private AudioClip audioClip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -5)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name =="Player")
        {
            switch (this.name)
            {
                case "TripleShot_Buff(Clone)": 
                    collision.transform.GetComponent<Player>().ActivateTripleShot();
                    AudioSource.PlayClipAtPoint(audioClip, transform.position);
                    Destroy(this.gameObject); break;
                case "Speed_Buff(Clone)":
                    collision.transform.GetComponent<Player>().ActivateSpeedBuff();
                    AudioSource.PlayClipAtPoint(audioClip, transform.position);
                    Destroy(this.gameObject); break;
                case "Shield_Buff(Clone)":
                    collision.transform.GetComponent<Player>().ActivateShield();
                    AudioSource.PlayClipAtPoint(audioClip, transform.position);
                    Destroy(this.gameObject); break;
            }
        }
        
    }
}
