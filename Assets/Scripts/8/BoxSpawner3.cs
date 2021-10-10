using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner3 : MonoBehaviour
{
    public GameObject boxPrefab;
    public float areaLebar, areaPanjang, objectSizeMax, respawnInterval;
    // pool list
    private Dictionary<string, List<GameObject>> pool;
    private List<GameObject> boxList;
    float areaTengah;

    // Start is called before the first frame update
    void Start()
    {
        areaTengah = 0.5f;
        spawnBox();
    }

    private void spawnBox()
    {
        // init pool
        pool = new Dictionary<string, List<GameObject>>();
        GameObject box;
        int objectCount;
        objectCount = Random.Range(1, 100);
        for (int i = 0; i < objectCount; i++)
        {
            box = GenerateFromPool(boxPrefab);
            float randomScale = Random.Range(0, objectSizeMax) + 0.25f;
            box.transform.localScale = new Vector2(randomScale, randomScale);
        }
    }
    private GameObject GenerateFromPool(GameObject item)
    {
        //generate random position that doesnt collide with ball
        Vector2 position = generateRandomPosition();

        if (pool.ContainsKey(item.name))
        {
            // if item available in pool
            if (pool[item.name].Count > 0)
            {
                GameObject newItemFromPool = pool[item.name][0];
                pool[item.name].Remove(newItemFromPool);
                newItemFromPool.SetActive(true);
                newItemFromPool.transform.position = position;
                return newItemFromPool;
            }
        }
        else
        {
            // if item list not defined, create new one
            pool.Add(item.name, new List<GameObject>());
        }

        // create new one if no item available in pool
        GameObject newItem = Instantiate(item, position, Quaternion.identity);
        newItem.name = item.name;
        return newItem;
    }

    public void ReturnToPool(GameObject item)
    {
        if (!pool.ContainsKey(item.name))
        {
            Debug.LogError("INVALID POOL ITEM!!");
        }
        pool[item.name].Add(item);
        item.SetActive(false);
        StartCoroutine("spawn");
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(respawnInterval);
        GenerateFromPool(boxPrefab);
    }
    // Update is called once per frame
    private bool checkInsideSphere(Vector2 position)
    {
        GameObject ball = GameObject.Find("Ball");
        float distance = Vector2.Distance(position, ball.transform.position);
        if (distance < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector2 generateRandomPosition()
    {
        //menyimpan posisi instantiate
        float randomX, randomY;
        Vector2 position;
        do
        {
            randomX = Random.RandomRange(-areaLebar + areaTengah, areaLebar - areaTengah);
            randomY = Random.RandomRange(-areaPanjang + areaTengah, areaPanjang - areaTengah);
            position = new Vector2(randomX, randomY);
        } while (checkInsideSphere(position));
        return position;

    }
    private static BoxSpawner3 instance = null;
    public static BoxSpawner3 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BoxSpawner3>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = " BoxSpawner";
                    instance = go.AddComponent<BoxSpawner3>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }
}
