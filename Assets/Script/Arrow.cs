using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ����� �� ������, � ������� ������ ������, ��� "Apple"
        if (collision.gameObject.CompareTag("Apple"))
        {
            // ���� ��� "���������" ������, ������ ���������� ��������
            return; // ������� �� ������, ����� �� ������������� ������
        }

        // ���� ��� �� "���������" ������
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // ��������� ������
            rb.velocity = Vector3.zero; // ������������� ������
        }

        // ������������ ������ � �������, � ������� ��� ������
        transform.SetParent(collision.transform);
    }
}
