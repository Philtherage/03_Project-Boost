using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrusterForce = 1f;
    [SerializeField] float rotateForce = 1f;

   
    const string COLLISION_TAG_FRIENDLY = "Friendly";
    const string COLLISION_TAG_FUEL = "Fuel";
    const string COLLISION_TAG_FINISH = "Finish";

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

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case COLLISION_TAG_FRIENDLY:
                Debug.Log("Ok");
                break;

            case COLLISION_TAG_FUEL:
                Debug.Log("Fuel");
                break;
            case COLLISION_TAG_FINISH:
                FindObjectOfType<LevelLoader>().LoadNextScene();
                break;
            
            default:
                FindObjectOfType<LevelLoader>().LoadLevelOne();
                Debug.Log("Dead");
                break;
        }
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
