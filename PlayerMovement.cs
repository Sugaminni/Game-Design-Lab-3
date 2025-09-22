using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables for movement and shooting
    public float turnSpeed = 20f;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    public GameObject bullet;     // bullet prefab 
    public Transform shotSpawn;   // shot spawn point 

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Checks for shoot input in Update
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Shoot();
        }
    }

    // Move the player in FixedUpdate
    void FixedUpdate()
    {
        // Stores the input axes
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        // Plays footstep sound
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        // Creates a rotation based on the movement vector
        Vector3 desiredForward = Vector3.RotateTowards(
            transform.forward, 
            m_Movement, 
            turnSpeed * Time.deltaTime, 
            0f
        );
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    // Apply animation to rigidbody
    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(
            m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude
        );
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    // shoots a bullet
    void Shoot()
    {
        GameObject g = Instantiate(bullet, shotSpawn.position, shotSpawn.rotation) as GameObject;
        Destroy(g, 1.5f);  // auto-destroy after 1.5s
    }
}
