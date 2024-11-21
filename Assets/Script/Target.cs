using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject particleEffectPrefab; // ������ ������� ������

    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ������ �� ������ � ������
        if (collision.gameObject.CompareTag("Arrow"))
        {
            // ��������� ������ ������
            
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
