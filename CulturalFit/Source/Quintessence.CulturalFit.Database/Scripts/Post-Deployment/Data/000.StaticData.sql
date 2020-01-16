IF NOT EXISTS(SELECT * FROM [Language] WHERE Id = 1)
BEGIN
	INSERT INTO [Language] VALUES (1, 'Nederlands', 'nl')
END

IF NOT EXISTS(SELECT * FROM [Language] WHERE Id = 2)
BEGIN
	INSERT INTO [Language] VALUES (2, 'Français', 'fr')
END

IF NOT EXISTS(SELECT * FROM [Language] WHERE Id = 3)
BEGIN
	INSERT INTO [Language] VALUES (3, 'English', 'en')
END


IF NOT EXISTS(SELECT * FROM [TheoremListRequestType] WHERE Id = 1)
BEGIN
	INSERT INTO [TheoremListRequestType](Id, Type) VALUES(1, 'As is')
END

IF NOT EXISTS(SELECT * FROM [TheoremListRequestType] WHERE Id = 2)
BEGIN
	INSERT INTO [TheoremListRequestType](Id, Type) VALUES(2, 'As is & to be')
END


IF NOT EXISTS(SELECT * FROM [TheoremListType] WHERE Id = 1)
BEGIN
	INSERT INTO [TheoremListType](Id, Type) VALUES(1, 'As is')
END

IF NOT EXISTS(SELECT * FROM [TheoremListType] WHERE Id = 2)
BEGIN
	INSERT INTO [TheoremListType](Id, Type) VALUES(2, 'To be')
END

IF NOT EXISTS(SELECT * FROM [Setting] WHERE Id = 1)
BEGIN
	INSERT INTO [Setting](Id, [Key], Value) VALUES(1, 'mailserver', 'vm-quintmail')
END

IF NOT EXISTS(SELECT * FROM [Setting] WHERE Id = 2)
BEGIN
	INSERT INTO [Setting](Id, [Key], Value) VALUES(2, 'mailfrom', 'secretariaat@quintessence.be')
END

IF NOT EXISTS(SELECT * FROM [Setting] WHERE Id = 3)
BEGIN
	INSERT INTO [Setting](Id, [Key], Value) VALUES(3, 'siteUrl', 'http://culturalfit.myquintessence.be/site/{0}/Login/Index')
END

IF NOT EXISTS(SELECT * FROM [Setting] WHERE Id = 4)
BEGIN
	INSERT INTO [Setting](Id, [Key], Value) VALUES(4, 'companyName', 'Quintessence Consulting NV')
END

IF NOT EXISTS(SELECT * FROM [Setting] WHERE Id = 5)
BEGIN
	INSERT INTO [Setting](Id, [Key], Value) VALUES(5, 'mailCc', 'secretariaat@quintessence.be')
END

--TheoremListRequestTypeId: 1 = as is, 2 = as is & to be
--LanguageId: 1 = nl, 2= fr, 3 = en
IF NOT EXISTS(SELECT * FROM [EmailTemplate] WHERE LanguageId = 1 AND TheoremListRequestTypeId = 1)
BEGIN
INSERT INTO [EmailTemplate](Id, [Subject], Body, LanguageId, TheoremListRequestTypeId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) 
				VALUES(NEWID(), 'Organisatiecultuur', '<div style="font-family:Century Gothic; font-size: 12px;">
														Beste,
														<p>
														Om tijdens onze assessment en development centers de fit te kunnen nagaan tussen de kandidaat en de cultuur die er binnen uw organisatie heerst, vragen we u om onderstaande vragenlijst in te vullen. 
														Een organisatiecultuur is geen tastbaar gegeven en is tevens onderhevig aan verandering; deze vragenlijst verschaft ons de mogelijkheid om een beter beeld te krijgen van de bedrijfscultuur. 
														Dit stelt ons in staat om een goede inschatting te maken van de fit tussen de kandidaat en de cultuur van uw organisatie. 
														Het is echter geenszins de bedoeling om een volledig cultuuronderzoek uit te voeren.
														<p>
														<p>
														U kan de vragenlijst terugvinden op <a href="<!--siteUrl-->"><!--siteUrl--></a>
														<p>
														<p>
														<b>Verificatiecode: <!--verificationCode--></b>
														</p>
														<p>
														U hebt tijd om de vragenlijst in te vullen tot  <!--deadline-->.<br/>
														Als u nog vragen zou hebben, aarzel dan niet om ons te contacteren.
														</p>
														<p>
														Met vriendelijke groeten
														</p>
														<!--companyName-->
														<div>', 
														1, 1, 'NT AUTHORITY\IUSR', GETDATE(), 0, NEWID());
END

IF NOT EXISTS(SELECT * FROM [EmailTemplate] WHERE LanguageId = 2 AND TheoremListRequestTypeId = 1)
BEGIN
INSERT INTO [EmailTemplate](Id, [Subject], Body, LanguageId, TheoremListRequestTypeId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) 
				VALUES(NEWID(), 'Culture organisationelle', '<div style="font-family:Century Gothic; font-size: 12px;">
																Cher,
																<p>
																Au cours de nos assessment centers pour évaluer la concordance entre le candidat et la culture qui prévaut au sein de votre organisation, 
																nous vous demandons de remplir le questionnaire ci-dessous. Une culture organisationnelle n''existe pas de données tangibles et est sujette à changement, 
																ce questionnaire nous fournit l''occasion de mieux comprendre la culture d''entreprise. Cela nous permet de faire une bonne estimation de l''adéquation entre 
																le candidat et la culture de votre organisation. Toutefois, il n''est pas destiné à fournir une culture complète de mener des recherches.
																<p>
																<p>
																Vous pouvez trouver le questionnaire sur <a href="<!--siteUrl-->"><!--siteUrl--></a>
																<p>
																<p>
																<b>Code de vérification: <!--verificationCode--></b>
																</p>
																<p>
																Vous pouvez remplir le questionnaire jusqu''à <!--deadline-->.<br/>
																Si vous avez encore des questions, n''hésitez pas de nous contacter.
																</p>
																<p>
																Cordialement,
																</p>
																<!--companyName-->
																<div>', 
																2, 1, 'NT AUTHORITY\IUSR', GETDATE(), 0, NEWID());
END

IF NOT EXISTS(SELECT * FROM [EmailTemplate] WHERE LanguageId = 3 AND TheoremListRequestTypeId = 1)
BEGIN
INSERT INTO [EmailTemplate](Id, [Subject], Body, LanguageId, TheoremListRequestTypeId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) 
				VALUES(NEWID(), 'Organizational culture', '<div style="font-family:Century Gothic; font-size: 12px;">
															Dear,
															<p>
															In order to be able to assess the fit between the candidate and your organization’s culture , we ask you to complete 
															following questionnaire. An organisation''s culture is no tangible data and is also very subject to change; This questionnaire 
															provides us the possibility to get a better view of your organisation''s culture. This allows us to make a good estimate of 
															the fit between the candidate and the culture of your organisation. It is however not the intention to do a full examination 
															of the culture. 
															<p>
															<p>
															You can find the questionnaire on <a href="<!--siteUrl-->"><!--siteUrl--></a>
															<p>
															<p>
															<b>Verification Code: <!--verificationCode--></b>
															</p>
															<p>
															You can fill in the questionnaire until <!--deadline-->.<br/>
															If you happen to have any questions, do not hesitate to contact us.
															</p>
															<p>
															Sincerely,
															</p>
															<!--companyName-->
															<div>',
															3, 1, 'NT AUTHORITY\IUSR', GETDATE(), 0, NEWID());
END

IF NOT EXISTS(SELECT * FROM [EmailTemplate] WHERE LanguageId = 1 AND TheoremListRequestTypeId = 2)
BEGIN
INSERT INTO [EmailTemplate](Id, [Subject], Body, LanguageId, TheoremListRequestTypeId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) 
				VALUES(NEWID(), 'Organisatiecultuur', '<div style="font-family:Century Gothic; font-size: 12px;">
														Beste,
														<p>
														Om tijdens onze assessment en development centers de fit te kunnen nagaan tussen de kandidaat en de cultuur die er binnen uw organisatie heerst, vragen we u om onderstaande vragenlijst in te vullen. 
														Een organisatiecultuur is geen tastbaar gegeven en is tevens onderhevig aan verandering; deze vragenlijst verschaft ons de mogelijkheid om een beter beeld te krijgen van de bedrijfscultuur. 
														Dit stelt ons in staat om een goede inschatting te maken van de fit tussen de kandidaat en de cultuur van uw organisatie. 
														Het is echter geenszins de bedoeling om een volledig cultuuronderzoek uit te voeren.
														<p>
														<p>
														U kan de vragenlijst terugvinden op <a href="<!--siteUrl-->"><!--siteUrl--></a>
														<p>
														<p>
														<b>Verificatiecode: <!--verificationCode--></b>
														</p>
														<p>
														U hebt tijd om de vragenlijst in te vullen tot  <!--deadline-->.<br/>
														Als u nog vragen zou hebben, aarzel dan niet om ons te contacteren.
														</p>
														<p>
														Met vriendelijke groeten
														</p>
														<!--companyName-->
														<div>', 
														1, 2, 'NT AUTHORITY\IUSR', GETDATE(), 0, NEWID());
END

IF NOT EXISTS(SELECT * FROM [EmailTemplate] WHERE LanguageId = 2 AND TheoremListRequestTypeId = 2)
BEGIN
INSERT INTO [EmailTemplate](Id, [Subject], Body, LanguageId, TheoremListRequestTypeId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) 
				VALUES(NEWID(), 'Culture organisationelle', '<div style="font-family:Century Gothic; font-size: 12px;">
																Cher,
																<p>
																Au cours de nos assessment centers pour évaluer la concordance entre le candidat et la culture qui prévaut au sein de votre organisation, 
																nous vous demandons de remplir le questionnaire ci-dessous. Une culture organisationnelle n''existe pas de données tangibles et est sujette à changement, 
																ce questionnaire nous fournit l''occasion de mieux comprendre la culture d''entreprise. Cela nous permet de faire une bonne estimation de l''adéquation entre 
																le candidat et la culture de votre organisation. Toutefois, il n''est pas destiné à fournir une culture complète de mener des recherches.
																<p>
																<p>
																Vous pouvez trouver le questionnaire sur <a href="<!--siteUrl-->"><!--siteUrl--></a>
																<p>
																<p>
																<b>Code de vérification: <!--verificationCode--></b>
																</p>
																<p>
																Vous pouvez remplir le questionnaire jusqu''à <!--deadline-->.<br/>
																Si vous avez encore des questions, n''hésitez pas de nous contacter.
																</p>
																<p>
																Cordialement,
																</p>
																<!--companyName-->
																<div>', 
																2, 2, 'NT AUTHORITY\IUSR', GETDATE(), 0, NEWID());
END

IF NOT EXISTS(SELECT * FROM [EmailTemplate] WHERE LanguageId =3 AND TheoremListRequestTypeId = 2)
BEGIN
INSERT INTO [EmailTemplate](Id, [Subject], Body, LanguageId, TheoremListRequestTypeId, Audit_CreatedBy, Audit_CreatedOn, Audit_IsDeleted, Audit_VersionId) 
				VALUES(NEWID(), 'Organizational culture', '<div style="font-family:Century Gothic; font-size: 12px;">
															Dear,
															<p>
															In order to be able to assess the fit between the candidate and your organization’s culture , we ask you to complete 
															following questionnaire. An organisation''s culture is no tangible data and is also very subject to change; This questionnaire 
															provides us the possibility to get a better view of your organisation''s culture. This allows us to make a good estimate of 
															the fit between the candidate and the culture of your organisation. It is however not the intention to do a full examination 
															of the culture.
															<p>
															<p>
															You can find the questionnaire on <a href="<!--siteUrl-->"><!--siteUrl--></a>
															<p>
															<p>
															<b>Verification Code: <!--verificationCode--></b>
															</p>
															<p>
															You have time to fill in the questionnaire until <!--deadline-->.<br/>
															If you happen to have any questions, do not hesitate to contact us.
															</p>
															<p>
															Sincerely,
															</p>
															<!--companyName-->
															<div>',
															3, 2, 'NT AUTHORITY\IUSR', GETDATE(), 0, NEWID());
END