using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreMove : MonoBehaviour
{
    public float speed;
    //rigidbody untuk object
    Rigidbody2D rb;
    //referensi ke camera utama
    Camera Camera;
    Vector2 point;
    public Text scoreText;
    public AudioClip sFx;
    private int score;
    public bool scene9;
    private bool currentScene;

    public bool CurrentScene { get => currentScene; set => currentScene = value; }

    // Start is called before the first frame update
    void Start()
    {
        Camera = Camera.main;
        rb = gameObject.GetComponent<Rigidbody2D>();

        if (scene9)
        {
            SoundManager.Instance.PlaySfx("backsound");
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score :" + score.ToString();
    }

    private void FixedUpdate()
    {
        point = Camera.ScreenToWorldPoint(Input.mousePosition);
        Move(point);
    }

    public void Move(Vector2 pointPosition)
    {
        //untuk move menggunakan mouse
        rb.position = Vector3.MoveTowards(transform.position, pointPosition, speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crys")
        {
            score++;

            if (CurrentScene)
            {
                Invoke("RespawnBox", 3f);

                if (scene9)
                {
                    SoundManager.Instance.PlaySfx("eat");
                }
            }
        }
    }


}
