using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class PlayerHealth : NetworkBehaviour
{
    public NetworkVariable<float> Health = new NetworkVariable<float>();

    private Image hpBar;
    private void Start() 
    {
        hpBar = GameObject.Find("HP_bar_green").GetComponent<Image>();
        Debug.Log(hpBar.tag);
    }

    private NetworkObject networkObject;

    public void TakeDamage(float amount)
    {
        Health.Value -= amount;
        hpBar.fillAmount = Health.Value / 100;
        Debug.Log("Health = " + Health.Value.ToString());
    }

    private void Awake()
    {
        networkObject = GetComponent<NetworkObject>();

        Health.OnValueChanged += Die;
    }

    private void Die(float previousValue, float nextValue)
    {
        if (nextValue <= 0)
        {
            networkObject.Despawn();

            Destroy(gameObject);
        }
    }
}
