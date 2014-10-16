using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Mirror.v1.Data;
using Google.Apis.Mirror.v1;
using Google.Apis.Services;
using Google.Apis.Authentication;
using DotNetOpenAuth.OAuth2;
using System.IO;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;

namespace MirrorApiPoc
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            while (i != 0)
            {
                Console.WriteLine("Ingrese un numero");
                i = Int32.Parse(Console.ReadLine());

                if (i == 1)
                {
                    Console.WriteLine("Ingrese el codigo de autorizacion");
                    string authCode = Console.ReadLine();
                    Console.WriteLine("Ingrese su mail");
                    string email = Console.ReadLine();
                    Console.WriteLine("Ingrese el texto a enviar");
                    string texto = Console.ReadLine();
                    TimelineItem item = new TimelineItem();
                    item.Text = texto;

                    /* Puse el mail, pero puede ser cualquier cosa. Lo uso solo para identificar al usuario y poder acceder desp
                       a su correspondiente auth token y refresh token*/
                    IAuthenticator credentials = AuthenticationUtils.GetCredentials(email, authCode, "Sigo sin enteder pa que sirve esto");
                    //MirrorService service = BuildService(credentials);
                    //service.Timeline.Insert(item).Fetch();
                }
                else if (i == 2)
                {
                    Console.WriteLine("Ingrese su mail");
                    string email = Console.ReadLine();
                    Console.WriteLine("Ingrese el texto a enviar");
                    string texto = Console.ReadLine();

                    Credential credential = GlassContext.Instancia.GetCredential(email);
                    AuthorizationState state = new AuthorizationState()
                    {
                        AccessToken = credential.AccessToken,
                        RefreshToken = credential.RefreshToken
                    };
                    MirrorService service = BuildService(AuthenticationUtils.GetAuthenticatorFromState(state));
                    Image image = Image.FromFile("C:\\Users\\Guido\\Desktop\\1405290025332.jpg");
                    var ms = new MemoryStream();
                    image.Save(ms, ImageFormat.Jpeg);
                    ms.Position = 0;
                    TimelineItem itemAttachment = InsertTimelineItem(service, "Equipo argentino", "image/jpeg", ms, "DEFAULT");
                }
            }
        }

        static MirrorService BuildService(IAuthenticator credentials)
        {
            return new MirrorService(new BaseClientService.Initializer()
            {
                Authenticator = credentials
            });

        }

        static TimelineItem InsertTimelineItem(MirrorService service, String text, String contentType, Stream attachment, String notificationLevel)
        {
            TimelineItem timelineItem = new TimelineItem();
            timelineItem.Text = text;
            if (!String.IsNullOrEmpty(notificationLevel))
            {
                timelineItem.Notification = new NotificationConfig()
                {
                    Level = notificationLevel
                };
            }
            try
            {
                if (!String.IsNullOrEmpty(contentType) && attachment != null)
                {
                    // Insert both metadata and media.
                    TimelineResource.InsertMediaUpload request = service.Timeline.Insert(
                        timelineItem, attachment, contentType);
                    request.Upload();
                    return request.ResponseBody;
                }
                else
                {
                    // Insert metadata only.
                    return service.Timeline.Insert(timelineItem).Fetch();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return null;
            }
        }
    }
}
