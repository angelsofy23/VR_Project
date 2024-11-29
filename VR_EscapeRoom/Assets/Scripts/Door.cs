using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoorScript
{
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour
    {
        public bool open;
        public float smooth = 1.0f;
        float DoorOpenAngle = -90.0f;
        float DoorCloseAngle = 0.0f;
        public AudioSource asource;
        public AudioClip openDoor, closeDoor;

        // Referência à alavanca
        public SVLever lever; // Adicione a referência à alavanca

        void Start()
        {
            asource = GetComponent<AudioSource>();
        }

        void Update()
        {
            // Se a alavanca estiver ligada, abra a porta, senão, feche
            if (lever != null && lever.leverIsOn)
            {
                if (!open)
                {
                    OpenDoor();
                }
            }
            else
            {
                if (open)
                {
                    CloseDoor();
                }
            }

            // Lógica de animação da porta (abertura e fechamento suave)
            if (open)
            {
                var target = Quaternion.Euler(0, DoorOpenAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
            }
            else
            {
                var target1 = Quaternion.Euler(0, DoorCloseAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
            }
        }

        public void OpenDoor()
        {
            open = true;
            asource.clip = openDoor;
            asource.Play();
        }

        public void CloseDoor()
        {
            open = false;
            asource.clip = closeDoor;
            asource.Play();
        }
    }
}