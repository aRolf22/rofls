 using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;
    
    public int currentHealth;
    public int maxHealth;

    public float damageInvincLength = 1f; // Сколько секунд будет длиться неуязвимость после получения урона
    private float invincCount;

    private void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }


    void Update()
    {
        if (invincCount > 0) 
        {
            invincCount -= Time.deltaTime;

            if (invincCount <= 0) 
            {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 1f);
            }
        }

    }

    public void DamagePlayer() 
    {
        if (invincCount <= 0) // Урон нанесётся только если invincCount уже добрался от значения damageInvincLength до 0 или меньше (т.е. если неуязвимость прошла)
        {
            AudioManager.instance.PlaySFX(11);  // звук Player Hurt

            invincCount = damageInvincLength;

            currentHealth--;

            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 0.45f);

            if (currentHealth <= 0) 
            {
                PlayerController.instance.gameObject.SetActive(false);

                UIController.instance.deathScreen.SetActive(true);
                AudioManager.instance.PlayGameOver();

                AudioManager.instance.PlaySFX(9);  // звук Player Death
            }


            // Обновляем инфу на UI при получении игроком урона
            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }

    public void MakeInvincible(float length) 
    {
        invincCount = length;
        PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 0.45f);
    } 

    public void HealPlayer(int healAmount) 
    {
        currentHealth += healAmount;
        
        if (currentHealth > maxHealth) 
        {
            currentHealth = maxHealth;
        }

        // Обновляем инфу на UI при получении игроком урона
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
