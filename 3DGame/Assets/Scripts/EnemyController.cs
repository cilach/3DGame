using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Transform playerPos;

	void Start () {
        playerPos = GameObject.Find("Player").transform;
	}
	

	void Update () {
        //Если получили координаты игрока ,поворачиваем енеми и перемещаем
        if (playerPos)
        {
            transform.LookAt(playerPos);
            transform.position += transform.forward * Time.deltaTime;
        }
	}
}
