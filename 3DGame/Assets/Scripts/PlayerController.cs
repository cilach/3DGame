using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float horizontal;
    public float vertical;
    //Вектор движения
    public Vector3 velocity;

    public Animator anim;
    public Camera cam;
    public Rigidbody rb;
    public GameObject mario;

    

	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	

	void Update () {
        //Переменные ,которые отвечают за перемещение
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(horizontal * 3, 0, vertical * 3);

        //Вращение игрока по наведению мыши
        RaycastHit hit;
        //Создаем луч
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //Столкновение луча
        if (Physics.Raycast(ray, out hit)) {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
        // Для анимации
        velocity = transform.InverseTransformDirection(rb.velocity);

        anim.SetFloat("Vertical", velocity.z);
        anim.SetFloat("Horizontal", velocity.x);

	}
}
