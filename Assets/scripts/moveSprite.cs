using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//walk animation help
//https://chatgpt.com/share/6733451e-cc34-8001-843a-91fd4efa6731
public class MoveSprite : MonoBehaviour
{
    public string left;
    public string right;
    public string jump;
    public string crouch;
    public float jumpForce = 5f;
    private Vector3 originalScale;
    private Vector3 originalBodyPosition;
    public bool faceLeft = true;
    public bool faceRight = false;
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float crouchSpeed = 3f;
    public float rotateRange = 30f;
    public float rotationSpeed = 5f;

    public Transform frontLeg;
    public Transform backLeg;
    public Transform body;
    public GameObject inventory;

    private float rotationTimer = 0f;

    private bool isJumping = false;
    private bool isCrouching = false;

    private Rigidbody2D rb;

    void Start()
    {originalScale = transform.localScale;
        originalBodyPosition = body.localPosition;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float currentSpeed = moveSpeed;
        bool isMovingHorizontally = false;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {     currentSpeed = sprintSpeed;
        }

        if (Input.GetKey(crouch))
        { isCrouching = true;
            body.localPosition = new Vector3(originalBodyPosition.x, originalBodyPosition.y - 0.4f, originalBodyPosition.z);
            currentSpeed = crouchSpeed; }
        else
        {  isCrouching = false;
  body.localPosition = originalBodyPosition;
        }

        if (Input.GetKey(left))
        {  transform.Translate(new Vector3(-currentSpeed, 0f, 0f) * Time.deltaTime);
            isMovingHorizontally = true;
            faceLeft = true;
            faceRight = false;
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        if (Input.GetKey(right))
        {
 transform.Translate(new Vector3(currentSpeed, 0f, 0f) * Time.deltaTime);
          isMovingHorizontally = true;
            faceRight = true;
            faceLeft = false;
          transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        if (Input.GetKeyDown(jump) && !isJumping)
        {
     rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
        
//https://chatgpt.com/share/6733451e-cc34-8001-843a-91fd4efa6731
        if (isMovingHorizontally)
        {
           rotationTimer += currentSpeed * Time.deltaTime * rotationSpeed;
            float frontLegAngle = Mathf.Sin(rotationTimer) * rotateRange;
            float backLegAngle = -Mathf.Sin(rotationTimer) * rotateRange;

            frontLeg.localRotation = Quaternion.Euler(0f, 0f, frontLegAngle);
            backLeg.localRotation = Quaternion.Euler(0f, 0f, backLegAngle);
        }
        else
        {
            frontLeg.localRotation = Quaternion.Euler(0f, 0f, 0f);
            backLeg.localRotation = Quaternion.Euler(0f, 0f, 0f);
            rotationTimer = 0f;
        }

        transform.rotation = Quaternion.identity;

        if (inventory != null)
        {
            inventory.transform.position = new Vector3(transform.position.x, transform.position.y + 2.8f, inventory.transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isJumping = false;
        }
    }
}
