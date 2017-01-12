## Okta Authentication Flow C# Command Line Sample

This project demonstrates the use of Okta Authentication and Session APIs with [Okta .NET SDK](https://github.com/okta/oktasdk-csharp)  through the most common [transaction state models](http://developer.okta.com/docs/api/resources/authn.html#transaction-state).

Specifically, you will learn when to expect an Okta state token (which you get when the user is expected to perform an additional operation, such as resetting their password or going a second-factor authentication) vs. a session token (when the user is fully authenticated and an Okta session is ready to be created) and what to do with them depending on the authentication state you're in.

### Configuration
Follow the steps below to set up the sample:  
1. Edit the app.config file and uncomment the 4 lines in the `appSettings` section. 
2. Create an Okta developer organization at [http://developer.okta.com](http://developer.okta.com).  
3. Paste the url of your Okta developer organization (the one from the main user dashboard, not the one from the Admin dashboard with the "-admin" suffix in the url) into the `OktaTenantUrl` of your app.config file
3. (Optional) Go to Admin -> Security -> API and press the "Create Token" button to create an API token. The API token is required if you want the code sample to call the Okta [Sessions API](http://developer.okta.com/docs/api/resources/sessions.html) in order to renew the current user's session.
4. (Optional) Copy the API Token and paste it into the `OktaApiKey` key of your app.config file.
5. You may optionally fill out the `OktaUserLogin` and `OktaUserPassword` values if you want the sample to use these pre-configured values. Otherwise, you will be prompted to enter a user name and password at runtime.

## Testing various flows
Though this sample does not implement all the possible authentication flows the Okta platform enables, it should allow you to test a good variety of them. Namely, we recommend that you test the following configurations:  

  *  [Enable second-factor authentication](https://help.okta.com/en/prod/Content/Topics/Security/Security_Policies.htm) with Okta Verify, Google Authenticator and SMS Authentication (the only second factors this code sample currently supports). You must first enabled the second factors and then create an MFA sign-on policy (for the whole organization, not for a specific app).
  * Try setting up your organization to force passwords to expire (in Security -> General) or to be about to expire (in order to prompt the "Password Expiration Warning" notification)  
 

