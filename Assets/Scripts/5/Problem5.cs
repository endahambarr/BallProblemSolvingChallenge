using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Problem5 : MonoBehaviour
{
    public float speed;
    //rigidbody untuk object
    Rigidbody2D rb;
    //referensi ke camera utama
    Camera Camera;
    Vector2 point;

    // Start is called before the first frame update
    void Start()
    {
        Camera = Camera.main;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        point = Camera.ScreenToWorldPoint(Input.mousePosition); 
    }

    private void FixedUpdate()
    {
        Move(point);
    }

    public void Move(Vector2 pointPosition)
    {
        //untuk move menggunakan mouse
        rb.position = Vector3.MoveTowards(transform.position, pointPosition, speed * Time.deltaTime);
    }
}
