using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitToLoad = 4f; // задержка при переходе на уровень
    public string nextLevel; // название сцены уровня для его загрузки
    public bool isPaused;
    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
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
    
}
