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

        public UnityEngine.UI.Text redPlayerText;
        public UnityEngine.UI.Text bluePlayerText;

        public static bool connected = false;

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
                                if (index == 0)
                                {
                                    redPlayerText.text += " Connected";
                                }
                                else if (index == 1)
                                {
                                    bluePlayerText.text += " Connected";
                                }

                                connected = true;
                            }
                            else
                            {
                                players[index].SetPlayerController(networkPlayers[index]);
                                if(index == 0)
                                {
                                    redPlayerText.text += " Connected";
                                }
                                else if (index == 1)
                                {
                                    bluePlayerText.text += " Connected";
                                }
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
                            int pieceId = reader.ReadInt32();
                            float x = reader.ReadSingle();
                            float z = reader.ReadSingle();
                            float rotation = reader.ReadSingle();

                            Transform unitsParentTransform = GameObject.Find("Units").transform;
                            for (int i = 0; i < unitsParentTransform.childCount; i++)
                            {
                                if (pieceId == i)
                                {
                                    Transform t = unitsParentTransform.GetChild(i);
                                    MovePiece(t, x, z, rotation);
                                    break;
                                }
                            }

                            while (reader.Position < reader.Length)
                            {
                                GameObject.Find("RedLaserGun").transform.rotation = new Quaternion(reader.ReadSingle(),
                                    reader.ReadSingle(),
                                    reader.ReadSingle(),
                                    reader.ReadSingle());
                                GameObject.Find("BlueLaserGun").transform.rotation = new Quaternion(reader.ReadSingle(),
                                    reader.ReadSingle(),
                                    reader.ReadSingle(),
                                    reader.ReadSingle());
                            }
                            StartCoroutine(TurnEnd());
                        }
                    }
                    else if (message.Tag == Tags.ChangeLaserDirectionTag)
                    {
                        while (reader.Position < reader.Length)
                        {
                            GameObject.Find("RedLaserGun").transform.rotation = new Quaternion(reader.ReadSingle(),
                                reader.ReadSingle(),
                                reader.ReadSingle(),
                                reader.ReadSingle());
                            GameObject.Find("BlueLaserGun").transform.rotation = new Quaternion(reader.ReadSingle(),
                                reader.ReadSingle(),
                                reader.ReadSingle(),
                                reader.ReadSingle());
                        }
                    }
                }
            }
        }

        IEnumerator TurnEnd()
        {
            float time = 0f;

            while(time < 1.0f)
            {
                time += Time.deltaTime;
                yield return null;
            }

            if (redTurn)
            {
                redTurnEnd.Raise();
            }
            else
            {
                blueTurnEnd.Raise();
            }
        }

        void MovePiece(Transform t, float x, float z, float rotation)
        {
            MoveableUnit unit = t.gameObject.GetComponent<MoveableUnit>();
            unit.SpawnGhost();
            unit.NetworkMoveUnit(x, z);
            unit.NetworkRotateUnit(rotation);
        }

        public void ChangePlayer()
        {
            redTurn = !redTurn;
        }

        void RemovePlayer(object sender, MessageReceivedEventArgs e, DarkRiftReader reader)
        {

        }
    }
}