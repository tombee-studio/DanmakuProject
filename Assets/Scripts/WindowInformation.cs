using UnityEngine;

public static class WindowInformation
{
    static Vector3 SCREEN_UP_RIGHT { get => new Vector3(Screen.width, Screen.height, 0); }
    static Vector3 SCREEN_DOWN_LEFT { get => new Vector3(0, 0, 0); }
    public static Vector3 UP_RIGHT { get => Camera.main.ScreenToWorldPoint(SCREEN_UP_RIGHT); }
    public static Vector3 DOWN_LEFT { get => Camera.main.ScreenToWorldPoint(SCREEN_DOWN_LEFT); }
}
