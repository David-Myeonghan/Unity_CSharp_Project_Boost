using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); // 'Generics'
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            rigidbody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddRelativeForce(Vector3.left);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddRelativeForce(Vector3.right);
        }
    }
}
