using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public Gun theGun;

    public float waitToBeCollected = 0.5f; // Задержка перед тем, как предмет можно будет подобрать

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waitToBeCollected > 0) 
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && waitToBeCollected <= 0) 
        {
            bool hasGun = false;
            foreach(Gun gunToCheck in PlayerController.instance.availableGuns)
            {
                if (theGun.weaponName == gunToCheck.weaponName) // Проверяем есть ли у игрока уже такое оружие
                {
                    hasGun = true;
                }
            }

            if (!hasGun)
            {
                Gun gunClone = Instantiate(theGun);
                gunClone.transform.parent = PlayerController.instance.gunArm; // делаем оружия дочерним объектом gunArm (которое является дочерним объектом Player)
                // Настройки, чтобы подобранное оружие нормально появлялось в руке:
                gunClone.transform.position = PlayerController.instance.gunArm.position;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale = Vector3.one;

                PlayerController.instance.availableGuns.Add(gunClone);
                PlayerController.instance.currentGun = PlayerController.instance.availableGuns.Count - 1; // Переключаемся на последнее значение в массиве доступных оружий, т.е. по факту - на последнее поднятое оружие
                PlayerController.instance.SwitchGun();
            }

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(7); // звук Pickup Health
        }
        
    }
}
