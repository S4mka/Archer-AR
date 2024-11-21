using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab; // Префаб мишени
    public float minRadius = 0.5f; // Минимальный радиус
    public float maxRadius = 2.0f; // Максимальный радиус
    public int numberOfTargets = 10; // Количество мишеней для спавна
    public LayerMask placementLayer; // Слой, на который будут проверяться спавн

    private ARRaycastManager arRaycastManager;

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        SpawnTargets();
    }

    void SpawnTargets()
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            if (spawnPosition != Vector3.zero)
            {
                Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        for (int attempts = 0; attempts < 10; attempts++) // Попробуем 10 раз найти подходящую позицию
        {
            // Генерируем случайный угол и радиус
            float angle = Random.Range(0f, 360f);
            float radius = Random.Range(minRadius, maxRadius);

            // Вычисляем позицию на основе угла и радиуса
            Vector3 potentialPosition = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                0f, // Задайте высоту по необходимости
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );

            // Проверяем, есть ли коллайдеры в этой позиции
            if (IsPositionValid(potentialPosition))
            {
                return potentialPosition + transform.position;
            }
        }

        return Vector3.zero; // Если не удалось найти подходящую позицию
    }

    bool IsPositionValid(Vector3 position)
    {
        // Проверяем, есть ли коллайдеры в заданной позиции
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.5f, placementLayer);
        return hitColliders.Length == 0; // Позиция валидна, если нет коллайдеров
    }
}

