using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{

    public static CharacterSelectManager instance;

    public PlayerController activePlayer;
    public CharacterSelector activeCharSelect;

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
