using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    public float speed = 7.5f;
    public Rigidbody2D theRB;

    public GameObject impactEffect;

    public int damageToGive = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.linearVelocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Instantiate(impactEffect, transform.position, transform.rotation); // Спавним эффект удара в месте попадания пули
        
        if (other.tag == "PlayerBullet") //
        {                               //
                                        //
        }                               //
        else                            //
        {                                //
            Destroy(gameObject); // Уничтожаем саму пулю

            AudioManager.instance.PlaySFX(4);    // звук Impact

            if (other.tag == "Enemy") // "Если у объекта, в который попала пуля, есть тэг "Enemy"
            {
                other.GetComponent<EnemyController>().DamageEnemy(damageToGive); // Вызываем у EnemyController метод DamageEnemy
            }
        }                                   //
    }

    void OnBecameInvisible() 
    {
        Destroy(gameObject); 
    }
}