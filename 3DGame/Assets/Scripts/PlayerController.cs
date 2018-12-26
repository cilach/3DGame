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
    public Game game;

    public float firePause = 0.5f;
    public int hp = 10;
    public int ammo = 30;
    public bool isFire = true;

    //Пуля
    public GameObject fireBall;
    //Место спама пули
    public GameObject fireSpawn;

   //Двигающиеся платформы
    public GameObject Platform_1;

    public GameObject gun1;
    public GameObject gun2;
    public Animator gun1_Anim;
    //Collider m_collider;

    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        game = cam.GetComponent<Game>();

        //Двигающиеся платформы
        Platform_1 = GameObject.Find("Is_Platform1");
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
        //При начажити на лкм стреляем
        if (Input.GetKey(KeyCode.Mouse0)) {
            StartCoroutine(Fire());
        }

	}

    IEnumerator Fire() {
        //Если разрешено стрелять и есть пули
        if (isFire && ammo > 0) {
            isFire = false;
            ammo -= 1;
            //Создаем пулю
            Instantiate(fireBall,fireSpawn.transform.position,transform.rotation);
            //Выдерживаем паузу
            yield return new WaitForSeconds(firePause);
            isFire = true;

         }
    }

    void OnCollisionEnter(Collision collider)
    {
        // Если столкнулись с Enemy , отнимаем 1 хп
        if (collider.gameObject.tag == "Enemy") {
            hp -= 1;
        }
        //Если нажали на кнопку
        if (collider.gameObject.tag == "Button")
        {
            StartCoroutine(game.PushButton());
        }
        if (collider.gameObject.tag == "Ball")
        {
            hp -= 1;
        }
        if (collider.gameObject.name == "Gatling")
        {
            gun1_Anim = GameObject.Find("Gatling").GetComponent<Animator>();
            gun1_Anim.enabled = false; 
             // При убийстве енеми выпадет бонус
             // if (Random.Range(0, 5) == 1) { Instantiate(bonus, transform.position, transform.rotation); }
             gun1 = GameObject.Find("Gatling");
            gun2 = GameObject.Find("LaserGun");
            mario = GameObject.Find("Player");
            
            gun1.transform.parent = GameObject.Find("Player").transform;
            gun1.transform.position = new Vector3(gun2.transform.position.x, gun2.transform.position.y, gun2.transform.position.z);
            gun1.transform.rotation = new Quaternion(mario.transform.rotation.x, mario.transform.rotation.y, mario.transform.rotation.z, mario.transform.rotation.w);
           
           fireSpawn = GameObject.Find("Fire Spawn");
            //  m_collider = GetComponent<Collider>();
            // m_collider.enabled = false; можно просто отключить
            //  Destroy(m_collider);

            Destroy(gun2);
            ammo = ammo + 50;
            firePause = 0.3f;

        }
    }

  


private void OnTriggerEnter(Collider collider)
    {
        //Если столкнулись с бонусом
        if (collider.gameObject.tag == "Bonus") {
            if (Random.Range(0, 2) == 1)
            {
                hp += 1;
            }
            else ammo += 10;
            Destroy(collider.gameObject);
        }
        //Если дошли до финиша, запускаем конец игры
        if (collider.gameObject.tag == "End Game")
        {
            StartCoroutine(game.EndGame(this.gameObject));
            Application.LoadLevel("lvl2");
        }
        //Если встали на платформу , платформа становится родителем игрока
        if (collider.gameObject.name == "Is_Platform1") { this.transform.SetParent(Platform_1.transform, true); }

    }
    
    //Если сошли с платформы убрать перента
    void OnTriggerExit(Collider Platform)
    {
        //Если игрок сошел с платформы,сбрасываем родителя в деффолтного
        if (Platform.gameObject.name == "Is_Platform1")
        {
            this.transform.parent = null;
        }
        //Debug.Log("WORK!");
    }

}
