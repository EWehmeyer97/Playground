using UnityEngine;
using UnityEngine.UI;

public class TogglePaginationMenu : MonoBehaviour
{
    [SerializeField] protected Toggle[] toggles;
    [SerializeField] protected Button nextButton;
    [SerializeField] protected Button backButton;

    private int trackedValue = 0;

    protected int TrackedValue { get { return trackedValue; } }

    protected virtual void Awake()
    {
        //UI Navigation
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.AddListener(Sort);
        }
        if (nextButton != null) nextButton.onClick.AddListener(Next);
        if (backButton != null) backButton.onClick.AddListener(Back);
    }

    protected void Next()
    {
        trackedValue++;
        if (trackedValue == toggles.Length)
            trackedValue = 0;

        toggles[trackedValue].isOn = true;
    }

    protected void Back()
    {
        trackedValue--;
        if (trackedValue == -1)
            trackedValue = toggles.Length - 1;

        toggles[trackedValue].isOn = true;
    }

    public virtual void Sort(bool arg)
    {
        if (!arg)
            return;

        int sortIndex;
        for (sortIndex = 0; sortIndex < toggles.Length; sortIndex++)
            if (toggles[sortIndex].isOn)
                break;

        trackedValue = sortIndex;
    }

    protected void ResetValue()
    {
        toggles[0].SetIsOnWithoutNotify(true);
        trackedValue = 0;
    }
}
