using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject platform;


    bool movingRight;

    
    void Start()
    {
        
    }

    void Update()
    {
        if (platform.transform.position.y > 1.6)
        {
            movingRight = false;
        }
        else if (platform.transform.position.y < -2.42)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            platform.transform.Translate(0,0.005f,0);
        }
        else
            platform.transform.Translate(0, -0.005f, 0);
    }

    
}
