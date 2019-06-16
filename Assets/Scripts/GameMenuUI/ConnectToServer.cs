using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift.Client.Unity;
using DarkRift;
using DarkRift.Client;
using DarkRift.Server;

public class ConnectToServer : MonoBehaviour {

    string ipAddressToConnectTo;

    public void SetIpAddress(string ip)
    {
        ipAddressToConnectTo = ip;
    }

    public void AttemptToConnect()
    {
        if(ipAddressToConnectTo.ToLower() == "ace")
        {
            ipAddressToConnectTo = "73.131.25.197";
        }
        else if(ipAddressToConnectTo.ToLower() == "home")
        {
            ipAddressToConnectTo = "127.0.0.1";
        }
        UnityClient client = GameObject.Find("Network").GetComponent<UnityClient>();
        System.Net.IPAddress ipAddress = System.Net.IPAddress.Parse(ipAddressToConnectTo);
        client.Connect(ipAddress, 4296, IPVersion.IPv4);
    }
}
