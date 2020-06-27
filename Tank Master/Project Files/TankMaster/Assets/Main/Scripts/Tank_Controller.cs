using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TankDemo
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Tank_Inputs))]
    public class Tank_Controller : MonoBehaviour
    {
        public float tankSpeed = 15f;
        public float tankRotationSpeed = 20f;

        public Transform turretTransform;
        public float turretLagSpeed = 0.5f;

        public Transform reticleTransform;

        AudioSource audioSource;
        Animator animator;

        private Rigidbody rb;
        private Tank_Inputs input;
        private Vector3 finalTurretLookDir;

        public Transform firePoint;
        public Bullet bullet;
        public float bulletSpeed;
        public bool isFiring;
        public float timeBetweenShots;
        private float shotCounter;
        public GameObject smokeParticle;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            input = GetComponent<Tank_Inputs>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
            Cursor.visible = false;
        }

        private void Update()
        {

            if (isFiring)
            {
                shotCounter -= Time.deltaTime;

                if (shotCounter <= 0)
                {       
                    shotCounter = timeBetweenShots;

                    Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as Bullet;

                    newBullet.speed = bulletSpeed;

                }
            }

            else
            {
                shotCounter = 0;
            }

       
            if (Input.GetMouseButtonDown(0))
            {
                isFiring = true;

                animator.SetBool("Fire", true);

                smokeParticle.SetActive(true);

                audioSource.Play();
            }

            if (Input.GetMouseButtonUp(0))
            {
                isFiring = false;

                animator.SetBool("Fire", false);

                smokeParticle.SetActive(false);
            }
        }

        void FixedUpdate()
        {
            if(rb && input)
            {
                HandleMovement();
                HandleTurret();
                HandleReticle();

            }
        }

        protected virtual void HandleMovement()
        {

            Vector3 wantedPosition = transform.position + (transform.forward * input.ForwardInput * tankSpeed * Time.deltaTime);
            rb.MovePosition(wantedPosition);


            Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * (tankRotationSpeed * input.RotationInput * Time.deltaTime));
            rb.MoveRotation(wantedRotation);
        }

        protected virtual void HandleTurret()
        {
            if(turretTransform)
            {
                Vector3 turretLookDir = input.ReticlePosition - turretTransform.position;
                turretLookDir.y = 0f;

                finalTurretLookDir = Vector3.Lerp(finalTurretLookDir, turretLookDir, Time.deltaTime * turretLagSpeed);
                turretTransform.rotation = Quaternion.LookRotation(finalTurretLookDir);
            }
        }

        protected virtual void HandleReticle()
        {
            if(reticleTransform)
            {
                reticleTransform.position = input.ReticlePosition;
            }
        }

    }
}
