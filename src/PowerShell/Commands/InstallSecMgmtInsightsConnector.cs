// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Online.SecMgmt.PowerShell.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using Graph;
    using Models.Authentication;
    using Network;

    [Cmdlet(VerbsLifecycle.Install, "SecMgmtInsightsConnector", SupportsShouldProcess = true)]
    [OutputType(typeof(string))]
    public class InstallSecMgmtInsightsConnector : MgmtAsyncCmdlet
    {
        /// <summary>
        /// Gets or sets the display name for the Azure Active Directory application that will be created.
        /// </summary>
        [Alias("AppDisplayName")]
        [Parameter(HelpMessage = "Display name for the Azure Active Directory application that will be created.", Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ApplicationDisplayName { get; set; }

        /// <summary>
        /// Executes the operations associated with the cmdlet.
        /// </summary>
        public override void ExecuteCmdlet()
        {
            Scheduler.RunTask(async () =>
            {
                GraphServiceClient client = MgmtSession.Instance.ClientFactory.CreateGraphServiceClient() as GraphServiceClient;
                client.AuthenticationProvider = new GraphAuthenticationProvider();

                WriteDebug("Creating an Azure Active Directory application");

                Application app = await client.Applications.Request().AddAsync(new Application
                {
                    DisplayName = ApplicationDisplayName,
                    PublicClient = new PublicClientApplication
                    {
                        RedirectUris = new[] { "https://oauth.powerbi.com/views/oauthredirect.html" }
                    },
                    RequiredResourceAccess = GetRequiredResourceAccess(),
                    SignInAudience = "AzureADMultipleOrgs"
                }, CancellationToken).ConfigureAwait(false);

                WriteDebug($"Creating a service principal for the {app.AppId} application");

                ServicePrincipal servicePrincipal = await client.ServicePrincipals.Request().AddAsync(new ServicePrincipal
                {
                    AppId = app.AppId
                }).ConfigureAwait(false);

                await client.Oauth2PermissionGrants.Request().AddAsync(new OAuth2PermissionGrant
                {
                    ClientId = servicePrincipal.Id,
                    ConsentType = "AllPrincipals",
                    ResourceId = await GetServicePrincipalId(client, "c5393580-f805-4401-95e8-94b7a6ef2fc2").ConfigureAwait(false),
                    Scope = "ActivityFeed.Read ActivityFeed.ReadDlp ServiceHealth.Read"
                });

                WriteDebug($"Creating the Microsoft Graph OAuth2 permission grant for the {app.AppId} application");

                await client.Oauth2PermissionGrants.Request().AddAsync(new OAuth2PermissionGrant
                {
                    ClientId = servicePrincipal.Id,
                    ConsentType = "AllPrincipals",
                    ResourceId = await GetServicePrincipalId(client, "00000003-0000-0000-c000-000000000000").ConfigureAwait(false),
                    Scope = "AuditLog.Read.All DeviceManagementApps.Read.All DeviceManagementConfiguration.Read.All DeviceManagementManagedDevices.Read.All DeviceManagementServiceConfig.Read.All Directory.Read.All IdentityRiskyUser.Read.All InformationProtectionPolicy.Read Policy.Read.All Reports.Read.All SecurityEvents.Read.All User.Read"
                });

            }, true);
        }

        private List<RequiredResourceAccess> GetRequiredResourceAccess()
        {
            return new List<RequiredResourceAccess>
            {
                new RequiredResourceAccess
                {
                    ResourceAppId = "c5393580-f805-4401-95e8-94b7a6ef2fc2",
                    ResourceAccess = new[]
                    {
                        new ResourceAccess { Id = new Guid("594c1fb6-4f81-4475-ae41-0c394909246c"), Type = "Scope" },
                        new ResourceAccess { Id = new Guid("4807a72c-ad38-4250-94c9-4eabfe26cd55"), Type = "Scope" },
                        new ResourceAccess { Id = new Guid("e2cea78f-e743-4d8f-a16a-75b629a038ae"), Type = "Scope" }
                    }
                },
                new RequiredResourceAccess
                {
                    ResourceAppId = "00000003-0000-0000-c000-000000000000",
                    ResourceAccess = new[]
                    {
                        new ResourceAccess { Id = new Guid("e4c9e354-4dc5-45b8-9e7c-e1393b0b1a20"), Type = "Scope" },
                        new ResourceAccess { Id = new Guid("4edf5f54-4666-44af-9de9-0144fb4b6e8c"), Type = "Scope" },
                        new ResourceAccess { Id = new Guid("f1493658-876a-4c87-8fa7-edb559b3476a"), Type = "Scope" },
                        new ResourceAccess { Id = new Guid("314874da-47d6-4978-88dc-cf0d37f0bb82"), Type = "Scope" },
                        new ResourceAccess { Id = new Guid("8696daa5-bce5-4b2e-83f9-51b6defc4e1e"), Type = "Scope" },
                        new ResourceAccess { Id = new Guid("06da0dbc-49e2-44d2-8312-53f166ab848a"), Type = "Scope" },
                        new ResourceAccess { Id = new Guid("d04bb851-cb7c-4146-97c7-ca3e71baf56c"), Type = "Scope" },
                        new ResourceAccess { Id = new Guid("4ad84827-5578-4e18-ad7a-86530b12f884"), Type = "Scope" },
                        new ResourceAccess { Id = new Guid("572fea84-0151-49b2-9301-11cb16974376"), Type = "Scope" },
                        new ResourceAccess { Id = new Guid("02e97553-ed7b-43d0-ab3c-f8bace0d040c"), Type = "Scope" },
                        new ResourceAccess { Id = new Guid("64733abd-851e-478a-bffb-e47a14b18235"), Type = "Scope" },
                        new ResourceAccess { Id = new Guid("e1fe6dd8-ba31-4d61-89e7-88639da4683d"), Type = "Scope" }
                    }
                }
            };
        }

        private async Task<string> GetServicePrincipalId(IGraphServiceClient client, string resourceAppId)
        {
            IGraphServiceServicePrincipalsCollectionPage servicePrincipal = await client.ServicePrincipals.Request().Filter($"AppId eq '{resourceAppId}'").GetAsync().ConfigureAwait(false);

            return servicePrincipal[0].Id;
        }
    }
}