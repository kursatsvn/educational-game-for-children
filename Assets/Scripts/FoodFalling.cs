using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodFalling : MonoBehaviour
{
    public float fallSpeed = 5f;

    void Update()
    {
        transform.Translate(0, -fallSpeed * Time.deltaTime, 0);

        if (transform.position.y < -5f)
        {
            Destroy(gameObject);

        }
    }
}