using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turel_Enemy : MonoBehaviour {

    public GameObject fireBall;
    public GameObject fireSpawn;
    public float firePause = 0.5f;
    public int hp = 5;

    void Start()
    {
        StartCoroutine(Fire());
    }

    void Update()
    {
    }
    IEnumerator Fire()
    {
        Instantiate(fireBall, fireSpawn.transform.position, fireSpawn.transform.rotation);
        yield return new WaitForSeconds(firePause);
        //Debug.Log("After Waiting 2 Seconds");
        StartCoroutine(Fire());
    }
    private void OnCollisionEnter(Collision collider)
    {
        //Если мы столкнулись с пулей
        if (collider.gameObject.tag == "Ball")
        {
            if (hp > 1)
            {
                hp -= 1;
            }
            else
            {
                // При убийстве енеми выпадет бонус
               // if (Random.Range(0, 5) == 1) { Instantiate(bonus, transform.position, transform.rotation); }
                Destroy(this.gameObject);
            }

        }
    }
}
