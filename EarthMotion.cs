using UnityEngine;

public class EarthMotion : MonoBehaviour
  
       
{
    private Vector2 EarthPosition;
    private Vector2 EarthVelocity;
    private Vector2 EarthAcceleration;
    private float gravityMassConstant = 4*Mathf.PI*Mathf.PI;
    private Vector2 iniitialPosition = new Vector2(1,0);
    private Vector2 iniitialVelocity = new Vector2(0,2*Mathf.PI);

    public float totalTime = 10;
    private float timeMotion;
    public float stepTime = 0.01f;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = iniitialPosition;
        EarthVelocity = iniitialVelocity;
        EarthPosition = iniitialPosition;
        timeMotion = 0;
                       
    }

    // Update is called once per frame
    void Update()
    {
        if (timeMotion < totalTime)
        {
            EarthAcceleration =  GravitationalAcceleration();
            (EarthPosition, EarthVelocity, timeMotion) = EulerMethod(EarthPosition, EarthVelocity, EarthAcceleration, timeMotion);
                      
            transform.position = EarthPosition;
            Debug.Log(timeMotion);
        }
        else { return; }
    }

    (Vector2, Vector2 , float) EulerMethod( Vector2 position , Vector2 velocity, Vector2 acceleration, float time ) {
        position = position + velocity * (stepTime);
        velocity = velocity + acceleration * (stepTime);
        time = time + stepTime;
        return (position, velocity, time);
         }

      Vector2  GravitationalAcceleration() {

        float distanceSquared = EarthPosition.magnitude*EarthPosition.magnitude;
        Vector2 unitVector = -EarthPosition.normalized;

        Vector2 acceleration = (gravityMassConstant/distanceSquared)*unitVector;
        return acceleration;
    }
}
