using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Camera cam;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public GameObject StartVFX;
    public GameObject EndVFX;

    private Quaternion rotation;
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FillLists();
        DisbleLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            EnableLaser();
        }
        
        if(Input.GetButton("Fire1"))
        {
            UpdateLaser();
        }

        if(Input.GetButtonUp("Fire1"))
        {
            DisbleLaser();
        }
        
        RotateToMouse();
    }
    
    void EnableLaser()
    {
        lineRenderer.enabled = true;
           for (int i = 0; i < particles.Count; i++)
            particles[i].Play();
    }

    void UpdateLaser()
    {   
        var mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);

        lineRenderer.SetPosition(0, (Vector2)firePoint.position);
        StartVFX.transform.position = (Vector2)firePoint.position;

        lineRenderer.SetPosition(1, mousePos);

        Vector2 direction = mousePos - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude); 
        
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
           
        }
        EndVFX.transform.position = lineRenderer.GetPosition(1);
    }

    void DisbleLaser()
    {
        lineRenderer.enabled = false;
        for (int i = 0; i < particles.Count; i++)
            particles[i].Stop();
    }

    void RotateToMouse()
    {
        Vector2 direction = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = rotation;
    }

    void FillLists()
    {
        for (int i = 0; i < StartVFX.transform.childCount; i++)
        {
            var ps = StartVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)

                particles.Add(ps);
        }

        for (int i = 0; i < EndVFX.transform.childCount; i++)
        {
            var ps = EndVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
                particles.Add(ps);
        }
    }
}
