using UnityEngine;

public class LevelExit : MonoBehaviour
{
    // public string levelToLoad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // SceneManager.LoadScene(levelToLoad); теперь это работает через LevelManager
            StartCoroutine(LevelManager.instance.LevelEnd()); // Запускаем корутин, который создается  при помощи IEnumerator в levelmanager
        }
    }
}
