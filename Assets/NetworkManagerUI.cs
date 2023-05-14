using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button _hostButton, _clientButton;

    private void Awake()
    {
        _hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        _clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
