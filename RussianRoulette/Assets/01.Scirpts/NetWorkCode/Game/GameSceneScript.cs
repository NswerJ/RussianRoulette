using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class GameSceneScript : MonoBehaviour
{
    enum Mode
    {
        SelectHost = 0,
        Connection,
        Game,
        Disconnection,
        Error,
    };

    enum HostType
    {
        None = 0,
        Server,
        Client,
    };

    private Mode m_mode;
    private IPAddress ipAddr;
    private HostType hostType;
    private const int m_port = 50765;
    private int m_counter = 0;
    public NetworkManager network;

    private void Awake()
    {
        m_mode = Mode.SelectHost;
        hostType = HostType.None;

        string hostname = Dns.GetHostName();
        IPHostEntry iphost = Dns.GetHostEntry(Dns.GetHostName());
        ipAddr = iphost.AddressList[1];
    }

    private void Start()
    {
        RestoreGameState();
    }

    private void RestoreGameState()
    {
        m_mode = (Mode)PlayerPrefs.GetInt("GameMode");
        hostType = (HostType)PlayerPrefs.GetInt("HostType");

        Debug.Log(m_mode);

        switch (m_mode)
        {
            case Mode.SelectHost:
                OnUpdateSelectHost();
                break;
            case Mode.Connection:
                OnUpdateConnection();
                break;
            case Mode.Game:
                OnUpdateGame();
                break;
            case Mode.Disconnection:
                // Handle disconnection mode
                break;
            case Mode.Error:
                // Handle error mode
                break;
            default:
                break;
        }
    }

    void OnUpdateSelectHost()
    {
        Debug.Log(hostType);

        switch (hostType)
        {
            case HostType.Server:
                {
                    bool ret = network.ConnectToServer(ipAddr, m_port);
                    m_mode = ret ? Mode.Connection : Mode.Error;
                    Debug.Log("HostType Server");
                    network.m_isServer = true;
                }
                break;

            case HostType.Client:
                {
                    bool ret = network.ConnectToServer(ipAddr, m_port);
                    m_mode = ret ? Mode.Connection : Mode.Error;
                    Debug.Log("HostType Client");
                }
                break;

            default:
                break;
        }
    }

    void OnUpdateConnection()
    {
        if (network.IsConnected() == true)
        {
            m_mode = Mode.Game;

            GameObject game = GameObject.Find("Roullet");
            game.GetComponent<RoulletOfDeath>().GameStart();
        }
    }

    void OnUpdateGame()
    {
        GameObject game = GameObject.Find("Roullet");
        if (game.GetComponent<RoulletOfDeath>().IsGameOver() == true)
        {
            m_mode = Mode.Disconnection;
        }
    }
}
