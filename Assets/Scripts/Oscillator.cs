using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3 (0f,0f,0f);
    [SerializeField] float peroid = 2f;
    [Range(0,1)]
    [SerializeField] float movementFactor = 1f;


    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(peroid <= Mathf.Epsilon) { return; }
        
        float cycles = Time.time / peroid; // grows continually from 0
        
        
        const float tau = Mathf.PI * 2; // about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1.

        movementFactor = rawSinWave / 2f + 0.5f; // divides by 2 output is -.5 to +.5 then + .5 to get it 0 to +1.

        Vector3 offset = movementVector * movementFactor;

        transform.position = startingPos + offset ;

        
    }


}
