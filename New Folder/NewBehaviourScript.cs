using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] ПродуктыРеакции;
    public GameObject Уран;
    public GameObject Нейтрон;
    
    public new ParticleSystem particleSystem;

    

    // Start is called before the first frame update
    void Start()
    {
        //particleSystem = GetComponent<ParticleSystem>();
        for (int i =0; i < 3; i ++)
        {
            //SpawnN(transform.position);
        }
    }

    void SpawnN(Vector2 position)
    {
        particleSystem.Play();
        GameObject n = Instantiate(Нейтрон, position, Quaternion.identity);
        Rigidbody2D rb = n.GetComponent<Rigidbody2D>();
        rb.velocity = Random.insideUnitCircle * 5f; 
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        particleSystem.Play();
        if (collision.gameObject.CompareTag("N"))
        {
            particleSystem.Play();
            Fission();
            Destroy(collision.gameObject);
        }
    }
    public void Fission()
    {
        particleSystem.Play();
        GameObject Kr =Instantiate(ПродуктыРеакции[0], transform.position, Quaternion.identity);
        Rigidbody2D rb0 = Kr.GetComponent<Rigidbody2D>();
        rb0.velocity = Random.insideUnitCircle * 7f;

        GameObject Ba = Instantiate(ПродуктыРеакции[1], transform.position, Quaternion.identity);
        Rigidbody2D rb1 = Ba.GetComponent<Rigidbody2D>();
        rb1.velocity = Random.insideUnitCircle * 7f;
        
        for (int i = 0; i < Random.Range(3,7); i++) // Примерно 3 нейтрона при делении
        {
            SpawnN(transform.position);
        }
        
        Destroy(gameObject);
         
    }

    public void RestartScene()
    {
        // Получаем название текущей сцены
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Перезагружаем текущую сцену
        SceneManager.LoadScene(currentSceneName);
    }
}
