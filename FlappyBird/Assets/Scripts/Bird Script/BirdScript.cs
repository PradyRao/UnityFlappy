using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyGame
{
    public class BirdScript : MonoBehaviour
    {

        public static BirdScript instance;
        private float forwardSpeed = 3f;
        private float bounceSpeed = 4f;
        private bool didFlap;
        public bool isAlive;
        public int score;

        private Button flapButton;

        [SerializeField] //same as myRigidBody = GetComponent<RigidBody2D>();
        private Rigidbody2D myRigidBody;
        [SerializeField]
        private Animator ani;
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip flapClip, pointClip, diedClip;

        private void Awake()
        {
            score = 0;
            if(instance == null)
            {
                instance = this;
            }

            isAlive = true;
            flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
            flapButton.onClick.AddListener(() => flapBird());

            setCameraX();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (isAlive)
            {
                Vector3 temp = transform.position;
                temp.x += forwardSpeed * Time.deltaTime;
                transform.position = temp;

                if (didFlap)
                {
                    didFlap = false;
                    myRigidBody.velocity = new Vector2(0, bounceSpeed);
                    ani.SetTrigger("Flap");
                    audioSource.PlayOneShot(flapClip);
                }

                if(myRigidBody.velocity.y >= 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    float angle = 0f;
                    angle = Mathf.Lerp(0, -90, -myRigidBody.velocity.y / 7); //from 0 to -90 in the given time (can be different from 7)
                    transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }
        }

        void setCameraX()
        {
            CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
        }

        public float getPositionX()
        {
            return transform.position.x;
        }

        public void flapBird()
        {
            didFlap = true;
        }

        void OnCollisionEnter2D(Collision2D target)
        {
            if (target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe")
            {
                if (isAlive)
                {
                    isAlive = false;
                    ani.SetTrigger("Dead");
                    audioSource.PlayOneShot(diedClip);
                }
            }
                
        }

        void OnTriggerEnter2D(Collider2D target)
        {
            if(target.tag == "PipeHolder")
            {
                score++;
                audioSource.PlayOneShot(pointClip);
            }
                
        }
    }
}

