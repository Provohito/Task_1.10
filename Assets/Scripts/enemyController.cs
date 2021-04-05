using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    bool movingRight = true;
    float speed = 3f;

    private void Update()
    {
        if (this.transform.position.x < 5.8f)
        {
            movingRight = false;
        }
        else if (this.transform.position.x > 13.8f)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        
        
           
        
    }
}
