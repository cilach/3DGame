using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

    public Transform player;

    public float distance = 2f;
    public float height = 2f;


	void Start () {
        //Присваиваем player текущую позицию объкта
        player = GameObject.Find("Player").transform;
        //Поворот по оси х
        this.transform.eulerAngles = new Vector3(50, 0, 0);
	}
	
	
	void Update () {
        //Позиция камеры
        this.transform.position = new Vector3(player.position.x, player.position.y + height, player.position.z - distance);

    }
}
