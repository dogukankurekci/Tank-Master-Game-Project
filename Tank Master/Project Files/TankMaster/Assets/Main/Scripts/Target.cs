using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    Collider collider;

    // Mermimiz target-a çarptığında çıkacak olan vurma efektini bu oluşturduğumuz GameObject kısmına atıyoruz. Nedeni ise aktifliğini kod ile değiştirip vurma anında bu efekti oynatmak.
    public GameObject particle;

    void Start()
    {
        // Objenin Animator component-ına erişiyoruz
        animator = GetComponent<Animator>();

        // Objenin AudioSource component-ına erişiyoruz
        audioSource = GetComponent<AudioSource>();

        collider = GetComponent<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        // Target-ın collider-ı Bullet tag-ine sahip objeye çarparsa olacak işlemler
        if (other.tag == "Bullet")
        {
            // Target objesinin vurulma animasyonunu başlatıyor
            animator.SetTrigger("Target");
            // Vurulma sesini başlatıyor
            audioSource.Play();
            // Efect-i aktif hale getiriyor
            particle.SetActive(true);

            collider.enabled = false;
        }
    }
}
