//using System.Drawing;
//using NUnit.Framework.Constraints;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class environmentPulse : MonoBehaviour
{
    [SerializeField] float pulseSize = 1.5f;
    [SerializeField] float returnSpeed;
    private Vector3 startSize;

    void Start()
    {
        startSize = transform.localScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startSize, Time.deltaTime * returnSpeed);     
    }

    public void Pulse()
    {
        transform.localScale = startSize * pulseSize;
    }
}
