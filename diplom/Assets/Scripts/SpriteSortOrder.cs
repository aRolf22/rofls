using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{

    private SpriteRenderer theSR;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();

        theSR.sortingOrder = Mathf.RoundToInt(transform.position.y * -10f); // чем выше по Y объект, тем меньше у него sortingOrder => он будет на более глубоком слое
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
