using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab; // ������ ������
    public float minRadius = 0.5f; // ����������� ������
    public float maxRadius = 2.0f; // ������������ ������
    public int numberOfTargets = 10; // ���������� ������� ��� ������
    public LayerMask placementLayer; // ����, �� ������� ����� ����������� �����

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
        for (int attempts = 0; attempts < 10; attempts++) // ��������� 10 ��� ����� ���������� �������
        {
            // ���������� ��������� ���� � ������
            float angle = Random.Range(0f, 360f);
            float radius = Random.Range(minRadius, maxRadius);

            // ��������� ������� �� ������ ���� � �������
            Vector3 potentialPosition = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                0f, // ������� ������ �� �������������
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );

            // ���������, ���� �� ���������� � ���� �������
            if (IsPositionValid(potentialPosition))
            {
                return potentialPosition + transform.position;
            }
        }

        return Vector3.zero; // ���� �� ������� ����� ���������� �������
    }

    bool IsPositionValid(Vector3 position)
    {
        // ���������, ���� �� ���������� � �������� �������
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.5f, placementLayer);
        return hitColliders.Length == 0; // ������� �������, ���� ��� �����������
    }
}

