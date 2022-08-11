using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;    
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;

    private Camera mainCamera;
    private Rigidbody rb;

    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        PlayerWrapAround();
        RotateShip();
    }

    void FixedUpdate()
    {  
        if(movementDirection == Vector3.zero) {return;}
        
        rb.AddForce(movementDirection * forceMagnitude * Time.deltaTime, ForceMode.Force);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        
    }

    private void ProcessInput()
    {
        if(Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            movementDirection = transform.position - worldPosition;
            movementDirection.z = 0f;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    private void PlayerWrapAround()
    {
        float offset = 0.1f;
        Vector3 newPosition = transform.position;
        Vector3 viewportPoistion = mainCamera.WorldToViewportPoint(newPosition);

        //off the right side of the device
        if(viewportPoistion.x > 1)
        {
            newPosition.x = -newPosition.x + offset;
        }

        //off the right side of the device
        if(viewportPoistion.x < 0)
        {
            newPosition.x = -newPosition.x - offset;
        }

        //off the top side of the device
        if(viewportPoistion.y > 1)
        {
            newPosition.y = -newPosition.y + offset;
        }

        //off the bottom side of the device
        if(viewportPoistion.y < 0)
        {
            newPosition.y = -newPosition.y - offset;
        }
        transform.position = newPosition;
    }

    private void RotateShip()
    {
        if (rb.velocity == Vector3.zero) {return;}
        Quaternion targetRotation = Quaternion.LookRotation(rb.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
