using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //direction = PlayerController.instance.transform.position - transform.position;
        //direction.Normalize();
        direction = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;

        if(!BossController.instance.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player") 
        {
            PlayerHealthController.instance.DamagePlayer();
        }

        Destroy(gameObject);
 
        AudioManager.instance.PlaySFX(4);    // звук Impact
    }

    private void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}