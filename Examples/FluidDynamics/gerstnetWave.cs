using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;




public class gerstnetWave : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

   
    private Vector3 position;
    public Vector2 initialPosition =  new Vector2(0, 0);

    private float distanceNodo = 1f;

    public float waveLength = 2f;
    public float amplitude = 5f;
    public float frequency; 
    public float stepTime = 0.01f;
    public float phase = 0f;
    private float totalTime = 100f;
    
    private float angle = Mathf.PI/2;
    private float time = 0;
    private Vector2 waveVector;
    private float gravity = 9.8f;

    


    void Start()
    {
        position = new Vector3(initialPosition.x, 0, initialPosition.y);
        transform.position = position;
        waveVector = (2 * Mathf.PI/waveLength )* new Vector2( Mathf.Cos(angle) ,Mathf.Sin(angle)/waveLength );
        frequency = Mathf.Sqrt(gravity * waveVector.magnitude);

    }

    // Update is called once per frame
    void Update()
    {
           time += stepTime;
           position = calculateGerstnerWavePosition(time);

           Debug.Log(time);

          transform.position = position;

        
    }

    Vector3 calculateGerstnerWavePosition(float time) {

        Vector2 planePosition;
        float height;
        Vector3 newPosition;


        planePosition = initialPosition - (waveVector / waveVector.magnitude) * amplitude * Mathf.Sin(Vector2.Dot(waveVector, initialPosition) - frequency * time + phase);
        height = amplitude * Mathf.Cos(Vector2.Dot(waveVector, initialPosition) - frequency * time + phase);
        

        newPosition = new Vector3(planePosition.x, height, planePosition.y);

        return newPosition;

           
    }
}
