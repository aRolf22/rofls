using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitToLoad = 4f; // задержка при переходе на уровень
    public string nextLevel; // название сцены уровня для его загрузки
    public bool isPaused;

    public int currentCoins;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;

        UIController.instance.coinText.text = currentCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }



        // ЧИТ ДЛЯ ДЕБАГА - Получить 100 монет
        if (Input.GetKeyDown(KeyCode.Alpha9)) 
        {
            GetCoins(100);
        }
    }
    public IEnumerator LevelEnd() //для создания корутина (типо указывая IEnumerator, мы можем делать паузу в выполение метода при помощи yield return )
    {
        AudioManager.instance.PlayerLevelWin(); // победная музыка
        PlayerController.instance.canMove = false;
        UIController.instance.StartFadeToBlack(); // затухание экрана
        yield return new WaitForSeconds(waitToLoad); // время чтобы насладиться победной музыкой
        SceneManager.LoadScene(nextLevel); // меняем сцену на след лвл
    }

    public void PauseUnpause()
    {
        if(!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        } else
        {
            UIController.instance.pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    } 
    
    public void GetCoins(int amount) 
    {
        currentCoins += amount;
               
        // Обновляем инфу на UI при получении монет
        UIController.instance.coinText.text = currentCoins.ToString();
    }

    public void SpendCoins(int amount) 
    {
        currentCoins -= amount;

        if (currentCoins <= 0) 
        {
            currentCoins = 0;
        }

        // Обновляем инфу на UI при трате монет
        UIController.instance.coinText.text = currentCoins.ToString();
    }
    
}