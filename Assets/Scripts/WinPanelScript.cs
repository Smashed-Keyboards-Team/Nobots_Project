using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanelScript : MonoBehaviour
{
    #region Variables
    public static WinPanelScript i;
    [SerializeField] private Image background;          // TODO: Pasar a HUD
    [SerializeField] private GameObject scorePanel;     // TODO: Cambiar a CanvasGroup
    [Header("Parámetros temporales")]
    [SerializeField] private float preFadeInTime = 1;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float preScoreTime;
    [SerializeField] private float preFadeOutTime;
    [SerializeField] private float fadeOutTime;

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
    }

    public static void ClosePanel()
    {
        Debug.Log("CloseWin");
        HUD.i.CursorClean();
        i.HideScore();
        GameManager.gm.LoadNext();
    }

    /// <summary>
    /// Oculta el WinPanel con un fundido wachi.
    /// </summary>
    public static void HidePanel() { i.StartCoroutine("Hide"); }
    private IEnumerator Hide()
    {
        Debug.Log("HideWin");
        HideScore();
        yield return new WaitForSeconds(preFadeOutTime);
        yield return StartCoroutine(FadeO());
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

    /// <summary>
    /// Hace un fadeout quitando la negrura de la pantalla.
    /// </summary>
    public void FadeOut() { StartCoroutine(FadeO()); }
    private IEnumerator FadeO()
    {
        Debug.Log("FadeOutWin");
        background.CrossFadeAlpha(0, fadeOutTime, true);
        return new WaitForSecondsRealtime(fadeOutTime);
    }

    private void ShowScore()
    {
        scorePanel.SetActive(true);
    }

    private void HideScore()
    {
        scorePanel.SetActive(false);
    }
}
