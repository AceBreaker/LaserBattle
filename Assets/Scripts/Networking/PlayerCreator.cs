using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift.Client.Unity;
using DarkRift;
using DarkRift.Client;

namespace LaserBattle
{
    public class PlayerCreator : MonoBehaviour {
        const byte SPAWN_TAG = 0;

        [SerializeField]
        [Tooltip("The DarkRift client to communicate on.")]
        UnityClient client;

        public Player[] players;
        public NetworkPlayerController[] networkPlayers;

        bool redTurn = true;
        public GameEvent redTurnEnd;
        public GameEvent blueTurnEnd;

        private void Awake()
        {
            if (client == null)
            {
                Debug.LogError("Client unassigned in PlayerSpawner.");
                Application.Quit();
            }

            client.MessageReceived += MessageReceived;
        }

        void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Debug.Log("CreatePlayer entered");
            using (Message message = e.GetMessage())
            {
                using (DarkRiftReader reader = message.GetReader())
                {
                    if(message.Tag == Tags.CreatePlayerTag)
                    {

                        while(reader.Position < reader.Length)
                        {
                            ushort id = reader.ReadUInt16();
                            int index = reader.ReadInt32();
                            
                            Debug.Log(id + " " + index);

                            if(id == client.ID)
                            {
                                //Do nothing
                            }
                            else
                            {
                                players[index].SetPlayerController(networkPlayers[index]);
                            }
                        }
                    }
                    if (message.Tag == Tags.DespawnPlayerTag)
                    {
                        RemovePlayer(sender, e, reader);
                    }
                    else if (message.Tag == Tags.MovePieceTag)
                    {
                        while (reader.Position < reader.Length)
                        {
                            //ushort id = reader.ReadUInt16();
                            //Debug.Log(id);
                            int pieceId = reader.ReadInt32();
                            Debug.Log(pieceId);
                            float x = reader.ReadSingle();
                            Debug.Log(x);
                            float z = reader.ReadSingle();
                            Debug.Log(z);
                            float rotation = reader.ReadSingle();
                            Debug.Log(rotation);

                            Transform unitsParentTransform = GameObject.Find("Units").transform;
                            for(int i = 0; i < unitsParentTransform.childCount; i++)
                            {
                                if(pieceId == i)
                                {
                                    Transform t = unitsParentTransform.GetChild(i);
                                        t.position = new Vector3(x, t.position.y, z);
                                    t.localRotation = Quaternion.Euler(new Vector3(0.0f, rotation + 180f, 0.0f));
                                    break;
                                }
                            }
                            if (redTurn)
                            {
                                redTurnEnd.Raise();
                            }
                            else
                            {
                                blueTurnEnd.Raise();
                            }
                            //redTurn = !redTurn;
                        }
                    }
                }
            }
        }

        public void ChangePlayer()
        {
            Debug.Log("adsfkjhasdfkjlhasdfkljhasd");
            redTurn = !redTurn;
        }

        void RemovePlayer(object sender, MessageReceivedEventArgs e, DarkRiftReader reader)
        {

        }
    }
}