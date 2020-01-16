insert into projectcandidatecategorydetailtype3
	select newid(), projectcandidate.id, projectcategorydetailtype3.id, null, null, 0, 10, null, null, null, null, null, null, 0, 0, 0, null, null, 0, 0, 'QuintessenceUser', getdate(), null, null, null, null, 0, newid()
	from projectcandidate
	inner join projectcategorydetail
		on projectcategorydetail.projectid = projectcandidate.projectid
	inner join projectcategorydetailtype3
		on projectcategorydetailtype3.id = projectcategorydetail.id
	left join projectcandidatecategorydetailtype3
		on projectcandidatecategorydetailtype3.projectcandidateid = projectcandidate.id
		and projectcandidatecategorydetailtype3.projectcategorydetailtype3id = projectcategorydetailtype3.id
	where projectcandidatecategorydetailtype3.id is null

insert into projectcandidatecategorydetailtype2
	select newid(), projectcandidate.id, projectcategorydetailtype2.id, null, 0, 0, null, null, null, null, null, null, 0, 0, 0, null, null, 0, 0, 'QuintessenceUser', getdate(), null, null, null, null, 0, newid()
	from projectcandidate
	inner join projectcategorydetail
		on projectcategorydetail.projectid = projectcandidate.projectid
	inner join projectcategorydetailtype2
		on projectcategorydetailtype2.id = projectcategorydetail.id
	left join projectcandidatecategorydetailtype2
		on projectcandidatecategorydetailtype2.projectcandidateid = projectcandidate.id
		and projectcandidatecategorydetailtype2.projectcategorydetailtype2id = projectcategorydetailtype2.id
	where projectcandidatecategorydetailtype2.id is null

insert into projectcandidatecategorydetailtype1
	select newid(), projectcandidate.id, projectcategorydetailtype1.id, 0, 0, null, null, null, null, null, null, 0, 0, 0, null, null, 0, 0, 'QuintessenceUser', getdate(), null, null, null, null, 0, newid()
	from projectcandidate
	inner join projectcategorydetail
		on projectcategorydetail.projectid = projectcandidate.projectid
	inner join projectcategorydetailtype1
		on projectcategorydetailtype1.id = projectcategorydetail.id
	left join projectcandidatecategorydetailtype1
		on projectcandidatecategorydetailtype1.projectcandidateid = projectcandidate.id
		and projectcandidatecategorydetailtype1.projectcategorydetailtype1id = projectcategorydetailtype1.id
	where projectcandidatecategorydetailtype1.id is null