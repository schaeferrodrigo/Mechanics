using UnityEngine;




public class MeshgerstnetWave : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //unity object properties
    public Transform[] nodes;
    public int numberOfNodes = 10;
    public float distanceNodes = 1f;


   //initial condition
    public Vector2 initialPosition =  new Vector2(0, 0);
    private Vector2[] allInitialPositions;


    //wave properties
    public float waveLength = 2f;
    public float amplitude = 5f;
    public float frequency; 
    public float phase = 0f;
    private float angle = Mathf.PI/2;
    private Vector2 waveVector;

    //external properties
    private float gravity = 9.8f;

    //time properties
    private float time = 0;
    public float stepTime = 0.01f;





    void Start()
    {
        generateInitialPosition(); 
        setInitialTransform();
        waveVector = (2 * Mathf.PI/waveLength )* new Vector2( Mathf.Cos(angle) ,Mathf.Sin(angle));
        frequency = Mathf.Sqrt(gravity * waveVector.magnitude);

    }

    // Update is called once per frame
    void Update()
    {
        time += stepTime;
        for (int i = 0; i < numberOfNodes; i++) {
            
            nodes[i].position = calculateGerstnerWavePosition(allInitialPositions[i], time);
        }
        
    }

    Vector3 calculateGerstnerWavePosition(Vector2 initialPoint, float time) {

        Vector2 planePosition;
        float height;
        Vector3 newPosition;


        planePosition = initialPoint - (waveVector / waveVector.magnitude) * amplitude * Mathf.Sin(Vector2.Dot(waveVector, initialPoint) - frequency * time + phase);
        height = amplitude * Mathf.Cos(Vector2.Dot(waveVector, initialPoint) - frequency * time + phase);
        

        newPosition = new Vector3(planePosition.x, height, planePosition.y);

        return newPosition;

           
    }

    void  generateInitialPosition() {

        allInitialPositions = new Vector2[numberOfNodes];

        float distance = 0;

        for (int i = 0; i < numberOfNodes; i++) {
                     
            allInitialPositions[i] = new Vector2(initialPosition.x, initialPosition.y + distance);
            Debug.Log(allInitialPositions[i]);
            distance += distanceNodes;

        }

    }

    void setInitialTransform() {

        for (int i = 0; i < numberOfNodes; i++)
        {
            nodes[i].position = new Vector3(allInitialPositions[i].x, 0, allInitialPositions[i].y );

        }

    }
    
 
}
