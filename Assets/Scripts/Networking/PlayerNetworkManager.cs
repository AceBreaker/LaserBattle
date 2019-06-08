using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift.Client;
using DarkRift;
using DarkRift.Client.Unity;

namespace LaserBattle
{
    public class PlayerNetworkManager : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("The DarkRift Client to communicate on.")]
        UnityClient client;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}