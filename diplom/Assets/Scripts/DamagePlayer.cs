using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) // Тригеры для объектов, через которые можно пройти (ставим галочку is Trigger в компоненте коллайдера)
    {
        if (other.tag == "Player") 
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.tag == "Player") 
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }


    private void OnCollisionEnter2D(Collision2D other) // Коллизия для объектов, через которые нельзя можно пройти
    {
        if (other.gameObject.tag == "Player") 
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }
}
