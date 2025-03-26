using UnityEngine;

public class TriangleCollision : MonoBehaviour
{
    // particle properties
    private Vector2 position;
    private Vector2 velocity;
    //private Vector2 acceleration; There is no acceleration

    //time properties
    private float time;
    public float stepTime = 0.01f;
    public float totalTime = 10f;

    //triangle properties
    public struct Wall
    {
        public Vector2 normalVector;
        public float displacement;
        public float epsilonFactor;

        public Wall(Vector2 normal, float displ, float epsilon)
        {

            this.normalVector = normal.normalized;
            this.displacement = displ/normal.magnitude;
            this.epsilonFactor = epsilon;

        }
    };

    public float epsilon = 1;
    private Wall[] triangle;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // create triangle
        triangle = new Wall[] {

            new Wall(new Vector2(5,-2.5f) ,0 , epsilon ), //left wall
            new Wall(new Vector2(0,1) , 0 , epsilon), //bottom wall
            new Wall(new Vector2(-5,-2.5f) ,25, epsilon) //right wall

        };

        //set initial position
        position = new Vector2(1, 1);
        velocity = new Vector2(3, 4);


        //time
        time = 0;

        // object position
        transform.position = position;

    }

    // Update is called once per frame
    void Update()
    {

        if (time < totalTime)
        {
            Vector2 newPosition, newVelocity;
            (newPosition, newVelocity, time) = EulerMethod(position, velocity, time);
            (position, velocity) = checkCollision(triangle, newPosition, position, newVelocity, velocity);

            transform.position = newPosition;

        }
        else { return; }

    }
     (Vector2, Vector2, float) EulerMethod(Vector2 position, Vector2 velocity, float time)
        {

            Vector2 newPosition = position + velocity * stepTime;
            Vector2 newVelocity = velocity;
            time += stepTime;

            return (newPosition, newVelocity, time);
        }



    (Vector2, Vector2) checkWallCollision(Wall wall, Vector2 newPos, Vector2 oldPos, Vector2 newVel, Vector2 oldVel)
    {

        float oldDot = Vector2.Dot(oldPos, wall.normalVector);
        float newDot = Vector2.Dot(newPos, wall.normalVector);

        float oldValue = oldDot + wall.displacement;
        float newValue = newDot + wall.displacement;


        if (oldValue * newValue < 0)
        {

            //update velocity
            float velocityDot = Vector2.Dot(wall.normalVector, newVel);
            Vector2 reflectionVelocity = newVel - (1 + wall.epsilonFactor) * velocityDot * wall.normalVector;

            //update position
            float penetration = -newValue;
            Vector2 correctedPosition = newPos + (1 + wall.epsilonFactor) * penetration * wall.normalVector;
            correctedPosition += 0.01f * wall.normalVector;


            return (correctedPosition, reflectionVelocity);
        }
        else
        {
            return (newPos, newVel);
        }

     }
    (Vector2, Vector2) checkCollision(Wall[] triangle, Vector2 newPos, Vector2 oldPos, Vector2 newVel, Vector2 oldVel)
    {

        int i = 0;
        Vector2 finalPos = newPos;
        Vector2 finalVel = newVel;

        while (i < triangle.Length)
        {
            Vector2 tempPos, tempVel;
            (tempPos, tempVel) = checkWallCollision(triangle[i], finalPos, oldPos, finalVel, oldVel);

            if (tempPos != finalPos) { i = 0;
                finalPos = tempPos;
                finalVel = tempVel;
            }
            else { i++; } 

        }

        return (finalPos, finalVel);
    }


}
    
