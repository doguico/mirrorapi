using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Mirror.v1.Data;
using Google.Apis.Mirror.v1;

namespace MirrorAPI
{
    public class Contacts
    {
        /// <summary>
        /// Insert a new contact for the current user.
        /// </summary>
        /// <param name='service'>Authorized Mirror service.</param>
        /// <param name='contactId'>ID of the contact to insert.</param>
        /// <param name='displayName'>
        /// Display name for the contact to insert.
        /// </param>
        /// <param name='iconUrl'>URL of the contact's icon.</param>
        /// <returns>
        /// The inserted contact on success, null otherwise.
        /// </returns>
        public static Contact InsertContact(MirrorService service, String contactId, String displayName, String iconUrl)
        {
            Contact contact = new Contact()
            {
                Id = contactId,
                DisplayName = displayName,
                ImageUrls = new List<String>() { iconUrl }
            };
            try
            {
                return service.Contacts.Insert(contact).Fetch();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Print information for a contact.
        /// </summary>
        /// <param name='service'>Authorized Mirror service</param>
        /// <param name='contactId'>
        /// ID of the Contact to print information for.
        /// </param>
        public static void PrintContact(MirrorService service,
            String contactId)
        {
            try
            {
                Contact contact = service.Contacts.Get(contactId).Fetch();

                Console.WriteLine(
                    "Contact displayName: " + contact.DisplayName);
                if (contact.ImageUrls != null)
                {
                    foreach (String imageUrl in contact.ImageUrls)
                    {
                        Console.WriteLine("Contact imageUrl: " + imageUrl);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }

        /// <summary>
        /// Print all contacts for the current user.
        /// </summary>
        /// <param name='service'>Authorized Mirror service.</param>
        public static void PrintAllContacts(MirrorService service)
        {
            try
            {
                ContactsListResponse contacts =
                    service.Contacts.List().Fetch();

                foreach (Contact contact in contacts.Items)
                {
                    Console.WriteLine("Contact ID: " + contact.Id);
                    Console.WriteLine("  > displayName: " + contact.DisplayName);
                    if (contact.ImageUrls != null)
                    {
                        foreach (String imageUrl in contact.ImageUrls)
                        {
                            Console.WriteLine("  > imageUrl: " + imageUrl);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }
    }
}
