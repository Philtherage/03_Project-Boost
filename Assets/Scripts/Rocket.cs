using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrusterForce = 1f;
    [SerializeField] float rotateForce = 1f;

    bool isThrusting;

    Rigidbody rigidBody;
    AudioSource rocketThrustSFX;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rocketThrustSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
        PlayThrustSFX();
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation

        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(Vector3.forward, rotateForce * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward, -rotateForce * Time.deltaTime);

        }
        rigidBody.freezeRotation = false; // resume physics control of rotation
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrusterForce);
            isThrusting = true;
        }
        else
        {
            isThrusting = false;
        }
    }

    private void PlayThrustSFX()
    {
        if (isThrusting)
        {
            if (!rocketThrustSFX.isPlaying) 
            { 
                rocketThrustSFX.Play();
            }
        }
        else
        {
            rocketThrustSFX.Stop();
        }
    }
}
