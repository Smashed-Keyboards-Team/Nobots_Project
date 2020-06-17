using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanelScript : MonoBehaviour
{
    #region Variables
    public static WinPanelScript i;
    public Image background;          // TODO: Pasar a HUD
    public GameObject scorePanel;                       // TODO: Cambiar a CanvasGroup
    [Header("Parámetros temporales")]
    [SerializeField] private float preFadeInTime = 0;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float preScoreTime;

    #endregion
    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        background.canvasRenderer.SetAlpha(0.0f);
        scorePanel.SetActive(false);
    }

    /// <summary>
    /// Muestra el WinPanel con un fundido wachi.
    /// </summary>
    public static void ShowPanel() { i.StartCoroutine("Show"); }
    private IEnumerator Show()
    {
        Debug.Log("ShowWin");
        yield return new WaitForSeconds(preFadeInTime);
        yield return StartCoroutine(FadeI());
        yield return new WaitForSeconds(preScoreTime);
        ShowScore();
        HUD.i.CursorClean();


        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    /// <summary>
    /// Hace un fade in dejando la pantalla en negro.
    /// </summary>
    public void FadeIn() { StartCoroutine(FadeI()); }
    private IEnumerator FadeI()
    {
        Debug.Log("FadeInWin");
        background.CrossFadeAlpha(1, fadeInTime, true);
        return new WaitForSecondsRealtime(fadeInTime);
    }

    private void ShowScore()
    {
        scorePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
