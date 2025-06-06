//using System.Drawing;
//using NUnit.Framework.Constraints;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class cursorPulseClick : MonoBehaviour
{
    [SerializeField] float pulseSize = 1.5f;
    [SerializeField] float returnSpeed;
    private Vector3 startSize;

    [SerializeField] Vector3 maxSizeToClick = new Vector3(1.05f, 1.05f, 1.05f); // Сложность попадания в такт

    void Start()
    {
        startSize = transform.localScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startSize, Time.deltaTime * returnSpeed);

        if (Input.GetMouseButtonDown(0) && transform.localScale.x >= startSize.x && transform.localScale.x <= maxSizeToClick.x)
        {
            //this.GetComponent<Image>().color = Color.red;
            Pulse();
        }
        
    }

    public void Pulse()
    {
        transform.localScale = startSize * pulseSize;
    }
}
