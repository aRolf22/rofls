using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitToLoad = 4f; // задержка при переходе на уровень
    public string nextLevel; // название сцены уровня для его загрузки
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
    public IEnumerator LevelEnd() // непоятные еврейские фокусы для создания корутина (типо указывая IEnumerator, мы мождем делает паузу в выполение метода при помощи yield return )
    {
        AudioManager.instance.PlayerLevelWin(); // победная музыка
        PlayerController.instance.canMove = false;
        UIController.instance.StartFadeToBlack(); // затухание экрана
        yield return new WaitForSeconds(waitToLoad); // время чтобы наслаждиться победной музыкой
        SceneManager.LoadScene(nextLevel); // меняем сцену на след лвл
    }
}
