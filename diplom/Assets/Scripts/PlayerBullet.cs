using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D theRB;

    public GameObject impactEffect;
    public int damageToGive = 50;

    // Время жизни пули
    public float lifetime = 2.0f; // Время в секундах
    private float timeAlive;

    void Start()
    {
        timeAlive = 0f; // Инициализация времени жизни
    }

    void Update()
    {
        theRB.linearVelocity = transform.right * speed;

        // Увеличиваем время жизни
        timeAlive += Time.deltaTime;

        // Если время жизни превышает заданное, уничтожаем пулю
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(4);

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        }

        if (other.tag == "Boss")
        {
            BossController.instance.TakeDamage(damageToGive);
            Instantiate(BossController.instance.hitEffect, transform.position, transform.rotation);
        }
    }

    void OnBecameInvisible() 
    {
        Destroy(gameObject); 
    }
}