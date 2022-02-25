CREATE TABLE HeroAvailablePowers (
	id int IDENTITY(1,1) PRIMARY KEY,
	HeroId int NOT NULL FOREIGN KEY REFERENCES SuperHero(id),
	PowerId int NOT NULL FOREIGN KEY REFERENCES SuperPower(id)
);
