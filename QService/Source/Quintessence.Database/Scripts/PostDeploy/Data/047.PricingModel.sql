IF NOT EXISTS(SELECT * FROM [PricingModel] WHERE [PricingModel].[Id] = 1)
	INSERT INTO [PricingModel](Name) VALUES ('Time & Material')

IF NOT EXISTS(SELECT * FROM [PricingModel] WHERE [PricingModel].[Id] = 2)
	INSERT INTO [PricingModel](Name) VALUES ('Fixed price')