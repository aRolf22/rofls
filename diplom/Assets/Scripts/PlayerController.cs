using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform gunArm;

    private Camera theCam;

    public Animator anim;

    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots; // кулдаун выстрела при зажатой ЛКМ
    private float shotCounter; 


    public static PlayerController instance; // Это экземпляр этого же скрипта. Приравниваем этот скрипт к этому instance, чтобы враг (EnemyController), пуля (EnemyBullet), хп (PlayerHealthController) могли легко найти игрока на карте
    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        theCam = Camera.main; // Оно само найдёт камеру на сцене, у которой будет тэг "MainCamera", а этот тэг у камеры по дефолту
    }


    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // transform.position += new Vector3 (moveInput.x, moveInput.y, 0) * Time.deltaTime * moveSpeed;

        theRB.linearVelocity = moveInput * moveSpeed;

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

    
    
        if (Input.GetMouseButtonDown(0)) 
        {
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShots; // без этой строчки будет появляться две пули почти одновременно. Одна в этом if, другая в следующем. Так что при создании этой пули сразу откатываем кулдаун
        }

        if (Input.GetMouseButton(0)) 
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0) 
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
            }
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

    }
}
