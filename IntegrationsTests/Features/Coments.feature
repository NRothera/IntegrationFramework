Feature: Comments
	In order to add my commentary to relevant posts
	As a user of this social service
	I want to be able to comment on various posts

Scenario: Validate the response headers are returned correctly
	Given I request a comment with id 1
	Then I ensure the server set response headers are correct

Scenario: Return the JSON body from the Comments endpoint
	Given I request a comment with id 1
	Then the "Comments" response contains all the required properties

Scenario: Empty JSON body is returned with invalid id for Comments
	Given I request a comment with id -1
	Then I can validate that the "Comments" response is empty

Scenario: Ensure the response time is within acceptable limits
	Given I request a comment with id -1
	Then I can check the response time is under 1000 milliseconds

	