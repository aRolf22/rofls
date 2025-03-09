using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public GameObject buyMessage;
    private bool inBuyZone;
    public bool isHealthRestore, isHealthUpgrade, isWeapon;
    public int itemCost;
    public int healthUpgradeAmout;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inBuyZone)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(LevelManager.instance.currentCoins >= itemCost)
                {
                    LevelManager.instance.SpendCoins(itemCost);

                    if(isHealthRestore)
                    {
                        PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
                    }
                    
                    if (isHealthUpgrade)
                    {
                        PlayerHealthController.instance.IncreaseMaxHealth(healthUpgradeAmout);
                    }
                    
                    gameObject.SetActive(false);
                    inBuyZone = false;
                    AudioManager.instance.PlaySFX(18);
                }else
                {
                    AudioManager.instance.PlaySFX(19);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            buyMessage.SetActive(true);
            
            inBuyZone = true;
        }
    }

        private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            buyMessage.SetActive(false);
            
            inBuyZone = false;
        }
    }
}
