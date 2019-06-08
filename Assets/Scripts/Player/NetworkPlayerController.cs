using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;

namespace LaserBattle
{
    [CreateAssetMenu(menuName = "PlayerControllers/NetworkHuman")]
    public class NetworkPlayerController : PlayerController
    {
        const byte MOVEMENT_TAG = 1;

        public UnityClient client { get; set; }

        private void OnEnable()
        {
            //client = GameObject.Find("Network").GetComponent<UnityClient>();
        }

        public override void Initialize(PlayerNumbers pNumber)
        {
            //client = GameObject.Find("Network").GetComponent<UnityClient>();
        }

        public override void Update()
        {

        }

        public override void FinalizeMove()
        {

        }

        //public void MessageReceived(object sender, )
    }
}