using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankDemo
{
    public class Tank_Inputs : MonoBehaviour
    {

        [Header("Input Properties")]
        public Camera camera;

        public AudioSource walkSound;


        private Vector3 reticlePosition;
        public Vector3 ReticlePosition
        {
            get { return reticlePosition; }
        }

        private Vector3 reticleNormal;
        public Vector3 ReticleNormal
        {
            get { return reticleNormal; }
        }

        private float forwardInput;
        public float ForwardInput
        {
            get { return forwardInput; }
        }

        private float rotationInput;
        public float RotationInput
        {
            get { return rotationInput; }
        }




        void Update()
        {
            if(camera)
            {
                HandleInputs();
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                walkSound.enabled = true;
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
                walkSound.enabled = false;

            if(Input.GetKey(KeyCode.Escape))
                SceneManager.LoadScene(0);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(reticlePosition, 0.5f);
        }






        protected virtual void HandleInputs()
        {
            Ray screenRay = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(screenRay, out hit))
            {
                reticlePosition = hit.point;
                reticleNormal = hit.normal;
            }

            forwardInput = Input.GetAxis("Vertical");
            rotationInput = Input.GetAxis("Horizontal");

        }

    }
}
