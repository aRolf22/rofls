using UnityEngine;

public class CharacterSelector : MonoBehaviour
{

    private bool canSelect;

    public GameObject message;

    public PlayerController playerToSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSelect) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 playerPos = PlayerController.instance.transform.position;

                Destroy(PlayerController.instance.gameObject);

                PlayerController newPlayer = Instantiate(playerToSpawn, playerPos, playerToSpawn.transform.rotation);
                PlayerController.instance = newPlayer;

                gameObject.SetActive(false); // скрываем селектор персонажа, после его выбора

                CameraController.instance.target = newPlayer.transform;

                CharacterSelectManager.instance.activePlayer = newPlayer; // Запомнили за какого персонажа играем теперь (по дефолту Player - Sneaker)
                CharacterSelectManager.instance.activeCharSelect.gameObject.SetActive(true); // Делаем видимым селектор персонажа, за которого игрок играл до смены персонажа
                CharacterSelectManager.instance.activeCharSelect = this;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canSelect = true;
            message.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canSelect = false;
            message.SetActive(false);
        }
    }
}
