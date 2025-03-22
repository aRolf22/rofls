using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public BossAction[] actions;
    private int currentAction;
    private float actionCounter;

    private float shotCounter;
    private Vector2 moveDirection;
    public Rigidbody2D theRB;

    public int currentHealth;

    public GameObject deathEffect, hitEffect;
    public GameObject levelExit;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actionCounter = actions[currentAction].actionLenght;
    }

    // Update is called once per frame
    void Update()
    {
        if (actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;

            //handel movement
            moveDirection = Vector2.zero;

            if(actions[currentAction].shouldMove)
            {
                if(actions[currentAction].shoudChasePlayer)
                {
                    moveDirection = PlayerController.instance.transform.position - transform.position;
                    moveDirection.Normalize();
                }

                if(actions[currentAction].moveToPoint)
                {
                    moveDirection = actions[currentAction].pointToMoveTo.position - transform.position;
                    
                }

            }

            theRB.linearVelocity = moveDirection * actions[currentAction].moveSpeed;

            //handel shooting
            if(actions[currentAction].shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter <= 0)
                {
                    shotCounter = actions[currentAction].timeBetweenShots;

                    foreach(Transform t  in actions[currentAction].shotPoints)
                    {
                        Instantiate(actions[currentAction].itemToShoot, t.position, t.rotation);
                    }

                }
            }

        }else 
        {
            currentAction++;
            if(currentAction >= actions.Length)
            {
                currentAction = 0;
            }

            actionCounter = actions[currentAction].actionLenght;
        }
    }


    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);

            Instantiate(deathEffect, transform.position, transform.rotation);

            if(Vector3.Distance(PlayerController.instance.transform.position, levelExit.transform.position) <2f)
            {
                levelExit.transform.position += new  Vector3(4f, 0f, 0f);
            }

            levelExit.SetActive(true);
        }
    }

}

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    
    public float actionLenght;

    public bool shouldMove;
    public bool shoudChasePlayer;
    public float moveSpeed;
    public bool moveToPoint;
    public Transform pointToMoveTo;

    public bool shouldShoot;
    public GameObject itemToShoot;
    public float timeBetweenShots;
    public Transform[] shotPoints;




}
