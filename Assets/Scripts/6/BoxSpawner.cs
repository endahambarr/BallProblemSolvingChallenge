using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public float minX, maxX, minY, maxY, objectSizeMax;
    public GameObject BoxPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        int objectCount = 5;
        GameObject box;
        for (int i = 0; i < objectCount; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            box = Instantiate(BoxPrefabs, new Vector2(randomX, randomY), Quaternion.identity);
            box.transform.localScale = new Vector2 (Random.Range(0, objectSizeMax) + 0.5f , Random.Range(0, objectSizeMax) + 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
