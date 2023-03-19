Feature: VerifyUsers

A short summary of the feature

@Smoke @Regression @GetUsers
Scenario: Verify GET Users request
	Given the Users send a GET request to the Users API endpoint
	Then the response message should be 200 OK
	And the response body should contain 10 users.

@Smoke @Regression @GetUsers
Scenario: Verify GET User request by Id
	Given I send a GET request to the User API endpoint with id=8
	Then the response message should be 200 OK
	And the response body should contain the user information for Nicholas Runolfsdottir V.

@Smoke @Regression @PostUsers
Scenario: Verify POST Users request
	Given I send a POST request to the Users API endpoint with the data
	Then the response message should be 201 created
	And the response body should contain the posted data

