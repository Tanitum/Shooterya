using UnityEngine;
using Unity.Netcode;

public class PlayerHealth : NetworkBehaviour
{
    public NetworkVariable<int> Health = new NetworkVariable<int>();

    private NetworkObject networkObject;

    public void TakeDamage(int amount)
    {
        Health.Value -= amount;
    }

    private void Awake()
    {
        networkObject = GetComponent<NetworkObject>();

        Health.OnValueChanged += Die;
    }

    private void Die(int previousValue, int nextValue)
    {
        if (nextValue <= 0)
        {
            networkObject.Despawn();

            Destroy(gameObject);
        }
    }
}
