Feature: AutomationEngineer 
	Automation of Automation engineer level sub api route

Background:
	Given I'm authorized to API
		| Parameter  | Value    |
		| username   | testUser |
		| password   | test     |
		| grant_type | password |

@CleanTestData
Scenario: Post request for companies adds new company
	When I send Post request to create "company"
		| Parameter | Value        |
		| Name      | TestCompany  |
	And I get all "company"
	Then 1 companies are returned in response
	And companies list contains companies
		| Name         |
		| TestCompany  |


@CleanTestData
Scenario: GetAll request for companies returns correct results
	When I send Post request to create "company"
		| Parameter | Value        |
		| Name      | TestCompany  |
		| Name      | TestCompany1 |
	And I get all "company"
	Then 2 companies are returned in response
	And companies list contains companies
		| Name         |
		| TestCompany  |
		| TestCompany1 |


@CleanTestData
Scenario: GetById request for companies returns correct results
	When I send Post request to create "company"
		| Parameter | Value        |
		| Name      | TestCompany  |
		| Name      | TestCompany1 |
	And I get all "company"
	And I send GetById request for "company" with name "TestCompany"
	Then companies response contains correct values
		| Name        |
		| TestCompany |

@CleanTestData
Scenario: DeleteById request for companies deletes company
	When I send Post request to create "company"
		| Parameter | Value        |
		| Name      | TestCompany  |
		| Name      | TestCompany1 |
	And I get all "company"
	And I send DeleteById request for "company" with name "TestCompany"
	And I get all "company"
	Then 1 companies are returned in response
	And companies list doesn't contain company
		| Name        |
		| TestCompany |

@CleanTestData
Scenario: Error is returned on attempt to get company by non existing id
	When I get all "company"
	Then 0 companies are returned in response
	When I send GetById request for "company" with id 1
	Then error status code "NotFound" is returned in response

@CleanTestData
Scenario: Error is returned on attempt to delete company by non existing id
	When I get all "company"
	Then 0 companies are returned in response
	When I send DeleteById request for "company" with id 1
	Then error status code "NotFound" is returned in response

@CleanTestData
Scenario: Post request for companies returns error for empty Name parameter
	When I send Post request to create "company"
		| Parameter | Value |
		| Name      |       |
	Then response contains error status code "BadRequest" and error message "Invalid request."

@CleanTestData
Scenario: Post request for companies returns error for incorrect parameter
	When I send Post request to create "company"
		| Parameter | Value |
		| id        | 18    |
	Then response contains error status code "BadRequest" and error message "Invalid request."

@CleanTestData
Scenario: Post request for companies returns error on adding company with existing name
	When I send Post request to create "company"
		| Parameter | Value        |
		| Name      | TestCompany  |
	And I send Post request to create "company"
		| Parameter | Value        |
		| Name      | TestCompany  |
	Then response contains error status code "BadRequest" and error message "Duplicate results found for identifier."

@CleanTestData
Scenario: Post request for employees adds new employee
	When I send Post request to create "employee"
		| Parameter | Value  |
		| Name      | Patric |
	And I get all "employee"
	Then 1 employees are returned in response
	And employees list contains employees
		| Name   |
		| Patric |

@CleanTestData
Scenario: GetAll request for employees returns correct results
	When I send Post request to create "employee"
		| Parameter | Value      |
		| Name      | John Snow  |
		| Name      | Aria Stark |
	And I get all "employee"
	Then 2 employees are returned in response
	And employees list contains employees
		| Name       |
		| John Snow  |
		| Aria Stark |

@CleanTestData
Scenario: GetById request for employees returns correct results
	When I send Post request to create "employee"
		| Parameter | Value     |
		| Name      | Sam Smith |
		| Name      | John Dow  |
	And I get all "employee"
	And I send GetById request for "employee" with name "Sam Smith"
	Then employees response contains correct values
		| Name      |
		| Sam Smith |

@CleanTestData
Scenario: DeleteById request for employees deletes employee
	When I send Post request to create "employee"
		| Parameter | Value      |
		| Name      | Jane Dow   |
		| Name      | Clark Kent |
	And I get all "employee"
	And I send DeleteById request for "employee" with name "Clark Kent"
	And I get all "employee"
	Then 1 employees are returned in response
	And employees list doesn't contain employee
		| Name       |
		| Clark Kent |

@CleanTestData
Scenario: Error is returned on attempt to get employee by non existing id
	When I get all "employee"
	Then 0 employees are returned in response
	When I send GetById request for "employee" with id 1
	Then error status code "NotFound" is returned in response

@CleanTestData
Scenario: Error is returned on attempt to delete employee by non existing id
	When I get all "employee"
	Then 0 employees are returned in response
	When I send DeleteById request for "employee" with id 1
	Then error status code "NotFound" is returned in response

@CleanTestData
Scenario: Post request for employees returns error for empty Name parameter
	When I send Post request to create "employee"
		| Parameter | Value |
		| Name      |       |
	Then response contains error status code "BadRequest" and error message "Invalid request."

@CleanTestData
Scenario: Post request for employees returns error for incorrect parameter
	When I send Post request to create "employee"
		| Parameter | Value |
		| id        | 18    |
	Then response contains error status code "BadRequest" and error message "Invalid request."


@CleanTestData
Scenario: Post request for employees returns error on adding employee with existing name
	When I send Post request to create "employee"
		| Parameter | Value           |
		| Name      | Luke  Skywalker |
	And I send Post request to create "employee"
		| Parameter | Value           |
		| Name      | Luke  Skywalker |
	Then response contains error status code "BadRequest" and error message "Duplicate results found for identifier."
