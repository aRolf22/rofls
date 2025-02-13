using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; // Это экземпляр этого же скрипта. Приравниваем этот скрипт к этому instance, чтобы враг (EnemyController), пуля (EnemyBullet), хп (PlayerHealthController) и прочие могли легко найти игрока на карте
    
    
    public float moveSpeed; // moveSpeed - это константа для обычной скорости игрока
    

    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform gunArm;

    private Camera theCam;

    public Animator anim;

    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots; // кулдаун выстрела при зажатой ЛКМ
    private float shotCounter; 

    public SpriteRenderer bodySR;

    private float activeMoveSpeed; // activeMoveSpeed - именно эту скорость игрока будем использовать для передвижения (изменение velocity) и менять её в опр. ситуациях. Например когда хотим сделать дэш, мы её приравняем до dashSpeed, а потом вернемся к значению moveSpeed  
    public float dashSpeed = 8f; // Скорость дэша
    public float dashLength = 0.5f; // Длительность дэша (типа длительность эффекта скорости. Но если и менять этот параметр слишком значительно, то придется ещё и анимацию поправлять)
    public float dashCooldown = 1f; // кд дэша
    public float dashInvincibility = 0.5f; // Длительность эффекта неуязвимости при вызове дэша
    [HideInInspector] // Это значит, что public переменные снизу не будут отображаться в инспекторе в юнити. Нам надо, чтобы dashCounter мог вызываться в скрипте Breakables, но не надо, чтобы мы могли случайно его изменить в инспекторе
    public float dashCounter;
    private float dashCoolCounter;

    [HideInInspector] // ну выше ты уже написал зачем его юзают
    public bool canMove = true; // юзаем это чтобы не давать игроку двигаться (конкретно щас при выходе, реализовано в levelmanager в IEnumerator ставим false)
    
    
    
    

    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        theCam = Camera.main; // Оно само найдёт камеру на сцене, у которой будет тэг "MainCamera", а этот тэг у камеры по дефолту

        activeMoveSpeed = moveSpeed;
    }


    void Update()
    {   
        if(canMove)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();

            // transform.position += new Vector3 (moveInput.x, moveInput.y, 0) * Time.deltaTime * moveSpeed;

            theRB.linearVelocity = moveInput * activeMoveSpeed;

            Vector3 mousePos = Input.mousePosition; // координаты курсора
            Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition); // Переводим положение игрока в мире -> положение игрока в окне игры

            // Отзеркаливаем игрока, если координаты мыши левее координат игрока.
            if (mousePos.x < screenPoint.x) 
            {
                transform.localScale = new Vector3(-1, 1, 1);
                gunArm.localScale = new Vector3(-1, -1, 1);
            }
            else 
            {
                transform.localScale = Vector3.one;
                gunArm.localScale = Vector3.one;
            }


            // rotate gun arm
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y); // Расстояние между игроком и курсором
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg; // Математическая формула для расчета угла между ними
            gunArm.rotation = Quaternion.Euler(0, 0, angle); // Поворот gunArm

        

            // Стрельба
            if (Input.GetMouseButtonDown(0)) 
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots; // без этой строчки будет появляться две пули почти одновременно. Одна в этом if, другая в следующем. Так что при создании этой пули сразу откатываем кулдаун
                
                AudioManager.instance.PlaySFX(12); // звук Shoot1
            }

            if (Input.GetMouseButton(0)) 
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0) 
                {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    shotCounter = timeBetweenShots;
                    
                    AudioManager.instance.PlaySFX(12); // звук Shoot1
                }
            }


            // dash
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0) // Если кд откатился и дэш сейчас уже закончен/неактивен
                {
                    activeMoveSpeed = dashSpeed;    
                    dashCounter = dashLength;
                    
                    anim.SetTrigger("dash");
                    
                    PlayerHealthController.instance.MakeInvincible(dashInvincibility);

                    AudioManager.instance.PlaySFX(8); // звук Player Dash
                }
            }

            if (dashCounter > 0) //  По сути - Если дэш активирован 
            {
                dashCounter -= Time.deltaTime;  
                if (dashCounter <= 0) // Если действия дэша кончилось 
                {
                    activeMoveSpeed = moveSpeed; // Возвращаемся к нормальной скорости
                    dashCoolCounter = dashCooldown; // Заводим кд
                }
            }
            if (dashCoolCounter > 0) 
            {
                dashCoolCounter -= Time.deltaTime; // Уменьшение кд
            }



            // Анимация ходьбы
            if (moveInput != Vector2.zero) 
            {
                anim.SetBool("isMoving", true);
            }
            else 
            {
                anim.SetBool("isMoving", false);
            }
        } else { // делаем так чтобы микрочелик переставал двигаться в том направление, в котором  двигался до паузу (yield return new WaitForSeconds(waitToLoad); (это в levelmanager) )
            theRB.linearVelocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
    }
}
