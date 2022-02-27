using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject astroidPrefab;
    public Transform astroidsParent;
    public GameObject shipPrefab;

    private Vector3 player1Start;
    private Transform player2Start;

    private List<GameObject> astroids = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        var position = GameManager.instance.player1.transform.position;
        player1Start = new Vector3(position.x, position.y, position.z);
        //player2Start = GameManager.instance.player2.transform;
        SpawnAstroids(50);
    }

    public void SpawnAstroids(int maxAstroids)
    {
        for (var i = 0; i < maxAstroids; i++)
        {
            float spawnY = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y,
                Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x,
                Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            while (spawnY < -3)
            {
                spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y,
                    Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            }

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            var astroid = Instantiate(astroidPrefab, spawnPosition, Quaternion.identity);
            astroid.transform.parent = astroidsParent;
            astroids.Add(astroid);
        }
    }

    public void DestroyAstroids()
    {
        foreach (var astroid in astroids){
            Destroy(astroid);
        }

        astroids = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}