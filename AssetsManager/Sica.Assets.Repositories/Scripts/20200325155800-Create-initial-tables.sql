CREATE TABLE assets (
	id uniqueidentifier NOT NULL,
	model varchar(200) NOT NULL,
	description text NULL,
	purchased_at datetime,
	maintenance_on datetime

	CONSTRAINT assets_pkey PRIMARY KEY (id)
);