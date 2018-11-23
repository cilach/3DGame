using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public GameObject[] spawn;
    public GameObject enemy;

    public int count = 0;

    public float spawnTime = 1f;

    public GameObject bridge;
    public GameObject bridgeCollider;
    public GameObject button;

    //Отвечает за спам мобов 
    public bool isPlaying = true;

    public GameObject player;
    public PlayerController playerController;

    void Start()
    {
        Transform spawns = GameObject.Find("Spawns").transform;
        spawn = new GameObject[6];
        //Заносим наши точки спама в массив
        for (int i = 0; i < spawns.childCount; i++)
            spawn[i] = spawns.FindChild(i.ToString()).gameObject;
        playerController = player.GetComponent<PlayerController>();

        bridge.SetActive(false);

        StartCoroutine(Spawner());
    }
     void Update()
    {   
        //Если у персонажа нету хп , то он умерает и перезапуск игры
        if (playerController.hp <= 0) { StartCoroutine(PlayerDie()); }
    }

    IEnumerator Spawner()
    {
        if (isPlaying)
        {
            //Если объектов больше указаного количества, то изменяем время спавна
            Transform enemys = GameObject.Find("EnemyClons").transform;
            if (enemys.childCount > 2) { spawnTime = 2f; }

            int i = Random.Range(0, 6);

            //Создаем объект енеми  и переименовываем его
            var myObject = Instantiate(enemy, spawn[i].transform.position, spawn[i].transform.rotation);
            myObject.name = "Enemy" + count++;
            //Указываем родителя
            myObject.transform.parent = GameObject.Find("EnemyClons").transform;
        }
        //Функциия задержки
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(Spawner());
    }

    public IEnumerator PushButton() {
        //Активируем мост и деактивируем bridgeCollider
        bridgeCollider.SetActive(false);
        bridge.SetActive(true);
        button.transform.position -= button.transform.up * Time.deltaTime;
        if (button.transform.position.y > 0.2f)
        {
            yield return null;
            StartCoroutine(PushButton());
        }
        else yield break;
    }
    public IEnumerator EndGame(GameObject obj) {
        isPlaying = false;
        //Ищем всех врагов
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        //Удаляем их и перезапускаем картутин
        if (enemy)
        {
            Destroy(enemy);
            yield return null;
            StartCoroutine(EndGame(obj));
        }
        else yield break;
    }
    //Запуск новой игры
    void StartGame() {
        //Активируем модельку игрока , присваиваем хп и ammo
        player.SetActive(true);
        playerController.hp = 10;
        playerController.ammo = 30;
        //Спамим enemy
        isPlaying = true;
    }
   // Игрок умерает 
    IEnumerator PlayerDie()
    {
        //Запуск карутина для удалению enemy
        StartCoroutine(EndGame(player));
        //Деактивируем персонажа 
        player.SetActive(false);
        //Перемещаем в начало координат
        player.transform.position = new Vector3(0,0.5f,0);
        //Даем время картуниу 2 сек
        yield return new WaitForSeconds(2f);
        StartGame();
    }
}
