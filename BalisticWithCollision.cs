using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Balistic : MonoBehaviour
{
    private Vector2 position;
    private Vector2 velocity;
    private Vector2 acceleration;

    private float time;
    public float totalTime = 10f;
    public float stepTime = 0.001f;
    public float velocityinit = 10f;
    private float gravity = 9.81f;
    public float dragConstant = 0;
    private Vector2 gravityACC;

    private Vector2 groundNormalVector = new Vector2(0, 1);
    public float collisionFactor = 1f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravityACC = new Vector2(0, -gravity);
        position = new Vector2(0, 0);
        velocity = new Vector2(velocityinit, velocityinit);
        acceleration = Acceleration(velocity);
        time = 0;
        transform.position = position;

    }

    // Update is called once per frame
    void Update()
    {
        if (time < totalTime)
        {
            Vector2 newPosition, newVelocity;
            (newPosition, newVelocity, time) = EulerMethod(position, velocity, time);
            (position, velocity) = CheckGroundCollision(newPosition, position, newVelocity, velocity);
            acceleration = Acceleration(velocity);

            transform.position = position;



            Debug.Log("Time = " + time);

        }
        else { return; }

    }

    Vector2 Acceleration(Vector2 velocity)
    {

        return -dragConstant * velocity + gravityACC;
    }


    (Vector2, Vector2, float) EulerMethod(Vector2 position, Vector2 velocity, float time)
    {

        Vector2 newPosition = position + velocity * stepTime;
        Vector2 newVelocity = velocity + acceleration * stepTime;
        time += stepTime;

        return (newPosition, newVelocity, time);

    }

    (Vector2, Vector2) CheckGroundCollision(Vector2 newPos, Vector2 oldPos, Vector2 newVel, Vector2 oldVel)
    {
        float oldDot = Vector2.Dot(oldPos, groundNormalVector);
        float newDot = Vector2.Dot(newPos, groundNormalVector);

        if (oldDot * newDot < 0)
        {
            // Calculate reflection velocity
            float velocityDot = Vector2.Dot(newVel, groundNormalVector);
            Vector2 reflectedVelocity = newVel - (1 + collisionFactor) * velocityDot * groundNormalVector;

            // Adjust position to prevent sinking
            float penetrationDepth = -newDot; // Since newDot is negative (below ground)
            Vector2 correctedPosition = newPos + (1 + collisionFactor) * penetrationDepth * groundNormalVector;
            correctedPosition += 0.01f * groundNormalVector; // Small offset to prevent ground sticking

            return (correctedPosition, reflectedVelocity);
        }
        else
        {

            return (newPos, newVel);
        }
    }
}
