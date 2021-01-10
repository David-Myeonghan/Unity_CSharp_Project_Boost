using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    //public static float mainThrust = 700f;
    [SerializeField] float rcsThrust = 100f; // sensable default
    [SerializeField] float mainThrust = 700f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;



    Rigidbody rigidbody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending};
    State currentState = State.Alive; // default value;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); // 'Generics'
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (currentState != State.Alive)
        {
            return; // end this method here. // ignore collisions when dead.
        }
        switch (collision.gameObject.tag) 
        {
            case "Friendly":
                currentState = State.Alive;
                // do nothing
                //print("OK");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        audioSource.Stop();
        currentState = State.Transcending;
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay); // parameterise time => do not use magic number! // mean: Execute 'LoadNextScene' method after one second. Use method name as string!! // little delay
    }

    private void StartDeathSequence()
    {
        //print("Hit something deadly");
        audioSource.Stop();
        currentState = State.Dying;
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        // Kill player
        Invoke("LoadFirstLevel", levelLoadDelay); // parameterise time
    }



    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); 
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotateInput()
    {
        rigidbody.freezeRotation = true; // take manual control of roation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame); // anti-clockwise // left-handed system
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidbody.freezeRotation = false; // resume physics control of rotation
    }


}
