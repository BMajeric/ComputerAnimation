using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Propulsion = 0,
    Waterfall = 1,
    //Smoke = 2
};

public class ParticleSpawner : MonoBehaviour
{
    public Queue<GameObject> queuedParticles;

    public GameObject origin;
    public GameObject particlePrefab;

    [Header("Origin Animation Settings")]
    public bool animate = false;
    public Transform startingPos;
    public Transform endingPos;
    [Tooltip("How many frames it takes for the animation to finish")]
    public float frames = 600;
    private bool animationDirection = true;        // true when running in one direction, false in the other
    private int frameCounter = 0;

    [Header("Particle System Settings")]
    public EffectType effectType;
    public float spawnRate;
    public int maxParticles;
    private bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        queuedParticles = new Queue<GameObject>();

        for (int i = 0; i < maxParticles; i++)
        {
            GameObject instantiatedcube = Instantiate(particlePrefab, origin.transform.position, Quaternion.identity);
            queuedParticles.Enqueue(instantiatedcube);
        }

        InvokeRepeating("SpawnParticles", 0, 1 / spawnRate);
    }

    private void Update()
    {
        if (animate)
        {
            if (animationDirection)
            {
                origin.transform.position = startingPos.position - (startingPos.position - endingPos.position) * frameCounter / frames;
            } else
            {
                origin.transform.position = endingPos.position - (endingPos.position - startingPos.position) * frameCounter / frames;

            }
            if (frameCounter == frames)
            {
                animationDirection = !animationDirection;
                frameCounter = 0;
            }
            frameCounter++;
        }
    }

    private void SpawnParticles()
    {
        GameObject nextParticle = queuedParticles.Dequeue();

        nextParticle.transform.position = origin.transform.position;
        Vector3 randomForce = new Vector3(0, 0, 0);
        if (effectType == EffectType.Propulsion)
        {
            particlePrefab.GetComponent<Rigidbody>().useGravity = false;
            randomForce = new Vector3(Random.Range(1f, 10f), Random.Range(-3f, 3f), Random.Range(-2f,2f));
            nextParticle.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            nextParticle.GetComponent<Rigidbody>().AddForce(randomForce, ForceMode.VelocityChange);
        } else if (effectType == EffectType.Waterfall)
        {
            particlePrefab.GetComponent<Rigidbody>().useGravity = true;
            randomForce = new Vector3(Random.Range(2f, 5f), Random.Range(1f, 2f), 0);
            nextParticle.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            nextParticle.GetComponent<Rigidbody>().AddForce(randomForce, ForceMode.VelocityChange);
        }
        //Debug.Log(randomForce);

        queuedParticles.Enqueue(nextParticle);
    }
}
