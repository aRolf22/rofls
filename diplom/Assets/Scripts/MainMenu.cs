using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToload;
    public GameObject deletePanel;

    public CharacterSelector[] charactersToDelete;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(levelToload);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DeleteSave()
    {
        deletePanel.SetActive(true);
    }

    public void ConfirmDelete()
    {
        deletePanel.SetActive(false);

        foreach(CharacterSelector theChar in charactersToDelete)
        {
            PlayerPrefs.SetInt(theChar.playerToSpawn.name, 0);
        }

    }

    public void CancelDelete()
    {
        deletePanel.SetActive(false);
    }
}