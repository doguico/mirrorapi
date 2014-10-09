using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Mirror.v1;
using Google.Apis.Mirror.v1.Data;

namespace MirrorAPI
{
    public class Subscriptions
    {
        /// <summary>
        /// Subscribe to notifications for the current user.
        /// </summary>
        /// <param name='service'>Authorized Mirror service.</param>
        /// <param name='collection'>
        /// Collection to subscribe to (supported values are "timeline" and "locations").
        /// </param>
        /// <param name='userToken'>
        /// Opaque token used by the Glassware to identify the user the notification pings are sent for (recommended).
        /// </param>
        /// <param name='verifyToken'>
        /// Opaque token used by the Glassware to verify that the notification pings are sent by the API (optional).
        /// </param>
        /// <param name='callbackUrl'>
        /// URL receiving notification pings (must be HTTPS).
        /// </param>
        /// <param name='operation'>
        /// List of operations to subscribe to. Valid values are "UPDATE", "INSERT" and "DELETE" or {@code null} to subscribe to all.
        /// </param>
        public static void SubscribeToNotifications
            (MirrorService service, String collection, String userToken, String verifyToken, String callbackUrl, List<String> operation)
        {
            Subscription subscription = new Subscription()
            {
                Collection = collection,
                UserToken = userToken,
                VerifyToken = verifyToken,
                CallbackUrl = callbackUrl,
                Operation = operation
            };
            try
            {
                service.Subscriptions.Insert(subscription).Fetch();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }
    }
}
