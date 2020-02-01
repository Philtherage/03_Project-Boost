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
    bool isTranstioning = false;

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
        if(!isTranstioning)
        {          
            Rotate();
            Thrust();            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isTranstioning) { return; }
        if (!FindObjectOfType<DebugSettings>()) { return; }
        if(FindObjectOfType<DebugSettings>().GetCollisionOff()) { return; }

        switch (collision.gameObject.tag)
        {
            case COLLISION_TAG_FRIENDLY:
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
        isTranstioning = true;
        audioSource.Stop();
        levelFinishVFX.Play();
        audioSource.PlayOneShot(levelFinishSFX);
        StartCoroutine(FindObjectOfType<LevelLoader>().LoadDelay(isDead));
    }

    private void Dying()
    {
        audioSource.Stop();
        isTranstioning = true;
        isDead = true;
        deathVFX.Play();
        audioSource.PlayOneShot(deathSFX);
        StartCoroutine(FindObjectOfType<LevelLoader>().LoadDelay(isDead));
    }

    private void Rotate()
    {
       
        if (Input.GetKey(KeyCode.A))
        {
            RotateManually(rotateForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateManually(-rotateForce);
        }
        
    }

    private void RotateManually(float rotateForce)
    {
        rigidBody.freezeRotation = true; // take manual control of rotation
        transform.Rotate(Vector3.forward, rotateForce * Time.deltaTime);
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
            StopThrusting();
        }

    }

    private void StopThrusting()
    {
        audioSource.Stop();
        rocketJetVFX.Stop();
    }
}
