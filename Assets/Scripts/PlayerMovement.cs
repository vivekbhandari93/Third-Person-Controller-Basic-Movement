using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] int speed = 10;
    [SerializeField] float smoothTime = 0.1f;
    [SerializeField] Transform camera;
    float currentVelocity;


    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            //getting the angle to rotate (also adding camera rotation)
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;

            //smoothing the change in angle
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);

            //rotating the player with that angle
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // making rotation into the direction to move
            Vector3 rotatedDerection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //moving the player in rotated direction
            controller.Move(rotatedDerection.normalized * speed * Time.deltaTime);
        }
    }
}
