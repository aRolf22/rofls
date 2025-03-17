using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    public static CharacterTracker instance;
    public int currentHealth, maxHealth, currentCoins;
    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
