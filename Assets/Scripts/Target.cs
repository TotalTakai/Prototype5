using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private readonly float xSpawnBoundary = 4;
    private readonly float ySpawnPosition = -2;
    private readonly float minSpeed = 10;
    private readonly float maxSpeed = 15;
    private readonly float maxTorque = 10;
    private Rigidbody targetRb;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        SetSpawnInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // sets the target's launch force at spawn, position, and torque
    private void SetSpawnInfo()
    {
        // Sets target's jump force
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);

        //sets target's torque
        targetRb.AddTorque(RandomTorque(), ForceMode.Impulse);

        //Sets target's spawn position
        transform.position = RandomSpawnPos();
    }

    // Calculates Random force needed to be added to taget spawn
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    // Calculates random Torque to be added to target's spawn
    private Vector3 RandomTorque()
    {
        return new Vector3(Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque));
    }

    // Calculates random spawn position for each target's spawn
    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xSpawnBoundary, xSpawnBoundary), ySpawnPosition);
    }

    // Destroys the target whenever we click on it
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    // Destroys the target whenever it reaches the Trigger game object (Sensor)
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
