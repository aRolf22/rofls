using System.Diagnostics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Rigidbody2D theRB;
    public float moveSpeed;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    public Animator anim;

    public int health = 150;

    public GameObject[] splatterEffects; // Набор спрайтов, которые будут оставаться после смерти
    public GameObject hitEffect; // Эффект "кровотечения"

    // Стрельба
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public float shootRange;

    public SpriteRenderer theBody;


    void Start()
    {
        
    }


    void Update()
    {
        if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy) // Если враг рендерится (отображается хотя бы частично на экране, или во вьюпорте) И если игрок жив (не уничтожен)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer) // ищем игрока по экземпляру класса PlayerController - instance. Скорее всего это помешало бы мультиплееру, т.к. враги охотились бы только на один instance (игрока). Хотя можно было бы искать по имени/тэгу
            {
                moveDirection = (PlayerController.instance.transform.position - transform.position);
            }
            else 
            {
                moveDirection = Vector3.zero;
            }
            moveDirection.Normalize();

            theRB.linearVelocity = moveDirection * moveSpeed;


            // Стрельба
            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= shootRange) 
            {
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0) 
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);

                    AudioManager.instance.PlaySFX(13);    // звук Shoot2
                }
            }
        }
        else // иначе пусть враг стоит на месте
        {
            theRB.linearVelocity = Vector2.zero;
        }

        // Анимация ходьбы
        if (moveDirection != Vector3.zero) 
        {
            anim.SetBool("isMoving", true);
        }
        else 
        {
            anim.SetBool("isMoving", false);
        }
    }


    public void DamageEnemy(int damage) 
    {
        health -= damage;

        AudioManager.instance.PlaySFX(2);   // звук Enemy Hurt

        Instantiate(hitEffect, transform.position, transform.rotation);

        if (health <= 0) 
        {
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(1);    // звук Enemy Death

            int selectedSplatter = Random.Range(0, splatterEffects.Length); // Рандомим номер спрайта из массива с ними
            int rotationRandomMultiplier = Random.Range(0, 4); // Необязательная штука, но нужна, что рандомный спрайт еще и справнился с рандомным углом Rotation. Будем умножать текущее значение по Z рандомно на 0, 1, 2 или 3
            Instantiate(splatterEffects[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotationRandomMultiplier * 90f)); // Спавн рандомного спрайта


        }
    } 
}