using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitEnemies : MonoBehaviour
{

    public GameObject Enemy_RANGE_RED;
    public GameObject Enemy_RANGE_GREEN;
    public List<GameObject> allEnemies;
    private void Awake()
    {
        var enemiesCoordinatesX = new Queue<float>();
        var enemiesCoordinatesY = new Queue<float>();


        // RANGE RED enemies
        enemiesCoordinatesX.Enqueue(12f);
        enemiesCoordinatesX.Enqueue(22f);
        enemiesCoordinatesX.Enqueue(30f);

        enemiesCoordinatesY.Enqueue(-10f);
        enemiesCoordinatesY.Enqueue(-3f);
        enemiesCoordinatesY.Enqueue(-16f);


        while (enemiesCoordinatesX.Count != 0) allEnemies.Add( Instantiate(Enemy_RANGE_RED, new Vector3(enemiesCoordinatesX.Dequeue(), enemiesCoordinatesY.Dequeue(), 0), Quaternion.identity));

        // RANGE GREEN enemies
        enemiesCoordinatesX.Enqueue(3f);
        enemiesCoordinatesX.Enqueue(27f);

        enemiesCoordinatesY.Enqueue(-15f);
        enemiesCoordinatesY.Enqueue(2f);


        while (enemiesCoordinatesX.Count != 0) allEnemies.Add(Instantiate(Enemy_RANGE_GREEN, new Vector3(enemiesCoordinatesX.Dequeue(), enemiesCoordinatesY.Dequeue(), 0), Quaternion.identity));
        

    }

    public void resetEnemies() {
        foreach (var e in allEnemies) Destroy(e);
        allEnemies = new List<GameObject>();
        Awake();
    }

    void Start()
    {
        
    }


}
