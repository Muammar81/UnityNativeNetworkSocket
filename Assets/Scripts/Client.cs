using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{
    #region netwrok variables
    private const int MAX_CONNECTIONS = 100;
    private const string SERVER_IP = "127.0.0.1";
    private const int SERVER_WIN_PORT = 8999;
    private const int SERVER_WEB_PORT = 8998;
    private const int BUFFER_SIZE = 1024;

    private byte[] buffer = new byte[BUFFER_SIZE];
    private int winHostID;

    private QosType channelType;
    private byte err;
    #endregion

    private int connectionID = -1;
    private bool isConnected;

    private void Start()
    {
        GlobalConfig globalConfig = new GlobalConfig();
        NetworkTransport.Init(globalConfig);

        //host topology
        ConnectionConfig connectionConfig = new ConnectionConfig();
        connectionConfig.AddChannel(QosType.Unreliable);

        HostTopology hostTopo = new HostTopology(connectionConfig, MAX_CONNECTIONS);

        //connecting to host
        winHostID = NetworkTransport.AddHost(hostTopo, 0);
        channelType = QosType.Unreliable;

#if UNITY_WEBGL
        connectionID = NetworkTransport.Connect(winHostID:, SERVER_IP, SERVER_WEB_PORT, 0, out err);
#else
        connectionID = NetworkTransport.Connect(winHostID, SERVER_IP, SERVER_WIN_PORT, 0, out err);
#endif
        if (connectionID >= 0)
            isConnected = true;
    }
}
