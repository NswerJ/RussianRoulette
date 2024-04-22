using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequenceManager : MonoBehaviour
{
    public static SequenceManager instance;

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

    public void OnClick_BtnMakeRoom(string Scenename)
    {
        hostType = HostType.Server;
        SaveGameState();
        SceneManager.LoadScene(Scenename);
    }

    public void OnClick_BtnEnterRoom(string Scenename)
    {
        hostType = HostType.Client;
        SaveGameState();
        SceneManager.LoadScene(Scenename);
    }

    public void OnClick_Exit()
    {
        hostType = HostType.None;
        m_mode = Mode.Disconnection;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        m_mode = Mode.SelectHost;
        hostType = HostType.None;

        string hostname = Dns.GetHostName();
        IPHostEntry iphost = Dns.GetHostEntry(Dns.GetHostName());
        ipAddr = iphost.AddressList[1];
    }

    private void SaveGameState()
    {
        PlayerPrefs.SetInt("GameMode", (int)m_mode);
        PlayerPrefs.SetInt("HostType", (int)hostType);
    }
}
