using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeead;

    public Transform target;
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
    }
    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget; // меняем таргет камеры на другую комнату
    }
}
