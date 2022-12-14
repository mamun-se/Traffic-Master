using UnityEngine;

public static class HapticManager
{
    #if UNITY_ANDROID && !UNITY_EDITOR
        public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService","vibrator");
    #else
        public static AndroidJavaClass unityPlayer;
        public static AndroidJavaObject currentActivity;
        public static AndroidJavaObject vibrator;
    #endif

    public static bool isAndroid()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            return true;
        #else
            return false;
        #endif
    }

    public static void DoVibrate(long miliseconds = 250)
    {
        if (isAndroid())
        {
            vibrator.Call("vibrate",miliseconds);
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    public static void CancelVibration()
    {
        if (isAndroid())
        {
            vibrator.Call("cancel");
        }
    }
}
