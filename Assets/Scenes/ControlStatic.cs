using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlStatic : MonoBehaviour
{
    public Slider rcsControl;

    // Start is called before the first frame update
    void Start()
    {
        rcsControl.value = 700;
        //Rocket.mainThrust = 700;
        //Rocket.mainThrust = rcsControl.value;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rocket.mainThrust = rcsControl.value;
        //Debug.Log(Rocket.mainThrust);
        //Debug.Log(rcsControl.value);
    }
}
