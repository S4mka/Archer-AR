using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, имеет ли объект, в который попала стрела, тег "Apple"
        if (collision.gameObject.CompareTag("Apple"))
        {
            // Если это "проходная" мишень, стрела продолжает движение
            return; // Выходим из метода, чтобы не останавливать стрелу
        }

        // Если это не "проходная" мишень
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Отключаем физику
            rb.velocity = Vector3.zero; // Останавливаем стрелу
        }

        // Присоединяем стрелу к объекту, в который она попала
        transform.SetParent(collision.transform);
    }
}
