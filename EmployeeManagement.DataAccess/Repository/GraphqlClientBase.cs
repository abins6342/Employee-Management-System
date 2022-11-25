using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EmployeeManagement.DataAccess.Repository
{
    public abstract class GraphqlClientBase
    {
        public readonly GraphQLHttpClient _graphQLHttpClient;
        public GraphqlClientBase()
        {
            if (_graphQLHttpClient == null)
            {
                _graphQLHttpClient = GetGraphQlApiClient();
            }
        }

        public GraphQLHttpClient GetGraphQlApiClient()
        {
            var endpoint = "https://equipped-lab-14.hasura.app/v1/graphql";

            var httpClientOption = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(endpoint)
            };
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-hasura-admin-secret", "qyLmKjPnfaYJSJsgiRPIvNyI8uRVYr05F5int2FnlM0Xh8YLvdtSD3PIsYbh5SQJ");

            return new GraphQLHttpClient(httpClientOption, new NewtonsoftJsonSerializer(), httpClient);
        }
    }
}
