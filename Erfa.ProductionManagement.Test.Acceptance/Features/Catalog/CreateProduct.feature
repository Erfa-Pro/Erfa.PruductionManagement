Feature: Create Product



Scenario: Product is created successfully
	Given valid CreateProductRequest with ProductNumber "210120F", Description "Reolsøjle for 120 cm", ProductionTimeSec 55.62, MaterialProductName "1,25 x 120"
	When creating a new product
	Then the response status is Success
	And the product is created with ProductNumber "210120F"

