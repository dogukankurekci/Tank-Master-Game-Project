using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    void Update()
    {
        //Mermiyi; 3 boyutlu Vector3 uzayının forward yönüne, kullanıcıdan alınan speed değerine göre hareketlendirir.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Merminin collider-ı Target tag-ine sahip objeye çarparsa olacak işlemler
        if(other.tag == "Target")
        {
            // Mermiyi yok ediyor
            Destroy(this.gameObject);
        }
    }
}
