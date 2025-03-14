using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;

    public float waitToBeCollected;


    void Start()
    {
        
    }

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
            LevelManager.instance.GetCoins(coinValue);

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(5); // звук Pickup Coin
        }
    }
}
