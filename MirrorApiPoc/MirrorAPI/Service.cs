using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Mirror.v1;
using Google.Apis.Authentication;
using Google.Apis.Services;
using Utils;
using DotNetOpenAuth.OAuth2;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Google.Apis.Mirror.v1.Data;

namespace MirrorAPI
{
    public class Service
    {
        #region Singleton Pattern
        private static Service instance = null;

        private Service() { }

        public static Service GetInstance()
        {
            if (instance == null)
            {
                instance = new Service();
            }
            return instance;
        }
        #endregion

        private MirrorService BuildService(IAuthenticator credentials)
        {
            return new MirrorService(new BaseClientService.Initializer()
            {
                Authenticator = credentials
            });
        }

        private IAuthenticator Login(string pAuthorizationCode, string pEmail, string pState)
        {
            if (!String.IsNullOrEmpty(pAuthorizationCode))
            {
                return AuthenticationUtils.GetCredentials(pEmail, pAuthorizationCode, pState);
            }
            else
            {
                IAuthorizationState authorizationState = AuthenticationUtils.GetStoredCredentials(pEmail);
                return AuthenticationUtils.GetAuthenticatorFromState(authorizationState);
            }
        }

        public void ManuallyPaginatedCard(string pAuthorizationCode, string pEmail)
        {
            IAuthenticator authenticator = Login(pAuthorizationCode, pEmail, String.Empty);
            MirrorService service = BuildService(authenticator);
            TimelineItem item = new TimelineItem();

            string html = @"<article>
                             <section>
                               <p>First page</p>
                             </section>
                            </article>

                            <article>
                             <section>
                               <p>Second page</p>
                             </section>
                            </article>

                            <article>
                             <section>
                               <p>Third page</p>
                             </section>
                            </article>";


            StaticCard.AddHtml(item, html);
            service.Timeline.Insert(item).Fetch();
        }

        public void BundlingCards(string pAuthorizationCode, string pEmail)
        {
            IAuthenticator authenticator = Login(pAuthorizationCode, pEmail, String.Empty);
            MirrorService service = BuildService(authenticator);
            TimelineItem item = new TimelineItem();
            item.BundleId = "1";
            item.IsBundleCover = true;
            StaticCard.AddHtml(item, "<div><img src='http://cdn.screenrant.com/wp-content/uploads/Oliver-Stone-Breaking-Bad.jpg' /> </div>");

            TimelineItem item2 = new TimelineItem();
            item2.BundleId = "1";
            StaticCard.AddHtml(item2, "<div><img src='http://images.amcnetworks.com/amctv.com/wp-content/uploads/2014/04/BB-explore-S4-980x551-clean.jpg' /> </div>");

            TimelineItem item3 = new TimelineItem();
            item3.BundleId = "1";
            StaticCard.AddHtml(item3, "<div><img src='http://cdn.self-titledmag.com/wp-content/uploads/2011/10/breaking-bad-will-return-to-amc-in-august.jpg' /> </div>");


            service.Timeline.Insert(item).Fetch();
            service.Timeline.Insert(item2).Fetch();
            service.Timeline.Insert(item3).Fetch();
        }

        public void SendAttachment(string pAuthorizationCode, string pEmail, string pContentType, Stream pStream)
        {
            IAuthenticator authenticator = Login(pAuthorizationCode, pEmail, "");
            MirrorService service = BuildService(authenticator);
            TimelineItem item = new TimelineItem();

            StaticCard.AddText(item, "Mandando video prueba 1");
            StaticCard.AddBuiltInActions(item, new string[] { "PLAY_VIDEO", "DELETE" });

            item = service.Timeline.Insert(item).Fetch();
            Attachment attachment = InsertAttachment(service, item.Id, pContentType, pStream);
        }

        private static Attachment InsertAttachment(MirrorService service, String itemId, String contentType, Stream stream)
        {
            try
            {
                TimelineResource.AttachmentsResource.InsertMediaUpload request = service.Timeline.Attachments.Insert(itemId, stream, contentType);
                request.Upload();
                return request.ResponseBody;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return null;
            }
        }

        public void SendPinCard(string pEmail)
        {
            IAuthenticator authenticator = Login("", pEmail, "");
            MirrorService service = BuildService(authenticator);
            TimelineItem item = new TimelineItem();
            StaticCard.PinCard(item);
            item = service.Timeline.Insert(item).Fetch();
        }

        public void GetLastLocation(string pAuthorizationCode, string pEmail)
        {
            IAuthenticator authenticator = Login("", pEmail, "");
            MirrorService service = BuildService(authenticator);
            Locations.PrintLatestLocation(service);
        }

        public void GetAllLocations(string pAuthorizationCode, string pEmail)
        {
            IAuthenticator authenticator = Login("", pEmail, "");
            MirrorService service = BuildService(authenticator);
            Locations.PrintAllLocations(service);
        }

        public void SubscribeToLocations(string pAuthorizationCode, string pEmail) 
        {
            IAuthenticator authenticator = Login("", pEmail, "");
            MirrorService service = BuildService(authenticator);
            Subscriptions.SubscribeToNotifications(service, "locations", String.Empty, String.Empty, "https://mirrornotifications.appspot.com/forward?url=http://example.com/notify/callback", null); 
        }
    }
}
