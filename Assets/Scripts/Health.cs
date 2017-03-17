using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

//[NetworkSettings(sendInterval = 0.01f)]
public class Health : NetworkBehaviour {
    public const float MaxHealth=100.0f;
    [SyncVar (hook = "UpdateHealth")]public float CurrentHealth= MaxHealth;
    public RectTransform PrivateToolHealth;
    public RectTransform PublicToolHealth;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void TakeDamage(float damage)
    {
        if (!isServer)
        {
            return;
        }
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
           Debug.Log("Die");
        }
    }
    public void UpdateHealth(float health)
    {
        PrivateToolHealth.sizeDelta = new Vector2(health * 2, PrivateToolHealth.sizeDelta.y);
        PublicToolHealth.sizeDelta = new Vector2(health * 2, PublicToolHealth.sizeDelta.y);
    }
}
