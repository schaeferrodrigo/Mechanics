using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class AngularToLinear : MonoBehaviour
{   

    // disk properties
    public float radius = 5f;
    private float angle;


    //disk dinamics
   
    public float angularVelocity;
    public float angularAcceleration = 0;

    // time properties

    public float stepTime = 0.1f;
    private float time;
    public float totalTime = 10.0f;



    //objects
    private GameObject platform, ball;

    void Start()
    {   
        //creating object
        platform = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        platform.transform.localScale = new Vector3(radius * 2, 0.2f, radius * 2);

        ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ball.transform.localScale = Vector3.one * 0.5f;
        ball.transform.position = new Vector3(radius, 0.5f, 0);


        // initial conditions
        time = 0;
        angularVelocity = 2.0f;
        angle = 0;


    }

    void Update()
    {

        if (time > totalTime) { return; }

        else {

            (angle, angularVelocity, time) = dynamicsEquations(angle, angularVelocity);

            platform.transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg * angle, 0);


            //s = r * theta

            float x = radius * Mathf.Cos(angle);
            float z = radius * Mathf.Sin(angle);
            ball.transform.position = new Vector3(x, 0.5f, z);


            Vector3 tangentialVelocity = new Vector3(-Mathf.Sin(angle), 0, Mathf.Cos(angle)) * angularVelocity * radius;
            Debug.DrawRay(ball.transform.position, tangentialVelocity * 2f, Color.green);
        }
                
    }

    (float, float, float) dynamicsEquations(float oldAngle, float oldVelocity)
    {

        float newAngle, newVelocity;

        newAngle = oldAngle + oldVelocity * stepTime + 0.5f * angularAcceleration * stepTime * stepTime;
        newVelocity = oldVelocity + angularAcceleration * stepTime;
        time += stepTime;


        return (newAngle, newVelocity, time);

    }
}

