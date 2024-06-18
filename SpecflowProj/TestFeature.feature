Feature: SpecflowTests

In order to list new products for my shop
As a business owner
I want to create a product 

@tag1
Scenario: Create new product
	Given I am on the Create new product page
	And I enter the product name Pear
	And I enter a price value
	And I enter a description of the product that it is a juicy fresh Pear
	When I create the product
	Then I should see the Index page 
