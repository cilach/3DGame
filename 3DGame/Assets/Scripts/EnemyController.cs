using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour {

    public Transform playerPos;
    public int hp = 2;
    public GameObject bonus;

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

    //collider - объкт с которым мы столкнулись
    private void OnCollisionEnter(Collision collider)
    {
        //Если мы столкнулись с пулей
        if (collider.gameObject.tag == "Ball") {
            if (hp > 1)
            {
                hp -= 1;
            }
            else {
                // При убийстве енеми выпадет бонус
                if (Random.Range(0,5) == 1) { Instantiate(bonus, transform.position, transform.rotation); }
                Destroy(this.gameObject);
            }

        }
    }
}
