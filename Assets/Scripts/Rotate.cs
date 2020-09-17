using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the PickUp once per frame
        //done by a certain amount (15 on x, 30 on y, 40 on z.)
        //multiply it by deltaTime to make it per second
        //Time Interval: Time.time vs deltaTime (0.02 seconds)
        //Similar to Update vs FixedUpdate

        transform.Rotate(new Vector3(15, 30, 40) * Time.deltaTime);
    }
}
