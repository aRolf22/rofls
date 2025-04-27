using UnityEngine;
using UnityEngine.VFX;

public class Flamethrower : MonoBehaviour
{
    public VisualEffect flamethrower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flamethrower.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            flamethrower.Play();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            flamethrower.Stop();
        }
    }
}
