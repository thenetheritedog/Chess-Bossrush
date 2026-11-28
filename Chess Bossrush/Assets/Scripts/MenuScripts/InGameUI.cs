using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI : MenuScript
{
    VisualElement hpFillPlayer;
    VisualElement hpFillEnemy;

    protected override void OnRootReady()
    {
        
    }

    public void SetPlayerHealth(float percentage)
    {
        hpFillPlayer.style.width = new StyleLength(Length.Percent(percentage * 100));
    }

    public void SetEnemyHealth(float percentage)
    {
        hpFillEnemy.style.width = new StyleLength(Length.Percent(percentage * 100));
    }

}
