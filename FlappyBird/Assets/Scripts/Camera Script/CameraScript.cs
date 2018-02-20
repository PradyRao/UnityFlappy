using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyGame
{
    public class CameraScript : MonoBehaviour
    {

        public static float offsetX;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (BirdScript.instance.isAlive)
            {
                moveTheCamera();
            }
        }

        void moveTheCamera()
        {
            Vector3 temp = transform.position;
            temp.x = BirdScript.instance.getPositionX() + offsetX;
            transform.position = temp;
        }
    }
}