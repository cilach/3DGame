using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public GameObject[] spawn;
    public GameObject enemy;

    public int count=0;

    public float spawnTime = 1f;

	void Start () {
        Transform spawns = GameObject.Find("Spawns").transform;
        spawn = new GameObject[6];
        //Заносим наши точки спама в массив
        for (int i = 0; i < spawns.childCount; i++)
            spawn[i] = spawns.FindChild(i.ToString()).gameObject;
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner(){
        //Если объектов больше указаного количества, то изменяем время спавна
        Transform enemys = GameObject.Find("EnemyClons").transform;
        if (enemys.childCount > 2) { spawnTime = 2f; }

        int i = Random.Range(0, 6);

        //Создаем объект енеми  и переименовываем его
        var myObject = Instantiate(enemy, spawn[i].transform.position, spawn[i].transform.rotation) ;
        myObject.name = "Enemy" + count++;
        //Указываем родителя
        myObject.transform.parent = GameObject.Find("EnemyClons").transform;

        //Функциия задержки
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(Spawner());
         
            
    }
}
