using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeead;

    public Transform target;

    public Camera mainCamera, bigMapCamera;

    private bool bigMapActive; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
  private void Awake()
    {
        instance = this;
       
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target !=null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeead * Time.deltaTime); // двигаем камеру на координаты комнаты, но игнорим ось z (если z камеры будет равен z комнаты, то камера войдет в текустуру комнаты и будет черный экран)
        }

        if (Input.GetKeyDown(KeyCode.M)) 
        {
            if (!bigMapActive)
            {
                ActivateBigMap();
            }
            else 
            {
                DeactivateBigMap();
            }
        }
    }
    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget; // меняем таргет камеры на другую комнату
    }

    public void ActivateBigMap() 
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive = true;

            bigMapCamera.enabled = true;
            mainCamera.enabled = false;

            PlayerController.instance.canMove = false; // Игрок не может двигаться, когда карта открыта

            Time.timeScale = 0f; // Чтобы враги не могли двигаться, пока карта открыта

            UIController.instance.mapDisplay.SetActive(false);
            UIController.instance.bigMapText.SetActive(true);
        }

    }

    public void DeactivateBigMap() 
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive = false;

            bigMapCamera.enabled = false;
            mainCamera.enabled = true;     

            PlayerController.instance.canMove = true;

            Time.timeScale = 1f;

            UIController.instance.mapDisplay.SetActive(true);
            UIController.instance.bigMapText.SetActive(false);
        }
    }
}
