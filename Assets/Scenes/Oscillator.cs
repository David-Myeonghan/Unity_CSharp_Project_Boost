using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // disallow for object not to have two script?!!
public class Oscillator : MonoBehaviour
{
    // move objects in 10 x,y,z, respectively, in 2 periods.
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f); 
    [SerializeField] float period = 2f; // two seconds.

    //todo remove from inspector late
    //[Range(0,1)] [SerializeField] 
    float movementFactor; // 0 for not moved, 1 for fully moved.

    Vector3 startingPos; // must be stored for absolute movement

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position; // absolute movement
        
    }

    // Update is called once per frame
    void Update()
    {
        // set movement factor 

        float cycles = Time.time / period; // grows continually from 0

        // #Mathf.Epsion, or 0f
        if (period <= Mathf.Epsilon) { return; } // Mathf.Epsiolon is the smallest number we can represent.

        const float tau = Mathf.PI * 2; // about 6.28 = PI * 2
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

        //print(rawSinWave); from -1 to 1

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
        
    }
}
