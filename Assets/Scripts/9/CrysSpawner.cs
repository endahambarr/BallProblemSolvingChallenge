using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrysSpawner : MonoBehaviour
{
    public GameObject crysPrefab;
    public float areaLebar, areaPanjang, objectSizeMax, respawnInterval;
    // pool list
    private Dictionary<string, List<GameObject>> pool;
    private List<GameObject> crysList;
    float areaTengah;

    // Start is called before the first frame update
    void Start()
    {
        areaTengah = 0.5f;
        spawnCrys();
    }

    private void spawnCrys()
    {
        // init pool
        pool = new Dictionary<string, List<GameObject>>();
        GameObject crys;
        int objectCount;
        objectCount = Random.Range(1, 100);
        for (int i = 0; i < objectCount; i++)
        {
            crys = GenerateFromPool(crysPrefab);
            float randomScale = Random.Range(0, objectSizeMax) + 0.25f;
            crys.transform.localScale = new Vector2(randomScale, randomScale);
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
        GenerateFromPool(crysPrefab);
    }
    // Update is called once per frame
    private bool checkInsideSphere(Vector2 position)
    {
        GameObject core = GameObject.Find("Core");
        float distance = Vector2.Distance(position, core.transform.position);
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
    private static CrysSpawner instance = null;
    public static CrysSpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CrysSpawner>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = " CrysSpawner";
                    instance = go.AddComponent<CrysSpawner>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }
}
