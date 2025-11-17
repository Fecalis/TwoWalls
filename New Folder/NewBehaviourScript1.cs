using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    public float speed = 5f;
    public GameObject Уран;
    public float neutronLifetime = 25.31415f;
    private float timer;

    void Start()
    {
        timer = neutronLifetime;
        // Задать случайную траекторию движения нейтрона
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.insideUnitCircle.normalized * speed;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверка на столкновение с атомом урана-235
        if (collision.gameObject.CompareTag("Uranium235"))
        {

            // Вызов деления урана-235
            //collision.gameObject.GetComponent<NewBehaviourScript>().Fission();
            Destroy(gameObject); // Уничтожить нейтрон после столкновения
        }

        if(collision.gameObject.CompareTag("N"))
        {

        }
    }

    void Update()
    {
        // Уменьшение таймера
        timer -= Time.deltaTime;

        // Проверка таймера для распада нейтрона
        if (timer <= 0)
        {
            Decay();
        }
    }

    void Decay()
    {
        // Создание продуктов распада
        //Instantiate(protonPrefab, transform.position, Quaternion.identity);
        //Instantiate(electronPrefab, transform.position, Quaternion.identity);

        // Уничтожение нейтрона
        Destroy(gameObject);
    }
}