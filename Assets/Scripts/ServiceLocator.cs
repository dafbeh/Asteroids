using UnityEngine;

public static class ServiceLocator
{
    private static Audio service_;

    public static Audio getAudio()
    {
        return service_;
    }

    public static void provide(Audio service)
    {
        service_ = service;
    }

}
