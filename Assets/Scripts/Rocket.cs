using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrusterForce = 1f;
    [SerializeField] float rotateForce = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0f, thrusterForce, 0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward, -rotateForce); 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward, rotateForce);
            
        }
    }
}
