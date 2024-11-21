using System.Collections;
using UnityEngine;

public class PlatformRightLeft : MonoBehaviour
{
    public float moveDistanceX = 3f;
    public float moveSpeedX = 2f;
    public float moveDistanceY = 3f;
    public float moveSpeedY = 2f;

    private Vector3 startPosition;
    public bool movingRight = false;
    private bool movingUp = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    { MoveHorizontal();
        MoveVertical();
    }

    private void MoveHorizontal()
    {
        if (movingRight)
        {   transform.position += Vector3.right * moveSpeedX * Time.deltaTime;
            if (transform.position.x >= startPosition.x + moveDistanceX)
            {
                movingRight = false;
            }
        }
        else
        {  transform.position += Vector3.left * moveSpeedX * Time.deltaTime;
            if (transform.position.x <= startPosition.x - moveDistanceX)
            {
                movingRight = true;
            }
        }
    }

    private void MoveVertical()
    {if (movingUp)
        {
            transform.position += Vector3.up * moveSpeedY * Time.deltaTime;
            if (transform.position.y >= startPosition.y + moveDistanceY)
            {
                movingUp = false;
            }
        }  else
        {
            transform.position += Vector3.down * moveSpeedY * Time.deltaTime;
            if (transform.position.y <= startPosition.y - moveDistanceY)
            {
                movingUp = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {  if (collision.gameObject.CompareTag("player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
