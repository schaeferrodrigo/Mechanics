using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class elasticCollision : MonoBehaviour
{

    public Transform particle1;
    public Transform particle2;

    
    
    // particle properties
    public struct Particle {
        public Vector2 position;
        public Vector2 velocity;
        public float mass;

        public Particle(Vector2 pos, Vector2 vel, float m) { 
        
            this.position = pos;
            this.velocity = vel;
            this.mass = m;
   
        }
    }

    private int numberOfParticles = 2;
    


    // time
    private float time;
    public float stepTime = 0.01f;
    public float totalTime = 10f;
    private Particle[] system;

    public float distance = 0.01f;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        system = new Particle[] {
            new Particle(new Vector2(-3 ,0), new Vector2(4,0), 2),
            new Particle(new Vector2(3,0) , new Vector2(-2, 0), 3)
        };

        time = 0;

        particle1.transform.position = system[0].position;
        particle2.transform.position = system[1].position;
               
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= totalTime) { return; }
        else
        {

            Vector2[] newPosition = new Vector2[numberOfParticles];
            Vector2[] newVelocity = new Vector2[numberOfParticles];


            //Euler Method

            for (int i = 0; i < numberOfParticles; i++)
            {
                (newPosition[i], newVelocity[i], time) = EulerMethod(system[i].position, system[i].velocity, time);
            }

            if (Vector2.SqrMagnitude(newPosition[0] - newPosition[1]) <= distance) {
            
                Vector2 vel1 = velocityAfterCollision(system[0], system[1]);
                Vector2 vel2 = velocityAfterCollision(system[1], system[0]);

                newVelocity[0] = vel1;
                newVelocity[1] = vel2;
            
            }
            system[0].position = newPosition[0];
            system[1].position = newPosition[1];
            system[0].velocity = newVelocity[0];
            system[1].velocity = newVelocity[1];

            particle1.transform.position = system[0].position;
            particle2.transform.position = system[1].position;


        }

    }
    (Vector2, Vector2, float) EulerMethod(Vector2 position, Vector2 velocity, float time)
    {

        Vector2 newPosition = position + velocity * stepTime;
        Vector2 newVelocity = velocity;
        time += stepTime;

        return (newPosition, newVelocity, time);
    }

    Vector2 velocityAfterCollision(Particle particle_1, Particle particle_2) { 

        float totalMass = particle_1.mass + particle_2.mass;
        float difference = particle_1.mass - particle_2.mass;

        float afterVelocity = (difference * particle_1.velocity.x + 2 * particle_2.mass * particle_2.velocity.x) / (totalMass);

        return new Vector2(afterVelocity, 0);
    }
}
