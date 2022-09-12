using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged") || collision.gameObject.CompareTag("Obstacle"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
