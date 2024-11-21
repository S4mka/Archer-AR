using UnityEngine;

public class BowController : MonoBehaviour
{
    public GameObject arrowPrefab; // ������ ������
    public Transform arrowSpawnPoint; // �����, ������ ����� �������� ������
    public float maxDrawDistance = 3f; // ������������ ���������
    public float launchForceMultiplier = 10f; // ��������� ���� �������
    public LineRenderer bowString; // ����� ��� ������������ ������
    playerContor playerContor;

    private bool isDrawing = false;
    private float drawDistance = 0f;
    private GameObject currentArrow; // ������� ������, ������� �� ����������


    void Update()
    {
        if (playerContor._ongrab == true) // ������ ���������
        {
            StartDrawing();
            DrawBow();
        }
        if (playerContor._ongrab == false)
        {
            ReleaseArrow();
        }

        // ��������� ������������ ������
        UpdateBowString();
    }

    void StartDrawing()
    {
        isDrawing = true;
        drawDistance = 0f;

        // ������� ������ � ������������� �� �� �����
        if (currentArrow == null)
        {
            currentArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            currentArrow.transform.SetParent(arrowSpawnPoint); // ����������� ������ � ����� ������
        }
    }

    void DrawBow()
    {
        if (isDrawing)
        {
            drawDistance += Time.deltaTime; // ����������� ��������� � ����������� �� �����
            drawDistance = Mathf.Clamp(drawDistance, 0, maxDrawDistance);

            // ��������� ������� ������ �� ����� ���������
            currentArrow.transform.position = arrowSpawnPoint.position - arrowSpawnPoint.forward * drawDistance;
            currentArrow.transform.rotation = arrowSpawnPoint.rotation; // ������������� ���������� ��������
        }
    }

    void ReleaseArrow()
    {
        isDrawing = false;

        // �������� ������ �� ����� ������
        currentArrow.transform.SetParent(null);

        // ������� ������
        Rigidbody rb = currentArrow.GetComponent<Rigidbody>();

        // ��������� ���� �������
        float launchForce = drawDistance * launchForceMultiplier;
        rb.AddForce(arrowSpawnPoint.forward * launchForce, ForceMode.Impulse);

        // ���������� ��������� � ������� ������
        drawDistance = 0f;
        currentArrow = null; // ������� ������ �� ������� ������
    }

    void UpdateBowString()
    {
        if (bowString != null)
        {
            Vector3[] positions = new Vector3[2];
            positions[0] = arrowSpawnPoint.position; // ������ ������
            positions[1] = arrowSpawnPoint.position + arrowSpawnPoint.forward * drawDistance; // ����� ������
            bowString.SetPositions(positions);
        }
    }
}










