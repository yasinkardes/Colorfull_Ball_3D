using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myBounds : MonoBehaviour
{
    public Transform vectorLeft;
    public Transform vectorRight;
    public Transform vectorBack;
    public Transform vectorForward;

    public void LateUpdate()
    {
        Vector3 viewPos = transform.position;

        viewPos.z = Mathf.Clamp(viewPos.z, vectorBack.transform.position.z, vectorForward.transform.position.z);
        viewPos.x = Mathf.Clamp(viewPos.x, vectorLeft.transform.position.x, vectorRight.transform.position.x);

        transform.position = viewPos;
    }
}
