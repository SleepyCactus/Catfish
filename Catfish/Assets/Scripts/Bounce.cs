using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] float Offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BounceOffset();
    }

    private void BounceOffset()
    {

        Vector3 bouncePosition = transform.position;
        bouncePosition.y = 1+ Mathf.Cos(Offset+ (Time.fixedTime * 15)) * 0.15f;
        transform.position = bouncePosition;

    }
}
