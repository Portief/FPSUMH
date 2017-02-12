using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Vida : NetworkBehaviour {
    public const int MaxVida=100;
    [SyncVar (hook = "ActualizarVida")]public int ActualVida=MaxVida;
    public RectTransform BarraVidaPrivada;
    public RectTransform BarraVidaPublica;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void TakeDamage(int daño)
    {
        if (!isServer)
        {
            return;
        }
        ActualVida -= daño;
        if (ActualVida<=0)
        {
           Debug.Log("Muerto");
        }
    }
    public void ActualizarVida(int VidaActual)
    {
        BarraVidaPrivada.sizeDelta = new Vector2(VidaActual * 2, BarraVidaPrivada.sizeDelta.y);
        BarraVidaPublica.sizeDelta = new Vector2(VidaActual * 2, BarraVidaPrivada.sizeDelta.y);
    }
}
