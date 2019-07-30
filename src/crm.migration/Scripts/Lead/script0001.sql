
CREATE TABLE Lead (
	leadid varchar NOT NULL,
	leadowner varchar NULL,
	firstname varchar NULL,
	lastname varchar NOT NULL,
	company varchar NOT NULL,
	email varchar NULL,
	title varchar NULL,
	description varchar NULL,
	street varchar NULL,
	city varchar NULL,
	state varchar NULL,
	zipcode varchar NULL,
	country varchar NULL, 
	CONSTRAINT lead_pk PRIMARY KEY (leadId)
);
