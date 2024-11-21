using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset;  

    void Start() {
      
        if (player != null) {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate() {
        if (player != null) {
           
            transform.position = player.position + offset;
        }
    }
}
