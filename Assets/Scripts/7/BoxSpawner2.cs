using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner2 : MonoBehaviour
{
    public float areaLebar, areaPanjang, objectSizeMax;
    public GameObject BoxPrefabs;
    float areaTengah;
    // Start is called before the first frame update
    void Start()
    {
        areaTengah = 0.5f;
        int objectCount;
        objectCount = Random.Range(1, 100);
        GameObject box;
        for (int i = 0; i < objectCount; i++)
        {
            float randomX = Random.Range(-areaLebar + areaTengah, areaLebar - areaTengah);
            float randomY = Random.Range(-areaPanjang + areaTengah, areaPanjang - areaTengah);
            box = Instantiate(BoxPrefabs, new Vector2(randomX, randomY), Quaternion.identity);
            box.transform.localScale = new Vector2(Random.Range(0, objectSizeMax) + 0.5f, Random.Range(0, objectSizeMax) + 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
