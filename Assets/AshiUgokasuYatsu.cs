using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Players
{
    public class AshiUgokasuYatsu : MonoBehaviour
    {

        [SerializeField]
        private Rigidbody2D rigidbody;
        int piyo = 0;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (piyo == 1)
                {
                    return;
                }
                piyo = 1;
                rigidbody.AddTorque(1000f);

            }
            else
            {
                piyo = 0;

            }

        }
    }

}
