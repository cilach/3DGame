using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


	void Start () {
        //Удаляем текущий объект
        Destroy(this.gameObject, 2f);
	}
	

	void Update () {
        //Перемещаем шар
        transform.position += transform.forward * 5 * Time.deltaTime;
	}
    //При столкновении этого объкта  с другими объктами удаляем его
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
