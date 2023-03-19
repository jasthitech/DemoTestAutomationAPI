using System;
using TechTalk.SpecFlow;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoTestAutomationAPI.Helpers;
using System.Net;

namespace DemoTestAutomationAPI.StepDefinitions
{
    [Binding]
    public class VerifyUsersStepDefinitions
    {
        [Given(@"the Users send a GET request to the Users API endpoint")]
        public async Task GivenTheUsersSendAGETRequestToTheUsersAPIEndpoint()
        {
            await APICalls.GetUsersAsync();
        }

        [Then(@"the response message should be (.*) OK")]
        public void ThenTheResponseMessageShouldBeOK(int responseCode)
        {
            Assert.AreEqual(HttpStatusCode.OK, APICalls.response.StatusCode);
        }

        [Then(@"the response body should contain (.*) users\.")]
        public void ThenTheResponseBodyShouldContainUsers_(int recordsCount)
        {
            APICalls.GetRecordsCountAsync(recordsCount);            
        }

        [Given(@"I send a GET request to the User API endpoint with id=(.*)")]
        public async Task GivenISendAGETRequestToTheUserAPIEndpointWithId(int userId)
        {
            await APICalls.GetUsersByIdAsync(userId);
        }

        [Then(@"the response body should contain the user information for Nicholas Runolfsdottir V\.")]
        public void ThenTheResponseBodyShouldContainTheUserInformationForNicholasRunolfsdottirV_()
        {
            var name = APICalls.GetNameFromRecordAsync("Nicholas Runolfsdottir V");            
        }

        [Given(@"I send a POST request to the Users API endpoint with the data")]
        public void GivenISendAPOSTRequestToTheUsersAPIEndpointWithTheData()
        {
            APICalls.PostNewUserRecord();
        }

        [Then(@"the response message should be (.*) created")]
        public void ThenTheResponseMessageShouldBeCreated(int responseCode)
        {
            APICalls.ValidatePostStatusCode();
        }

        [Then(@"the response body should contain the posted data")]
        public void ThenTheResponseBodyShouldContainThePostedData()
        {
            APICalls.ValidatePostResponseBody();
        }


    }
}
