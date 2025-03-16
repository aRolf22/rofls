using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots; // кулдаун выстрела при зажатой ЛКМ
    private float shotCounter; 

    public string weaponName;
    public Sprite gunUI;

    public int itemCost; // Цена этого оружие в shop-комнате
    public Sprite gunShopSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {
            if (shotCounter > 0) 
            {
                shotCounter -= Time.deltaTime;
            }
            else 
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) 
                {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    shotCounter = timeBetweenShots; // без этой строчки будет появляться две пули почти одновременно. Одна в этом if, другая в следующем. Так что при создании этой пули сразу откатываем кулдаун
                    
                    AudioManager.instance.PlaySFX(12); // звук Shoot1
                }

                /*if (Input.GetMouseButton(0)) 
                {
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0) 
                    {
                        Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                        shotCounter = timeBetweenShots;
                        
                        AudioManager.instance.PlaySFX(12); // звук Shoot1
                    }
                }*/
            }
        }
    }
}
