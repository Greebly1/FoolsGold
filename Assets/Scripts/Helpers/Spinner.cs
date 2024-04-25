using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float spinSpeed = 1;

    private void Update()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot += new Vector3(0, spinSpeed * Time.deltaTime, 0);

        transform.Rotate(new Vector3(0, spinSpeed * Time.deltaTime, 0));
    }
}
