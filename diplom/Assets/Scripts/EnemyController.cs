using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Rigidbody2D theRB;
    public float moveSpeed;

    // для Skelet и Blob (движение за игроком)
    [Header("Chase Player")]
    public bool shouldChasePlayer;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    // для Coward (движение от игрока)
    [Header("Run away")]
    public bool shouldRunAway;
    public float runawayRange;

    // для Blob (рандомное движение по комнате пока не встретит игрока)
    [Header("Wandering")]
    public bool shouldWander;
    public float wanderLength, pauseLength;
    private float wanderCounter, pauseConunter;
    private Vector3 wanderDirection;

    // для Fire (движение по маршруту (патруль))
    [Header("Patrolling")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    [Header("Shooting")]
    // Стрельба
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public float shootRange;

    [Header("Variables")]
    public SpriteRenderer theBody;

     public Animator anim;

    public int health = 150;

    public GameObject[] splatterEffects; // Набор спрайтов, которые будут оставаться после смерти
    public GameObject hitEffect; // Эффект "кровотечения"


    void Start()
    {
        if(shouldWander)
        {
             pauseConunter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
        }
    }


    void Update()
    {
        if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy) // Если враг рендерится (отображается хотя бы частично на экране, или во вьюпорте) И если игрок жив (не уничтожен)
        {
            moveDirection = Vector3.zero;

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer && shouldChasePlayer) // ищем игрока по экземпляру класса PlayerController - instance. Скорее всего это помешало бы мультиплееру, т.к. враги охотились бы только на один instance (игрока). Хотя можно было бы искать по имени/тэгу
            {
                moveDirection = (PlayerController.instance.transform.position - transform.position);
            } else
            {
                if(shouldWander)
                {
                    if(wanderCounter > 0)
                    {
                        wanderCounter -= Time.deltaTime;
                        
                        // move the enemy
                        moveDirection = wanderDirection;

                        if (wanderCounter <= 0)
                        {
                            pauseConunter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
                        }
                    }

                    if(pauseConunter > 0)
                    {
                        pauseConunter -= Time.deltaTime;
                        
                        if(pauseConunter <= 0)
                        {
                            wanderCounter = Random.Range(wanderLength * .75f, wanderLength * 1.25f);

                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                        }

                    }
                }
                
                if(shouldPatrol)
                {
                    moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;

                    if(Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < .2f)
                    {
                        currentPatrolPoint++;
                        if(currentPatrolPoint >= patrolPoints.Length)
                        {
                            currentPatrolPoint = 0;
                        }
                    }
                }
            }

            if(shouldRunAway && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < runawayRange)
            {
                moveDirection = transform.position - PlayerController.instance.transform.position;
            }
            
            /*else 
            {
                moveDirection = Vector3.zero;
            }*/


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