using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Mirror.v1.Data;

namespace MirrorAPI
{
    public class StaticCard
    {
        #region metodosAlPedo
        public static void AddHtml(TimelineItem pItem, string pHtml)
        {
            pItem.Html = pHtml;
        }

        public static void AddText(TimelineItem pItem, string pText)
        {
            pItem.Text = pText;
        }

        public static void SetBundleCover(TimelineItem pItem)
        {
            pItem.IsBundleCover = true;
        }
        #endregion

        public static void AddNotificationLevel(TimelineItem pItem, string notificationLevel)
        {
            pItem.Notification = new NotificationConfig()
            {
                Level = notificationLevel
            };
        }

        public static void AddBuiltInActions(TimelineItem pItem, string[] pActions)
        {
            if (pItem.MenuItems == null)
            {
                pItem.MenuItems = new List<MenuItem>();
            }
            foreach (string action in pActions)
            {
                MenuItem item = new MenuItem();
                item.Action = action;
                pItem.MenuItems.Add(item);
            }
        }

        public static void AddCustomAction(TimelineItem pItem)
        {
            if (pItem.MenuItems == null)
            {
                pItem.MenuItems = new List<MenuItem>();
            }

            /* MenuItem item;
             * item.Action
             * item.Id
             * item.RemoveWhenSelected
             * item.Values
             */
            /* Un MenuItem tiene un conjunto de Menu values */
            /* MenuValue value;
             * value.State
             * value.IconUrl
             * value.DisplayName
             */
        }

        public static void BundleCards(List<TimelineItem> pItems, string pBundleId)
        {
            foreach (TimelineItem item in pItems)
            {
                item.BundleId = pBundleId;
            }
        }

        public static void PinCard(TimelineItem pItem)
        {
            AddBuiltInActions(pItem, new string[] { "TOGGLE_PINNED" });
        }
    }
}
