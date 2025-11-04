using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
        
    //[Header("Nombres de escenas")]
    //[SerializeField] private string escenaGame1 = "Game1";
    //[SerializeField] private string escenaGame2 = "Game2";
    //[SerializeField] private string escenaGame3 = "Game3";
    //[SerializeField] private string escenaSocial = "Social";

    private void Start()
    {
        
    }
    public void IrAScena(int escena)
    {
        SceneController.Instance.CargarEscena(escena);
    }
    
}
