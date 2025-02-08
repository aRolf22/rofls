using UnityEngine;

public class Breakables : MonoBehaviour
{

    public GameObject[] brokenPieces;
    public int maxPieces = 5;

    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent; // Шанс выпадения дропа


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Smash() // Ломаем ящик
    {
        Destroy(gameObject);

        AudioManager.instance.PlaySFX(0); // нулевой звук в массиве это звук разрушения коробок. По сути это нежелательный хардкод, но пусть пока будет так. Если будут разные звуки, то лучше будет создать отдельную переменную типа breakSound и там указывать номер звука

        // show broken pieces
        int piecesToDrop = Random.Range(1, maxPieces);
        for(int i = 0; i < piecesToDrop; i++) 
        {
            int randomPiece = Random.Range(0, brokenPieces.Length);
            Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
        }

        // drop items
        if (shouldDropItem) 
        {
            float dropChance = Random.Range(0f, 100f); // крутим рулетку от 0 до 100
                    
            if (dropChance < itemDropPercent) // Если на рулетке выпало значение от 0 до itemDropPercent, то дропаем предмет
            {
                int randomItem = Random.Range(0, itemsToDrop.Length);
                Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player") 
        {
            if (PlayerController.instance.dashCounter > 0) // Если игрок в дэше
            {
                Smash();
            }
        }

        if (other.tag == "PlayerBullet") 
        {
            Smash();
        }
    }
}
