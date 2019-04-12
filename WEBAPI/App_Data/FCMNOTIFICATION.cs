using System;

public static class FCMNOTIFICATION
{
    public static bool SendPushNotification(string device_token, string body_data,Guid guid, int devicetype)
    {
        bool IsSendToFcm = false;
        try
        {
            FCMNotification fcmpush = new FCMNotification();
            FCMResponse s = fcmpush.PushNotifyAsync(device_token, body_data, guid, devicetype);
            if (s.success > 0)
            {
                IsSendToFcm = true;
            }
            else
            {
                IsSendToFcm = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return IsSendToFcm;
    }
}