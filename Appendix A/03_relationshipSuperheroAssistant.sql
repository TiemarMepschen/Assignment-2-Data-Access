ALTER TABLE Assistent
ADD heroId int NOT NUll

ALTER TABLE Assistent
ADD CONSTRAINT HeroId
FOREIGN KEY (HeroId) REFERENCES dbo.SuperHero(id)
