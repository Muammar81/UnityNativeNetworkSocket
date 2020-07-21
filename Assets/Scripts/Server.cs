using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    #region netwrok variables
    protected const int MAX_CONNECTIONS = 100;
    protected const string SERVER_IP = "127.0.0.1";
    protected const int SERVER_WIN_PORT = 8999;
    protected const int SERVER_WEB_PORT = 8998;
    protected const int BUFFER_SIZE = 1024;
    protected byte[] buffer = new byte[BUFFER_SIZE];
    protected int winHostID;
    protected int webHostID;
    protected QosType channelType;
    protected byte err;
    #endregion

    private bool isInitialized;
    private void Start()
    {

        GlobalConfig globalConfig = new GlobalConfig();
        NetworkTransport.Init(globalConfig);

        //host topology
        ConnectionConfig connectionConfig = new ConnectionConfig();
        connectionConfig.AddChannel(QosType.Unreliable);

        HostTopology hostTopo = new HostTopology(connectionConfig, MAX_CONNECTIONS);

        //adding hosts
        winHostID = NetworkTransport.AddHost(hostTopo, SERVER_WIN_PORT);
        webHostID = NetworkTransport.AddWebsocketHost(hostTopo, SERVER_WEB_PORT);

        channelType = QosType.Unreliable;

        isInitialized = true;
    }


    private void Update()
    {
        if (!isInitialized)
            return;

        int outHostID, outConnectionID, outChannelID;
        int receivedSize;


        NetworkEventType netEvent = NetworkTransport.Receive(out outHostID, out outConnectionID, out outChannelID, buffer, buffer.Length, out receivedSize, out err);

        if (netEvent == NetworkEventType.Nothing)
            return;

        switch (netEvent)
        {
            case NetworkEventType.ConnectEvent:
                Debug.Log($"Connection from: {outConnectionID}, through channel: {outChannelID}");
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log($"Disconnected from: {outConnectionID}, through channel: {outChannelID}");
                break;
            case NetworkEventType.DataEvent:
                Debug.Log($"Data from: {outConnectionID}, through channel: {outChannelID}, Message: {System.Text.Encoding.UTF8.GetString(buffer)}");
                break;
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.BroadcastEvent:
                break;
        }

    }

    private void sendToClient()
    {
        NetworkTransport.Send
    }

}



public abstract class NetworkSocket
{
    protected const int MAX_CONNECTIONS = 100;
    protected const string SERVER_IP = "127.0.0.1";
    protected const int SERVER_WIN_PORT = 8999;
    protected const int SERVER_WEB_PORT = 8998;
    protected const int BUFFER_SIZE = 1024;
    protected byte[] buffer = new byte[BUFFER_SIZE];
    protected int winHostID;
    protected int webHostID;
    protected QosType channelType;
    protected byte err;
}