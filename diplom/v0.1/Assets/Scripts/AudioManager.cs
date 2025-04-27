using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource levelMusic, gameOverMusic, winMusic;

    public AudioSource[] sfx;

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

    public void PlayGameOver() 
    {
        levelMusic.Stop();

        gameOverMusic.Play();
    }

    public void PlayerLevelWin() 
    {
        levelMusic.Stop();

        winMusic.Play();
    }

    public void PlaySFX(int sfxToPlay) 
    {
        sfx[sfxToPlay].Stop(); // на случай если тот же звук вызывается несколько раз почти одновременно (например сломали 2 ящика сразу) - будем прерывать предыдущий звук и сразу начинать его сначала, чтобы они не перекрывали друг друга   
        sfx[sfxToPlay].Play();
    }
}
