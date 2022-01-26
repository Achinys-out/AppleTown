using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitEnemies : MonoBehaviour
{

    public GameObject Enemy_RANGE;
    public GameObject Enemy_CLOSE;
    private void Awake()
    {
        var enemiesCoordinatesX = new Queue<float>();
        var enemiesCoordinatesY = new Queue<float>();

        enemiesCoordinatesX.Enqueue(-7f);
        enemiesCoordinatesX.Enqueue(14f);
        enemiesCoordinatesX.Enqueue(16f);

        enemiesCoordinatesY.Enqueue(-29f);
        enemiesCoordinatesY.Enqueue(-13f);
        enemiesCoordinatesY.Enqueue(-5f);

        while (enemiesCoordinatesX.Count != 0) {
            Instantiate(Enemy_RANGE, new Vector3(enemiesCoordinatesX.Dequeue(), enemiesCoordinatesY.Dequeue(), 0), Quaternion.identity);
        }


    }


    void Start()
    {
        
    }


}
