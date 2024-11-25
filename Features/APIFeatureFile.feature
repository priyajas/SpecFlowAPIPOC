Feature: To test the Get Request API


@REQ1
Scenario:  Get Request Testing
Given  the user sends a request with url as "https://reqres.in/api/users"
Then request should be successful with status code as 200

@REQ2
Scenario:  Post Request Testing
		Given  the user sends a post request with url as "https://reqres.in/api/users"
		Then user should get a successul response

@REQ3
	Scenario:  Put Request Testing
		Given  the user sends a put request with url as "https://reqres.in/api/users/1"
		Then user should get a successul response

@REQ4
Scenario:  Post Request Fail Testing
	Given  the user sends a post request with url as "https://reqres.in/api/users"
	Then user should get a accepted response

	