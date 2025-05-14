using System.Diagnostics;
using UnityEngine;

public class wave : MonoBehaviour
{
    private Vector2 position;
    private float time;
    public float amplitude = 5f;
    public float stepTime = 0.01f;
    public float period = 3f;
    public float phase = 0;
    public Vector2 initialPosition = new Vector2(0,0);

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
        position = initialPosition;

        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        time += stepTime;
        position.y = sineWave();

        transform.position = position;

        
    }

    float sineWave() { 
          return amplitude*Mathf.Sin(2*Mathf.PI*time/period + phase );
    }
}
