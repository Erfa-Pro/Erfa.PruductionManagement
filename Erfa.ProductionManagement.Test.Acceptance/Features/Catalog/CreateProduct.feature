Feature: Create Product

A short summary of the feature


Scenario: Products get created successfully
	Given valid CreateProductRequest:
		| ProductNumber | Description          | ProductionTImeSec | MaterialProductNAme |
		| 210120F       | Reolsøjle for 120 cm | 55.62             | 1,25 x 120          |
		| 210200F       | Reolsøjle for 200 cm | 92.7              | 1,25 x 120          |
	When I create a product
	Then the product is created

Scenario: Products not created
	Given invalid CreateProductRequest
	When I create a product
	Then the product is not created
