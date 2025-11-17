using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript2 : MonoBehaviour
{
    //public Slider Sl;

    //public Slider SlX;
    //public Slider SlY;
    //public Slider SlTime;
    public Camera Cam;
    public GameObject c;

    public int Choise=0;

    public GameObject[] AllNucl;

    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 20f;
    public float minY = 10f;
    public float maxY = 80f;

    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main;
        Vector3 pos = transform.position;
        pos = new Vector3(0f,0f,-1f);
    }

    public void Pause()
    {
        // Проверяем, была ли нажата клавиша "P" (или любая другая клавиша на ваш выбор)


        // Инвертируем статус паузы
        isPaused = !isPaused;

        // Если сцена на паузе, устанавливаем время в игре равным 0 (пауза), иначе - возвращаем нормальную скорость времени
        Time.timeScale = isPaused ? 0.0001f : 1;

    }
    private void OnMouseDown()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
        Vector3 pos = transform.position;

        // Передвижение камеры с помощью свайпов мыши
        if (Input.GetMouseButton(0))  // Правая кнопка мыши
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            //if (hit.collider != null)
            {
                pos += new Vector3(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), 0f) * panSpeed * Cam.orthographicSize * Mathf.Clamp(Time.deltaTime, 0.0075f, 0.009f);
            }
        }

        // Приближение колесиком мыши
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        Cam.orthographicSize -= scrollValue * 100f * Mathf.Clamp(Time.deltaTime, 0.1f, 2f);
        Cam.orthographicSize = Mathf.Clamp(Cam.orthographicSize, minY, maxY);

        transform.position = pos;

        if (Input.GetMouseButtonDown(0))  // Правая кнопка мыши
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            //if (hit.collider != null)
            {

                Vector2 MposInput = transform.position;
                MposInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Debug.Log("нихуя");
                if (Choise == 1)
                {
                    GameObject Kr = Instantiate(AllNucl[0], new Vector3(MposInput.x, MposInput.y, 0f), Quaternion.identity);
                    Debug.Log("почти");
                }

                if (Choise == 2)
                {
                    GameObject Kr = Instantiate(AllNucl[1], MposInput, Quaternion.identity);

                }
                if (Choise == 3)
                {
                    GameObject Kr = Instantiate(AllNucl[2], MposInput, Quaternion.identity);

                }
                if (Choise == 4)
                {
                    GameObject Kr = Instantiate(AllNucl[3], MposInput, Quaternion.identity);

                }
                if (Choise == 5)
                {
                    GameObject block = Instantiate(AllNucl[4], MposInput, Quaternion.identity);

                }
                Vector2 mousePosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Конвертация координат мыши в координаты мира
                RaycastHit2D hit1 = Physics2D.Raycast(mousePosition1, Vector2.zero); // Выпуск луча из позиции мыши
                if (Choise == 6)
                {
                    if (hit1.collider != null) // Проверка, попал ли луч в какой-либо объект
                    {
                        Destroy(hit1.collider.gameObject); // Удаление объекта
                    }

                }
            }

        }



        if (isPaused == false)
        {
            //Time.timeScale = SlTime.value;
        }
        //c.transform.position = new Vector3(SlX.value, SlY.value,-1f);

        
        ///Cam.orthographicSize = Sl.value;
    }

    public void createKr()
    {
        Choise = 1;
        Debug.Log("ура");


    }
    public void createBa()
    {
        Choise = 2;



    }
    public void createU()
    {
        Choise = 3;


    }
    public void create_n()
    {
        Choise = 4;


    }
    public void create_Wall()
    {
        Choise = 5;


    }
    public void delete()
    {
        Choise = 6;


    }
    public void create_0()
    {
        Choise = 0;

    }
    public void TimeX001()
    {

        Time.timeScale = 0.06f;
    }

    public void TimeX025()
    {
        Time.timeScale = 0.25f;

    }

    public void TimeX1()
    {
        Time.timeScale = 0.8f;

    }

    public void TimeX2()
    {
        Time.timeScale = 2f;

    }


    public void TimeX5()
    {

        Time.timeScale = 4f;
    }
    public void TimeX12()
    {

        Time.timeScale = 8f;
    }


}
