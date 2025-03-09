using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healAmount = 1;

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
            PlayerHealthController.instance.HealPlayer(healAmount);

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(7); // звук Pickup Health
        }
        
    }
}