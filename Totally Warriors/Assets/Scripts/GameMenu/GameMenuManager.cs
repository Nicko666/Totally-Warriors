using UnityEngine;

public class GameMenuManager : Singleton<GameMenuManager>
{
    [SerializeField] GameObject[] menu;

    public void SwitchMenu(string menuName)
    {
        foreach (var menu in menu)
        {
            menu.SetActive(false);

            if (menu.name == menuName)
            {
                menu.SetActive(true);
            }

        }
    }

}
