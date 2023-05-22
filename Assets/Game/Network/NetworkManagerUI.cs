using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button _hostButton, _clientButton;

    private void Awake()
    {
        _hostButton.onClick.AddListener(() =>
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            Debug.Log("SceneLoaded");
            NetworkManager.Singleton.StartHost();
            Debug.Log("Host started");
        });

        _clientButton.onClick.AddListener(() =>
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("SceneLoaded");
            NetworkManager.Singleton.StartClient();
        });
    }
}
