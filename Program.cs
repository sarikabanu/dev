using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Tshreading.Tasks;
using Okta.Core;
using Okta.Core.Clients;
using Okta.Core.Models;
using System.Web;
using System.Configuration;
using System.Net;

namespace Okta.Samples
{
    class Program
    {
        static string oktaUserLogin = string.Empty;
        static string oktaUserEmail = string.Empty;
        static string oktaTenantUrl = string.Empty;
        static string oktaApiKey = string.Empty;

        //Generic Okta client that can be used to instantiate other clients - requires an admin API key
        static OktaClient oktaClient = null;
        static UsersClient usersClient = null;

        static void Main(string[] args)
        {
            try
            {
                CreateUser();
                Console.WriteLine("\r\nThank you for trying out this code sample. Please press Enter to exit.");
                Console.ReadKey();

            }
            catch (OktaAuthenticationException oAE)
            {
                Console.Write(oAE.ToString());
                Console.ReadKey();
            }
            catch (OktaException ex)
            {
                Console.Write(ex.ToString());
                Console.WriteLine(ex.ErrorCode + ":" + ex.ErrorSummary);
                Console.ReadKey();
            }
        }
        public static void CreateUser()
        {
            oktaTenantUrl = ConfigurationManager.AppSettings["OktaTenantUrl"];
            oktaApiKey = ConfigurationManager.AppSettings["OktaApiKey"];
            oktaUserLogin = ConfigurationManager.AppSettings["NewUserLogin"];
            oktaUserEmail = ConfigurationManager.AppSettings["NewUserEmail"];

            Console.WriteLine("\r\nWelcome to the {0} Okta organization.\r\n", oktaTenantUrl);


            //A valid Api Key IS necessary when using the generic Okta Client (a convenience client to create other clients)
            if (!string.IsNullOrWhiteSpace(oktaApiKey))
            {
                oktaClient = new OktaClient(oktaApiKey, new Uri(oktaTenantUrl));

                //the OktaClient object can be used to instantiate other clients such as the UsersClient object (to manage Okta users)
                 usersClient = oktaClient.GetUsersClient();
            }
            try
            {
                if (string.IsNullOrEmpty(oktaUserLogin))
                {
                    Console.Write("Please enter the login of the new user (as an email address) and press Enter: ");
                    oktaUserLogin = Console.ReadLine();
                }

                if (string.IsNullOrEmpty(oktaUserEmail))
                {
                    Console.Write("Please enter the email address for your new user and press Enter: ");
                    oktaUserEmail = Console.ReadLine();
                }

                //create the Okta user
                User newUser = new User(oktaUserLogin, oktaUserEmail, "First Name", "Last Name");

                //this is what you would do to set a custom attribute on the Okta user's profile
                newUser.Profile.SetProperty("employeeNumber", "1234");

                //Activating the user though the API will trigger an email to the user's primary email (i.e. oktaUserEmail)
                usersClient.Add(newUser, true);

                Console.WriteLine("An activation email to {0} is on its way!", oktaUserEmail);
            }
            catch (OktaException oex)
            {
                Console.WriteLine(oex.ErrorCode + ":" + oex.ErrorSummary);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();

                throw;
            }

        }
    }
}