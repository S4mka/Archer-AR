using UnityEngine;

public class BowController : MonoBehaviour
{
    public GameObject arrowPrefab; // Префаб стрелы
    public Transform arrowSpawnPoint; // Точка, откуда будет вылетать стрела
    public float maxDrawDistance = 3f; // Максимальное натяжение
    public float launchForceMultiplier = 10f; // Множитель силы запуска
    public LineRenderer bowString; // Линия для визуализации тетивы
    playerContor playerContor;

    private bool isDrawing = false;
    private float drawDistance = 0f;
    private GameObject currentArrow; // Текущая стрела, которую мы натягиваем


    void Update()
    {
        if (playerContor._ongrab == true) // Начало натяжения
        {
            StartDrawing();
            DrawBow();
        }
        if (playerContor._ongrab == false)
        {
            ReleaseArrow();
        }

        // Обновляем визуализацию тетивы
        UpdateBowString();
    }

    void StartDrawing()
    {
        isDrawing = true;
        drawDistance = 0f;

        // Создаем стрелу и устанавливаем ее на место
        if (currentArrow == null)
        {
            currentArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            currentArrow.transform.SetParent(arrowSpawnPoint); // Привязываем стрелу к точке спавна
        }
    }

    void DrawBow()
    {
        if (isDrawing)
        {
            drawDistance += Time.deltaTime; // Увеличиваем натяжение в зависимости от ввода
            drawDistance = Mathf.Clamp(drawDistance, 0, maxDrawDistance);

            // Обновляем позицию стрелы во время натяжения
            currentArrow.transform.position = arrowSpawnPoint.position - arrowSpawnPoint.forward * drawDistance;
            currentArrow.transform.rotation = arrowSpawnPoint.rotation; // Устанавливаем правильное вращение
        }
    }

    void ReleaseArrow()
    {
        isDrawing = false;

        // Отделяем стрелу от точки спавна
        currentArrow.transform.SetParent(null);

        // Создаем стрелу
        Rigidbody rb = currentArrow.GetComponent<Rigidbody>();

        // Вычисляем силу запуска
        float launchForce = drawDistance * launchForceMultiplier;
        rb.AddForce(arrowSpawnPoint.forward * launchForce, ForceMode.Impulse);

        // Сбрасываем натяжение и текущую стрелу
        drawDistance = 0f;
        currentArrow = null; // Удаляем ссылку на текущую стрелу
    }

    void UpdateBowString()
    {
        if (bowString != null)
        {
            Vector3[] positions = new Vector3[2];
            positions[0] = arrowSpawnPoint.position; // Начало тетивы
            positions[1] = arrowSpawnPoint.position + arrowSpawnPoint.forward * drawDistance; // Конец тетивы
            bowString.SetPositions(positions);
        }
    }
}










