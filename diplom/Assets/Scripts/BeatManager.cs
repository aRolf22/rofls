using System.Linq;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;


public class BeatManager : MonoBehaviour
{
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Intervals[] intervals;

    void Start()
    {
        for (int i = 0; i++; i < GameObject.FindGameObjectsWithTag("environment").Length())
        {
            
        }
    }

    private void Update()
    {
        if (audioSource.resource.name == "70bpm_drum")
        {
            bpm = 70f;
        }
        else if (audioSource.resource.name == "50bpm_drum")
        {
            bpm = 50f;
        }

        foreach (Intervals interval in intervals)
        {
            float sampledTime = (audioSource.timeSamples / (audioSource.clip.frequency * interval.GetIntervalLength(bpm)));
            interval.CheckForNewInterval(sampledTime);
        }
    }

    public void BeatDebug()
    {
        Debug.Log("Beat!");
    }
}

[System.Serializable]
public class Intervals
{
    [SerializeField] private float steps;
    [SerializeField] private UnityEvent trigger;
    private int _lastInterval;

    public float GetIntervalLength(float bpm) // по сути GetBeatLength
    {
        return 60f / (bpm * steps);
    }

    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != _lastInterval)
        {
            _lastInterval = Mathf.FloorToInt(interval);
            trigger.Invoke();
        }    
    }
}
