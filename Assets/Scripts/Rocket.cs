using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrusterForce = 1f;
    [SerializeField] float rotateForce = 1f;

    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip levelFinishSFX;

    [SerializeField] ParticleSystem deathVFX;
    [SerializeField] ParticleSystem levelFinishVFX;
    [SerializeField] ParticleSystem rocketJetVFX;
   
    const string COLLISION_TAG_FRIENDLY = "Friendly";
    const string COLLISION_TAG_FUEL = "Fuel";
    const string COLLISION_TAG_FINISH = "Finish";

    bool isDead;
    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    Rigidbody rigidBody;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {          
            Rotate();
            Thrust();            
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive) { return; }

        switch (collision.gameObject.tag)
        {
            case COLLISION_TAG_FRIENDLY:
                state = State.Alive;
                break;

            case COLLISION_TAG_FUEL:
                Debug.Log("Fuel");
                break;
            case COLLISION_TAG_FINISH:
                LevelWon();
                break;
            
            default:
                Dying();           
                break;
                
        }
    }

    private void LevelWon()
    {
        state = State.Transcending;
        audioSource.Stop();
        levelFinishVFX.Play();
        audioSource.PlayOneShot(levelFinishSFX);
        StartCoroutine(FindObjectOfType<LevelLoader>().LoadDelay(isDead));
    }

    private void Dying()
    {
        audioSource.Stop();
        state = State.Dying;
        isDead = true;
        deathVFX.Play();
        audioSource.PlayOneShot(deathSFX);
        StartCoroutine(FindObjectOfType<LevelLoader>().LoadDelay(isDead));
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
            

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineSFX);
                rocketJetVFX.Play();

            }

        }
        else
        {
            audioSource.Stop();
            rocketJetVFX.Stop();
        }

    }
    
}
